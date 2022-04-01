using AutoMapper;
using ProductManager.DTO;
using ProductManager.DTO.Category;
using ProductManager.DTO.Product;
using ProductManager.DTO.Purchase;
using ProductManager.DTO.Sale;
using ProductManager.Models;
using ProductManagerDTO.DTO.Category;
using ProductManagerDTO.DTO.Customer;
using ProductManagerDTO.DTO.Product;
using ProductManagerDTO.DTO.Purchase;
using ProductManagerDTO.DTO.Sale;
using ProductManagerDTO.DTO.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Helpers
{
    /// <summary>
    /// Helping automapper to map entities to DTOs.
    /// </summary>
    public class MapperInitilizer : Profile
    {
        /// <summary>
        /// Add Entities and DTOs so automapper can work.
        /// </summary>
        public MapperInitilizer()
        {
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductSingleResultDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Product, ProductCategoryDTO>().ReverseMap();
            CreateMap<Product, ProductNameIdDTO>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CustomerCreateDTO>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();
            CreateMap<Customer, CustomerNameIdDTO>().ReverseMap();

            CreateMap<Seller, SellerDTO>().ReverseMap();
            CreateMap<Seller, SellerCreateDTO>().ReverseMap();
            CreateMap<Seller, SellerUpdateDTO>().ReverseMap();
            CreateMap<Seller, SellerNameIdDTO>().ReverseMap();

            CreateMap<Sale, SaleDTO>().ReverseMap();
            CreateMap<Sale, SaleCreateDTO>().ReverseMap();
            CreateMap<Sale, SaleSingleResultDTO>().ReverseMap();
            CreateMap<Sale, SaleUpdateDTO>().ReverseMap();
            CreateMap<Sale, SaleSingleDTO>().ReverseMap();

            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseCreateDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseSingleResultDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseUpdateDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseSingleDTO>().ReverseMap();

            CreateMap<ApiUser,UserDTO>().ReverseMap();
        }
    }
}
