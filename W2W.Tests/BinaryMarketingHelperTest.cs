using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W2W.Marketing;

namespace W2W.Tests
{
    [TestClass]
    public class BinaryMarketingHelperTest
    {
        [TestMethod]
        public void GetParentCountTest()
        {
            BinaryMarketingHelper helper = new BinaryMarketingHelper();
            Assert.AreEqual(1, helper.GetParentCount("10", 5));
            Assert.AreEqual(5, helper.GetParentCount("101010010", 5));
        }

        [TestMethod]
        public void GetParentTest()
        {
            BinaryMarketingHelper helper = new BinaryMarketingHelper();
            Assert.AreEqual(null, helper.GetParent("10", 5));
            Assert.AreEqual("10", helper.GetParent("10101011", 6));
        }

        [TestMethod]
        public void GetPosByHashCodeTest()
        {
            BinaryMarketingHelper helper = new BinaryMarketingHelper();
            Assert.AreEqual(0, helper.GetPosByHashCode("10"));
            Assert.AreEqual(1, helper.GetPosByHashCode("1"));
        }

        [TestMethod]
        public void GetDeepByHashCodeTest()
        {
            BinaryMarketingHelper helper = new BinaryMarketingHelper();
            Assert.AreEqual(4, helper.GetDeepByHashCode("10101"));
            Assert.AreEqual(0, helper.GetDeepByHashCode("1"));
        }

        [TestMethod]
        public void GetLevelHashCodesTest()
        {
            BinaryMarketingHelper helper = new BinaryMarketingHelper();
            var hashes = helper.GetLevelHashCodes("101", 2);
            Assert.AreEqual(4, hashes.Count());
            Assert.AreEqual("10100", hashes.ElementAt(0));
            Assert.AreEqual("10101", hashes.ElementAt(1));
            Assert.AreEqual("10110", hashes.ElementAt(2));
            Assert.AreEqual("10111", hashes.ElementAt(3));
        }


    }
}
