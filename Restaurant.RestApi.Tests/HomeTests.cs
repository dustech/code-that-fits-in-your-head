using Microsoft.AspNetCore.Mvc.Testing; // to use WebApplicationFactory

namespace Dustech.Restaurant.RestApi.Tests;

public class HomeTests
{
  [Fact(DisplayName = "HomeIsOk")]
  public async Task HomeIsOk()
  {
    using var factory = new WebApplicationFactory<Program>();
    var client = factory.CreateClient();

    var response = await client
        .GetAsync(new Uri("", UriKind.Relative))
        .ConfigureAwait(false);

    Assert.True(
        response.IsSuccessStatusCode,
        $"Actual status code: {response.StatusCode}.");

  }

}
