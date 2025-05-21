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
        using (SqlConnection connection = new())
        {
            connection.Open();
            string queryString = "SELECT * FROM Products";
            using (SqlCommand command = new(queryString, connection))
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
                            Quantity = reader.GetInt32(5),
                            Location = reader.GetString(6),
                            Unit = reader.GetString(7)
                        };
                        productsList.Add(product);
                    }
                }
            }
        }
        return productsList.ToArray();
    }

    public void AddProduct(Product product)
    {
        if (product.ProductId == 0)
        {
            products.Add(product);
            product.ProductId = products.Count;
        }
    }
    public void UpdateProduct(Product product)
    {
        if (product.ProductId == 0)
        {
            return;
        }
        Product? oldproduct = GetProductById(product.ProductId);
        if (oldproduct == null)
        {
            return;
        }
        oldproduct.Name = product.Name;
        oldproduct.Description = product.Description;
        oldproduct.Price = product.Price;
        oldproduct.BuyInPrice = product.BuyInPrice;
        oldproduct.Quantity = product.Quantity;
        oldproduct.Location = product.Location;
        oldproduct.Unit = product.Unit;
    }
    public void DeleteProduct(int id)
    {
        Product? product = GetProductById(id);
        if (product != null)
            products.Remove(product);
    }
}