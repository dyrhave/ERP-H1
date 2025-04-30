public partial class Database
{
    List<Product> sales = new();
    public Product? GetSaleById(int id)
    {
        foreach (var sale in sales)
        {
            if (sale.ProductId == id)
            {
                return sale;
            }
        }
        return null;

    }
    public Product[] GetSale()
    {
        return sales.ToArray();
    }

    public void AddSale(Product sale)
    {
        if (sale.ProductId == 0)
        {
            sales.Add(sale);
            sale.ProductId = sales.Count;
        }
    }
    public void UpdateSale(Product sale)
    {
        if (sale.ProductId == 0)
        {
            return;
        }
        Product? oldsale = GetSaleById(sale.ProductId);
        if (oldsale == null)
        {
            return;
        }
        /*oldsale.Name = sale.Name;
        oldsale.Description = sale.Description;
        oldsale.Price = sale.Price;
        oldsale.BuyInPrice = sale.BuyInPrice;
        oldsale.Stock = sale.Stock;
        oldsale.Location = sale.Location;
        oldsale.Unit = sale.Unit;*/
    }
    public void DeleteSale(int id)
    {
        Product? sale = GetSaleById(id);
        if (sale != null)
        sales.Remove(sale);
    }
}