using AutoMapper;
using Hahn.ApplicatonProcess.February2021.Domain.Models.API;
using Hahn.ApplicatonProcess.February2021.Domain.Models.Business;
using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.Mapping
{
    public class CommonProfile : Profile
    {

        public CommonProfile()
        {
            var culture = Thread.CurrentThread.CurrentCulture;

            CreateMap<AssetDto, Asset>()
               .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.AssetName))
               .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
               .ForMember(dest => dest.CountryOfDepartment, opt => opt.MapFrom(src => src.Country))
               .ForMember(dest => dest.EMailAdressOfDepartment, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => !String.IsNullOrEmpty(src.PurchaseDate) ? DateTime.Parse(src.PurchaseDate, culture) : DateTime.UtcNow))
               .ForMember(dest => dest.Broken, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Broken) && Boolean.Parse(src.Broken)));

            CreateMap<Asset, AssetDto>()
                .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.AssetName))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => Convert.ToInt32(src.Department)))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.CountryOfDepartment))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMailAdressOfDepartment))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate.ToString(culture)))
                .ForMember(dest => dest.Broken, opt => opt.MapFrom(src => src.Broken));

            CreateMap<PostAssetRequest, AssetDto>();
            CreateMap<PutAssetRequest, AssetDto>();

            CreateMap<AssetDto, GetAssetResponse>()
                .ForMember(dest=>dest.Asset, opt=>opt.MapFrom(src=>src));

            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => $"key{src}"));

            CreateMap<ExpandoObject, CountryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Single(x => x.Key == "name").Value))
                .ForMember(dest => dest.Alpha3Code, opt => opt.MapFrom(src => src.Single(x => x.Key == "alpha3Code").Value))
                .ForMember(dest => dest.TopLevelDomains, opt => opt.MapFrom(src => src.Single(x => x.Key == "topLevelDomain").Value))
                .ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Single(x => x.Key == "translations").Value));
            
        }
    }
}
