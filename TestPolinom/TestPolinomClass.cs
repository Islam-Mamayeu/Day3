using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Logic;


namespace TestPolinom
{
    [TestFixture]
    public class TestPolinomClass
    {
        [Test]
        public void PolinomEqualsTest(Polinom p1,Polinom p2)
        {

            double[] mas = { 4, 2, 6, 4, -3, 0, 4 };
            double[] mas2 = { 4, 2, 6, 4, -3, 0, 4 };
            p1 = new Polinom(mas);
            p2 = new Polinom(mas2);

            Assert.AreEqual(true, p1==p2);

        }

        public bool PolinomNotEqualsTest(Polinom p1, Polinom p2)
        {
            return false;
        }


        }
}
