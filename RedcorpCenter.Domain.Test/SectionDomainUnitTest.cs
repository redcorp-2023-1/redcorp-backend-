using Xunit;
using RedcorpCenter.Infraestructure.Models;
using RedcorpCenter.Infraestructure;
using Moq;

namespace RedcorpCenter.Domain.Test
{
    public class SectionDomainUnitTest
    {
        [Fact]
        public void save_ValidObject_ReturnTrue()
        {
            //Arrange
            Section section = new Section()
            {
                Section_Name = "section_name",
                Description = "section_description"
            };

            //Mock
            var _sectionInfraestructure = new Mock<ISectionInfraestructure>();
            _sectionInfraestructure.Setup(t => t.SaveAsync(section)).ReturnsAsync(true);
            SectionDomain sectionDomain = new SectionDomain(_sectionInfraestructure.Object);
            
            //Act
            var returnValue = sectionDomain.SaveAsync(section);

            //Assert
            Assert.True(returnValue.Result);
        }
        
        [Fact]
        public void save_InvalidObject_SectionName_ReturnException()
        {
            //Arrange
            Section section = new Section()
            {
                Section_Name = "few",
                Description = "section_description"
            };

            //Mock
            var _sectionInfraestructure = new Mock<ISectionInfraestructure>();
            _sectionInfraestructure.Setup(t => t.SaveAsync(section)).ReturnsAsync(true);
            SectionDomain sectionDomain = new SectionDomain(_sectionInfraestructure.Object);
            
            //Act
            var ex = Assert.ThrowsAsync<Exception>(() => sectionDomain.SaveAsync(section));

            //Assert
            Assert.Equal("El sintaxis es incorrecto", ex.Result.Message);
        }
        
        [Fact]
        public void save_InvalidObject_Description_ReturnException()
        {
            //Arrange
            Section section = new Section()
            {
                Section_Name = "section_name",
                Description = "few words"
            };

            //Mock
            var _sectionInfraestructure = new Mock<ISectionInfraestructure>();
            _sectionInfraestructure.Setup(t => t.SaveAsync(section)).ReturnsAsync(true);
            SectionDomain sectionDomain = new SectionDomain(_sectionInfraestructure.Object);
            
            //Act
            var ex = Assert.ThrowsAsync<Exception>(() => sectionDomain.SaveAsync(section));

            //Assert
            Assert.Equal("El sintaxis es incorrecto", ex.Result.Message);
        }
        
    }
}

