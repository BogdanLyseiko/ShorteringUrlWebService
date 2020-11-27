using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShorteringUrlWebService.Models.Entities
{
    public class ShortenedUrl
    {
        [MaxLength(6)]
        public string Id { get; set; }

        public string Url { get; set; }
    }
}