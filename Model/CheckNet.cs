using System;
using System.Net;

namespace LearnApplication.Model
{
    public class CheckNet
    {
        private static readonly IEnumerable<HttpStatusCode> _onlineStatusCodes =
        [
            HttpStatusCode.Accepted,HttpStatusCode.Found,HttpStatusCode.OK,
        ];

        public static bool Check(string url)
        {
            return !string.IsNullOrEmpty(url) && IsOnline(url);
        }

        private static bool IsOnline(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 3000;
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
