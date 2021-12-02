using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using TP214E.Data.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class TestsClasseCommande
    {
        private Recette recetteDeTest;
        private Commande commandeTest;

        private void InitialisterVariable()
        {
            recetteDeTest = new Recette();
            recetteDeTest.Prix = 3;
            commandeTest = new Commande();
        }

        [TestMethod()]
        public void VerifierPrixAjouterALaCommandeTest()
        {
            InitialisterVariable();
            decimal totalAvantAjoutRecette = commandeTest.Total;

            commandeTest.AjouterItemCommande(recetteDeTest);

            Assert.IsTrue(commandeTest.Total > totalAvantAjoutRecette);
        }

        [TestMethod()]
        public void VerifierCompteAjouterALaCommandeTest()
        {
            InitialisterVariable();
            int quantiteRecetteAvantAjoutRecette = commandeTest.Items.Count;

            commandeTest.AjouterItemCommande(recetteDeTest);

            Assert.IsTrue(commandeTest.Items.Count > quantiteRecetteAvantAjoutRecette);

        }

        [TestMethod()]
        public void VerifierPrixRetirerDeLaCommandeTest()
        {
            InitialisterVariable();
            decimal totalAvantAjoutRecette = commandeTest.Total;

            commandeTest.AjouterItemCommande(recetteDeTest);
            commandeTest.RetirerItemCommande(recetteDeTest);

            Assert.IsTrue(commandeTest.Total == totalAvantAjoutRecette);
        }

        [TestMethod()]
        public void VerifierCompteRetirerDeLaCommandeTest()
        {
            InitialisterVariable();

            commandeTest.AjouterItemCommande(recetteDeTest);
            commandeTest.RetirerItemCommande(recetteDeTest);

            Assert.IsTrue(commandeTest.Items.Count == 0);
        }

    }
}