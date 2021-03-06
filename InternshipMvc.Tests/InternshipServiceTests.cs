using InternshipMvc.Models;
using InternshipMvc.Services;
using System;
using System.Linq;
using Xunit;

namespace InternshipMvc.Tests
{
    public class InternshipServiceTests
    {
        [Fact]
        public void InitiallyContainsThreeMembers()
        {
            // Assume
            var intershipService = new InternshipService();

            // Act

            // Assert
            Assert.Equal(3, intershipService.GetMembers().Count);
        }

        [Fact]
        public void WhenAddMemberItShouldBeThere()
        {
            // Assume
            var intershipService = new InternshipService();

            // Act
            intershipService.AddMember(new Intern { Name = "Marko", RegistrationDateTime = DateTime.Parse("2021-04-01") });

            // Assert
            Assert.Equal(4, intershipService.GetMembers().Count);
            Assert.Contains("Marko", intershipService.GetMembers().Select(member => member.Name));
        }
    }
}