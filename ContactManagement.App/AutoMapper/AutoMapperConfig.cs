using AutoMapper;
using ContactManagement.App.ViewModels;
using ContactManagement.Business.Models;

namespace ContactManagement.App.AutoMapper;

public class AutoMapperConfig : Profile
{
	public AutoMapperConfig()
	{
        CreateMap<Contact, ContactViewModel>().ReverseMap();
    }
}
