using System.Collections.Generic;

namespace Smokeball___Development_Project
{
    public interface IHtmlParser
    {
        public int[] FindMatchingTextResultPositions(string HtmlContent, string targetText);
    }

    public class HtmlParser : IHtmlParser
    {
        public int[] FindMatchingTextResultPositions(string HtmlContent, string targetText)
        {
            string[] chunks = parseGoogleResultChunksFromHtml(HtmlContent);
            int idx = 0;
            List<int> result = new List<int>();
            foreach(string chunk in chunks)
            {
                int a = chunk.IndexOf("url?q=");
                int b = chunk.IndexOf(';');
                if (chunk.Contains(targetText))
                    result.Add(idx);
                idx++;
            }
            return result.ToArray();
        }

        private string[] parseGoogleResultChunksFromHtml(string HtmlContent)
        {
            // This Class ID used to identify search result is manually parsed from sample html returned by running the query. My recommendation is to use googles API
            string classId = "ZINbbc luh4tb xpd O9g5cc uUPGi";
            return HtmlContent.Split(classId);
        }
    }
}
