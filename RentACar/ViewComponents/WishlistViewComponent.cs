using Microsoft.AspNetCore.Mvc;
using RentACar.Extensions;

namespace RentACar.ViewComponents
{
    public class WishlistViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var wishlist = HttpContext.Session.GetObjectFromJson<List<int>>("wishlist") ?? new List<int>();
            return View(wishlist);
        }
    }
}
