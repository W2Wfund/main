using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using W2W.Marketing;
using W2W.ModelKBT.Entities;

namespace W2W.Tests
{
    [TestClass]
    public class MarketingTest
    {

        //[TestMethod]
        //public void GetHashCodeTest()
        //{
        //    Service1 service = new Service1();
        //    Assert.AreEqual("100", service.GetHashCode("10", new[] { "1", "10", "11" }));
        //    Assert.AreEqual("101", service.GetHashCode("10", new[] { "1", "10", "11", "100" }));
        //    Assert.AreEqual("1", service.GetHashCode(null, null));

        //    Assert.AreEqual("100", service.GetHashCode("1",
        //      new[] {
        //               "1", "10","11"
        //      }));

        //    Assert.AreEqual("10000000", service.GetHashCode("100000",
        //       new[] {
        //               "1", "10","100","101","1000","10000", "100000","1000000","1000001"
        //       }));
        //}

        [TestMethod]
        public void GetBinaryHashTableTest()
        {
            Service1 service = new Service1();
            List<MarketingPlace> structure = new List<MarketingPlace>();
            structure.Add(new MarketingPlace { id_object = 1, SortPosition = null, hash = "jjd3k1", id_parent = 9999 });
            structure.Add(new MarketingPlace { id_object = 10, SortPosition = 0, hash = "jjd3k10", id_parent = 1 });
            structure.Add(new MarketingPlace { id_object = 11, SortPosition = 1, hash = "jjd3k11", id_parent = 1 });
            structure.Add(new MarketingPlace { id_object = 100, SortPosition = 0, hash = "jjd3k100", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 110, SortPosition = 0, hash = "jjd3k110", id_parent = 11 });
            structure.Add(new MarketingPlace { id_object = 101, SortPosition = 1, hash = "jjd3k101", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 111, SortPosition = 1, hash = "jjd3k111", id_parent = 11 });
            structure.Add(new MarketingPlace { id_object = 1000, SortPosition = 0, hash = "jjd3k1000", id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 1100, SortPosition = 0, hash = "jjd3k1100", id_parent = 110 });
            structure.Add(new MarketingPlace { id_object = 1010, SortPosition = 0, hash = "jjd3k1010", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 1110, SortPosition = 0, hash = "jjd3k1110", id_parent = 111 });
            structure.Add(new MarketingPlace { id_object = 1001, SortPosition = 1, hash = "jjd3k1001", id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 1101, SortPosition = 1, hash = "jjd3k1101", id_parent = 110 });
            structure.Add(new MarketingPlace { id_object = 1011, SortPosition = 1, hash = "jjd3k1011", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 1111, SortPosition = 1, hash = "jjd3k1111", id_parent = 111 });
            structure.Add(new MarketingPlace { id_object = 10000, SortPosition = 0, hash = "jjd3k10000", id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 11000, SortPosition = 0, hash = "jjd3k11000", id_parent = 1100 });
            structure.Add(new MarketingPlace { id_object = 10100, SortPosition = 0, hash = "jjd3k10100", id_parent = 1010 });
            structure.Add(new MarketingPlace { id_object = 11100, SortPosition = 0, hash = "jjd3k11100", id_parent = 1110 });
            structure.Add(new MarketingPlace { id_object = 10010, SortPosition = 0, hash = "jjd3k10010", id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 11010, SortPosition = 0, hash = "jjd3k11010", id_parent = 1101 });
            structure.Add(new MarketingPlace { id_object = 10110, SortPosition = 0, hash = "jjd3k10110", id_parent = 1011 });
            structure.Add(new MarketingPlace { id_object = 11110, SortPosition = 0, hash = "jjd3k11110", id_parent = 1111 });
            structure.Add(new MarketingPlace { id_object = 10001, SortPosition = 1, hash = "jjd3k10001", id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 11001, SortPosition = 1, hash = "jjd3k11001", id_parent = 1100 });
            structure.Add(new MarketingPlace { id_object = 10101, SortPosition = 1, hash = "jjd3k10101", id_parent = 1010 });
            structure.Add(new MarketingPlace { id_object = 11101, SortPosition = 1, hash = "jjd3k11101", id_parent = 1110 });
            structure.Add(new MarketingPlace { id_object = 10011, SortPosition = 1, hash = "jjd3k10011", id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 11011, SortPosition = 1, hash = "jjd3k11011", id_parent = 1101 });
            structure.Add(new MarketingPlace { id_object = 10111, SortPosition = 1, hash = "jjd3k10111", id_parent = 1011 });
            structure.Add(new MarketingPlace { id_object = 11111, SortPosition = 1, hash = "jjd3k11111", id_parent = 1111 });
            structure.Add(new MarketingPlace { id_object = 100000, SortPosition = 0, hash = "jjd3k100000", id_parent = 10000 });
            structure.Add(new MarketingPlace { id_object = 110000, SortPosition = 0, hash = "jjd3k110000", id_parent = 11000 });
            structure.Add(new MarketingPlace { id_object = 101000, SortPosition = 0, hash = "jjd3k101000", id_parent = 10100 });
            structure.Add(new MarketingPlace { id_object = 111000, SortPosition = 0, hash = "jjd3k111000", id_parent = 11100 });
            structure.Add(new MarketingPlace { id_object = 100100, SortPosition = 0, hash = "jjd3k100100", id_parent = 10010 });
            structure.Add(new MarketingPlace { id_object = 110100, SortPosition = 0, hash = "jjd3k110100", id_parent = 11010 });
            structure.Add(new MarketingPlace { id_object = 101100, SortPosition = 0, hash = "jjd3k101100", id_parent = 10110 });
            structure.Add(new MarketingPlace { id_object = 111100, SortPosition = 0, hash = "jjd3k111100", id_parent = 11110 });
            structure.Add(new MarketingPlace { id_object = 100010, SortPosition = 0, hash = "jjd3k100010", id_parent = 10001 });
            structure.Add(new MarketingPlace { id_object = 110010, SortPosition = 0, hash = "jjd3k110010", id_parent = 11001 });
            structure.Add(new MarketingPlace { id_object = 101010, SortPosition = 0, hash = "jjd3k101010", id_parent = 10101 });
            structure.Add(new MarketingPlace { id_object = 111010, SortPosition = 0, hash = "jjd3k111010", id_parent = 11101 });
            structure.Add(new MarketingPlace { id_object = 100110, SortPosition = 0, hash = "jjd3k100110", id_parent = 10011 });
            structure.Add(new MarketingPlace { id_object = 110110, SortPosition = 0, hash = "jjd3k110110", id_parent = 11011 });
            structure.Add(new MarketingPlace { id_object = 101110, SortPosition = 0, hash = "jjd3k101110", id_parent = 10111 });
            structure.Add(new MarketingPlace { id_object = 111110, SortPosition = 0, hash = "jjd3k111110", id_parent = 11111 });
            structure.Add(new MarketingPlace { id_object = 100001, SortPosition = 1, hash = "jjd3k100001", id_parent = 10000 });
            structure.Add(new MarketingPlace { id_object = 110001, SortPosition = 1, hash = "jjd3k110001", id_parent = 11000 });
            structure.Add(new MarketingPlace { id_object = 101001, SortPosition = 1, hash = "jjd3k101001", id_parent = 10100 });
            structure.Add(new MarketingPlace { id_object = 111001, SortPosition = 1, hash = "jjd3k111001", id_parent = 11100 });
            structure.Add(new MarketingPlace { id_object = 100101, SortPosition = 1, hash = "jjd3k100101", id_parent = 10010 });
            structure.Add(new MarketingPlace { id_object = 110101, SortPosition = 1, hash = "jjd3k110101", id_parent = 11010 });
            structure.Add(new MarketingPlace { id_object = 101101, SortPosition = 1, hash = "jjd3k101101", id_parent = 10110 });
            structure.Add(new MarketingPlace { id_object = 111101, SortPosition = 1, hash = "jjd3k111101", id_parent = 11110 });
            structure.Add(new MarketingPlace { id_object = 100011, SortPosition = 1, hash = "jjd3k100011", id_parent = 10001 });
            structure.Add(new MarketingPlace { id_object = 110011, SortPosition = 1, hash = "jjd3k110011", id_parent = 11001 });
            structure.Add(new MarketingPlace { id_object = 101011, SortPosition = 1, hash = "jjd3k101011", id_parent = 10101 });
            structure.Add(new MarketingPlace { id_object = 111011, SortPosition = 1, hash = "jjd3k111011", id_parent = 11101 });
            structure.Add(new MarketingPlace { id_object = 100111, SortPosition = 1, hash = "jjd3k100111", id_parent = 10011 });
            structure.Add(new MarketingPlace { id_object = 110111, SortPosition = 1, hash = "jjd3k110111", id_parent = 11011 });
            structure.Add(new MarketingPlace { id_object = 101111, SortPosition = 1, hash = "jjd3k101111", id_parent = 10111 });
            structure.Add(new MarketingPlace { id_object = 111111, SortPosition = 1, hash = "jjd3k111111", id_parent = 11111 });

            var ht = service.GetBinaryHashTable(structure);
            var binaryHashes = service.Invent(ht);
            Assert.AreEqual(63, binaryHashes.Count);
            Assert.AreEqual((uint)1, binaryHashes["1"]);
            Assert.AreEqual((uint)10, binaryHashes["10"]);
            Assert.AreEqual((uint)11, binaryHashes["11"]);
            Assert.AreEqual((uint)100, binaryHashes["100"]);
            Assert.AreEqual((uint)110, binaryHashes["110"]);
            Assert.AreEqual((uint)101, binaryHashes["101"]);
            Assert.AreEqual((uint)111, binaryHashes["111"]);
            Assert.AreEqual((uint)1000, binaryHashes["1000"]);
            Assert.AreEqual((uint)1100, binaryHashes["1100"]);
            Assert.AreEqual((uint)1010, binaryHashes["1010"]);
            Assert.AreEqual((uint)1110, binaryHashes["1110"]);
            Assert.AreEqual((uint)1001, binaryHashes["1001"]);
            Assert.AreEqual((uint)1101, binaryHashes["1101"]);
            Assert.AreEqual((uint)1011, binaryHashes["1011"]);
            Assert.AreEqual((uint)1111, binaryHashes["1111"]);
            Assert.AreEqual((uint)10000, binaryHashes["10000"]);
            Assert.AreEqual((uint)11000, binaryHashes["11000"]);
            Assert.AreEqual((uint)10100, binaryHashes["10100"]);
            Assert.AreEqual((uint)11100, binaryHashes["11100"]);
            Assert.AreEqual((uint)10010, binaryHashes["10010"]);
            Assert.AreEqual((uint)11010, binaryHashes["11010"]);
            Assert.AreEqual((uint)10110, binaryHashes["10110"]);
            Assert.AreEqual((uint)11110, binaryHashes["11110"]);
            Assert.AreEqual((uint)10001, binaryHashes["10001"]);
            Assert.AreEqual((uint)11001, binaryHashes["11001"]);
            Assert.AreEqual((uint)10101, binaryHashes["10101"]);
            Assert.AreEqual((uint)11101, binaryHashes["11101"]);
            Assert.AreEqual((uint)10011, binaryHashes["10011"]);
            Assert.AreEqual((uint)11011, binaryHashes["11011"]);
            Assert.AreEqual((uint)10111, binaryHashes["10111"]);
            Assert.AreEqual((uint)11111, binaryHashes["11111"]);
            Assert.AreEqual((uint)100000, binaryHashes["100000"]);
            Assert.AreEqual((uint)110000, binaryHashes["110000"]);
            Assert.AreEqual((uint)101000, binaryHashes["101000"]);
            Assert.AreEqual((uint)111000, binaryHashes["111000"]);
            Assert.AreEqual((uint)100100, binaryHashes["100100"]);
            Assert.AreEqual((uint)110100, binaryHashes["110100"]);
            Assert.AreEqual((uint)101100, binaryHashes["101100"]);
            Assert.AreEqual((uint)111100, binaryHashes["111100"]);
            Assert.AreEqual((uint)100010, binaryHashes["100010"]);
            Assert.AreEqual((uint)110010, binaryHashes["110010"]);
            Assert.AreEqual((uint)101010, binaryHashes["101010"]);
            Assert.AreEqual((uint)111010, binaryHashes["111010"]);
            Assert.AreEqual((uint)100110, binaryHashes["100110"]);
            Assert.AreEqual((uint)110110, binaryHashes["110110"]);
            Assert.AreEqual((uint)101110, binaryHashes["101110"]);
            Assert.AreEqual((uint)111110, binaryHashes["111110"]);
            Assert.AreEqual((uint)100001, binaryHashes["100001"]);
            Assert.AreEqual((uint)110001, binaryHashes["110001"]);
            Assert.AreEqual((uint)101001, binaryHashes["101001"]);
            Assert.AreEqual((uint)111001, binaryHashes["111001"]);
            Assert.AreEqual((uint)100101, binaryHashes["100101"]);
            Assert.AreEqual((uint)110101, binaryHashes["110101"]);
            Assert.AreEqual((uint)101101, binaryHashes["101101"]);
            Assert.AreEqual((uint)111101, binaryHashes["111101"]);
            Assert.AreEqual((uint)100011, binaryHashes["100011"]);
            Assert.AreEqual((uint)110011, binaryHashes["110011"]);
            Assert.AreEqual((uint)101011, binaryHashes["101011"]);
            Assert.AreEqual((uint)111011, binaryHashes["111011"]);
            Assert.AreEqual((uint)100111, binaryHashes["100111"]);
            Assert.AreEqual((uint)110111, binaryHashes["110111"]);
            Assert.AreEqual((uint)101111, binaryHashes["101111"]);
            Assert.AreEqual((uint)111111, binaryHashes["111111"]);
        }

