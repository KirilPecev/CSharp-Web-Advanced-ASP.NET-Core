namespace CoffeeShop.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class OrderService : IOrderService
    {
        private readonly string[] status =
        {
            "Grinding beans",
            "Steaming milk",
            "Quality control",
            "Delivering...",
            "Picked up"
        };

        public OrderService()
        {
            this.random = new Random();
            this.indexes = new List<int>();
        }

        private readonly Random random;

        private readonly IList<int> indexes;

        public CheckResult GetUpdate(int id)
        {
            Thread.Sleep(1000);

            var index = this.indexes[id - 1];

            if (this.random.Next(0, 4) == 2)
            {
                if (this.status.Length > this.indexes[id - 1])
                {
                    var result = new CheckResult
                    {
                        IsNew = true,
                        Update = this.status[index],
                        IsFinished = this.status.Length - 1 == index
                    };

                    this.indexes[id - 1]++;
                    return result;
                }
            }

            return new CheckResult { IsNew = false };
        }

        public int NewOrder()
        {
            this.indexes.Add(0);
            return this.indexes.Count;
        }
    }
}
