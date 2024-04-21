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

            if (IsNullOrEmpty(url))
                return false;
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
            //WebRequest request = WebRequest.Create(url);
            //try
            //{
            //    //HttpWebResponse res = request.GetResponse() as HttpWebResponse;

            //    //if (res is null)
            //    //    return false;

            //    if (request.GetResponse() is not HttpWebResponse res)
            //        return false;

            //    if (res.StatusCode == HttpStatusCode.OK)
            //        return true;
            //}
            //catch (WebException)
            //{ 
            //   return false;
            //}







            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //    //request.Timeout = 1000;
            //    request.Timeout = 3000;
            //    try
            //    {
            //        WebResponse resp = request.GetResponse();
            //    }
            //    catch (WebException e)
            //    {
            //        if (((HttpWebResponse)e?.Response).StatusCode == HttpStatusCode.NotFound)
            //            return false;
            //    }
            //    return true;
        }
    }
}
