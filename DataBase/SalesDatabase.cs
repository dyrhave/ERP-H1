using TECHCOOL.UI;

public partial class Database
{
    List<SalesOrder> sales = new();
    public SalesOrder? GetSaleById(int id)
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
    public SalesOrder[] GetSales()
    {
        return sales.ToArray();
    }

    public void AddSale(SalesOrder sale)
    {
        if (sale.OrderId == 0)
        {
            sales.Add(sale);
            sale.OrderId = sales.Count;
        }
    }
    public void UpdateSale(SalesOrder sale)
    {
        if (sale.OrderId == 0)
        {
            return;
        }
        SalesOrder? oldsale = GetSaleById(sale.OrderId);
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
        SalesOrder? sale = GetSaleById(id);
        if (sale != null)
        sales.Remove(sale);
    }
}