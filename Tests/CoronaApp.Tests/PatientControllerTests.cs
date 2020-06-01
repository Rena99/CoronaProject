using CoronaApp.Dal;
using CoronaApp.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace CoronaApp.Tests
{
    public class PatientControllerTests : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly WebApplicationFactory<Api.Startup> _factory;
        public PatientControllerTests(WebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void GetPatient_ById_ReturnPatient()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/patient/123456789/0");

            // Assert
            response.EnsureSuccessStatusCode(); 
        }

        [Theory]
        [InlineData("/patient")]
        public async void AddPatient(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            string json = JsonConvert.SerializeObject("987654321", Formatting.Indented);

            var httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await client.PostAsync(url, httpContent);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void GetPatient()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IPatientRepository, PatientRepository>();
                });
            })
                .CreateClient();

            //Act
            var response = await client.GetAsync("/patient/123456789/0");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
