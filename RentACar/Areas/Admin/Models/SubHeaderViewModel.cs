using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Areas.Admin.Models
{
    public class SubHeaderViewModel
    {
        public string Title { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
}
