namespace Panda.AutoMapperProfiles
{
    using AutoMapper;
    using Models.Packages;
    using Services.DTOs.Packages;

    public class PackageProfile : Profile
    {
        public PackageProfile()
        {
            CreateMap<PackageDTO, PackageViewModel>();
            CreateMap<PackageDetailsDTO, PackageDetailsViewModel>();
            CreateMap<PackageDetailsDTO, PackageViewModel>();
        }
    }
}
