using AutoMapper;
using Domain.Entity;
using Dtos.Session;

namespace Services.Mapper.Profiles
{
	public class SessionProfile:Profile
	{
		public SessionProfile()
		{

			CreateMap<Session, SessionAddDto>().ReverseMap();
		}
	}
}
