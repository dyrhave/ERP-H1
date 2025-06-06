public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
  public decimal Price { get; set; } = 1;
    public decimal BuyInPrice { get; set; } = 1;
    public decimal Quantity { get; set; } 
    public string Location { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal ShowProfitMargin
    { 
      get{ return ProfitMargin(Price, BuyInPrice); }
      set{ ProfitMargin(Price, BuyInPrice); } 
    }
    public decimal ShowProfit
    { 
      get{ return profit(Price, BuyInPrice); }
      set{ profit(Price, BuyInPrice); } 
    }

   public decimal profit (decimal Price, decimal BuyInPrice)
   {
         return (int)(Price - BuyInPrice);
   }
   public decimal ProfitMargin (decimal Price, decimal BuyInPrice)
   {
       
     return (int)((Price - BuyInPrice) / Price * 100);

   }

}
