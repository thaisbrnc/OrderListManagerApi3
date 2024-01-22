using System.Text.Json.Serialization;

namespace OrderListManagerApi3.Models;

public class Group : EntityBase
{
    public List<Product> products = new List<Product>();
}

