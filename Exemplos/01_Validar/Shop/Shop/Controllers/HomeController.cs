using Shop.Models;
using System.Linq;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private ShopContext db = new ShopContext();

        public ActionResult Index()
        {
            var students = from s in db.Customers
                           select s;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}