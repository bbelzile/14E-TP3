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
            Dictionary<string, int> dictTest = new Dictionary<string, int> { { "tomates", 1 } };
            decimal prixTest = 1;

            Recette recetteTest = new Recette(nomTest, dictTest, prixTest);

            Assert.AreEqual(recetteTest.Nom, nomTest);
        }

        [TestMethod()]
        public void ConstructeurRecetteDictionnaireTest()
        {
            string nomTest = "tomates en dés";
            Dictionary<string, int> dictTest = new Dictionary<string, int> { { "tomates", 1 } };
            decimal prixTest = 1;

            Recette recetteTest = new Recette(nomTest, dictTest, prixTest);

            Assert.AreEqual(recetteTest.AlimentsQuantites, dictTest);
        }

        [TestMethod()]
        public void ConstructeurRecettePrixTest()
        {
            string nomTest = "tomates en dés";
            Dictionary<string, int> dictTest = new Dictionary<string, int> { { "tomates", 1 } };
            decimal prixTest = 1;

            Recette recetteTest = new Recette(nomTest, dictTest, prixTest);

            Assert.AreEqual(recetteTest.Prix, prixTest);
        }

    }
}