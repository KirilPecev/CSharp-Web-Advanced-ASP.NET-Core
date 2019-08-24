namespace CoffeeShop.Services
{
    using Models;

    public interface IOrderService
    {
        CheckResult GetUpdate(int id);

        int NewOrder();
    }
}
