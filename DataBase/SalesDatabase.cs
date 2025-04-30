public partial class Database
{
    List<SalesHeader> sales = new();
    public SalesHeader? GetSaleById(int id)
    {
        foreach (var sale in sales)
        {
            if (sale.OrderId == id)
            {
                return sale;
            }
        }
        return null;

    }
    public SalesHeader[] GetSale()
    {
        return sales.ToArray();
    }

    public void AddSale(SalesHeader sale)
    {
        if (sale.OrderId == 0)
        {
            sales.Add(sale);
            sale.OrderId = sales.Count;
        }
    }
    public void UpdateSale(SalesHeader sale)
    {
        if (sale.OrderId == 0)
        {
            return;
        }
        SalesHeader? oldsale = GetSaleById(sale.OrderId);
        if (oldsale == null)
        {
            return;
        }
        oldsale.OrderId = sale.OrderId;
        oldsale.OrderItems = sale.OrderItems;
        oldsale.State = sale.State;
        //sprøg simmon omkring S1 update
        
    }
    public void DeleteSale(int id)
    {
        SalesHeader? sale = GetSaleById(id);
        if (sale != null)
        sales.Remove(sale);
    }
}