using Microsoft.Data.SqlClient;
using MyApi.Models;


namespace MyApi.Repositories;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var listprod= new List<Product>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            // use connection here
            
            var cmd = new SqlCommand(@"SELECT Id, Title,
                           Description, 
                           Price, 
                           DiscountPercentage, 
                           Rating, 
                           Stock, 
                           Brand, 
                           Category, 
                           Thumbnail
        FROM tblProducts", connection);
            
            await using var r = await cmd.ExecuteReaderAsync();
            while (await r.ReadAsync())
            {
                listprod.Add(new Product
                {
                    Id = r.GetInt32(0),
                    Title = r.GetString(1),
                    Description = r.IsDBNull(2) ? "" : r.GetString(2),
                    Price = r.GetDecimal(3),
                    DiscountPercentage = r.GetDouble(4),
                    Rating = r.GetDouble(5),
                    Stock = r.GetInt32(6),
                    Brand = r.IsDBNull(7) ? "" : r.GetString(7),
                    Category = r.IsDBNull(8) ? "" : r.GetString(8),
                    Thumbnail = r.IsDBNull(9) ? "" : r.GetString(9),
                    Images = new List<string>()
                });
            }

            return listprod;

        }
    }
    
    public async Task InsertProductsAsync(IEnumerable<Product> products)
    {
        await using var con = new SqlConnection(_connectionString);
        await con.OpenAsync();

        foreach (var p in products)
        {
            var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM tblProducts WHERE Id=@Id)
                INSERT INTO tblProducts
                (Id, Title, Description, Price, DiscountPercentage, Rating, Stock, Brand, Category, Thumbnail)
                VALUES
                (@Id, @Title, @Description, @Price, @DiscountPercentage, @Rating, @Stock, @Brand, @Category, @Thumbnail)", con);

            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@Title", p.Title ?? "");
            cmd.Parameters.AddWithValue("@Description", (object?)p.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", p.Price);
            cmd.Parameters.AddWithValue("@DiscountPercentage", p.DiscountPercentage);
            cmd.Parameters.AddWithValue("@Rating", p.Rating);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.Parameters.AddWithValue("@Brand", (object?)p.Brand ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Category", (object?)p.Category ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Thumbnail", (object?)p.Thumbnail ?? DBNull.Value);

            await cmd.ExecuteNonQueryAsync();
        }
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
       var product=new Product();
       using (var connection = new SqlConnection(_connectionString))
       {
           await connection.OpenAsync();
           
            
           var cmd = new SqlCommand(@"SELECT Id, Title,
                           Description, 
                           Price, 
                           DiscountPercentage, 
                           Rating, 
                           Stock, 
                           Brand, 
                           Category, 
                           Thumbnail
        FROM tblProducts WHERE Id = @Id", connection);
           
           cmd.Parameters.AddWithValue("@Id", id);
           await using var r = await cmd.ExecuteReaderAsync();
           if (!await r.ReadAsync()) return null;

           product.Id = r.GetInt32(0);
           product.Title = r.GetString(1);
           product.Description = r.IsDBNull(2) ? "" : r.GetString(2);
           product.Price = r.GetDecimal(3);
           product.DiscountPercentage = r.GetDouble(4);
           product.Rating = r.GetDouble(5);
           product.Stock = r.GetInt32(6);
           product.Brand = r.IsDBNull(7) ? "" : r.GetString(7);
           product.Category = r.IsDBNull(8) ? "" : r.GetString(8);
           product.Thumbnail = r.IsDBNull(9) ? "" : r.GetString(9);



           return product;
           
       }

    }

    public async Task InsertProductIfNotExistsAsync(Product product)
    {
        await using var con = new SqlConnection(_connectionString);
        await con.OpenAsync();
        
        var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM tblProducts WHERE Id=@Id)
                INSERT INTO tblProducts
                (Id, Title, Description, Price, DiscountPercentage, Rating, Stock, Brand, Category, Thumbnail)
                VALUES
                (@Id, @Title, @Description, @Price, @DiscountPercentage, @Rating, @Stock, @Brand, @Category, @Thumbnail)", con);

        cmd.Parameters.AddWithValue("@Id", product.Id);
        cmd.Parameters.AddWithValue("@Title", product.Title ?? "");
        cmd.Parameters.AddWithValue("@Description", (object?)product.Description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Price", product.Price);
        cmd.Parameters.AddWithValue("@DiscountPercentage", product.DiscountPercentage);
        cmd.Parameters.AddWithValue("@Rating", product.Rating);
        cmd.Parameters.AddWithValue("@Stock", product.Stock);
        cmd.Parameters.AddWithValue("@Brand", (object?)product.Brand ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Category", (object?)product.Category ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Thumbnail", (object?)product.Thumbnail ?? DBNull.Value);

        await cmd.ExecuteNonQueryAsync();

    }
    
}