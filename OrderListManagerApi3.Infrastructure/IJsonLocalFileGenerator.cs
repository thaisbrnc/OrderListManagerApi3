using OrderListManagerApi3.Models;

namespace OrderListManagerApi3.Infrastructure
{
    public interface IJsonLocalFileGenerator
    {
        void Serialize(IList<Group> groups);

        IList<Group> Deserialize();
    }
}