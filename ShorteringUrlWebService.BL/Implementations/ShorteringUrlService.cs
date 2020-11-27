using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShorteringUrlWebService.BL.Abstractions;
using ShorteringUrlWebService.DAL.Abstractions;
using ShorteringUrlWebService.Models.Constants;
using ShorteringUrlWebService.Models.Entities;
using ShorteringUrlWebService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShorteringUrlWebService.BL.Implementations
{
    public class ShorteringUrlService : IShorteringUrlService
    {
        private readonly IShortenedUrlRepository _shortenedUrlRepository;

        public ShorteringUrlService(IShortenedUrlRepository shortenedUrlRepository)
        {
            _shortenedUrlRepository = shortenedUrlRepository;
        }

        public async Task<ResultViewModel<ShortenedUrl>> AddAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new ResultViewModel<ShortenedUrl>("Url could not be null");
            }

            if(!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) || !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return new ResultViewModel<ShortenedUrl>("Url is not valid");
            }

            var existingShortenUrl = _shortenedUrlRepository.Get().FirstOrDefault(x => x.Url == url);

            if (existingShortenUrl != null)
                return new ResultViewModel<ShortenedUrl>(existingShortenUrl);

            var shortenedUrl = GenerateShortenedUrl();

            return new ResultViewModel<ShortenedUrl>(await _shortenedUrlRepository.AddAsync(new ShortenedUrl { Id = shortenedUrl, Url = url }));
        }

        public async Task<List<ShortenedUrl>> GetAsync()
        {
            return await _shortenedUrlRepository.Get().ToListAsync();
        }

        public async Task<ResultViewModel<ShortenedUrl>> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new ResultViewModel<ShortenedUrl>("Id could not be null");
            }

            var shortenedUrl = await _shortenedUrlRepository.GetAsync(id);

            if (shortenedUrl == null)
            {
                return new ResultViewModel<ShortenedUrl>("Shortened url does not exist");
            }

            return new ResultViewModel<ShortenedUrl>(shortenedUrl);
        }

        private string GenerateShortenedUrl()
        {
            StringBuilder urlsafe = new StringBuilder();

            // possible unique tokens which equals: `57 billion 731 million 386 thousand 924´
            Enumerable.Range(ShortenedUrlConstants.ZERO_CODE, ShortenedUrlConstants.GENERATE_NUMBERS_COUNT)
              .Where(i => i < ShortenedUrlConstants.COLON_CODE || i > ShortenedUrlConstants.AT_SIGN_CODE && i < ShortenedUrlConstants.SQUARE_BRACKET_CODE || i > ShortenedUrlConstants.GRAVE_ACCENT_CODE)
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => urlsafe.Append(Convert.ToChar(i)));

            return urlsafe.ToString().Substring(new Random().Next(0, urlsafe.Length), new Random().Next(2, 6));
        }
    }
}
