namespace CoffeeShop.Controllers
{
    using Hubs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Models;
    using Services;
    using System.Threading.Tasks;

    public class CoffeeController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IHubContext<CoffeeHub> coffeeHub;

        public CoffeeController(IOrderService orderService, IHubContext<CoffeeHub> coffeeHub)
        {
            this.orderService = orderService;
            this.coffeeHub = coffeeHub;
        }

        [HttpPost]
        public async Task<IActionResult> OrderCoffee([FromBody]Order order)
        {
            await this.coffeeHub.Clients.All.SendAsync("NewOrder", order);
            var orderId = this.orderService.NewOrder();
            return this.Accepted(orderId);
        }
    }
}
