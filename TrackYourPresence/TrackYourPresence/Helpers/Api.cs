using Plugin.DeviceInfo;

namespace TrackYourPresence.Helpers
{
    public static class Api
    {
        private static string ApiBaseUri => "https://10.0.2.2:7013";

        public static string GetApiUrl(string path)
        {
            return path.StartsWith("/")
                ? ApiBaseUri + path
                : ApiBaseUri + "/" + path;
        }
    }
}