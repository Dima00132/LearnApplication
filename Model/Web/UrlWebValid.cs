using SQLite;
using SQLiteNetExtensions.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LearnApplication.Model.Web
{
    [Table("url_web_valid")]
    public sealed partial class UrlWebValid: ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("СardQuestion_id")]
        [ForeignKey(typeof(СardQuestion))]
        public int СardQuestionId { get; set; }


        [ObservableProperty]
        private string _url;
 
        [ObservableProperty]
        private bool _isUrlValid;

        public bool IsNullOrEmpty => string.IsNullOrEmpty(Url);
       

        public UrlWebValid(string url)
        {
            IsUrlValid = CheckNet.CheckAll(url);
            Url = url;
        }

        public UrlWebValid()
        {
        }

        public void Change(string url)
        {
            IsUrlValid = CheckNet.CheckAll(url);
            Url = url;
        }

        public UrlWebViewSource GetUrlWebViewSource(bool isLinkToErrorIfLinkIsEmpty = false)
        {
            if (!IsUrlValid & isLinkToErrorIfLinkIsEmpty)
                Url = "https://www.exai.com/blog/400-bad-request-error";
            return new UrlWebViewSource() { Url = Url};
        }
    }
}
