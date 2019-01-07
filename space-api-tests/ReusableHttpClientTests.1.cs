using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceApi.Entities;
using SpaceApi.Services;

namespace SpaceApiTests
{
    [TestClass]
    public class ReusableHttpClientTests
    {
        private string sampleLaunchPadJson = @"
            {
                ""padid"": 1,
                ""id"": ""kwajalein_atoll"",
                ""full_name"": ""Kwajalein Atoll Omelek Island"",
                ""status"": ""retired"",
                ""location"": {
                    ""name"": ""Omelek Island"",
                    ""region"": ""Marshall Islands"",
                    ""latitude"": 9.0477206,
                    ""longitude"": 167.7431292
                },
                ""vehicles_launched"": [
                    ""Falcon 1""
                ],
                ""attempted_launches"": 5,
                ""successful_launches"": 2,
                ""wikipedia"": ""https://en.wikipedia.org/wiki/Omelek_Island"",
                ""details"": ""SpaceX original launch site, where all of the Falcon 1 launches occured. Abandoned as SpaceX decided against upgrading the pad to support Falcon 9.""
            }
        ";

        [TestMethod]
        public void SpaceXFormatDeserializesFromSnakeToPascalCasing()
        {
            ReusableHttpClient client = new ReusableHttpClient();

            LaunchPad result = client.DeserializeJson<LaunchPad>(sampleLaunchPadJson);

            Assert.IsNotNull(result);

            Assert.AreEqual(5, result.AttemptedLaunches);
            Assert.AreEqual(
                "SpaceX original launch site, where all of the Falcon 1 launches occured. Abandoned as SpaceX decided against upgrading the pad to support Falcon 9.",
                result.Details
            );
            Assert.AreEqual("Kwajalein Atoll Omelek Island", result.FullName);
            Assert.AreEqual("kwajalein_atoll", result.Id);
            Assert.AreEqual(1, result.PadId);
            Assert.AreEqual("retired", result.Status);
            Assert.AreEqual(2, result.SuccessfulLaunches);
            Assert.AreEqual("https://en.wikipedia.org/wiki/Omelek_Island", result.Wikipedia);
            
            Assert.IsNotNull(result.VehiclesLaunched);
            Assert.AreEqual(1, result.VehiclesLaunched.Count);
            Assert.AreEqual("Falcon 1", result.VehiclesLaunched[0]);

            Assert.IsNotNull(result.Location);
            Assert.AreEqual("Omelek Island", result.Location.Name);
            Assert.AreEqual(167.7431292M, result.Location.Longitude);
            Assert.AreEqual(9.0477206M, result.Location.Latitude);
            Assert.AreEqual("Marshall Islands", result.Location.Region);
        }
    }
}
