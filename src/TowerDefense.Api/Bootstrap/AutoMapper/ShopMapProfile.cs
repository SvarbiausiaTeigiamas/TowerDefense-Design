using AutoMapper;
using TowerDefense.Api.Contracts.Shop;
using TowerDefense.Api.GameLogic.Shop;

namespace TowerDefense.Api.Bootstrap.AutoMapper
{
    public class ShopMapProfile : Profile
    {
        public ShopMapProfile()
        {
            CreateMap<IShop, GetShopItemsResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
