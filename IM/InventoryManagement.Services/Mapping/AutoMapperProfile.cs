using AutoMapper;
using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.DTOs.CategorySub;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Services.Mapping
{

    public class AutoMapperProfile : Profile
    {
        //public T GetValueOrDefault(T defaultValue)
        //{
        //    return HasValue ? value : defaultValue;
        //}

        public AutoMapperProfile()
        {
            #region Company DTO
            CreateMap<CompanyDto, Company>().ReverseMap();
            CreateMap<CompanyCreateDto, CompanyDto>().ReverseMap();
            CreateMap<CompanyUpdateDto, CompanyDto>().ReverseMap();
            
            #endregion

            #region Category DTO
            CreateMap<Category, CategoryDto>().ReverseMap();  //Get işlemleri için
            CreateMap<Category, CategoryUpdateDto>().ReverseMap(); //Update islemi için calısıyor

            CreateMap<Category, CategoryCreateDto>().ReverseMap(); //post islemi için
            CreateMap<CategoryDto, CategoryCreateDto>().ReverseMap(); //post islemi için
            CreateMap<Category, CategoryWithCategorySubDto>().ReverseMap(); //kategory ve alt kategorileri almak için
            #endregion


            #region CategorySub DTO
            CreateMap<CategorySub, CategorySubDto>().ReverseMap(); //Get islemi için
            CreateMap<CategorySubDto, CategorySubCreateDto>().ReverseMap(); // post için mapleme
            CreateMap<CategorySubDto, CategorySubUpdateDto>().ReverseMap(); //çalışıyor put içi
            #endregion

        }
    }
}
