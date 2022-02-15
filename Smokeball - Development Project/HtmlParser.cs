using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball___Development_Project
{
    internal interface IHtmlParser
    {
        public int[] FindMatchingTextResultPositions(string HtmlContent, string targetText);
    }

    internal class HtmlParser : IHtmlParser
    {
        public int[] FindMatchingTextResultPositions(string HtmlContent, string targetText)
        {
            string[] chunks = parseGoogleResultChunksFromHtml(HtmlContent);
            int idx = 0;
            List<int> result = new List<int>();
            foreach(string chunk in chunks)
            {
                if (chunk.Contains(targetText))
                    result.Add(idx);
                idx++;
            }

            return chunks.Select((chunk, idx) => chunk.Contains(targetText) ? idx : -1).Where(idx => idx != -1).ToArray();
            //return result.ToArray();
        }

        private string[] parseGoogleResultChunksFromHtml(string HtmlContent)
        {
            // This Class ID used to identify search result is manually parsed from sample html returned by running the query. My recommendation is to use googles API
            string classId = "egMi0 kCrYT";
            return HtmlContent.Split(classId);
        }
    }
}
