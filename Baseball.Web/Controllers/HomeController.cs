using Baseball.Data.Sql;
using Baseball.Web.Models;
using System.Configuration;
using System.Web.Mvc;

namespace Baseball.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginRequestModel model)
        {
            var security = new SqlSecurity(ConfigurationManager.ConnectionStrings["Baseball"].ConnectionString);
            var person = security.Authenticate(model.userName, model.password);



            return View();
        }
    }
}