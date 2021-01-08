public class Order 
{
    public int Id { get; set; }

    public Customer Customer { get; set; }

    public decimal SubTotal { get; set; }

    public IShippingMethod Shipping { get; set; }

    public Tax Tax { get; set; }

    public decimal GetTotal()
    {
        return Tax.ApplyTax(SubTotal + Shipping.Cost);
    }
}

public class Customer 
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }
}

public interface IShippingMethod 
{
    decimal Cost { get; }
}

public class RegularShipping 
{
    public decimal Cost => 2.99;
}

public class RushShipping 
{
    public decimal NumberOfDays { get; set; }

    public decimal Cost => ((10.0 - NumberOfDays) * 0.99) + 2.99;
}

public class NoRush
{
    public decimal Cost => 0.00;
}

public class Tax 
{
    public decimal LocalRate { get; set; }
    public decimal StateRate { get; set; }

    public decimal TotalRate => LocalRate + StateRate;

    public decimal ApplyTax(decimal amount)
    {
        return Math.Round(TotalRate * amount, 2);
    }
}

public enum ShippingType 
{
    OverNight,
    TwoDay,
    Regular,
    NoRush
}