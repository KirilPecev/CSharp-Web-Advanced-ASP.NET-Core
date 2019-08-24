using AutoMapper;
using FluffyDuffyMunchkinCats.Models;
using FluffyDuffyMunchkinCats.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FluffyDuffyMunchkinCats.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICatsService catsService;

        public HomeController(IMapper mapper,ICatsService catsService)
        {
            this.mapper = mapper;
            this.catsService = catsService;
        }

        public IActionResult Index()
        {
            var cats = this.catsService.GetAll();
            var catsModel = this.mapper.Map<CatViewModel[]>(cats);

            return View(catsModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
