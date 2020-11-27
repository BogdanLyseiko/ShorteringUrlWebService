using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShorteringUrlWebService.BL.Abstractions;
using ShorteringUrlWebService.Models.Entities;

namespace ShorteringUrlWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShorteringUrlController : ControllerBase
    {
        private readonly IShorteringUrlService _shorteringUrlService;

        public ShorteringUrlController(IShorteringUrlService shorteringUrlService)
        {
            _shorteringUrlService = shorteringUrlService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<List<ShortenedUrl>> GetAsync()
        {
            return await _shorteringUrlService.GetAsync();
        }

        [HttpGet]
        [Route("Get/{urlId}")]
        public async Task<IActionResult> GetAsync([FromRoute]string urlId)
        {
            var result = await _shorteringUrlService.GetAsync(urlId);
            
            if (!string.IsNullOrEmpty(result.Error))
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Body);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> PostAsync([FromBody]string url)
        {
            var result = await _shorteringUrlService.AddAsync(url);

            if (!string.IsNullOrEmpty(result.Error))
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Body);
        }
    }
}
