using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace Smokeball___Development_Project
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IHtmlParser _htmlParser;

        public MainViewModel(IHtmlParser HtmlParser)
        {
            _htmlParser = HtmlParser;
        }

        private int resultCount = 100;
        public int ResultCount
        {
            get { return resultCount; }
            set
            {
                resultCount = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ResultCount"));
                queryUrl = $"https://www.google.com.au/search?num={resultCount}&q=conveyancing+software";
            }
        }

        private string keyWords = @"conveyancing software";
        public string KeyWords
        {
            get { return keyWords; }
            set
            {
                keyWords = value;
                OnPropertyChanged(new PropertyChangedEventArgs("QueryURL"));
                if (!string.IsNullOrWhiteSpace(value))
                {
                    queryUrl = $"https://www.google.com.au/search?num=100&q={string.Join("+", keyWords.Split(' ').ToArray())}";
                }
            }
        }

        private string targetText = @"www.smokeball.com.au";
        public string TargetText
        {
            get { return targetText; }
            set
            {
                targetText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TargetText"));
            }
        }

        private string resultBoxText = string.Empty;
        public string ResultBoxText
        {
            get { return resultBoxText; }
            set
            {
                resultBoxText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ResultBoxText"));
            }
        }

        private string queryUrl = @"https://www.google.com.au/search?num=100&q=conveyancing+software";
        public string QueryUrl
        {
            get { return queryUrl; }
        }

        private string pageHtmlContent = string.Empty;
        public string PageHtmlContent
        {
            get { return pageHtmlContent; }
            set
            {
                pageHtmlContent = value;
            }
        }

        public void ExecuteGoogleQuery()
        {
            try
            {
#if !DEBUG
                HttpWebRequest googleRequest = (HttpWebRequest)WebRequest.Create(QueryUrl);
                HttpWebResponse response = (HttpWebResponse)googleRequest.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                {
                    PageHtmlContent = reader.ReadToEnd().Trim();
                }
#else
                PageHtmlContent = Resources.SampleHtml;
#endif
                int[] matches = _htmlParser.FindMatchingTextResultPositions(PageHtmlContent, targetText);
                if (matches.Length > 0)
                    ResultBoxText = $"Results found on lines:\n[{string.Join(", ", matches)}]";
                else
                    ResultBoxText = $"No results found:\n[0]";
            }
            catch (Exception ex)
            {
                ResultBoxText = $"Search failed with message:\n{ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