        [TestMethod]
        public void FindPosTest()
        {
            Service1 service = new Service1();

            var pos = service.FindPos(
                new MarketingPlace { id_object = 1, hash = "1", id_parent = 999, SortPosition = null },
                new[]
                {
                    new MarketingPlace { id_object=1, hash="1", id_parent=999, SortPosition=null },
                    new MarketingPlace { id_object=10, hash="10", id_parent=1, SortPosition=0 },
                    new MarketingPlace { id_object=11, hash="11", id_parent=1, SortPosition=1 },
                });
                
            Assert.AreEqual((uint)10, pos.Parent.id_object);
            Assert.AreEqual(0, pos.Pos);


            pos = service.FindPos(
                new MarketingPlace { id_object = 10, hash = "10", id_parent = 1, SortPosition = 0 },
                new[]
                {
                    new MarketingPlace { id_object=1, hash="1", id_parent=999, SortPosition=null },
                    new MarketingPlace { id_object=10, hash="10", id_parent=1, SortPosition=0 },
                    new MarketingPlace { id_object=11, hash="11", id_parent=1, SortPosition=1 },
                    new MarketingPlace { id_object=100, hash="100", id_parent=10, SortPosition=0 },
                });

            Assert.AreEqual((uint)10, pos.Parent.id_object);
            Assert.AreEqual(1, pos.Pos);
        }

