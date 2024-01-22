using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace OrderListManagerApi3.Models
{
	public static class Database
	{
		public static List<Group> groups = new List<Group>();

        public static void Serialize()
        {
            string fileName = "Database.json";
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            string dataSerialize = JsonSerializer.Serialize(groups, options);
            File.WriteAllText(fileName, dataSerialize);
        }

        public static void Deserialize()
        {
            string fileName = "Database.json";
            string jsonString = File.ReadAllText(fileName);
            var options = new JsonSerializerOptions { IncludeFields = true };
            groups = JsonSerializer.Deserialize<List<Group>>(jsonString, options)!;
            
        }
    }


}

