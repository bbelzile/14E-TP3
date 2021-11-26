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
    public class CommandeTests
    {
        private Mock<iRecette> recetteDeTest;
        private Commande commandeTest;

        private void InitialisterVariable()
        {
            recetteDeTest = new Mock<iRecette>();
            recetteDeTest.Setup(x => x.Prix).Returns(4);
            commandeTest = new Commande();
        }

        [TestMethod()]
        public void VerifierPrixAjouterALaCommandeTest()
        {
            InitialisterVariable();
            Commande maCommande = new Commande();

            maCommande.AjouterItemCommande(recetteDeTest.Object);

            Assert.IsTrue(maCommande.Total > commandeTest.Total);
        }

        [TestMethod()]
        public void VerifierCompteAjouterALaCommandeTest()
        {
            InitialisterVariable();
            Commande maCommande = new Commande();

            maCommande.AjouterItemCommande(recetteDeTest.Object);

            Assert.IsTrue(maCommande.Items.Count > commandeTest.Items.Count);

        }

        [TestMethod()]
        public void VerifierPrixRetirerDeLaCommandeTest()
        {
            InitialisterVariable();
            Commande maCommande = new Commande();

            maCommande.AjouterItemCommande(recetteDeTest.Object);
            maCommande.RetirerItemCommande(recetteDeTest.Object);

            Assert.IsTrue(maCommande.Total == commandeTest.Total);
        }

        [TestMethod()]
        public void VerifierCompteRetirerDeLaCommandeTest()
        {
            InitialisterVariable();
            Commande maCommande = new Commande();

            maCommande.AjouterItemCommande(recetteDeTest.Object);
            maCommande.RetirerItemCommande(recetteDeTest.Object);

            Assert.IsTrue(maCommande.Items.Count == commandeTest.Items.Count);
        }

    }
}