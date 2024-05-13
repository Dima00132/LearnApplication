using SQLite;
using SQLiteNetExtensions.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace LearnApplication.Model.Web
{
    public static class AbstractShrinkStrategy 
    {
        public static  char[] AcceptableCharacters;
        public static  int AcceptableCharactersLength;
        public static  int MaxValue;

        static AbstractShrinkStrategy()
        {
            
            var rerrr = "0123456789ABCDEFGHIJKLMNPQRSTUVWXYZ".ToCharArray();
            MaxValue = (int)Math.Pow(rerrr.Length, 5);
            AcceptableCharacters  = "0123456789ABCDEFGHIJKLMNPQRSTUVWXYZ".ToCharArray();
            AcceptableCharactersLength = rerrr.Length;
        }

      

        public static string ConvertValueToAcceptableCharsString(int value)
        {
            value = Math.Abs(value);
            if (value > MaxValue)
            {
                value %= MaxValue;
            }
            var sb = new StringBuilder(AcceptableCharacters.Length);
            do
            {
                sb.Append(AcceptableCharacters[value % AcceptableCharactersLength]);
                value = value / AcceptableCharactersLength;
            } while (value != 0);
            return sb.ToString();
        }
    }


    [Table("url_web_valid")]
    public sealed partial class UrlWebValid: ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("СardQuestion_id")]
        [ForeignKey(typeof(CardQuestion))]
        public int СardQuestionId { get; set; }


        [ObservableProperty]
        private string _url = string.Empty;

        [ObservableProperty]
        private string _shortUrl = string.Empty;

        [ObservableProperty]
        private string _longUrl = string.Empty;

        [ObservableProperty]
        private bool _isUrlValid;



        public bool IsNullOrEmpty => string.IsNullOrEmpty(Url);
       

        public UrlWebValid(string url)
        {
            IsUrlValid = CheckNet.CheckAll(url);
            Url = url;
            if (string.IsNullOrEmpty(url))
                return;


            var g =  WebEncoders.Base64UrlEncode(BitConverter.GetBytes(url.GetHashCode()));
            //var g = AbstractShrinkStrategy.ConvertValueToAcceptableCharsString(url.GetHashCode());




        }

        public UrlWebValid()
        {
        }

        private string UrlParserFromLongToShort(string longUrl)
        {
            return "";
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

        public override bool Equals(object? obj)
        {
            if (obj is UrlWebValid url)
                return url.IsUrlValid == IsUrlValid & url.Url == Url;
            return false;
        }

        public override int GetHashCode()
        {
            var ureHashCode = Url.GetHashCode();
            var isUrlValidHashCode = IsUrlValid.GetHashCode();
            var carrentHashCode = unchecked(ureHashCode + isUrlValidHashCode);
            return carrentHashCode;
           
        }
    }
}
