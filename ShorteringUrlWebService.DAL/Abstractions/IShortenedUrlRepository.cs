using ShorteringUrlWebService.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShorteringUrlWebService.DAL.Abstractions
{
    public interface IShortenedUrlRepository
    {
        public Task<ShortenedUrl> GetAsync(string id);

        public IQueryable<ShortenedUrl> Get();

        public Task<ShortenedUrl> AddAsync(ShortenedUrl shortenedUrl);
    }
}