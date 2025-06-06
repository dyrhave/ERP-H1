using TECHCOOL.UI;

public class DecimalBox : TextBox
{
    decimal value;
    public override object Value
    {
        get => value;
        set
        {
            decimal.TryParse(value.ToString(), out this.value);
        }
    }
    public override string ToString()
    {
        return value.ToString();
    }

}