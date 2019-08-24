namespace Panda.AutoMapperProfiles
{
    using AutoMapper;
    using Panda.Extensions;
    using Panda.Models.Receipts;
    using Panda.Services.DTOs.Receipts;

    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<ReceiptDTO, ReceiptViewModel>()
                .ForMember(x => x.IssuedOn, opt => opt.MapFrom(src => src.IssuedOn.ToDate()));

            CreateMap<ReceiptDetailsDTO, ReceiptDetailsViewModel>()
                .ForMember(x => x.IssuedOn, opt => opt.MapFrom(src => src.IssuedOn.ToDate()));
        }
    }
}
