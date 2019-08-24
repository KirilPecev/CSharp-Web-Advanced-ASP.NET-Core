namespace FluffyDuffyMunchkinCats.Services
{
    using FluffyDuffyMunchkinCats.Models;
    using FluffyDuffyMunchkinCats.Models.Data;
    using System.Collections.Generic;

    public class CatsService : ICatsService
    {
        private readonly Context context;

        public CatsService(Context context)
        {
            this.context = context;
        }

        public void Add(Cat cat)
        {
            this.context.Cats.Add(cat);
            this.context.SaveChanges();
        }

        public IEnumerable<Cat> GetAll()
        {
            return this.context.Cats;
        }
    }
}
