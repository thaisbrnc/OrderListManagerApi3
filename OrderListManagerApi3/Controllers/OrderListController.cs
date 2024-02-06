using OrderListManagerApi3.Translate;
using OrderListManagerApi3.Services;
using OrderListManagerApi3.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using OrderListManagerApi3.Infrastructure;

namespace OrderListManagerApi3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderListController : ControllerBase
    {
        private readonly ILogger<OrderListController> _logger;
        private readonly Database _database;
        private ProductService _serviceProduct;
        private GroupService _serviceGroup;
        

        public OrderListController(ILogger<OrderListController> logger, Database database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpPut(Name = "AdicionarGrupo")]
        public string AddGroup(string name)
        {
            _serviceGroup = new GroupService(new GroupRepository(new GroupDto(), _database));
            return _serviceGroup.Add(name);
        }

        [HttpPost(Name = "AdicionarProduto")]
        public string AddProduct(string productName, string groupName)
        {
            _serviceProduct = new ProductService(new ProductRepository(new ProductDto(), _database));
            return _serviceProduct.Add(productName, groupName);
        }

        [HttpGet(Name = "BuscarLista")]
        public List<GroupDto> GetOrderList()
        {
            _serviceGroup = new GroupService(new GroupRepository(new GroupDto(), _database));
            return _serviceGroup.Get();
        }

        [HttpPatch(Name = "AtualizarNomeGrupo")]
        public GroupDto EditGroup(string group, string description)
        {
            _serviceGroup = new GroupService(new GroupRepository(new GroupDto(), _database));
            return _serviceGroup.Edit(group, description);
        }

        [HttpPatch(Name = "AtualizarNomeProduto")]
        public ProductDto EditProduct(string group, ProductDto productDto, string description)
        {
            _serviceProduct = new ProductService(new ProductRepository(new ProductDto(), _database));
            return _serviceProduct.Edit(group, productDto, description);
        }

        [HttpPatch(Name = "AtualizarSeleção")]
        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            _serviceProduct = new ProductService(new ProductRepository(new ProductDto(), _database));
            return _serviceProduct.UpdateChecked(group, productDto, isChecked);
        }

        [HttpDelete(Name = "RemoverProduto")]
        public string RemoveProduct(string group, ProductDto product)
        {
            _serviceProduct = new ProductService(new ProductRepository(new ProductDto(), _database));
            return _serviceProduct.Remove(group, product);
        }

        [HttpDelete(Name = "RemoverGrupo")]
        public string RemoveGroup(GroupDto group)
        {
            _serviceGroup = new GroupService(new GroupRepository(new GroupDto(), _database));
            return _serviceGroup.Remove(group);
        }
    }
}

