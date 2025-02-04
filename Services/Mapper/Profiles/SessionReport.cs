using AutoMapper;
using Dtos.Person;

namespace Services.Mapper.Profiles
{
	public class SessionReport: Profile
	{

		public SessionReport()
		{
			CreateMap<SessionReport, PersonAddReportDto>().ReverseMap();
			
		}
	}
}
