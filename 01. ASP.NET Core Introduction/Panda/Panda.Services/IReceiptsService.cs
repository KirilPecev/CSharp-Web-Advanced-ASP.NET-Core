namespace Panda.Services
{
    using Panda.Services.DTOs.Receipts;
    using System.Collections.Generic;

    public interface IReceiptsService
    {
        void CreateFromPackage(decimal weight, string packageId, string recipientId);

        IEnumerable<ReceiptDTO> GetAll();

        IEnumerable<ReceiptDTO> GetAllByUserId(string userId);

        ReceiptDetailsDTO GetReceiptById(string id);
    }
}

