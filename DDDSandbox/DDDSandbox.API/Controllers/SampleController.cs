using Microsoft.AspNetCore.Mvc;

namespace DDDSandbox.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class SampleController : ControllerBase
  {
    private static readonly string[] Numbers = new[]
    {
      "One", "Two"
    };

    private readonly ILogger<SampleController> _logger;

    public SampleController(ILogger<SampleController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IEnumerable<NumberDescription> GetAsync()
    {
      return Enumerable
        .Range(1, Numbers.Length)
        .Select(GenerateNumberDesc)
        .ToArray();
    }

    private static readonly Func<int, NumberDescription> GenerateNumberDesc = 
      index =>
        new NumberDescription
        {
          Date = DateTime.Now.AddDays(index),
          Summary = Numbers[Random.Shared.Next(Numbers.Length)]
        };
  }

  public class NumberDescription
  {
    public DateTime Date { get; set; }

    public string? Summary { get; set; }
  }
}