using ShoesApi.Models.ProductModel;

namespace ShoesApi.Models
{
    public class UserCardModel
    {
        public AspUsersTable? aspUsersTable;
        public List<ProductTable>? productTable { get; set; }
        public List<ProductImgTable>? productImageTables { get; set; }
        public List<UserCartTable>? userCartTables { get; set; }
    }
}
