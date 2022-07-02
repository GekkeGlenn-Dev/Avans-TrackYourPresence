using NUnit.Framework;

namespace UnitTesting.Xamarin.Helpers
{
    public class ApiTests
    {
        [Test]
        public void TestGetApiUrl()
        {
            string result = TrackYourPresence.Helpers.Api.GetApiUrl("Test/Route");
            
            Assert.NotNull(result);
            Assert.AreEqual("https://10.0.2.2:7013/Test/Route", result);
        }
    }
}