using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelper;

public class MappingProfile : Profile
{
    public MappingProfile() // map FROM to TO
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
        CreateMap<Item, AuctionDto>();

        CreateMap<CreateAuctionDto, Auction>().ForMember(dest => dest.Item, opt => opt.MapFrom(src => src));
        CreateMap<UpdateAuctionDto, Item>();
        CreateMap<CreateAuctionDto, Item>();
    }
}
