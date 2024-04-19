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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1000;
            //request.Timeout = 3000;
            try
            {
                WebResponse resp = request.GetResponse();
            }
            catch (WebException e)
            {
                if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotFound)
                    return false;
            }
            return true;
        }
    }
}
