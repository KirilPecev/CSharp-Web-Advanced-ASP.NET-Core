namespace Panda.Services
{
    using Microsoft.EntityFrameworkCore;
    using Panda.Data;
    using Panda.Models;
    using Panda.Services.DTOs.Receipts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReceiptsService : IReceiptsService
    {
        private const decimal FeeConst = 2.67M;

        private readonly PandaDbContext db;

        public ReceiptsService(PandaDbContext db)
        {
            this.db = db;
        }

        public void CreateFromPackage(decimal weight, string packageId, string recipientId)
        {
            Receipt receipt = new Receipt()
            {
                PackageId = packageId,
                RecipientId = recipientId,
                IssuedOn = DateTime.UtcNow,
                Fee = weight * FeeConst
            };

            this.db.Receipts.Add(receipt);
            this.db.SaveChanges();
        }

        public IEnumerable<ReceiptDTO> GetAll()
        {
            return this.db
                .Receipts
                .Include(x => x.Recipient)
                .Select(x => new ReceiptDTO
                {
                    Id = x.Id,
                    Fee = x.Fee,
                    Recipient = x.Recipient.UserName,
                    IssuedOn = x.IssuedOn
                })
                .AsEnumerable();
        }

        public IEnumerable<ReceiptDTO> GetAllByUserId(string userId)
        {
            return this.db
                 .Receipts
                 .Where(user => user.Recipient.Id == userId)
                 .Select(x => new ReceiptDTO
                 {
                     Id = x.Id,
                     Fee = x.Fee,
                     Recipient = x.Recipient.UserName,
                     IssuedOn = x.IssuedOn
                 })
                 .ToList();
        }

        public ReceiptDetailsDTO GetReceiptById(string id)
        {
            var currentReceipt = this.db
                .Receipts
                .Include(x => x.Package)
                .ThenInclude(x => x.Recipient)
                .FirstOrDefault(receipt => receipt.Id == id);

            ReceiptDetailsDTO model = new ReceiptDetailsDTO
            {
                Id = currentReceipt.Id,
                Description = currentReceipt.Package.Description,
                Fee = currentReceipt.Fee,
                IssuedOn = currentReceipt.IssuedOn,
                ShippingAddress = currentReceipt.Package.ShippingAddress,
                Weight = currentReceipt.Package.Weight,
                Recipient = currentReceipt.Recipient.UserName
            };

            return model;
        }
    }
}
