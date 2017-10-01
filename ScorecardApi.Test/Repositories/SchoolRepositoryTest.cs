using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScorecardApi.Models;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ScorecardApi.Test
{
    [TestClass]
    public class SchoolRepositoryTest
    {
        [TestMethod]
        public void ValidateJsonParse()
        {
            //Arrange
            var json = ReadEmbeddedTestFile("ScorecardApi.Test.Repositories.TestFiles.SchoolQueryResult.json");

            //Act
            var result = SchoolQueryResult.FromJson(json);

            //Assert
            result.Should().NotBeNull();
            result.Metadata.Should().NotBeNull();
            result.Metadata.Total.Should().Be(78);

            result.ResultSchools.Should().NotBeNullOrEmpty();
            var marquetteUniversity = result.ResultSchools.ToList().Single(x => x.Id == 239105);
            marquetteUniversity.City.Should().Be("Milwaukee");
            marquetteUniversity.TotalEnrollment.Should().Be(8143);
        }

        private string ReadEmbeddedTestFile(string fileName)
        {
            string result;
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}