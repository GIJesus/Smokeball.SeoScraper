using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Smokeball___Development_Project.Tests
{
    [TestClass()]
    public class SeoScraperTests
    {
        [TestMethod()]
        public void HtmlParserGet1()
        {
            MainViewModel mvm = new MainViewModel(new fakeHtmlParser());
            Assert.IsNotNull(mvm);
            mvm.TargetText = "1";
            mvm.ExecuteGoogleQuery();
            Assert.AreEqual(mvm.TargetText, $"Results found on lines:\n[ 1 ]");
        }

        [TestMethod()]
        public void HtmlParserGet3()
        {
            MainViewModel mvm = new MainViewModel(new fakeHtmlParser());
            Assert.IsNotNull(mvm);
            mvm.TargetText = "3";
            mvm.ExecuteGoogleQuery();
            Assert.AreEqual(mvm.TargetText, $"Results found on lines:\n[ 1, 3, 5 ]");
        }

        [TestMethod()]
        public void HtmlParserGet5()
        {
            MainViewModel mvm = new MainViewModel(new fakeHtmlParser());
            Assert.IsNotNull(mvm);
            mvm.TargetText = "5";
            mvm.ExecuteGoogleQuery();
            Assert.AreEqual(mvm.TargetText, $"Results found on lines:\n[ 2, 4, 6, 8, 10 ]");
        }

        [TestMethod()]
        public void HtmlParserGet0()
        {
            MainViewModel mvm = new MainViewModel(new fakeHtmlParser());
            Assert.IsNotNull(mvm);
            mvm.TargetText = "0";
            mvm.ExecuteGoogleQuery();
            Assert.AreEqual(mvm.TargetText, $"No results found:\n[ 0 ]");
        }
    }

    /// <summary>
    /// A fake IHtmlParser
    /// </summary>
    public class fakeHtmlParser : IHtmlParser
    {
        public int[] FindMatchingTextResultPositions(string HtmlContent, string targetText)
        {
            int[] result = new int[0];
            switch (targetText)
            {
                case "1":
                    {
                        result = new int[1] { 1 };
                    }
                    break;
                case "3":
                    {
                        result = new int[3] { 1, 3, 5 };
                    }
                    break;
                case "5":
                    {
                        result = new int[5] { 2, 4, 6, 8, 10 };
                    }
                    break;
                default:
                    {
                        result = new int[1] { 0 };
                    }
                    break;
            }
            return result;
        }
    }
}