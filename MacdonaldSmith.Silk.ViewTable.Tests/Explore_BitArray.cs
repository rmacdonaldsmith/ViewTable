using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MacdonaldSmith.Silk.ViewTable.Tests
{
    [TestFixture]
    public class Explore_BitArray
    {
        [Test]
        public void PlayingAround()
        {
            var elements = 10;
            var bitArray = new BitArray(elements);

            //set each bit with an even index to ON
            for (int i = 0; i < bitArray.Count; i++)
            {
                if (i%2 == 0)
                    bitArray.Set(i, true);
            }

            Assert.IsTrue(bitArray[2]);
            Assert.IsTrue(bitArray[4]);
            Assert.IsTrue(bitArray[6]);
            Assert.IsTrue(bitArray[8]);
        }
    }
}
