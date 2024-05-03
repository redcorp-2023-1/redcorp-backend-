using Xunit;
using RedcorpCenter.Infraestructure.Models;
using RedcorpCenter.Infraestructure;
using Moq;

namespace RedcorpCenter.Domain.Test
{
    public class SectionAndEmployeeDomainUnitTest
    {
        [Fact]
        public void save_ValidObject_ReturnTrue()
        {
            //Arrange
            SectionAndEmployee  sectionAndEmployee = new SectionAndEmployee()
            {
                Section_Id = 10,
                Employees_Id = 10
            };
            
            //Mock
            var _sectionAndEmployeeInfraestructure = new Mock<ISectionAndEmployeeInfraestructure>();
            _sectionAndEmployeeInfraestructure.Setup(t => t.SaveAsync(sectionAndEmployee)).ReturnsAsync(true);
            SectionAndEmployeeDomain sectionAndEmployeeDomain = new SectionAndEmployeeDomain(_sectionAndEmployeeInfraestructure.Object);
            
            //Act
            var returnValue = sectionAndEmployeeDomain.SaveAsync(sectionAndEmployee);

            //Assert
            Assert.True(returnValue.Result);
        }
        
        [Fact]
        public void save_InvalidObject_SectionId_returnException()
        {
            //Arrange
            SectionAndEmployee  sectionAndEmployee = new SectionAndEmployee()
            {
                Section_Id = -5,
                Employees_Id = 10
            };
            
            //Mock
            var _sectionAndEmployeeInfraestructure = new Mock<ISectionAndEmployeeInfraestructure>();
            _sectionAndEmployeeInfraestructure.Setup(t => t.SaveAsync(sectionAndEmployee)).ReturnsAsync(true);
            SectionAndEmployeeDomain sectionAndEmployeeDomain = new SectionAndEmployeeDomain(_sectionAndEmployeeInfraestructure.Object);
            
            //Act
            var ex = Assert.ThrowsAsync<Exception>(() => sectionAndEmployeeDomain.SaveAsync(sectionAndEmployee));

            //Assert
            Assert.Equal("El id del Section es invalido", ex.Result.Message);
        }
        
        [Fact]
        public void save_InvalidObject_EmployeeId_returnException()
        {
            //Arrange
            SectionAndEmployee  sectionAndEmployee = new SectionAndEmployee()
            {
                Section_Id = 12,
                Employees_Id = 0
            };
            
            //Mock
            var _sectionAndEmployeeInfraestructure = new Mock<ISectionAndEmployeeInfraestructure>();
            _sectionAndEmployeeInfraestructure.Setup(t => t.SaveAsync(sectionAndEmployee)).ReturnsAsync(true);
            SectionAndEmployeeDomain sectionAndEmployeeDomain = new SectionAndEmployeeDomain(_sectionAndEmployeeInfraestructure.Object);
            
            //Act
            var ex = Assert.ThrowsAsync<Exception>(() => sectionAndEmployeeDomain.SaveAsync(sectionAndEmployee));

            //Assert
            Assert.Equal("El id del Employee es invalido", ex.Result.Message);
        }
    }
}
