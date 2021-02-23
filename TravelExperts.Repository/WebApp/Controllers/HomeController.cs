using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelExperts.BLL;
using TravelExperts.Repository.Domain;
using TravelExpertsWebApp.Models;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                int custID = Convert.ToInt32(User.Identity.Name);

                var recommendations = PackageManager.GetRecommendations(custID,6);

                ViewBag.Recommendations = recommendations;

            }

            return View();
        }

        //To display packages -- Holly
        public IActionResult Package()
        {
            var context = new TravelExpertsContext();
            var package = PackageManager.GetAll()
                .Select(p => new PackageViewModel
                {
                    PackageId = p.PackageId,
                    PkgName = p.PkgName,
                    PkgDesc = p.PkgDesc,
                    PkgStartDate = p.PkgStartDate,
                    PkgEndDate = p.PkgEndDate,
                    PkgBasePrice = p.PkgBasePrice.ToString("c"),
                }).ToList();

            return View(package);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
