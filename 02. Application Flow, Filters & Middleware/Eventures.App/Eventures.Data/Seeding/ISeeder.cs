﻿namespace Eventures.Data.Seeding
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync();
    }
}
