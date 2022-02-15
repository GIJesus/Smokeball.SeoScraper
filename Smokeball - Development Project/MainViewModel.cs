using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball___Development_Project
{
    internal class MainViewModel : INotifyPropertyChanged
    {
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

        private string resultList = string.Empty;
        public string ResultList
        {
            get { return resultList; }
            set
            {
                resultList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ResultList"));
            }
        }

        private string queryUrl = @"https://www.google.com.au/search?num=100&q=conveyancing+software";
        public string QueryUrl
        {
            get { return queryUrl; }
        }

        public void ExecuteGoogleQuery()
        {
            HttpWebRequest googleRequest = (HttpWebRequest)WebRequest.Create(QueryUrl);
            HttpWebResponse resp = (HttpWebResponse)googleRequest.GetResponse();
            string htmlContent = string.Empty;
            using (StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(resp.CharacterSet)))
            {
                htmlContent = reader.ReadToEnd().Trim();
            }
            ResultList = $"Results found on lines:\n[{string.Join(", ", (new HtmlParser()).FindMatchingTextResultPositions(htmlContent, targetText))}]";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
