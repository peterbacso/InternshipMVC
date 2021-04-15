using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InternshipMvc.Tests
{
    public class StartupTests
    {
        [Fact]
        public void ShouldConvertDatabaseUrlToHerokuString()
        {
            // Assume
            string url = "postgres://pyhkpafmgvymsd:ebd1873eb08863cc97f852f3dfcb88bce95a9400d590ebdf75a16746f6c65066@ec2-99-80-200-225.eu-west-1.compute.amazonaws.com:5432/d7vdj8utke1etl";

            // Act
            string herokuConnectionString = Startup.ConvertDatabaseUrlToHerokuString(url);

            // Assert
            Assert.Equal("Server=ec2-99-80-200-225.eu-west-1.compute.amazonaws.com;Port=5432;Database=d7vdj8utke1etl;User Id=pyhkpafmgvymsd;Password=ebd1873eb08863cc97f852f3dfcb88bce95a9400d590ebdf75a16746f6c65066;Pooling=true;SSL Mode=Require;Trust Server Certificate=True;", herokuConnectionString);
        }
    }
}
