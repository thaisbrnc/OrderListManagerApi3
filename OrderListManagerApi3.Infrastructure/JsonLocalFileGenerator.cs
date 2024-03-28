using System.Text.Json;
using System.Text.Json.Serialization;
using OrderListManagerApi3.Models;

namespace OrderListManagerApi3.Infrastructure
{
	public class JsonLocalFileGenerator : IJsonLocalFileGenerator
	{
        public string path = @"bin/Debug/net7.0/Database.json";

        public void Serialize(IList<Group> groups)
        {
            try
            {
                //serializa dados
                var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
                string data = JsonSerializer.Serialize(groups, options);

                //cria arquivo, escreve os dados e fecha o arquivo
                StreamWriter writer = new StreamWriter(path);
                writer.Write(data);
                writer.Close();
            }
            catch (FileNotFoundException)
            {
                FileStream fileDatabase = File.Create(path);
                fileDatabase.Close();
            }
        }

        public IList<Group> Deserialize()
        {
            IList<Group> groups = new List<Group>();

            try
            {
                //le os dados do arquivo, transfere para uma string e fecha o arquivo
                StreamReader reader = new StreamReader(path);
                string data = reader.ReadToEnd();
                reader.Close();

                //desserializa para a lista
                if (!string.IsNullOrEmpty(data))
                {
                    var options = new JsonSerializerOptions { IncludeFields = true };
                    groups = JsonSerializer.Deserialize<IList<Group>>(data, options)!;
                } 
            }
            catch(FileNotFoundException)
            {
                FileStream fileDatabase = File.Create(path);
                fileDatabase.Close();
            }

            return groups;
        }
    }
}