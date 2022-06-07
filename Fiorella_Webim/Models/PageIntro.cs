using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorella_Webim.Models
{
    public class PageIntro
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
