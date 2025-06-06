using Microsoft.Data.SqlClient;

public partial class Database
{
    List<Product> products = new();
    public Product? GetProductById(int id)
    {
        foreach (var product in products)
        {
            if (product.ProductId == id)
            {
                return product;
            }
        }
        return null;

    }
    public Product[] GetProduct() // Temporary structure - fix when database is implemented
    {
        List<Product> productsList = new();
        SqlConnection conn = GetConnection();
        
            
            string queryString = "SELECT * FROM ProductDatabase";
            using (SqlCommand command = new(queryString, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new()
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            BuyInPrice = reader.GetDecimal(4),
                            Quantity = reader.GetDecimal(5),
                            Location = reader.GetString(6),
                            Unit = reader.GetString(7)
                        };
                        productsList.Add(product);
                    }
                }
            }
        
        return productsList.ToArray();
    }

    public void AddProduct(Product product)
    {
        SqlConnection conn = GetConnection();

        string sql = @"
        INSERT INTO ProductDatabase (Name, Description, Price, BuyInPrice, Quantity, Location, Unit)
        VALUES (@Name, @Description, @Price, @BuyInPrice, @Quantity, @Location, @Unit);
        SELECT SCOPE_IDENTITY();
    ";

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Name", product.Name);
        cmd.Parameters.AddWithValue("@Description", product.Description);
        cmd.Parameters.AddWithValue("@Price", product.Price);
        cmd.Parameters.AddWithValue("@BuyInPrice", product.BuyInPrice);
        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
        cmd.Parameters.AddWithValue("@Location", product.Location.ToString());
        cmd.Parameters.AddWithValue("@Unit", product.Unit);




        object result = cmd.ExecuteScalar();
        if (result != null && int.TryParse(result.ToString(), out int newId))
        {
            product.ProductId = newId;
        }
    }
    public void UpdateProduct(Product product)
    {
        SqlConnection conn = GetConnection();


        string sql = @"
            UPDATE ProductDatabase
            SET Name = @Name, Description = @Description, Price = @Price, BuyInPrice = @BuyInPrice, Quantity = @Quantity, Location = @Location, Unit = @Unit
            WHERE ProductId = @ProductId;
        ";

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
        cmd.Parameters.AddWithValue("@Name", product.Name);
        cmd.Parameters.AddWithValue("@Description", product.Description);
        cmd.Parameters.AddWithValue("@Price", product.Price);
        cmd.Parameters.AddWithValue("@BuyInPrice", product.BuyInPrice);
        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
        cmd.Parameters.AddWithValue("@Location", product.Location.ToString());
        cmd.Parameters.AddWithValue("@Unit", product.Unit);

        cmd.ExecuteNonQuery();
    }
    public void DeleteProduct(int id)
    {
        SqlConnection conn = GetConnection();


        string sql = "DELETE FROM ProductDatabase WHERE ProductId = @ProductId";
        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ProductId", id);

        cmd.ExecuteNonQuery();


        companies.RemoveAll(c => c.CompanyId == id);
    }
}