using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MicroserviciodeGestiondeClientes.Tests
{
    public class ClientesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ClientesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetClientes_ReturnsOkResult()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/clientes");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetClientes_ReturnsListOfClientes()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/clientes");

            // Act
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var clientes = JsonConvert.DeserializeObject<List<Cliente>>(responseString);

            // Assert
            Assert.NotNull(clientes);
            Assert.True(clientes.Count > 0);
        }

        [Fact]
        public async Task PostCliente_CreatesNewCliente()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Nombre de prueba",
                Apellido = "Apellido de prueba",
                NumeroCuenta = "123456789",
                Saldo = 1000,
                FechaNacimiento = DateTime.Parse("2000-01-01"),
                Direccion = "Dirección de prueba",
                Telefono = "123456789",
                CorreoElectronico = "prueba@example.com",
                Contraseña = "contraseña123",
                TipoCliente = "Tipo de prueba",
                EstadoCivil = "Estado civil de prueba",
                NumeroIdentificacion = "1234567890",
                Profesion = "Profesión de prueba",
                Genero = "Género de prueba",
                Nacionalidad = "Nacionalidad de prueba"
            };
            var requestBody = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

            // Act
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/clientes")
            {
                Content = requestBody
            };
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var createdCliente = JsonConvert.DeserializeObject<Cliente>(responseString);

            // Assert
            Assert.NotNull(createdCliente);
            Assert.Equal(cliente.Nombre, createdCliente.Nombre);
            Assert.Equal(cliente.Apellido, createdCliente.Apellido);
            Assert.Equal(cliente.NumeroCuenta, createdCliente.NumeroCuenta);
            Assert.Equal(cliente.Saldo, createdCliente.Saldo);
            Assert.Equal(cliente.FechaNacimiento, createdCliente.FechaNacimiento);
            Assert.Equal(cliente.Direccion, createdCliente.Direccion);
            Assert.Equal(cliente.Telefono, createdCliente.Telefono);
            Assert.Equal(cliente.CorreoElectronico, createdCliente.CorreoElectronico);
            Assert.Equal(cliente.Contraseña, createdCliente.Contraseña);
            Assert.Equal(cliente.TipoCliente, createdCliente.TipoCliente);
            Assert.Equal(cliente.EstadoCivil, createdCliente.EstadoCivil);
            Assert.Equal(cliente.NumeroIdentificacion, createdCliente.NumeroIdentificacion);
            Assert.Equal(cliente.Profesion, createdCliente.Profesion);
            Assert.Equal(cliente.Genero, createdCliente.Genero);
            Assert.Equal(cliente.Nacionalidad, createdCliente.Nacionalidad);
        }
    }
}