using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wintellect.PowerCollections;

public class ShoppingCenter
{
    private Dictionary<string, OrderedBag<Product>> byName;
    private Dictionary<string, OrderedBag<Product>> byProducer;
    private OrderedDictionary<decimal, OrderedBag<Product>> byPrice;
    private Dictionary<string, OrderedBag<Product>> byNameAndProducer;

    public ShoppingCenter()
    {
        this.byName = new Dictionary<string, OrderedBag<Product>>();
        this.byProducer = new Dictionary<string, OrderedBag<Product>>();
        this.byPrice = new OrderedDictionary<decimal, OrderedBag<Product>>();
        this.byNameAndProducer = new Dictionary<string, OrderedBag<Product>>();
    }

    public string AddProduct(string name, decimal price, string producer )
    {
        var newProduct = new Product(name, price, producer);
        this.byName.AppendValueToKey(name, newProduct);
        this.byProducer.AppendValueToKey(producer, newProduct);
        this.byPrice.AppendValueToKey(price, newProduct);
        this.byNameAndProducer.AppendValueToKey(name + producer, newProduct);
        return "Product added";
    }

    public string DeleteProducts(string producer)
    {
        if (!this.byProducer.ContainsKey(producer))
        {
            return "No products found";
        }
        var product = this.byProducer[producer];
        this.byProducer.Remove(producer);
        foreach (Product currProduct in product)
        {
            var name = currProduct.Name;
            var price = currProduct.Price;
            this.byName[name].Remove(currProduct);
            this.byPrice[price].Remove(currProduct);
            this.byNameAndProducer[name + producer].Remove(currProduct);
        }

        return $"{product.Count} products deleted";
    }

    public string DeleteProducts(string name, string producer)
    {
        var nameProducer = name + producer;
        if (!this.byNameAndProducer.ContainsKey(nameProducer))
        {
            return "No products found";
        }

        var product = this.byNameAndProducer[nameProducer];
        this.byNameAndProducer.Remove(nameProducer);
        foreach (Product currProduct in product)
        {
            var price = currProduct.Price;
            this.byName[name].Remove(currProduct);
            this.byPrice[price].Remove(currProduct);
            this.byProducer[producer].Remove(currProduct);
        }

        return $"{product.Count} products deleted";
    }

    public string FindProductsByName(string name)
    {
        if (!this.byName.ContainsKey(name) || this.byName[name].Count == 0)
        {
            return "No products found\n";
        }

        var result = this.byName[name];
        StringBuilder sb = new StringBuilder();
        foreach (Product product in result)
        {
            sb.Append(product).Append(Environment.NewLine);
        }

        return sb.ToString();
    }

    public string FindProductsByProducer(string producer)
    {
        if (!this.byProducer.ContainsKey(producer) || this.byProducer[producer].Count == 0)
        {
            return "No products found\n";
        }
        var result = this.byProducer[producer];
        StringBuilder sb = new StringBuilder();
        foreach (Product product in result)
        {
            sb.Append(product).Append(Environment.NewLine);
        }

        return sb.ToString();
    }

    public string FindProductsByPriceRange(decimal fromPrice, decimal toPrice)
    {
        var range = this.byPrice.Range(fromPrice, true, toPrice, true).SelectMany(p => p.Value).ToList();

        if (range.Count == 0)
        {
            return "No products found\n";
        }

        range.Sort();
        StringBuilder sb = new StringBuilder();
        foreach (Product product in range)
        {
            sb.Append(product).Append(Environment.NewLine);
        }

        return sb.ToString();
    }
}