namespace CoffeeShop.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Models;
    using Services;
    using System.Threading.Tasks;

    public class CoffeeHub : Hub
    {
        private readonly IOrderService orderService;

        public CoffeeHub(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task GetUpdateForOrder(int id)
        {
            CheckResult result;

            do
            {
                result = this.orderService.GetUpdate(id);

                if (result.IsNew)
                {
                    await this.Clients.Caller.SendAsync("ReceiveOrderUpdate", result.Update);
                }
            } while (!result.IsFinished);

            await this.Clients.Caller.SendAsync("Finished");
        }
    }
}
