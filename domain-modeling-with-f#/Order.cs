public class Order 
{
    public int Id { get; set; }

    public Customer Customer { get; set; }

    public decimal SubTotal { get; set; }

    public IShippingMethod Shipping { get; set; }

    public Tax Tax { get; set; }

    public decimal GetTotal()
    {
        return Math.Round(Tax.ApplyTax(SubTotal) + Shipping.Cost, 2);
    }
}

public class Customer 
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }
}

public class BaseShipping 
{
    public abstract decimal Cost { get; }

    public decimal AddShipping(decimal amount)
    {
        return Cost + amount;
    }
}

public class RegularShipping : BaseShipping
{
    public override decimal Cost => 2.99;
}

public class RushShipping : BaseShipping
{
    public decimal NumberOfDays { get; set; }

    public override decimal Cost => ((10.0 - NumberOfDays) * 0.99) + 2.99;
}

public class NoRush : BaseShipping
{
    public override decimal Cost => 0.00;
}

public class Tax 
{
    public decimal LocalRate { get; set; }
    public decimal StateRate { get; set; }

    public decimal TotalRate => LocalRate + StateRate;

    public decimal ApplyTax(decimal amount)
    {
        return TotalRate * amount;
    }
}