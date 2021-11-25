using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class RecetteTests
    {
        [TestMethod()]
        public void ConstructeurRecetteNomTest()
        {
            string nomTest = "tomates en dés";
            decimal prixTest = 1;
            Recette recetteTest = new Recette();

            recetteTest.Nom = nomTest;

            Assert.AreEqual(recetteTest.Nom, nomTest);
        }


    }
}