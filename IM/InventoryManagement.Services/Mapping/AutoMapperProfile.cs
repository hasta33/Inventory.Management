using AutoMapper;
using InventoryManagement.Core.DTOs.BrandModel;
using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Services.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Company DTO
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CompanyCreateDto>().ReverseMap();
            CreateMap<Company, CompanyUpdateDto>().ReverseMap();
            #endregion

            #region Category DTO
            CreateMap<Category, CategoryGetDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            #endregion


            #region BrandModel DTO
            CreateMap<BrandModel, BrandModelGetDto>().ReverseMap();
            CreateMap<BrandModel, BrandModelCreateDto>().ReverseMap();
            CreateMap<BrandModel, BrandModelUpdateDto>().ReverseMap();
            #endregion



        }
    }
}
