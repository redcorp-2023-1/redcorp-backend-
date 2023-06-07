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
                Name = "employee"
            };

            //Mock
            var _employeeInfraestructure = new Mock<IEmployeeInfraestructure>();
            _employeeInfraestructure.Setup(t => t.Save(employee)).Returns(true);
            EmployeeDomain employeeDomain = new EmployeeDomain(_employeeInfraestructure.Object);


            //Act
            var result = employeeDomain.Save(employee);


            //Test
            Assert.True(result);

        }

        [Fact]
        public void save_InvalidObject_returnException()
        {
            Employee employee = new Employee()
            {
                Name = "An"
            };

            var _employeeInfraestructure = new Mock<IEmployeeInfraestructure>();
            _employeeInfraestructure.Setup(t => t.Save(employee)).Returns(true);
            EmployeeDomain employeeDomain = new EmployeeDomain(_employeeInfraestructure.Object);

            var ex = Assert.Throws<Exception>(() => employeeDomain.Save(employee));

            Assert.Equal("The length of your name is invalid(>3)", ex.Message);
        }

        [Fact]
        public void save_InvalidObject_ReturnExceptionMax()
        {
            //Arrange
            Employee employee = new Employee()
            {
                Name = "Employee is very very largest"
            };

            //Mock
            var _employeeInfraestructure = new Mock<IEmployeeInfraestructure>();
            _employeeInfraestructure.Setup(t => t.Save(employee)).Returns(true);

            EmployeeDomain employeeDomain = new EmployeeDomain(_employeeInfraestructure.Object);

            //Act
            var ex = Assert.Throws<Exception>(() => employeeDomain.Save(employee));

            //Assert
            Assert.Equal("the name is more than 20", ex.Message);
        }

        [Fact]
        public void update_validObject_ReturnTrue()
        {
            //Arrange
            Employee employee = new Employee()
            {
                Id = 1,
                Name = "Ermelindo"
            };
            //Mock
            var _employeeInfraestructure = new Mock<IEmployeeInfraestructure>();
            _employeeInfraestructure.Setup(t => t.update(employee.Id, employee.Name)).Returns(true);

            //Act
            EmployeeDomain employeeDomain = new EmployeeDomain(_employeeInfraestructure.Object);
            var result = employeeDomain.update(employee.Id, employee.Name);

            //Assert
            Assert.True(result);
        }
    }
}
