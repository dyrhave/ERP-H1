using TECHCOOL.UI;

public class ProductEdit : Screen
{
    Product _product;
    public ProductEdit(Product product)
    {
        _product = product;
    }
    public override string Title { get; set; } = "Edit Products";
   

    protected override void Draw()
    {
        
        Form<Product> editor = new();
        
        editor.TextBox("Name", nameof(Product.Name));
        editor.TextBox("Description", nameof(Product.Description));
        editor.AddField("Price", new DecimalBox(){ Title = "Price", Property = nameof(Product.Price) }); 
        editor.AddField("BuyInPrice", new DecimalBox(){ Title = "BuyInPrice", Property = nameof(Product.BuyInPrice) });
        editor.AddField("Quantity", new DecimalBox(){ Title = "Quantity", Property = nameof(Product.Quantity) });
        editor.TextBox("Location", nameof(Product.Location));        
        editor.TextBox("Unit", nameof(Product.Unit));
        

        
        if (editor.Edit(_product))
    {
      Database.Instance.UpdateProduct(_product);
      Quit();
    }

        // Screen.AddKey(ConsoleKey.Escape, Back);
    }
}