﻿using System.Diagnostics.CodeAnalysis; // to use SuppressMessage
using Microsoft.AspNetCore.Mvc.Testing; // to use WebApplicationFactory

namespace Dustech.Restaurant.RestApi.Tests;

public class HomeTests
{
  [Fact(DisplayName = "HomeReturnsJson")]

  public async Task HomeReturnsJson()
  {
    using var factory = new WebApplicationFactory<Program>();
    var client = factory.CreateClient();

    using var request = new HttpRequestMessage(HttpMethod.Get, "");
    request.Headers.Accept.ParseAdd("application/json");
    var response = await client.SendAsync(request);

    Assert.True(
        response.IsSuccessStatusCode,
        $"Actual status code: {response.StatusCode}.");
    Assert.Equal(
        "application/json",
        response.Content.Headers.ContentType?.MediaType);

  }

}
