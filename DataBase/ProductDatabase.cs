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
    public Product[] GetProduct()
    {
        return products.ToArray();
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