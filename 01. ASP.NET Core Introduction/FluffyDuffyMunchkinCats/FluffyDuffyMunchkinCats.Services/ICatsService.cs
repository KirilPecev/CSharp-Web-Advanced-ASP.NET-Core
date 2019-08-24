namespace FluffyDuffyMunchkinCats.Services
{
    using FluffyDuffyMunchkinCats.Models;
    using System.Collections.Generic;

    public interface ICatsService
    {
        IEnumerable<Cat> GetAll();

        void Add(Cat cat);
    }
}
