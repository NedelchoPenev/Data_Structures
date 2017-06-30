using System;

public class Product : IComparable<Product>
{
    public Product(string name, decimal price, string producer)
    {
        this.Name = name;
        this.Price = price;
        this.Producer = producer;
    }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Producer { get; set; }

    public int CompareTo(Product other)
    {
        int result = this.Name.CompareTo(other.Name);
        if (result == 0)
        {
            result = this.Producer.CompareTo(other.Producer);

            if (result == 0)
            {
                result = this.Price.CompareTo(other.Price);
            }
        }

        return result;
    }

    public override string ToString()
    {
        return $"{{{this.Name};{this.Producer};{this.Price:F2}" + "}";
    }
}