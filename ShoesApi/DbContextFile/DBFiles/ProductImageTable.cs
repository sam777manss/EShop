namespace ShoesApi.DbContextFile.DBFiles
{
    public class ProductImageTable
    {
        public Guid? ProductImgId { get; set; } // primary key
        public Guid? ProductId { get; set; } // foreign key
        public string? ImageUrl { get; set; }
        public virtual AddProductTable? AddProductTables { get; set; } // to estamblish relation see fluent api
    }
}