        [TestMethod]
        public void IsFilledTest()
        {
            Service1 service = new Service1();
            List<MarketingPlace> structure = new List<MarketingPlace>();
            structure.Add(new MarketingPlace { id_object = 1000, id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 1001, id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 10000, id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 10001, id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 10010, id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 10011, id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 100000, id_parent = 10000 });
            structure.Add(new MarketingPlace { id_object = 100001, id_parent = 10000 });
            structure.Add(new MarketingPlace { id_object = 100010, id_parent = 10001 });
            structure.Add(new MarketingPlace { id_object = 100011, id_parent = 10001 });
            structure.Add(new MarketingPlace { id_object = 100100, id_parent = 10010 });
            structure.Add(new MarketingPlace { id_object = 100101, id_parent = 10010 });
            structure.Add(new MarketingPlace { id_object = 100110, id_parent = 10011 });
            structure.Add(new MarketingPlace { id_object = 100111, id_parent = 10011 });
            structure.Add(new MarketingPlace { id_object = 1000000, id_parent = 100000 });
            structure.Add(new MarketingPlace { id_object = 1000010, id_parent = 100001 });
            structure.Add(new MarketingPlace { id_object = 1000100, id_parent = 100010 });
            structure.Add(new MarketingPlace { id_object = 1000110, id_parent = 100011 });
            structure.Add(new MarketingPlace { id_object = 1001000, id_parent = 100100 });
            structure.Add(new MarketingPlace { id_object = 1001010, id_parent = 100101 });
            structure.Add(new MarketingPlace { id_object = 1001100, id_parent = 100110 });
            structure.Add(new MarketingPlace { id_object = 1001110, id_parent = 100111 });
            structure.Add(new MarketingPlace { id_object = 1000001, id_parent = 100000 });
            structure.Add(new MarketingPlace { id_object = 1000011, id_parent = 100001 });
            structure.Add(new MarketingPlace { id_object = 1000101, id_parent = 100010 });
            structure.Add(new MarketingPlace { id_object = 1000111, id_parent = 100011 });
            structure.Add(new MarketingPlace { id_object = 1001001, id_parent = 100100 });
            structure.Add(new MarketingPlace { id_object = 1001011, id_parent = 100101 });
            structure.Add(new MarketingPlace { id_object = 1001101, id_parent = 100110 });
            structure.Add(new MarketingPlace { id_object = 1001111, id_parent = 100111 });
            structure.Add(new MarketingPlace { id_object = 10000000, id_parent = 1000000 });
            structure.Add(new MarketingPlace { id_object = 10000100, id_parent = 1000010 });
            structure.Add(new MarketingPlace { id_object = 10001000, id_parent = 1000100 });
            structure.Add(new MarketingPlace { id_object = 10001100, id_parent = 1000110 });
            structure.Add(new MarketingPlace { id_object = 10010000, id_parent = 1001000 });
            structure.Add(new MarketingPlace { id_object = 10010100, id_parent = 1001010 });
            structure.Add(new MarketingPlace { id_object = 10011000, id_parent = 1001100 });
            structure.Add(new MarketingPlace { id_object = 10011100, id_parent = 1001110 });
            structure.Add(new MarketingPlace { id_object = 10000010, id_parent = 1000001 });
            structure.Add(new MarketingPlace { id_object = 10000110, id_parent = 1000011 });
            structure.Add(new MarketingPlace { id_object = 10001010, id_parent = 1000101 });
            structure.Add(new MarketingPlace { id_object = 10001110, id_parent = 1000111 });
            structure.Add(new MarketingPlace { id_object = 10010010, id_parent = 1001001 });
            structure.Add(new MarketingPlace { id_object = 10010110, id_parent = 1001011 });
            structure.Add(new MarketingPlace { id_object = 10011010, id_parent = 1001101 });
            structure.Add(new MarketingPlace { id_object = 10011110, id_parent = 1001111 });
            structure.Add(new MarketingPlace { id_object = 10000001, id_parent = 1000000 });
            structure.Add(new MarketingPlace { id_object = 10000101, id_parent = 1000000 });
            structure.Add(new MarketingPlace { id_object = 10001001, id_parent = 1000100 });
            structure.Add(new MarketingPlace { id_object = 10001101, id_parent = 1000110 });
            structure.Add(new MarketingPlace { id_object = 10010001, id_parent = 1001000 });
            structure.Add(new MarketingPlace { id_object = 10010101, id_parent = 1001010 });
            structure.Add(new MarketingPlace { id_object = 10011001, id_parent = 1001100 });
            structure.Add(new MarketingPlace { id_object = 10011101, id_parent = 1001110 });
            structure.Add(new MarketingPlace { id_object = 10000011, id_parent = 1000001 });
            structure.Add(new MarketingPlace { id_object = 10000111, id_parent = 1000011 });
            structure.Add(new MarketingPlace { id_object = 10001011, id_parent = 1000101 });
            structure.Add(new MarketingPlace { id_object = 10001111, id_parent = 1000111 });
            structure.Add(new MarketingPlace { id_object = 10010011, id_parent = 1001001 });
            structure.Add(new MarketingPlace { id_object = 10010111, id_parent = 1001011 });
            structure.Add(new MarketingPlace { id_object = 10011011, id_parent = 1001101 });
            structure.Add(new MarketingPlace { id_object = 10011111, id_parent = 1001111 });
            SetHashes(structure);

            Assert.AreEqual(true, service.IsFilled(
                new MarketingPlace { id_object = 100, hash = "12345abcdeabcde", id_parent = 10 },
                structure));


            structure = new List<MarketingPlace>();
            structure.Add(new MarketingPlace { id_object = 100, hash = "100", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 101, hash = "101", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 1000, hash = "1000", id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 1001, hash = "1001", id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 1010, hash = "1010", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 1011, hash = "1011", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 10000, hash = "10000", id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 10001, hash = "10001", id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 10010, hash = "10010", id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 10011, hash = "10011", id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 10100, hash = "10100", id_parent = 1010 });
            structure.Add(new MarketingPlace { id_object = 10101, hash = "10101", id_parent = 1010 });
            structure.Add(new MarketingPlace { id_object = 10110, hash = "10110", id_parent = 1011 });
            structure.Add(new MarketingPlace { id_object = 10111, hash = "10111", id_parent = 1011 });
            SetHashes(structure);

            Assert.AreEqual(false, service.IsFilled(
                new MarketingPlace { id_object = 10, hash = "12345abcde", id_parent = 1 },
                structure));
        }

        void SetHashes(IEnumerable<MarketingPlace> structure)
        {
            foreach (var item in structure)
            {
                item.hash = "";
                foreach (var c in item.id_object.ToString())
                {
                    if (c == '0')
                        item.hash += "abcde";
                    else
                        item.hash += "12345";
                }
            }
        }

