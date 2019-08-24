namespace FluffyDuffyMunchkinCats.Controllers
{
    using AutoMapper;
    using FluffyDuffyMunchkinCats.Models;
    using FluffyDuffyMunchkinCats.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class CatsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICatsService catsService;

        public CatsController(IMapper mapper, ICatsService catsService)
        {
            this.mapper = mapper;
            this.catsService = catsService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CatInputModel model)
        {
            var cat = this.mapper.Map<Cat>(model);
            this.catsService.Add(cat);

            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult Details(CatViewModel model)
        {
            var cat = this.catsService.GetAll().FirstOrDefault(x => x.Id == model.Id);

            var catModel = this.mapper.Map<CatViewModel>(cat);

            return this.View(catModel);
        }
    }
}
