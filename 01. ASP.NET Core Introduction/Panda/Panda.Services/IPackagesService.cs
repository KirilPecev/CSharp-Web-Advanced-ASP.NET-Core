namespace Panda.Services
{
    using DTOs.Packages;
    using System.Collections.Generic;

    public interface IPackagesService
    {
        void Create(string description, decimal weight, string shippingAddress, string recipientName);

        IEnumerable<PackageDetailsDTO> GetAllByStatus(string status);

        PackageDetailsDTO GetPackageDetails(string id);

        void Deliver(string id);

        void Ship(string id);

        void Acquire(string id);
    }
}