using Microsoft.AspNetCore.Mvc;
using Producer.Dtos;
using Producer.RabbitMQ;

namespace Producer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMessageProducer _messageProducer;

    public ProductController(IMessageProducer messageProducer)
    {
        _messageProducer = messageProducer;
    }

    [HttpPost("products")]
    public IActionResult Create([FromBody] ProductDto product)
    {
        _messageProducer.Send(product);

        return Ok(new
        {
            Message = "Product created and sent to RabbitMQ",
            Product = product
        });
    }
}