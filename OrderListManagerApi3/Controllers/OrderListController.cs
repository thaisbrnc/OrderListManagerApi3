using OrderListManagerApi3.Translate;
using OrderListManagerApi3.Services;
using OrderListManagerApi3.Infrastructure;
using OrderListManagerApi3.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<GroupDto> AddGroup(string name)
        {
            try
            {
                var group = _serviceGroup.Add(name);

                return Ok(group);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "AdicionarProduto")]
        public ActionResult<ProductDto> AddProduct(string productName, string groupName)
        {
            try
            {
                var product = _serviceProduct.Add(productName, groupName);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Name = "BuscarLista")]
        public ActionResult<List<GroupDto>> GetOrderList()
        {
            try
            {
                return Ok(_serviceGroup.Get());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch(Name = "AtualizarNomeGrupo")]
        public ActionResult<GroupDto> EditGroup(string group, string description)
        {
            try
            {
                return Ok(_serviceGroup.Edit(group, description));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch(Name = "AtualizarNomeProduto")]
        public ActionResult<ProductDto> EditProduct(string group, ProductDto productDto, string description)
        {
            try
            {
                return Ok(_serviceProduct.Edit(group, productDto, description));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch(Name = "AtualizarSeleção")]
        public ActionResult<ProductDto> UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            try
            {
                return Ok(_serviceProduct.UpdateChecked(group, productDto, isChecked));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete(Name = "RemoverProduto")]
        public ActionResult<string> RemoveProduct(string group, ProductDto product)
        {
            try
            {
                return Ok(_serviceProduct.Remove(group, product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete(Name = "RemoverGrupo")]
        public ActionResult<string> RemoveGroup(GroupDto group)
        {
            try
            {
                return Ok(_serviceGroup.Remove(group));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}