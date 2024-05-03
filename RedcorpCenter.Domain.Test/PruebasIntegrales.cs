using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RedcorpCenter.API.Request;
using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace RedcorpCenter.Domain.Test
{
    public class APIIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
            
        public APIIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_GetAllEmployees_Unauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Employee");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Test_SignupEmployee_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var jsonContent = new StringContent(
                @"{
                    ""name"": ""Fabrizzio"",
                    ""last_name"": ""Antonio"",
                    ""dni"": ""76330660"",
                    ""email"": ""example1@gmail.com"",
                    ""password"": ""1234"",
                    ""area"": ""Finanzas y Contabilidad"",
                    ""cargo"": ""Supervisor""
                }",
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await client.PostAsync("/api/Employee/Signup", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Verificar si la solicitud fue exitosa (código de estado 200-299)

            // Obtener el contenido de la respuesta como una cadena
            var responseContent = await response.Content.ReadAsStringAsync();

            // Analizar el contenido JSON para obtener el token
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            string token = responseObject.token.value;

            // Verificar que se haya obtenido un token válido
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public async Task Test_LoginEmployee_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var jsonContent = new StringContent(
                @"{
                    ""email"": ""example@gmail.com"",
                    ""password"": ""1234"",
                    ""roles"": ""admin""
                }",
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await client.PostAsync("/api/Employee/Login", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Verificar si la solicitud fue exitosa (código de estado 200-299)

            // Obtener el contenido de la respuesta como una cadena
            var responseContent = await response.Content.ReadAsStringAsync();

            // Analizar el contenido JSON para obtener el token
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            string token = responseObject.token.value;

            // Verificar que se haya obtenido un token válido
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public async Task Test_UpdateEmployee_Success()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Iniciar sesión para obtener el token de autenticación
            var loginJsonContent = new StringContent(
                @"{
                    ""email"": ""Example@gmail.com"",
                    ""password"": ""1234"",
                    ""roles"": ""Supervisor""
                }",
                        Encoding.UTF8,
                        "application/json"
            );
            var loginResponse = await client.PostAsync("/api/Employee/Login", loginJsonContent);
            loginResponse.EnsureSuccessStatusCode();
            var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
            dynamic loginResponseObject = JsonConvert.DeserializeObject(loginResponseContent);
            string token = loginResponseObject.token.value;
            string userId = loginResponseObject.user_id;
            // Configurar el token de autorización en el encabezado de la solicitud
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Datos del empleado a actualizar
            var updateJsonContent = new StringContent(
                @"{
                    ""name"": ""Fabrizzio Antonio"",
                    ""last_name"": ""Castro"",
                    ""dni"": ""76330660"",
                    ""email"": ""Example@gmail.com"",
                    ""area"": ""Sistemas"",
                    ""cargo"": ""Supervisor"",
                    ""photo"": ""https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTvhbfHoy6ZX-T8GsUNmVjX5rm6WGE6yKBmL8tApbevaYFdjhJ66SFYLv2ih1Gat3m3rxdi""
                }",
                        Encoding.UTF8,
                        "application/json"
                    );

            // Act
            var response = await client.PutAsync("/api/Employee/"+userId+"", updateJsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);   
        }
    }
}
