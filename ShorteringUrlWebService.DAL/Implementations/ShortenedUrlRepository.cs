using Microsoft.EntityFrameworkCore;
using ShorteringUrlWebService.DAL.Abstractions;
using ShorteringUrlWebService.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShorteringUrlWebService.DAL.Implementations
{
    public class ShortenedUrlRepository : IShortenedUrlRepository
    {
        private readonly DatabaseContext _dbContext;

        public ShortenedUrlRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShortenedUrl> AddAsync(ShortenedUrl shortenedUrl)
        {
            await _dbContext.ShortenedUrls.AddAsync(shortenedUrl);
            await _dbContext.SaveChangesAsync();

            return shortenedUrl;
        }

        public async Task<ShortenedUrl> GetAsync(string id)
        {
            return await _dbContext.ShortenedUrls.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public IQueryable<ShortenedUrl> Get()
        {
            return _dbContext.ShortenedUrls.AsQueryable();
        }
    }
}