using DDDSandbox.Sales.Messages.Commands;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace DDDSandbox.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    private readonly ILogger<OrdersController> _logger;
    private readonly IMessageSession _messageSession;

    public OrdersController(ILogger<OrdersController> logger, IMessageSession messageSession)
    {
      _messageSession = messageSession;
      _logger = logger;
    }

    [HttpPost]
    public async Task<PlaceOrderResponse> PostAsync([FromBody]PlaceOrderRequest request)
    {
      var command = new PlaceOrder { UserId = request.UserId };
      await _messageSession.Send(command);

      return new PlaceOrderResponse 
      {
        Message = "Request for a new order accepted."
      };
    }
  }

  public class PlaceOrderRequest
  {
    public Guid UserId { get; set; }
  }

  public class PlaceOrderResponse
  {
    public string? Message { get; set; }
  }
}