        [TestMethod]
        public void GetActivePlaceTest()
        {
            Service1 service = new Service1();

            List<MarketingPlace> structure = new List<MarketingPlace>();
            structure.Add(new MarketingPlace { id_object = 10, hash = "10", id_parent = 1 });
            structure.Add(new MarketingPlace { id_object = 11, hash = "11", id_parent = 1 });
            structure.Add(new MarketingPlace { id_object = 100, hash = "100", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 110, hash = "110", id_parent = 11 });
            structure.Add(new MarketingPlace { id_object = 101, hash = "101", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 111, hash = "111", id_parent = 11 });
            structure.Add(new MarketingPlace { id_object = 1000, hash = "1000", id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 1100, hash = "1100", id_parent = 110 });
            structure.Add(new MarketingPlace { id_object = 1010, hash = "1010", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 1110, hash = "1110", id_parent = 111 });
            structure.Add(new MarketingPlace { id_object = 1001, hash = "1001", id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 1101, hash = "1101", id_parent = 110 });
            structure.Add(new MarketingPlace { id_object = 1011, hash = "1011", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 1111, hash = "1111", id_parent = 111 });
            structure.Add(new MarketingPlace { id_object = 10000, hash = "10000", id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 11000, hash = "11000", id_parent = 1100 });
            structure.Add(new MarketingPlace { id_object = 10100, hash = "10100", id_parent = 1010 });
            structure.Add(new MarketingPlace { id_object = 11100, hash = "11100", id_parent = 1110 });
            structure.Add(new MarketingPlace { id_object = 10010, hash = "10010", id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 11010, hash = "11010", id_parent = 1101 });
            structure.Add(new MarketingPlace { id_object = 10110, hash = "10110", id_parent = 1011 });
            structure.Add(new MarketingPlace { id_object = 11110, hash = "11110", id_parent = 1111 });
            structure.Add(new MarketingPlace { id_object = 10001, hash = "10001", id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 11001, hash = "11001", id_parent = 1100 });
            structure.Add(new MarketingPlace { id_object = 10101, hash = "10101", id_parent = 1010 });
            structure.Add(new MarketingPlace { id_object = 11101, hash = "11101", id_parent = 1110 });
            structure.Add(new MarketingPlace { id_object = 10011, hash = "10011", id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 11011, hash = "11011", id_parent = 1101 });
            structure.Add(new MarketingPlace { id_object = 10111, hash = "10111", id_parent = 1011 });
            structure.Add(new MarketingPlace { id_object = 11111, hash = "11111", id_parent = 1111 });
            structure.Add(new MarketingPlace { id_object = 100000, hash = "100000", id_parent = 10000 });
            structure.Add(new MarketingPlace { id_object = 110000, hash = "110000", id_parent = 11000 });
            structure.Add(new MarketingPlace { id_object = 101000, hash = "101000", id_parent = 10100 });
            structure.Add(new MarketingPlace { id_object = 111000, hash = "111000", id_parent = 11100 });
            structure.Add(new MarketingPlace { id_object = 100100, hash = "100100", id_parent = 10010 });
            structure.Add(new MarketingPlace { id_object = 110100, hash = "110100", id_parent = 11010 });
            structure.Add(new MarketingPlace { id_object = 101100, hash = "101100", id_parent = 10110 });
            structure.Add(new MarketingPlace { id_object = 111100, hash = "111100", id_parent = 11110 });
            structure.Add(new MarketingPlace { id_object = 100010, hash = "100010", id_parent = 10001 });
            structure.Add(new MarketingPlace { id_object = 110010, hash = "110010", id_parent = 11001 });
            structure.Add(new MarketingPlace { id_object = 101010, hash = "101010", id_parent = 10101 });
            structure.Add(new MarketingPlace { id_object = 111010, hash = "111010", id_parent = 11101 });
            structure.Add(new MarketingPlace { id_object = 100110, hash = "100110", id_parent = 10011 });
            structure.Add(new MarketingPlace { id_object = 110110, hash = "110110", id_parent = 11011 });
            structure.Add(new MarketingPlace { id_object = 101110, hash = "101110", id_parent = 10111 });
            structure.Add(new MarketingPlace { id_object = 111110, hash = "111110", id_parent = 11111 });
            structure.Add(new MarketingPlace { id_object = 100001, hash = "100001", id_parent = 10000 });
            structure.Add(new MarketingPlace { id_object = 110001, hash = "110001", id_parent = 11000 });
            structure.Add(new MarketingPlace { id_object = 101001, hash = "101001", id_parent = 10100 });
            structure.Add(new MarketingPlace { id_object = 111001, hash = "111001", id_parent = 11100 });
            structure.Add(new MarketingPlace { id_object = 100101, hash = "100101", id_parent = 10010 });
            structure.Add(new MarketingPlace { id_object = 110101, hash = "110101", id_parent = 11010 });
            structure.Add(new MarketingPlace { id_object = 101101, hash = "101101", id_parent = 10110 });
            structure.Add(new MarketingPlace { id_object = 111101, hash = "111101", id_parent = 11110 });
            structure.Add(new MarketingPlace { id_object = 100011, hash = "100011", id_parent = 10001 });
            structure.Add(new MarketingPlace { id_object = 110011, hash = "110011", id_parent = 11001 });
            structure.Add(new MarketingPlace { id_object = 101011, hash = "101011", id_parent = 10101 });
            structure.Add(new MarketingPlace { id_object = 111011, hash = "111011", id_parent = 11101 });
            structure.Add(new MarketingPlace { id_object = 100111, hash = "100111", id_parent = 10011 });
            structure.Add(new MarketingPlace { id_object = 110111, hash = "110111", id_parent = 11011 });
            structure.Add(new MarketingPlace { id_object = 101111, hash = "101111", id_parent = 10111 });
            structure.Add(new MarketingPlace { id_object = 111111, hash = "111111", id_parent = 11111 });
            //12345abcde
            SetHashes(structure);



            Assert.AreEqual((uint)101,
                service.GetActivePlace(new[]{
                        new MarketingPlace { id_object = 1, hash = "12345", id_parent = 999 },
                        new MarketingPlace { id_object = 101, hash = "12345abcde12345", id_parent = 10 }
                    }, structure).id_object);




            structure.Add(new MarketingPlace { id_object = 1010, hash = "1010", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 1011, hash = "1011", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 10100, hash = "10100", id_parent = 1010 });
            structure.Add(new MarketingPlace { id_object = 10110, hash = "10110", id_parent = 1011 });
            structure.Add(new MarketingPlace { id_object = 10101, hash = "10101", id_parent = 1010 });
            structure.Add(new MarketingPlace { id_object = 10111, hash = "10111", id_parent = 1011 });
            structure.Add(new MarketingPlace { id_object = 101000, hash = "101000", id_parent = 10100 });
            structure.Add(new MarketingPlace { id_object = 101100, hash = "101100", id_parent = 10110 });
            structure.Add(new MarketingPlace { id_object = 101010, hash = "101010", id_parent = 10101 });
            structure.Add(new MarketingPlace { id_object = 101110, hash = "101110", id_parent = 10111 });
            structure.Add(new MarketingPlace { id_object = 101001, hash = "101001", id_parent = 10100 });
            structure.Add(new MarketingPlace { id_object = 101101, hash = "101101", id_parent = 10110 });
            structure.Add(new MarketingPlace { id_object = 101011, hash = "101011", id_parent = 10101 });
            structure.Add(new MarketingPlace { id_object = 101111, hash = "101111", id_parent = 10111 });
            structure.Add(new MarketingPlace { id_object = 1010000, hash = "1010000", id_parent = 101000 });
            structure.Add(new MarketingPlace { id_object = 1011000, hash = "1011000", id_parent = 101100 });
            structure.Add(new MarketingPlace { id_object = 1010100, hash = "1010100", id_parent = 101010 });
            structure.Add(new MarketingPlace { id_object = 1011100, hash = "1011100", id_parent = 101110 });
            structure.Add(new MarketingPlace { id_object = 1010010, hash = "1010010", id_parent = 101001 });
            structure.Add(new MarketingPlace { id_object = 1011010, hash = "1011010", id_parent = 101101 });
            structure.Add(new MarketingPlace { id_object = 1010110, hash = "1010110", id_parent = 101011 });
            structure.Add(new MarketingPlace { id_object = 1011110, hash = "1011110", id_parent = 101111 });
            structure.Add(new MarketingPlace { id_object = 1010001, hash = "1010001", id_parent = 101000 });
            structure.Add(new MarketingPlace { id_object = 1011001, hash = "1011001", id_parent = 101100 });
            structure.Add(new MarketingPlace { id_object = 1010101, hash = "1010101", id_parent = 101010 });
            structure.Add(new MarketingPlace { id_object = 1011101, hash = "1011101", id_parent = 101110 });
            structure.Add(new MarketingPlace { id_object = 1010011, hash = "1010011", id_parent = 101001 });
            structure.Add(new MarketingPlace { id_object = 1011011, hash = "1011011", id_parent = 101101 });
            structure.Add(new MarketingPlace { id_object = 1010111, hash = "1010111", id_parent = 101011 });
            structure.Add(new MarketingPlace { id_object = 1011111, hash = "1011111", id_parent = 101111 });
            structure.Add(new MarketingPlace { id_object = 10100000, hash = "10100000", id_parent = 1010000 });
            structure.Add(new MarketingPlace { id_object = 10110000, hash = "10110000", id_parent = 1011000 });
            structure.Add(new MarketingPlace { id_object = 10101000, hash = "10101000", id_parent = 1010100 });
            structure.Add(new MarketingPlace { id_object = 10111000, hash = "10111000", id_parent = 1011100 });
            structure.Add(new MarketingPlace { id_object = 10100100, hash = "10100100", id_parent = 1010010 });
            structure.Add(new MarketingPlace { id_object = 10110100, hash = "10110100", id_parent = 1011010 });
            structure.Add(new MarketingPlace { id_object = 10101100, hash = "10101100", id_parent = 1010110 });
            structure.Add(new MarketingPlace { id_object = 10111100, hash = "10111100", id_parent = 1011110 });
            structure.Add(new MarketingPlace { id_object = 10100010, hash = "10100010", id_parent = 1010001 });
            structure.Add(new MarketingPlace { id_object = 10110010, hash = "10110010", id_parent = 1011001 });
            structure.Add(new MarketingPlace { id_object = 10101010, hash = "10101010", id_parent = 1010101 });
            structure.Add(new MarketingPlace { id_object = 10111010, hash = "10111010", id_parent = 1011101 });
            structure.Add(new MarketingPlace { id_object = 10100110, hash = "10100110", id_parent = 1010011 });
            structure.Add(new MarketingPlace { id_object = 10110110, hash = "10110110", id_parent = 1011011 });
            structure.Add(new MarketingPlace { id_object = 10101110, hash = "10101110", id_parent = 1010111 });
            structure.Add(new MarketingPlace { id_object = 10111110, hash = "10111110", id_parent = 1011111 });
            structure.Add(new MarketingPlace { id_object = 10100001, hash = "10100001", id_parent = 1010000 });
            structure.Add(new MarketingPlace { id_object = 10110001, hash = "10110001", id_parent = 1011000 });
            structure.Add(new MarketingPlace { id_object = 10101001, hash = "10101001", id_parent = 1010100 });
            structure.Add(new MarketingPlace { id_object = 10111001, hash = "10111001", id_parent = 1011100 });
            structure.Add(new MarketingPlace { id_object = 10100101, hash = "10100101", id_parent = 1010010 });
            structure.Add(new MarketingPlace { id_object = 10110101, hash = "10110101", id_parent = 1011010 });
            structure.Add(new MarketingPlace { id_object = 10101101, hash = "10101101", id_parent = 1010110 });
            structure.Add(new MarketingPlace { id_object = 10111101, hash = "10111101", id_parent = 1011110 });
            structure.Add(new MarketingPlace { id_object = 10100011, hash = "10100011", id_parent = 1010001 });
            structure.Add(new MarketingPlace { id_object = 10110011, hash = "10110011", id_parent = 1011001 });
            structure.Add(new MarketingPlace { id_object = 10101011, hash = "10101011", id_parent = 1010101 });
            structure.Add(new MarketingPlace { id_object = 10111011, hash = "10111011", id_parent = 1011101 });
            structure.Add(new MarketingPlace { id_object = 10100111, hash = "10100111", id_parent = 1010011 });
            structure.Add(new MarketingPlace { id_object = 10110111, hash = "10110111", id_parent = 1011011 });
            structure.Add(new MarketingPlace { id_object = 10101111, hash = "10101111", id_parent = 1010111 });
            structure.Add(new MarketingPlace { id_object = 10111111, hash = "10111111", id_parent = 1011111 });
            SetHashes(structure);

            Assert.AreEqual((uint)1,
                     service.GetActivePlace(new[]{
                        new MarketingPlace { id_object = 1, hash = "12345", id_parent = 999 },
                        new MarketingPlace { id_object = 101, hash = "12345abcde12345", id_parent = 10 }
                    }, structure).id_object);
        }


