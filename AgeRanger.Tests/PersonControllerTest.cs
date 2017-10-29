using AgeRanger.Data;
using AgeRanger.Entity;
using AgeRanger.Service;
using AgeRanger.Service.DTO;
using AgeRanger.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace AgeRanger.Tests
{
    public class PersonControllerTest
    {
        private AgeRangerContext GetContext(string dbName)
        {
            var builder = new DbContextOptionsBuilder<AgeRangerContext>()
                .UseInMemoryDatabase(dbName);
            var context = new AgeRangerContext(builder.Options);

            AgeGroup[] groups = new AgeGroup[]
            {
                new AgeGroup() { Id = 1, MinAge = 0, MaxAge = 10, Description = "0 - 10"},
                new AgeGroup() { Id = 2, MinAge = 15, Description = "15 - "},
                new AgeGroup() { Id = 3, MinAge = 20, MaxAge = 30, Description = "20 - 20"},
            };

            context.AgeGroup.AddRange(groups);

            return context;
        }

        [Fact]
        public void TestCreatePersonWithinGroup()
        {

            AgeRangerContext context = GetContext("Test1");

            string expectedAgeGroup = "0 - 10";
            PersonDTO person = new PersonDTO()
            {
                FirstName = "Jack",
                LastName = "Hammer",
                Age = 5
            };

            var service = new PersonService(context);
            var controller = new PersonController(service);
            var result = controller.Create(person);

            var output = (PersonDTO)((CreatedAtRouteResult)result).Value;

            Assert.Equal(expectedAgeGroup, output.AgeGroup.Description);
        }

        [Fact]
        public void TestCreatePersonBetweenGroups()
        {
            AgeRangerContext context = GetContext("Test2");

            string expectedAgeGroup = null;
            PersonDTO person = new PersonDTO()
            {
                FirstName = "Jack",
                LastName = "Hammer",
                Age = 15
            };

            var service = new PersonService(context);
            var controller = new PersonController(service);
            var result = controller.Create(person);

            var output = (PersonDTO)((CreatedAtRouteResult)result).Value;

            Assert.Equal(expectedAgeGroup, output.AgeGroup.Description);
        }
    }
}
