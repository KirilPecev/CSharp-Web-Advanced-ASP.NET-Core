namespace FluffyDuffyMunchkinCats.Profiles
{
    using AutoMapper;
    using FluffyDuffyMunchkinCats.Models;

    public class CatProfile : Profile
    {
        public CatProfile()
        {
            CreateMap<Cat, CatViewModel>();
            CreateMap<CatInputModel, Cat>();
        }
    }
}
