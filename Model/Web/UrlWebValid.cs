namespace LearnApplication.Model.Web
{
    public sealed class UrlWebValid
    {
        private readonly UrlWebViewSource _urlWebViewSource;
        public readonly bool IsUrlValid;

        public UrlWebValid(UrlWebViewSource urlWebViewSource)
        {
            IsUrlValid = CheckNet.Check(urlWebViewSource.Url);
            _urlWebViewSource = urlWebViewSource;
        }

        public UrlWebViewSource GetUrl()
        {
            if (!IsUrlValid)
                _urlWebViewSource.Url = "https://www.exai.com/blog/400-bad-request-error";
            return _urlWebViewSource;
        }
    }
}
