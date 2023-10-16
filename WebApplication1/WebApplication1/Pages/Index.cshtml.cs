using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public string Description{ get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost()
        {
            if(Image != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", Image.FileName);

                using(var stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Image.CopyTo(stream);
                }

                TempData["ImagePath"] = "/images/" + Image.FileName;
                TempData["Description"] = Description;

                Response.Redirect("/ImagePage");
            }

        }
    }
}
