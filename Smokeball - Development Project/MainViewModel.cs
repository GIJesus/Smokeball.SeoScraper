using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Smokeball___Development_Project
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IHtmlParser _htmlParser;
        private int resultCount = 100;
        private string queryUrl = @"https://www.google.com.au/search?num=100&q=conveyancing+software";
        private string keyWords = @"conveyancing software";
        private string targetText = @"www.smokeball.com.au";
        private string resultList = string.Empty;

        public MainViewModel(IHtmlParser HtmlParser)
        {
            _htmlParser = HtmlParser;
        }

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

        public string TargetText
        {
            get { return targetText; }
            set
            {
                targetText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TargetText"));
            }
        }

        public string ResultBoxText
        {
            get { return resultList; }
            set
            {
                resultList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ResultList"));
            }
        }

        public string QueryUrl
        {
            get { return queryUrl; }
        }

        public void ExecuteGoogleQuery()
        {
            try
            {
                HttpWebRequest googleRequest = (HttpWebRequest)WebRequest.Create(QueryUrl);
                HttpWebResponse response = (HttpWebResponse)googleRequest.GetResponse();
                string htmlContent = string.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                {
                    htmlContent = reader.ReadToEnd().Trim();
                }
                int[] matches = _htmlParser.FindMatchingTextResultPositions(htmlContent, targetText);
                if (matches.Length > 0)
                    ResultBoxText = $"Results found on lines:\n[{string.Join(", ", matches)}]";
                else
                    ResultBoxText = $"No results found:\n[ 0 ]";
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
