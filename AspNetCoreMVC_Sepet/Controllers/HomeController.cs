using AspNetCoreMVC_Sepet.Models;
using AspNetCoreMVC_Sepet.Models.Context;
using AspNetCoreMVC_Sepet.Sessions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreMVC_Sepet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _context;

        public HomeController(ILogger<HomeController> logger, NorthwindContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                //Session işlemleri
                CartSession cartSession; //null
                if (SessionHelper.GetProductFromJson<CartSession>(HttpContext.Session,"sepet")==null)
                {
                    cartSession = new CartSession();
                }
                else
                {
                    cartSession = SessionHelper.GetProductFromJson<CartSession>(HttpContext.Session, "sepet");
                }

                CartViewModel cartViewModel = new CartViewModel
                {
                    Product = product,
                };

                cartSession.AddToCart(product.ProductId,cartViewModel);

                SessionHelper.SetProductFromJson(HttpContext.Session, "sepet", cartSession);

                TempData["SuccessStatus"] = $"{product.ProductName} sepete eklendi!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult MyCart()
        {
            //Session'a nasıl ulaşırız?
            var cart = SessionHelper.GetProductFromJson<CartSession>(HttpContext.Session, "sepet");
            if (cart != null)
            {
                return View(cart);
            }
            return RedirectToAction("Index");
        }

        //Dictionary: Key, Value mantığına sahip bünyesinde verileri tutmamıza olanak sağlayan bir koleksiyondur.
        [HttpPost]
        public IActionResult UpdateCart(Dictionary<int, int> Quantities)
        {
            var cartSession = SessionHelper.GetProductFromJson<CartSession>(HttpContext.Session, "sepet");

            if (cartSession != null) 
            {

                cartSession.UpdateCart(Quantities);

                SessionHelper.SetProductFromJson(HttpContext.Session, "sepet", cartSession);
                TempData["SuccessStatus"] = "Sepet güncellendi!";
            }
            else
            {
                TempData["ErrorStatus"] = "Sepetiniz boş";
            }

            return RedirectToAction("MyCart");
        }

        public IActionResult RemoveCartItem(int id) 
        {
            var cartSession = SessionHelper.GetProductFromJson<CartSession>(HttpContext.Session, "sepet");

            if (cartSession != null)
            {
                cartSession.DeleteCart(id);

                SessionHelper.SetProductFromJson(HttpContext.Session, "sepet", cartSession);
                TempData["SuccessStatus"] = "Ürün sepetten kaldırıldı!";
            }
            else
            {
                TempData["ErrorStatus"] = "Sepetiniz boş!";
            }
            
            return RedirectToAction("MyCart");
        }



        public IActionResult Privacy()
        {
            return View();
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}