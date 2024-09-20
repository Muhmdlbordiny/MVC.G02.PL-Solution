using Microsoft.AspNetCore.Mvc;
using MVC.G02.PL.ViewModels;
using MVC.G02.PL.Services;
using System.Diagnostics;
using System.Text;

namespace MVC.G02.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISingeltonService _singelton02;
        private readonly IScopedService    _scoped01;
        private readonly IScopedService    _scoped02;
        private readonly ITransientService _transient01;
        private readonly ITransientService _transient02;
        private readonly ISingeltonService _singelton01;

        public HomeController
            (ILogger<HomeController> logger,
            IScopedService    scoped01,
            IScopedService    scoped02,
            ITransientService transient01,
            ITransientService transient02,
            ISingeltonService singelton01,
            ISingeltonService singelton02

            )
        {
            _logger = logger;
            _scoped01 = scoped01;
            _scoped02 = scoped02;
            _transient01 = transient01;
            _transient02 = transient02;
            _singelton01 = singelton01;
            _singelton02 = singelton02;
        }
        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scoped01 :: {_scoped01.GetGuid()}\n");
            builder.Append($"scoped02 :: {_scoped02.GetGuid()}\n\n");
            builder.Append($"transient01 :: {_transient01.GetGuid()}\n\n");
            builder.Append($"transient02 :: {_transient02.GetGuid()}\n\n");
            builder.Append($" singelton01 :: {_singelton01.GetGuid()}\n\n");
            builder.Append($" singelton02 :: {_singelton02.GetGuid()}\n\n");
            return builder.ToString();

        }

        public IActionResult Index()
        {
            return View();
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
