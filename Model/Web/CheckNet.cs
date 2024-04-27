using System.Net;

namespace LearnApplication.Model.Web
{
    public class CheckNet
    {
        private static readonly IEnumerable<HttpStatusCode> _onlineStatusCodes =
        [
            HttpStatusCode.Accepted,HttpStatusCode.Found,HttpStatusCode.OK,
        ];

        public static bool CheckAll(string url)
        {
            return !IsNullOrEmpty(url) && IsFormedUriString(url) && IsOnline(url);
        }

        public static bool IsNullOrEmpty(string url)
        {
            return string.IsNullOrEmpty(url);
        }

        public static bool IsFormedUriString(string url)
        {
            return  Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        public static bool IsOnline(string url)
        {
            try
            {
                using var client = new HttpClient();
                using var result = client.GetAsync(url);
                if (result.Result.StatusCode == HttpStatusCode.OK)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
