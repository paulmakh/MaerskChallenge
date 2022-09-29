using System.Text;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MaerskChallenge.Model;

namespace IntegrationTests
{
    public class SortJobApiTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SortJobApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void PostUnsortedArray()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync("/api/SortJob", new StringContent("[1, 2, 3]", Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async void GetAllJobs()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/SortJob");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]

        public async void GetJobByGuid()
        {
            // Arrange
            var client = _factory.CreateClient();

            var response = await client.PostAsync("/api/SortJob", new StringContent("[1, 2, 3]", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var job = content.FromJson<Job>();

            // Act
            response = await client.GetAsync($"/api/SortJob/{job.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var receivedJob = (await response.Content.ReadAsStringAsync()).FromJson<Job>();
            receivedJob.Id.Should().Be(job.Id);
        }
    }
}