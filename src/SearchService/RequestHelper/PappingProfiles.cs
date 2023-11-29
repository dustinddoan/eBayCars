using AutoMapper;
using Contracts;

namespace SearchService;

public class PappingProfiles : Profile
{
    public PappingProfiles()
    {
        CreateMap<AuctionCreated, Item>();
        CreateMap<AuctionUpdated, Item>();
    }
}
