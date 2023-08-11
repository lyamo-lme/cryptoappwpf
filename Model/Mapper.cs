using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Model
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Cryptocurrency,CryptocurrencyModelView>().ReverseMap();
        }
    }
}
