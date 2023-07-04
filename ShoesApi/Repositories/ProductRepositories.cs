using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.DbContextFile;
using ShoesApi.DbContextFile.DBFiles;
using ShoesApi.Interfaces;
using ShoesApi.Models;
using ShoesApi.Models.ProductModel;

namespace ShoesApi.Repositories
{
    public class ProductRepositories : IProduct
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext context;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserRepositories));
        private UserManager<AppUser> userManager;

        public ProductRepositories(ApplicationDbContext context, UserManager<AppUser> userManager
            , IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<List<AddProductTable>> Categories(string category)
        {
            try
            {
                List<AddProductTable> product = context.AddProductTable.Where(c => c.ProductCategory == category).ToList();
                return product;
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return new List<AddProductTable>();
        }

        public async Task<ProductInfo> InfoById(string ProductId)
        {
            try
            {
                //QuickUrlLinks = (from c in _Context.CategoryMapping
                //                 join cat in _Context.Category on c.CategoryID equals cat.CategoryID
                //                 join user in _Context.tblUser on cat.UserID equals user.UserID
                //                 where user.UserID == CurrentAdminSession.UserID && c.isQuickLink == true
                //                 orderby c.SortOrder descending
                //                 select new QuickLinkViewModel { Name = c.Name, UrlId = c.CategoryMappingID, Url = c.CategoryURL, IsQuickLink = c.isQuickLink, sortOrder = c.SortOrder, isShared = false, CategoryId = c.CategoryID }).ToList();
                var data = context.AddProductTable.Where(c => c.ProductId.ToString() == ProductId).FirstOrDefault();

                ProductInfo Productinfo = new ProductInfo()
                {
                    ProductDetails = data,
                    images = new List<string>()
                };

                var info = (from image in context.ProductImageTable
                            where image.ProductId == data.ProductId
                            select new ProductImageTable
                            {
                                ProductImgId = image.ProductImgId,
                                ProductId = image.ProductId,
                                ImageUrl = image.ImageUrl
                            }).ToList();
                int i = 1;
                foreach (var image in info)
                {
                    if (i == 1)
                    {
                        Productinfo.ProductImgId = image.ProductImgId.ToString();
                    }
                    Productinfo.images.Add(image.ImageUrl.ToString());
                    i--;
                }

                return Productinfo;
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return new ProductInfo();
        }

        public async Task<IActionResult> ProductDetail(UserCart cart)
        {
            try
            {
                AppUser user = await userManager.FindByIdAsync(cart.UserId.ToString());
                if(!string.IsNullOrEmpty(user.State) && !string.IsNullOrEmpty(user.Zip_Code) 
                   && !string.IsNullOrEmpty(user.Country) && !string.IsNullOrEmpty(user.Address))
                {
                    context.UserCart.Add(cart);
                    await context.SaveChangesAsync();
                    return new StatusCodeResult(200);
                }
                else
                {
                    return new StatusCodeResult(406);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return new StatusCodeResult(500);
        }

        public async Task<IActionResult> AddProduct(AddProduct product)
        {
            try
            {
                // Create a new instance of Product using the data from the AddProduct object
                AddProductTable newProduct = new AddProductTable
                {
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductType = product.ProductType,
                    ProductCategory = product.ProductCategory,
                    ProductCategoryType = product.ProductCategoryType,
                    ProductCategoryDescription = product.ProductCategoryDescription,
                    VendorEmail = product.VendorEmail,
                    Price = product.Price,
                    Small = product.Small,
                    Medium = product.Medium,
                    Large = product.Large,
                    XL = product.XL,
                    XXL = product.XXL,
                    ProductId = product.ProductId,
                    MainImage = product.Files?.FirstOrDefault()?.FileName // fetch first main image
                };
                context.AddProductTable.Add(newProduct);
                foreach (var file in product.Files)
                {
                    var productImage = new ProductImageTable
                    {
                        ImageUrl = file.FileName,
                        AddProductTables = newProduct
                    };
                    context.ProductImageTable.Add(productImage);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return new StatusCodeResult(500);
        }

        public async Task<UserCardModel> UserCartDetails(string Uid)
        {
            try
            {
                AppUser userData = await userManager.FindByIdAsync(Uid);
                UserCardModel userCardModel = new UserCardModel()
                {
                    aspUsersTable = new AspUsersTable(),
                    productTable = new List<ProductTable>(),
                    productImageTables = new List<ProductImgTable>(),
                    userCartTables = new List<UserCartTable>()
                };
                AspUsersTable aspUsersTable = new AspUsersTable();
                userCardModel.aspUsersTable = _mapper.Map<AspUsersTable>(userData); // user data

                var data = (from ucart in context.UserCart
                            join product in context.AddProductTable on ucart.ProductId equals product.ProductId
                            where ucart.UserId == Guid.Parse(Uid)
                            select new { Product = product, UserCart = ucart }).ToList();

                ProductTable productTables = new ProductTable();
                List<AddProductTable> productTable = data.Select(up => up.Product).ToList();

                foreach(var product in productTable)
                {
                    productTables.ProductId = product.ProductId.ToString();
                    productTables.ProductName = product.ProductName;
                    productTables.ProductDescription = product.ProductDescription;
                    productTables.ProductType = product.ProductType;
                    productTables.ProductCategory = product.ProductCategory;
                    productTables.ProductCategoryDescription = product.ProductCategoryDescription;
                    productTables.ProductCategoryType = product.ProductCategoryType;
                    productTables.VendorEmail = product.VendorEmail;
                    productTables.MainImage = product.MainImage;
                    productTables.Price = product.Price;
                    productTables.Small = product.Small;
                    productTables.Medium = product.Medium;
                    productTables.Large = product.Large;
                    productTables.XL = product.XL;
                    productTables.XXL = product.XXL;

                    userCardModel.productTable.Add(productTables);
                }

                UserCartTable userCartTables = new UserCartTable();
                List<UserCart> userCarts = data.Select(up => up.UserCart).ToList();
                foreach (var userCart in userCarts)
                {
                    userCartTables.UserId = userCart.Id.ToString();
                    userCartTables.UserId = userCart.UserId.ToString();
                    userCartTables.ProductId = userCart.ProductId.ToString();
                    userCartTables.ProuctColor = userCart.ProuctColor;
                    userCartTables.ProductSize = userCart.ProductSize;
                    userCartTables.ProductSum = userCart.ProductSum;

                    userCardModel.userCartTables.Add(userCartTables);
                }
                return userCardModel;
                //userCardModel = UserDataInitialiser(Uid, userCardModel);
                //userCardModel = productAndUserCart(Uid, userCardModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return new UserCardModel();
        }

    }
}