        [TestMethod]
        public void GetParentTest()
        {
            List<MarketingPlace> structure = new List<MarketingPlace>();
            structure.Add(new MarketingPlace { id_object = 1, hash = "abc1", id_parent = 999 });
            structure.Add(new MarketingPlace { id_object = 10, hash = "abc10", id_parent = 1 });
            structure.Add(new MarketingPlace { id_object = 11, hash = "abc11", id_parent = 1 });
            structure.Add(new MarketingPlace { id_object = 101, hash = "abc101", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 110, hash = "abc110", id_parent = 11 });
            structure.Add(new MarketingPlace { id_object = 1011, hash = "abc1011", id_parent = 101 });
            structure.Add(new MarketingPlace { id_object = 1100, hash = "abc1100", id_parent = 110 });
            structure.Add(new MarketingPlace { id_object = 11001, hash = "abc11001", id_parent = 1100 });
            structure.Add(new MarketingPlace { id_object = 110010, hash = "abc110010", id_parent = 11001 });

            Service1 service = new Service1();
            Assert.AreEqual((uint)101, 
                service.GetParent(structure.Single(x=>x.id_object == 1011), 
                structure, 1).id_object);

            Assert.AreEqual((uint)11001, 
                service.GetParent(structure.Single(x => x.id_object == 110010), 
                structure, 1).id_object);

            Assert.AreEqual((uint)11,
                service.GetParent(structure.Single(x => x.id_object == 110010),
                structure, 4).id_object);

            Assert.AreEqual(null,
                service.GetParent(structure.Single(x => x.id_object == 110),
                structure, 5));
        }

