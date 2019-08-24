namespace Panda.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models.Packages;
    using Panda.Models;
    using Panda.Models.Home;
    using Panda.Services;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly AutoMapper.IMapper mapper;
        private readonly IPackagesService packagesService;

        public HomeController(IMapper mapper, IPackagesService packagesService)
        {
            this.mapper = mapper;
            this.packagesService = packagesService;
        }

        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();

            if (this.User.Identity.IsAuthenticated)
            {
                var pending = this.packagesService.GetAllByStatus("Pending").ToList();
                var delivered = this.packagesService.GetAllByStatus("Delivered");
                var shipped = this.packagesService.GetAllByStatus("Shipped");

                model.Pending = this.mapper.Map<PackageViewModel[]>(pending).ToList();
                model.Delivered = this.mapper.Map<PackageViewModel[]>(delivered).ToList();
                model.Shipped = this.mapper.Map<PackageViewModel[]>(shipped).ToList();
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
