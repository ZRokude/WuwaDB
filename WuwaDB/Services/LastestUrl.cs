namespace WuwaDB.Services
{
    public class LastestUrl
    {
        private protected string LastUrl { get; set; }
        public void UpdatePreviousUrl(string currentUrl)
        {
            LastUrl = currentUrl;
        }

        public string CheckLastUrl()
        {
            if (!string.IsNullOrEmpty(LastUrl))
                return LastUrl;
            return "";
        }
    }

}
