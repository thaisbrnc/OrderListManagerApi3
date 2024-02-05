using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using OrderListManagerApi3.Models;

namespace OrderListManagerApi3.Infrastructure
{
	public class Database
	{
		public List<Group> groups = new List<Group>();

        public void Serialize()
        {
            string fileName = "Database.json";
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            string dataSerialize = JsonSerializer.Serialize(groups, options);
            File.WriteAllText(fileName, dataSerialize);
        }

        public void Deserialize()
        {
            string fileName = "Database.json";
            string jsonString = File.ReadAllText(fileName);
            var options = new JsonSerializerOptions { IncludeFields = true };
            groups = JsonSerializer.Deserialize<List<Group>>(jsonString, options)!;
            
        }
    }


}

