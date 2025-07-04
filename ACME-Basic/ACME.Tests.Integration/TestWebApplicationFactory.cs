using Microsoft.AspNetCore.Mvc.Testing;

namespace ACME.Tests.Integration;

public class TestWebApplicationFactory<T>: WebApplicationFactory<T> where T: class
{

   
}