        [TestMethod]
        public void UpdateParentInvestShoulderSumTest()
        {
            List<MarketingPlace> structure = new List<MarketingPlace>();
            structure.Add(new MarketingPlace { id_object = 1, hash = "abc1", id_parent = 999, PartnerLeftShoulderInvestSum = 37100, PartnerRightShoulderInvestSum = 13000 });
            structure.Add(new MarketingPlace { id_object = 10, hash = "abc10", id_parent = 1, SortPosition = 0, PartnerLeftShoulderInvestSum = 26600, PartnerRightShoulderInvestSum = 2000 });
            structure.Add(new MarketingPlace { id_object = 11, hash = "abc11", id_parent = 1, SortPosition = 1, PartnerLeftShoulderInvestSum = 1400, PartnerRightShoulderInvestSum = 1600 });

            structure.Add(new MarketingPlace { id_object = 101, hash = "abc101", id_parent = 10, SortPosition = 0, PartnerLeftShoulderInvestSum = 10100, PartnerRightShoulderInvestSum = 11000 });
            structure.Add(new MarketingPlace { id_object = 110, hash = "abc110", id_parent = 10, SortPosition = 1, PartnerLeftShoulderInvestSum = 1000, PartnerRightShoulderInvestSum = 0 });

            structure.Add(new MarketingPlace { id_object = 102, hash = "abc101", id_parent = 11, SortPosition = 0, PartnerLeftShoulderInvestSum = 300, PartnerRightShoulderInvestSum = 100 });
            structure.Add(new MarketingPlace { id_object = 111, hash = "abc110", id_parent = 11, SortPosition = 1, PartnerLeftShoulderInvestSum = 0, PartnerRightShoulderInvestSum = 1500 });

            structure.Add(new MarketingPlace { id_object = 1011, hash = "abc1011", id_parent = 110, SortPosition = 0, PartnerLeftShoulderInvestSum = 100, PartnerRightShoulderInvestSum = 0 });
            structure.Add(new MarketingPlace { id_object = 1100, hash = "abc1100", id_parent = 101, SortPosition = 1, PartnerLeftShoulderInvestSum = 1000, PartnerRightShoulderInvestSum = 0 });
            structure.Add(new MarketingPlace { id_object = 11002, hash = "abc11001", id_parent = 1011, SortPosition = 0, PartnerLeftShoulderInvestSum = 0, PartnerRightShoulderInvestSum = 0 });


            structure.Add(new MarketingPlace { id_object = 1012, hash = "abc1011", id_parent = 102, SortPosition = 0, PartnerLeftShoulderInvestSum = 100, PartnerRightShoulderInvestSum = 100 });
            structure.Add(new MarketingPlace { id_object = 1013, hash = "abc1011", id_parent = 102, SortPosition = 1, PartnerLeftShoulderInvestSum = 100, PartnerRightShoulderInvestSum = 100 });


            structure.Add(new MarketingPlace { id_object = 1101, hash = "abc1100", id_parent = 111, SortPosition = 1, PartnerLeftShoulderInvestSum = 0, PartnerRightShoulderInvestSum = 500 });

            structure.Add(new MarketingPlace { id_object = 11001, hash = "abc11001", id_parent = 1101, SortPosition = 0, PartnerLeftShoulderInvestSum = 300, PartnerRightShoulderInvestSum = 100 });
            structure.Add(new MarketingPlace { id_object = 110010, hash = "abc110010", id_parent = 11001, SortPosition = 0, PartnerLeftShoulderInvestSum = 200, PartnerRightShoulderInvestSum = 0 });

            Service1 service = new Service1();
            var partner = new MarketingPlace { id_object = 110010, hash = "abc110010", id_parent = 11001, SortPosition = 0 };
            var parentPlace = service.GetParent(partner, structure, 1);
            decimal ShoulderSum = 0;
            decimal Sum = 1000;
            
            while (parentPlace != null)
            {
                if (parentPlace.id_object == 1)
                {
                    break;
                }
                if (partner.SortPosition == 0)
                {
                    ShoulderSum = parentPlace.PartnerLeftShoulderInvestSum != null ? (decimal)parentPlace.PartnerLeftShoulderInvestSum : 0;
                    parentPlace.PartnerLeftShoulderInvestSum += Sum;
                }
                else
                {
                    ShoulderSum = parentPlace.PartnerRightShoulderInvestSum != null ? (decimal)parentPlace.PartnerRightShoulderInvestSum : 0;
                    parentPlace.PartnerRightShoulderInvestSum += Sum;
                }
                partner = parentPlace;
                parentPlace = service.GetParent(parentPlace, structure, 1);
            }

            


            Assert.AreEqual((decimal)1300,
                service.GetParent(structure.Single(x => x.id_object == 110010),
                structure, 1).PartnerLeftShoulderInvestSum);

            Assert.AreEqual((decimal)1000,
                service.GetParent(structure.Single(x => x.id_object == 11001),
                structure, 1).PartnerLeftShoulderInvestSum);

            Assert.AreEqual((decimal)2500,
                service.GetParent(structure.Single(x => x.id_object == 1101),
                structure, 1).PartnerRightShoulderInvestSum);

            Assert.AreEqual((decimal)2600,
                service.GetParent(structure.Single(x => x.id_object == 111),
                structure, 1).PartnerRightShoulderInvestSum);

            Assert.AreEqual((decimal)13000,
                service.GetParent(structure.Single(x => x.id_object == 11),
                structure, 1).PartnerRightShoulderInvestSum);


            partner = new MarketingPlace { id_object = 11002, hash = "abc11001", id_parent = 1011, SortPosition = 0, PartnerLeftShoulderInvestSum = 0, PartnerRightShoulderInvestSum = 0 };
            parentPlace = service.GetParent(partner, structure, 1);
            while (parentPlace != null)
            {
                if (parentPlace.id_object == 1)
                {
                    break;
                }
                if (partner.SortPosition == 0)
                {
                    ShoulderSum = parentPlace.PartnerLeftShoulderInvestSum != null ? (decimal)parentPlace.PartnerLeftShoulderInvestSum : 0;
                    parentPlace.PartnerLeftShoulderInvestSum += Sum;
                }
                else
                {
                    ShoulderSum = parentPlace.PartnerRightShoulderInvestSum != null ? (decimal)parentPlace.PartnerRightShoulderInvestSum : 0;
                    parentPlace.PartnerRightShoulderInvestSum += Sum;
                }
                partner = parentPlace;
                parentPlace = service.GetParent(parentPlace, structure, 1);
            }


            Assert.AreEqual((decimal)1100,
                service.GetParent(structure.Single(x => x.id_object == 11002),
                structure, 1).PartnerLeftShoulderInvestSum);

            Assert.AreEqual((decimal)2000,
                service.GetParent(structure.Single(x => x.id_object == 1011),
                structure, 1).PartnerLeftShoulderInvestSum);

            Assert.AreEqual((decimal)3000,
                service.GetParent(structure.Single(x => x.id_object == 110),
                structure, 1).PartnerRightShoulderInvestSum);
        }


