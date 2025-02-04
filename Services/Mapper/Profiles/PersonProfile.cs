using AutoMapper;
using Domain.Entity;
using Dtos.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapper.Profiles
{
	public class PersonProfile : Profile
	{
		public PersonProfile()
		{
			CreateMap<Person, PersonCancelDto>().ReverseMap();
	
		}
	}
}
