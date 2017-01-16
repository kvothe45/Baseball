using Baseball.Data.Sql;
using Baseball.Web.Models;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;

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
            FormsAuthentication.SetAuthCookie(person.Username, true);

            return View();
        }

        public ActionResult Test()
        {
            var taskManager = new SqlTaskManager(ConfigurationManager.ConnectionStrings["BaseBall"].ConnectionString);
            var tasks = taskManager.GetAll();
            return View(tasks);

        }


    }
}