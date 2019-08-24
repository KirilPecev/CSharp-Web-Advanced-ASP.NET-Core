namespace Panda.Services
{
    using DTOs.Packages;
    using Microsoft.EntityFrameworkCore;
    using Panda.Data;
    using Panda.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class PackagesService : IPackagesService
    {
        private readonly PandaDbContext db;
        private readonly IReceiptsService receiptsService;

        public PackagesService(PandaDbContext db, IReceiptsService receiptsService)
        {
            this.db = db;
            this.receiptsService = receiptsService;
        }

        public void Create(string description, decimal weight, string shippingAddress, string recipientName)
        {
            var userId = this.db.Users.Where(x => x.UserName == recipientName).Select(x => x.Id).FirstOrDefault();
            if (userId == null)
            {
                return;
            }

            var package = new Package
            {
                Description = description,
                Weight = weight,
                Status = Status.Pending,
                ShippingAddress = shippingAddress,
                RecipientId = userId,
                EstimatedDeliveryTime = null
            };

            this.db.Packages.Add(package);
            this.db.SaveChanges();
        }

        public void Deliver(string id)
        {
            var package = this.db.Packages.FirstOrDefault(x => x.Id == id);
            if (package == null)
            {
                return;
            }

            package.Status = Status.Delivered;
            this.db.SaveChanges();
        }

        public void Ship(string id)
        {
            Random rn = new Random();
            var package = this.db.Packages.FirstOrDefault(x => x.Id == id);
            if (package == null)
            {
                return;
            }

            package.Status = Status.Shipped;
            package.EstimatedDeliveryTime = DateTime.UtcNow.AddDays(rn.Next(20, 40));
            this.db.SaveChanges();
        }

        public void Acquire(string id)
        {
            var package = this.db.Packages.FirstOrDefault(x => x.Id == id);
            if (package == null)
            {
                return;
            }

            package.Status = Status.Acquired;
            this.db.SaveChanges();

            this.receiptsService.CreateFromPackage(package.Weight, package.Id, package.RecipientId);
        }

        public IEnumerable<PackageDetailsDTO> GetAllByStatus(string status)
        {
            List<PackageDetailsDTO> packages = this.db.Packages.Where(x => x.Status.ToString() == status)
                .Include(x => x.Recipient)
                            .Select(x => new PackageDetailsDTO
                            {
                                Description = x.Description,
                                Id = x.Id,
                                Recipient = x.Recipient.UserName,
                                ShippingAddress = x.ShippingAddress,
                                Weight = x.Weight
                            }).ToList();

            return packages;
        }

        public PackageDetailsDTO GetPackageDetails(string packageId)
        {
            var currentPackage = this.db
                .Packages
                .Include(x => x.Recipient)
                .FirstOrDefault(package => package.Id == packageId);

            string deliveryTime = GetEstimatedDeliveryTime(currentPackage);

            PackageDetailsDTO detailsDTO = new PackageDetailsDTO()
            {
                Description = currentPackage.Description,
                Recipient = currentPackage.Recipient.UserName,
                ShippingAddress = currentPackage.ShippingAddress,
                Status = currentPackage.Status.ToString(),
                Weight = currentPackage.Weight,
                EstimatedDeliveryTime = deliveryTime
            };

            return detailsDTO;
        }

        private static string GetEstimatedDeliveryTime(Package currentPackage)
        {
            string deliveryTime = string.Empty;

            if (currentPackage.Status == Status.Pending)
            {
                deliveryTime = "N/A";
            }
            else if (currentPackage.Status == Status.Shipped)
            {
                deliveryTime = currentPackage.EstimatedDeliveryTime?.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("en-EN"));
            }
            else
            {
                deliveryTime = "Delivered";
            }

            return deliveryTime;
        }
    }
}