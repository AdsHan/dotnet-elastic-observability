using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OBT.Products.API.DTO;
using OBT.Products.Domain.Entities;
using OBT.Products.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace OBT.Products.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        /// <summary>
        /// Obtêm os produtos
        /// </summary>
        /// <returns>Coleção de objetos da classe Produto</returns>                
        /// <response code="200">Lista dos produtos</response>        
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">Nenhum produto foi localizado</response>         
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.GetAllAsync();

            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET: api/products/5
        /// <summary>
        /// Obtêm as informações do produto pelo seu Id
        /// </summary>
        /// <param name="id">Código do produto</param>
        /// <returns>Objetos da classe Produto</returns>                
        /// <response code="200">Informações do Producto</response>
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">O produto não foi localizado</response>         
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/products/
        /// <summary>
        /// Grava o produto
        /// </summary>   
        /// <remarks>
        /// Exemplo request:
        ///
        ///     POST / Produto
        ///     {
        ///         "title": "Sandalia",
        ///         "description": "Sandália Preta Couro Salto Fino",
        ///         "price": 249.50,
        ///         "quantity": 100       
        ///     }
        /// </remarks>        
        /// <returns>Retorna objeto criado da classe Produto</returns>                
        /// <response code="201">O produto foi incluído corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ActionName("NewProduct")]
        public async Task<IActionResult> PostAsync([FromBody] ProductDTO productDTO)
        {
            var product = new ProductModel()
            {
                Title = productDTO.Title,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity
            };

            await _productRepository.AddAsync(product);

            await _productRepository.SaveAsync();

            return CreatedAtAction("NewProduct", new { id = product.Id }, product);
        }
    }
}
