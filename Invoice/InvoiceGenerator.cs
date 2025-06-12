public class InvoiceGenerator
{
    private Database _database; 
    private string _templatePath = "Invoice/invoice_template.html";

    public InvoiceGenerator(Database db)
    {
        _database = db;
    }
    public string GenerateInvoiceHtml(int orderId)
    {
        SalesOrderHeader? order = _database.GetSalesOrderWithOrderLines(orderId);
        if (order == null) return "<p>Error: Order not found.</p>";

        Customer? customer = _database.GetCustomerByIdWithPerson(order.CustomerId);
        if (customer == null) return "<p>Error: Customer not found.</p>";

        string htmlContent;
        string cssContent;
        try
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", ".."));
            string absoluteTemplatePath = Path.Combine(projectRoot, _templatePath);
            string absoluteCssPath = Path.Combine(projectRoot, "Invoice/styles.css");

            if (!File.Exists(absoluteTemplatePath))
            {
                return $"<p>Error: Template file not found at {absoluteTemplatePath}</p>";
            }

            if (!File.Exists(absoluteCssPath))
            {
                return $"<p>Error: CSS file not found at {absoluteCssPath}</p>";
            }

            htmlContent = File.ReadAllText(absoluteTemplatePath);
            cssContent = File.ReadAllText(absoluteCssPath);
        }
        catch (Exception ex)
        {
            return $"<p>Error reading template files: {ex.Message}</p>";
        }

        string embeddedCss = $"<style>\n{cssContent}\n</style>";
        htmlContent = htmlContent.Replace("<link rel=\"stylesheet\" href=\"styles.css\">", embeddedCss);
        htmlContent = htmlContent.Replace("{{CustomerName}}", $"{customer.FirstName} {customer.LastName}");
        htmlContent = htmlContent.Replace("{{CustomerAddress}}", $"{customer.Street} {customer.StreetNumber}, {customer.City}, {customer.PostCode} {customer.Country}");
        htmlContent = htmlContent.Replace("{{CustomerEmail}}", customer.Email);
        htmlContent = htmlContent.Replace("{{OrderNumber}}", order.OrderId.ToString());
        htmlContent = htmlContent.Replace("{{OrderDate}}", order.Created);
        string itemsHtml = "";
        foreach (var lineItem in order.OrderLines)
        {
            Product? product = _database.GetProductById(lineItem.ProductId);
            string productName = product != null ? product.Name : "Product Not Found";
            itemsHtml += $"<tr><td>{productName}</td><td>{lineItem.Quantity}</td><td>DKK {lineItem.UnitPrice:N2}</td><td>DKK {lineItem.LineTotal:N2}</td></tr>\n";
        }
        htmlContent = htmlContent.Replace("<!-- {{InvoiceItems}} -->", itemsHtml);

        decimal subtotal = order.OrderAmount;
        decimal vatRate = 0.25m; // 25% VAT
        decimal vatAmount = subtotal * vatRate;
        decimal grandTotal = subtotal + vatAmount;

        htmlContent = htmlContent.Replace("{{Subtotal}}", subtotal.ToString("N2"));
        htmlContent = htmlContent.Replace("{{VATAmount}}", vatAmount.ToString("N2"));
        htmlContent = htmlContent.Replace("{{GrandTotal}}", grandTotal.ToString("N2"));

        return htmlContent;
    }

    public void SaveInvoiceToFile(int orderId, string filePath)
    {
        string html = GenerateInvoiceHtml(orderId);
        try
        {
            File.WriteAllText(filePath, html);
            Console.WriteLine($"Invoice saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving invoice: {ex.Message}");
        }
    }
}