﻿using Microsoft.AspNetCore.Mvc;
using ShoesApi.DbContextFile.DBFiles;
using ShoesApi.Models;
using ShoesApi.Models.ProductModel;

namespace ShoesApi.Interfaces
{
    public interface IProduct
    {
        public Task<List<AddProductTable>> Categories(string category);
        public Task<ProductInfo> InfoById(string ProductId);
        public Task<IActionResult> AddProduct(AddProduct addProduct);
        public Task<IActionResult> ProductDetail(UserCart cart);
        public Task<UserCardModel> UserCartDetails(string Uid);
    }
}
