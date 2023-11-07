using Microsoft.AspNetCore.Mvc;
using static Dustech.Restaurant.RestApi.Constants;

namespace Dustech.Restaurant.RestApi.Controllers;

[Route(BASEURL)]
public class HomeController : ControllerBase
{
  public IActionResult Get()
  {
    return Ok(new { message = "Hello, World!" });
  }
}