        [TestMethod]
        public void CalcStructValueTest()
        {
            List<MarketingPlace> structure = new List<MarketingPlace>();
            var place10 = new MarketingPlace { id_object = 10, hash = "dsk210", id_parent = 1 };
            structure.Add(new MarketingPlace { id_object = 1, hash = "dsk21", id_parent = 999 });
            structure.Add(place10);
            structure.Add(new MarketingPlace { id_object = 100, hash = "dsk2100", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 101, hash = "dsk2101", id_parent = 10 });
            structure.Add(new MarketingPlace { id_object = 1000, hash = "dsk21000", id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 1001, hash = "dsk21001", id_parent = 100 });
            structure.Add(new MarketingPlace { id_object = 10000, hash = "dsk210000", id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 10001, hash = "dsk210001", id_parent = 1000 });
            structure.Add(new MarketingPlace { id_object = 10010, hash = "dsk210010", id_parent = 1001 });
            structure.Add(new MarketingPlace { id_object = 10011, hash = "dsk210011", id_parent = 1001 });
            SetHashes(structure);

            Service1 service = new Service1();
            Assert.AreEqual(4, service.CalcStructValue(place10, structure, 2));

            structure = new List<MarketingPlace>();
            var place110 = new MarketingPlace { id_object = 110, hash = "110", id_parent = 11 };
            structure.Add(place110);
            structure.Add(new MarketingPlace { id_object = 1100, hash = "1100", id_parent = 110 });
            structure.Add(new MarketingPlace { id_object = 1101, hash = "1101", id_parent = 110 });
            structure.Add(new MarketingPlace { id_object = 11000, hash = "11000", id_parent = 1100 });
            structure.Add(new MarketingPlace { id_object = 11001, hash = "11001", id_parent = 1100 });
            structure.Add(new MarketingPlace { id_object = 11010, hash = "11010", id_parent = 1101 });
            structure.Add(new MarketingPlace { id_object = 11011, hash = "11011", id_parent = 1101 });
            structure.Add(new MarketingPlace { id_object = 110000, hash = "110000", id_parent = 11000 });
            structure.Add(new MarketingPlace { id_object = 110001, hash = "110001", id_parent = 11000 });
            structure.Add(new MarketingPlace { id_object = 110010, hash = "110010", id_parent = 11001 });
            structure.Add(new MarketingPlace { id_object = 110011, hash = "110011", id_parent = 11001 });
            structure.Add(new MarketingPlace { id_object = 110111, hash = "110111", id_parent = 11011 });
            SetHashes(structure);
            Assert.AreEqual(11, service.CalcStructValue(place110, structure, 3));
        }

