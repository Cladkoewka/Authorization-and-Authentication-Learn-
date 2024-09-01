using Experiments.Models;
using Microsoft.AspNetCore.Mvc;

namespace Experiments.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new List<Product>();

    [HttpGet]
    public IActionResult GetAll() => 
        Ok(_products);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _products.Find(p => p.Id == id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        if (product == null)
            return BadRequest("Product is null");
        
        _products.Add(product);
        return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Product product)
    {
        var existingProduct = _products.Find(p => p.Id == id);
        if (existingProduct == null)
            return NotFound();

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _products.Find(p => p.Id == id);
        if (product == null)
            return NotFound();

        _products.Remove(product);
        return NoContent();
    }
}