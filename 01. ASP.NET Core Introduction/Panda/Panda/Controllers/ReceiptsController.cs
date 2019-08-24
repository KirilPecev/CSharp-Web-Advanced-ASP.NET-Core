namespace Panda.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Panda.Models.Receipts;
    using Panda.Services;
    using System.Security.Claims;

    public class ReceiptsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IReceiptsService receiptsService;

        public ReceiptsController(IMapper mapper, IReceiptsService receiptsService)
        {
            this.mapper = mapper;
            this.receiptsService = receiptsService;
        }

        [Authorize(Roles = "Admin, User")]
        public IActionResult Details(string id)
        {
            var receipt = this.receiptsService.GetReceiptById(id);

            ReceiptDetailsViewModel model = this.mapper.Map<ReceiptDetailsViewModel>(receipt);

            return this.View(model);
        }

        [Authorize(Roles = "Admin, User")]
        public IActionResult Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var receipts = this.receiptsService.GetAllByUserId(userId);
            if (this.User.IsInRole("Admin"))
            {
                receipts = this.receiptsService.GetAll();
            }

            RecepitsListViewModel model = new RecepitsListViewModel()
            {
                Receipts = this.mapper.Map<ReceiptViewModel[]>(receipts)
            };

            return this.View(model);
        }
    }
}
