using OrderListManagerApi3.Translate;
using OrderListManagerApi3.Services;
using OrderListManagerApi3.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using OrderListManagerApi3.Infrastructure;
using System.Web.Http.Cors;

namespace OrderListManagerApi3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
    public class OrderListController : ControllerBase
    {
        private readonly ILogger<OrderListController> _logger;
        private readonly ProductService _serviceProduct;
        private readonly GroupService _serviceGroup;

        public OrderListController(ILogger<OrderListController> logger)
        {
            _logger = logger;
            _serviceGroup = new GroupService(new GroupRepository(new GroupDto(), new JsonLocalFileGenerator()));
            _serviceProduct = new ProductService(new ProductRepository(new ProductDto(), new JsonLocalFileGenerator()));
        }

        [HttpPut(Name = "AdicionarGrupo")]
        public GroupDto AddGroup(string name)
        {
            return _serviceGroup.Add(name);
        }

        [HttpPost(Name = "AdicionarProduto")]
        public ProductDto AddProduct(string productName, string groupName)
        {
            return _serviceProduct.Add(productName, groupName);
        }

        [HttpGet(Name = "BuscarLista")]
        public List<GroupDto> GetOrderList()
        {
            return _serviceGroup.Get();
        }

        [HttpPatch(Name = "AtualizarNomeGrupo")]
        public GroupDto EditGroup(string group, string description)
        {
            return _serviceGroup.Edit(group, description);
        }

        [HttpPatch(Name = "AtualizarNomeProduto")]
        public ProductDto EditProduct(string group, ProductDto productDto, string description)
        {
            return _serviceProduct.Edit(group, productDto, description);
        }

        [HttpPatch(Name = "AtualizarSeleção")]
        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            return _serviceProduct.UpdateChecked(group, productDto, isChecked);
        }

        [HttpDelete(Name = "RemoverProduto")]
        public string RemoveProduct(string group, ProductDto product)
        {
            return _serviceProduct.Remove(group, product);
        }

        [HttpDelete(Name = "RemoverGrupo")]
        public string RemoveGroup(GroupDto group)
        {
            return _serviceGroup.Remove(group);
        }
    }
}