using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using TP214E.Data;
using Moq;

namespace TP214E.Pages.Tests
{
    [TestClass()]
    public class PageAlimentTests
    {
        [TestMethod()]
        public void VerificationCasEnvoieRetourneTrueSiAlimentNull()
        {
           
            bool resultat = PageAliment.VerificationCasEnvoie(null);

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void VerificationCasEnvoieRetourneFalseSiAlimentNotNull()
        {
            Aliment aliment = new Aliment();

            bool resultat = PageAliment.VerificationCasEnvoie(aliment);

            Assert.IsFalse(resultat);
        }
    }
}