using Xunit;
using RedcorpCenter.Infraestructure.Models;
using RedcorpCenter.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace RedcorpCenter.Domain.Test
{
    public class EmployeeDomainUnitTest
    {
        [Fact]
        public void save_ValidObject_ReturnTrue()
        {
            //Arrange
            Employee employee = new Employee()
            {
                Name = "employee",
                last_name = "empleado"
            };

            //Mock
            var _employeeInfraestructure = new Mock<IEmployeeInfraestructure>();
            var _encryptDomain = new Mock<IEncryptDomain>();
            var _tokenDomain = new Mock<ITokenDomain>();
            _employeeInfraestructure.Setup(t => t.SaveAsync(employee)).ReturnsAsync(true);
            EmployeeDomain employeeDomain = new EmployeeDomain(_employeeInfraestructure.Object,
                _encryptDomain.Object, _tokenDomain.Object);


            //Act
            var returnValue = employeeDomain.SaveAsync(employee);


            //Test
            Assert.True(returnValue.Result);

        }

        [Fact]
        public void save_InvalidObject_returnException()
        {
            Employee employee = new Employee()
            {
                Name = "An",
                last_name = "wa"
            };

            var _employeeInfraestructure = new Mock<IEmployeeInfraestructure>();
            _employeeInfraestructure.Setup(t => t.SaveAsync(employee)).ReturnsAsync(true);
            var _encryptDomain = new Mock<IEncryptDomain>();
            var _tokenDomain = new Mock<ITokenDomain>();
            EmployeeDomain employeeDomain = new EmployeeDomain(_employeeInfraestructure.Object,
                _encryptDomain.Object, _tokenDomain.Object);

            var ex = Assert.ThrowsAsync<Exception>(() => employeeDomain.SaveAsync(employee));

            Assert.Equal("La longitud del nombre o apellido es menor a 3 caracteres", ex.Result.Message);
        }

        [Fact]
        public void save_InvalidObject_ReturnExceptionMax()
        {
            //Arrange
            Employee employee = new Employee()
            {
                Name = "Employee is very very largest",
                last_name = "Employee lastname is very very largest"
            };

            //Mock
            var _employeeInfraestructure = new Mock<IEmployeeInfraestructure>();
            var _encryptDomain = new Mock<IEncryptDomain>();
            var _tokenDomain = new Mock<ITokenDomain>();
            _employeeInfraestructure.Setup(t => t.SaveAsync(employee)).ReturnsAsync(true);

            EmployeeDomain employeeDomain = new EmployeeDomain(_employeeInfraestructure.Object,
                _encryptDomain.Object, _tokenDomain.Object);

            //Act
            var ex = Assert.ThrowsAsync<Exception>(() => employeeDomain.SaveAsync(employee));

            //Assert
            Assert.Equal("La longitud del nombre es mayor a 20 caracteres", ex.Result.Message);
        }

        [Fact]
        public void update_validObject_ReturnTrue()
        {
            //Arrange
            Employee employee = new Employee()
            {
                Id = 1,
                Name = "Ermelindo",
                last_name = "Caceres",
                email = "121232@upc.edu.pe",
                area = "Finanzas",
                cargo = "Jefe de proyectos"
            };
            //Mock
            var _employeeInfraestructure = new Mock<IEmployeeInfraestructure>();
            var _encryptDomain = new Mock<IEncryptDomain>();
            var _tokenDomain = new Mock<ITokenDomain>();
            _employeeInfraestructure.Setup(t => t.update(employee.Id, employee.Name, employee.last_name, employee.email, employee.area, employee.cargo)).Returns(true);

            //Act
            EmployeeDomain employeeDomain = new EmployeeDomain(_employeeInfraestructure.Object,
                _encryptDomain.Object, _tokenDomain.Object);
            var result = employeeDomain.update(employee.Id, employee.Name, employee.last_name, employee.email, employee.area, employee.cargo);

            //Assert
            Assert.True(result);
        }
    }
}