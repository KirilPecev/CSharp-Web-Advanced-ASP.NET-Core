namespace Panda.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Panda.Models.Packages;
    using Panda.Services;
    using System.Collections.Generic;

    public class PackagesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IPackagesService packagesService;

        public PackagesController(IMapper mapper, IUsersService usersService, IPackagesService packagesService)
        {
            this.mapper = mapper;
            this.usersService = usersService;
            this.packagesService = packagesService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            IEnumerable<string> models = this.usersService.GetAllUserNames();

            ViewData["Recipients"] = models;

            return this.View(new PackageCreateInputModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(PackageCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                IEnumerable<string> models = this.usersService.GetAllUserNames();

                ViewData["Recipients"] = models;

                return this.View(model);
            }

            this.packagesService.Create(model.Description, model.Weight, model.ShippingAddress, model.Recipient);

            return this.Redirect("/");
        }

        [Authorize(Roles = "Admin, User")]
        public IActionResult Details(string id)
        {
            var package = this.packagesService.GetPackageDetails(id);

            PackageDetailsViewModel model = this.mapper.Map<PackageDetailsViewModel>(package);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delivered()
        {
            var packages = this.packagesService.GetAllByStatus("Delivered");

            IEnumerable<PackageDetailsViewModel> model = this.mapper.Map<PackageDetailsViewModel[]>(packages);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            var packages = this.packagesService.GetAllByStatus("Pending");

            IEnumerable<PackageDetailsViewModel> model = this.mapper.Map<PackageDetailsViewModel[]>(packages);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Shipped()
        {
            var packages = this.packagesService.GetAllByStatus("Shipped");

            IEnumerable<PackageDetailsViewModel> model = this.mapper.Map<PackageDetailsViewModel[]>(packages);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Ship(string id)
        {
            this.packagesService.Ship(id);

            return this.RedirectToAction(nameof(Shipped));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Deliver(string id)
        {
            this.packagesService.Deliver(id);

            return this.RedirectToAction(nameof(Delivered));
        }

        [Authorize(Roles = "Admin, User")]
        public IActionResult Acquire(string id)
        {
            this.packagesService.Acquire(id);

            return this.Redirect("/");
        }
    }
}