        [TestMethod]
        public void ChargeOnTheMainWalletTest()
        {
            Service1 service = new Service1();

            Assert.AreEqual(false, service.ChargeOnTheMainWallet(0, 1));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(0, 2));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(0, 3));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(0, 4));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(0, 5));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(0, 6));


            Assert.AreEqual(true, service.ChargeOnTheMainWallet(1, 1));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(1, 2));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(1, 3));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(1, 4));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(1, 5));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(1, 6));

            Assert.AreEqual(true, service.ChargeOnTheMainWallet(2, 1));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(2, 2));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(2, 3));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(2, 4));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(2, 5));
            Assert.AreEqual(false, service.ChargeOnTheMainWallet(2, 6));

            Assert.AreEqual(true, service.ChargeOnTheMainWallet(3, 1));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(3, 2));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(3, 3));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(3, 4));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(3, 5));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(3, 6));

            Assert.AreEqual(true, service.ChargeOnTheMainWallet(4, 1));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(4, 2));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(4, 3));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(4, 4));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(4, 5));
            Assert.AreEqual(true, service.ChargeOnTheMainWallet(4, 6));
        }

        [TestMethod]
        public void IsRightInvestSumTest()
        {
            Service1 service = new Service1();
            Assert.AreEqual(true, service.IsRightInvestSum(1500, 1000, 3500, 500));
            Assert.AreEqual(false, service.IsRightInvestSum(4422, 4000, 9500, 500));
        }

        [TestMethod]
        public void GetInvestBonusLevelsTest()
        {
            Service1 service = new Service1();
            Assert.AreEqual(2, service.GetInvestBonusLevels(0));
            Assert.AreEqual(5, service.GetInvestBonusLevels(2));
        }


        [TestMethod]
        public void GetInvestRankTest()
        {
            Service1 service = new Service1();
            Assert.AreEqual(0, service.GetInvestRank(1100));
            Assert.AreEqual(2, service.GetInvestRank((decimal)66e3));
        }



        [TestMethod]
        public void TransformInfinityBonusPercentTest()
        {
            Service1 service = new Service1();
            var chain = service.TransformInfinityBonusPercent(new List<decimal> { 5, 5, 0, 7, 6 });
            Assert.AreEqual(chain.Count(), 5);
            Assert.AreEqual(chain.ElementAt(0), 5);
            Assert.AreEqual(chain.ElementAt(1), 0);
            Assert.AreEqual(chain.ElementAt(2), 0);
            Assert.AreEqual(chain.ElementAt(3), 2);
            Assert.AreEqual(chain.ElementAt(4), 0);


            chain = service.TransformInfinityBonusPercent(new List<decimal> { 0, 1, 3, 0, 5, 5 });
            Assert.AreEqual(chain.Count(), 6);
            Assert.AreEqual(chain.ElementAt(0), 0);
            Assert.AreEqual(chain.ElementAt(1), 1);
            Assert.AreEqual(chain.ElementAt(2), 2);
            Assert.AreEqual(chain.ElementAt(3), 0);
            Assert.AreEqual(chain.ElementAt(4), 2);
            Assert.AreEqual(chain.ElementAt(5), 0);
        }


        [TestMethod]
        public void GetInfinityPercentTest()
        {
            Service1 service = new Service1();
            Assert.AreEqual(0, service.GetInfinityPercent(2));
            Assert.AreEqual(0.05, service.GetInfinityPercent(3));
            Assert.AreEqual(0.06, service.GetInfinityPercent(4));
            Assert.AreEqual(0.08, service.GetInfinityPercent(5));
            Assert.AreEqual(0.1, service.GetInfinityPercent(6));
            Assert.AreEqual(0.1, service.GetInfinityPercent(7));
        }

        [TestMethod]
        public void GetInfinityPercentChainTest()
        {
            Dictionary<uint, uint> structure = new Dictionary<uint, uint>();
            structure.Add(1, 0);
            structure.Add(11, 1);
            structure.Add(12, 1);
            structure.Add(13, 1);
            structure.Add(111, 11);
            structure.Add(112, 11);
            structure.Add(121, 12);
            structure.Add(122, 12);
            structure.Add(1211, 121);

            structure.Add(131, 13);
            structure.Add(1311, 131);
            structure.Add(13111, 1311);


            Dictionary<uint, decimal> percents = new Dictionary<uint, decimal>();
            percents.Add(1, 5);
            percents.Add(11, 2);
            percents.Add(12, 3);
            percents.Add(13, 7);
            percents.Add(111, 1);
            percents.Add(112, 0);
            percents.Add(121, 0);
            percents.Add(122, 2);
            percents.Add(1211, 3);

            percents.Add(131, 4);
            percents.Add(1311, 12);
            percents.Add(13111, 0);

            Service1 service = new Service1();
            var chain = service.GetInfinityPercentChain(structure, percents, 1211);
            Assert.AreEqual(3, chain.Count);
            Assert.AreEqual(0, chain[0]);
            Assert.AreEqual(3, chain[1]);
            Assert.AreEqual(5, chain[2]);


            chain = service.GetInfinityPercentChain(structure, percents, 13111);
            Assert.AreEqual(4, chain.Count);
            Assert.AreEqual(12, chain[0]);
            Assert.AreEqual(4, chain[1]);
            Assert.AreEqual(7, chain[2]);
            Assert.AreEqual(5, chain[3]);

        }

        [TestMethod]
        public void CalcInfinityPercentsTest()
        {
            Dictionary<uint, uint> structure = new Dictionary<uint, uint>();
            structure.Add(1, 0);

            structure.Add(11, 1);
            structure.Add(12, 1);
            structure.Add(13, 1);
            structure.Add(14, 1);

            structure.Add(111, 11);
            structure.Add(112, 11);
            structure.Add(113, 11);

            structure.Add(121, 12);
            structure.Add(122, 12);
            structure.Add(123, 12);
            structure.Add(124, 12);

            structure.Add(131, 13);
            structure.Add(132, 13);
            structure.Add(133, 13);
            structure.Add(134, 13);
            structure.Add(135, 13);
            structure.Add(136, 13);

            structure.Add(141, 14);
            structure.Add(142, 14);
            structure.Add(143, 14);
            structure.Add(144, 14);
            structure.Add(145, 14);
            structure.Add(146, 14);

            Dictionary<uint, int> ranks = new Dictionary<uint, int>();
            ranks.Add(1, 4);

            ranks.Add(11, 1);
            ranks.Add(12, 1);
            ranks.Add(13, 1);
            ranks.Add(14, 1);

            ranks.Add(111, 1);
            ranks.Add(112, 1);
            ranks.Add(113, 1);

            ranks.Add(121, 2);
            ranks.Add(122, 0);
            ranks.Add(123, 3);
            ranks.Add(124, 4);

            ranks.Add(131, 1);
            ranks.Add(132, 1);
            ranks.Add(133, 1);
            ranks.Add(134, 1);
            ranks.Add(135, 1);
            ranks.Add(136, 0);

            ranks.Add(141, 1);
            ranks.Add(142, 1);
            ranks.Add(143, 1);
            ranks.Add(144, 1);
            ranks.Add(145, 1);
            ranks.Add(146, 1);

            Service1 service = new Service1();
            var percents = service.CalcInfinityPercents(structure, ranks);
            Assert.AreEqual(24, percents.Count);
            Assert.AreEqual(0, percents[1]);
            Assert.AreEqual(0.05, percents[11]);
            Assert.AreEqual(0, percents[12]);
            Assert.AreEqual(0.08, percents[13]);
            Assert.AreEqual(0.10, percents[14]);
            Assert.AreEqual(0, percents[111]);
            Assert.AreEqual(0, percents[112]);
            Assert.AreEqual(0, percents[113]);
            Assert.AreEqual(0, percents[121]);
            Assert.AreEqual(0, percents[122]);
            Assert.AreEqual(0, percents[123]);
            Assert.AreEqual(0, percents[124]);
            Assert.AreEqual(0, percents[131]);
            Assert.AreEqual(0, percents[132]);
            Assert.AreEqual(0, percents[133]);
            Assert.AreEqual(0, percents[134]);
            Assert.AreEqual(0, percents[135]);
            Assert.AreEqual(0, percents[136]);
            Assert.AreEqual(0, percents[141]);
            Assert.AreEqual(0, percents[142]);
            Assert.AreEqual(0, percents[143]);
            Assert.AreEqual(0, percents[144]);
            Assert.AreEqual(0, percents[145]);
            Assert.AreEqual(0, percents[146]);
        }

        [TestMethod]
        public void CalcInvestValuesTest()
        {
            Dictionary<uint, uint> structure = new Dictionary<uint, uint>();
            structure.Add(1, 0);
            structure.Add(11, 1);
            structure.Add(12, 1);
            structure.Add(13, 1);
            structure.Add(111, 11);
            structure.Add(112, 11);
            structure.Add(121, 12);
            structure.Add(122, 12);
            structure.Add(1211, 121);

            Dictionary<uint, decimal> investments = new Dictionary<uint, decimal>();
            investments.Add(1, 0);
            investments.Add(11, (decimal)1e3);
            investments.Add(12, (decimal)5e3);
            investments.Add(13, 0);
            investments.Add(111, (decimal)1e3);
            investments.Add(112, (decimal)2e3);
            investments.Add(121, (decimal)1e3);
            investments.Add(122, (decimal)3.5e3);
            investments.Add(1211, (decimal)1e3);

            Service1 service = new Service1();
            var values = service.CalcInvestValues(structure, investments);
            Assert.AreEqual(9, values.Count);
            Assert.AreEqual(6e3, values[1]);
            Assert.AreEqual(4e3, values[11]);
            Assert.AreEqual(9.5e3, values[12]);
            Assert.AreEqual(0, values[13]);
            Assert.AreEqual(1e3, values[111]);
            Assert.AreEqual(2e3, values[112]);
            Assert.AreEqual(2e3, values[121]);
            Assert.AreEqual(3.5e3, values[122]);
            Assert.AreEqual(1e3, values[1211]);
        }

        [TestMethod]
        public void CalcInvestRanksTest()
        {
            Dictionary<uint, decimal> values = new Dictionary<uint, decimal>();
            values.Add(1, 1000);
            values.Add(11, 16000);
            values.Add(12, 36000);
            values.Add(13, 0);

            Service1 service = new Service1();
            var ranks = service.CalcInvestRanks(values);

            Assert.AreEqual(4, ranks.Count);
            Assert.AreEqual(0, ranks[1]);
            Assert.AreEqual(4, ranks[11]);
            Assert.AreEqual(3, ranks[12]);
            Assert.AreEqual(-1, ranks[13]);
        }


        [TestMethod]
        public void IsAllowCreateNewPlaceTest()
        {
            var service = new Service1();
            Assert.AreEqual(true, service.IsAllowCreateNewPlace(1, 4));
            Assert.AreEqual(false, service.IsAllowCreateNewPlace(3, 8));
            Assert.AreEqual(false, service.IsAllowCreateNewPlace(0, 6));
        }


        [TestMethod]
        public void GetForInvestReminderSumTest()
        {
            var service = new Service1();
            Assert.AreEqual(1000, service.GetForInvestReminderSum(1000, 100));
            Assert.AreEqual(1250, service.GetForInvestReminderSum(1500, -250));
            Assert.AreEqual(0, service.GetForInvestReminderSum(2100, -2500));
        }

        [TestMethod]
        public void GetSteppedSumTest()
        {
            var service = new Service1();
            Assert.AreEqual(1000, service.GetSteppedSum(1120, 500));
            Assert.AreEqual(2000, service.GetSteppedSum(2330, 1000));
        }

        [TestMethod]
        public void GetRefAwardTest()
        {
            var service = new Service1();
            Assert.AreEqual(200, service.GetRefAward(1000, 1200));
            Assert.AreEqual(0, service.GetRefAward(2000, 800));
        }

        [TestMethod]
        public void countWorkDays()
        {
            DateTime date = DateTime.ParseExact("2019-04-25", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            int countAddDay = 6;
            
            var service = new Service1();
            countAddDay = countAddDay > 0 ? service.countAddDaysForStartDate(date, countAddDay) : 0;
            DateTime startDate = date.AddDays(countAddDay);
            Assert.AreEqual(263, service.countWorkDays(startDate));

            date = DateTime.ParseExact("2019-05-14", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            countAddDay = 6;
            
            countAddDay = countAddDay > 0 ? service.countAddDaysForStartDate(date, countAddDay) : 0;
            startDate = date.AddDays(countAddDay);
            Assert.AreEqual(262, service.countWorkDays(startDate));
        }

    }
}
