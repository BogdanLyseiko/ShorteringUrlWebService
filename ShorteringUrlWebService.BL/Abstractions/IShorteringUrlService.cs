using ShorteringUrlWebService.Models.Entities;
using ShorteringUrlWebService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShorteringUrlWebService.BL.Abstractions
{
    public interface IShorteringUrlService
    {
        Task<List<ShortenedUrl>> GetAsync();

        Task<ResultViewModel<ShortenedUrl>> AddAsync(string url);

        Task<ResultViewModel<ShortenedUrl>> GetAsync(string id);
    }
}