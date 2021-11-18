using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class CommandeTests
    {
        [TestMethod()]
        public void VerifierPrixAjouterALaCommandeTest()
        {
            Recette recetteTest = new Recette("tomates en dés", new Dictionary<string, int> { { "tomate", 1 } }, 2);
            Commande maCommande = new Commande();
            Commande maDeuxiemeCommande = new Commande();

            maCommande.AjouterItemCommande(recetteTest);

            Assert.IsTrue(maCommande.Total > maDeuxiemeCommande.Total);
        }

        [TestMethod()]
        public void VerifierCompteAjouterALaCommandeTest()
        {
            Recette recetteTest = new Recette("tomates en dés", new Dictionary<string, int> { { "tomate", 1 } }, 2);
            Commande maCommande = new Commande();
            Commande maDeuxiemeCommande = new Commande();

            maCommande.AjouterItemCommande(recetteTest);

            Assert.IsTrue(maCommande.Items.Count > maDeuxiemeCommande.Items.Count);

        }

        [TestMethod()]
        public void VerifierPrixRetirerDeLaCommandeTest()
        {
            Recette recetteTest = new Recette("tomates en dés", new Dictionary<string, int> { { "tomate", 1 } }, 2);
            Commande maCommande = new Commande();
            Commande maDeuxiemeCommande = new Commande();

            maCommande.AjouterItemCommande(recetteTest);
            maCommande.RetirerItemCommande(recetteTest);

            Assert.IsTrue(maCommande.Total == maDeuxiemeCommande.Total);
        }

        [TestMethod()]
        public void VerifierCompteRetirerDeLaCommandeTest()
        {
            Recette recetteTest = new Recette("tomates en dés", new Dictionary<string, int> { { "tomate", 1 } }, 2);
            Commande maCommande = new Commande();
            Commande maDeuxiemeCommande = new Commande();
            maCommande.AjouterItemCommande(recetteTest);
            maCommande.RetirerItemCommande(recetteTest);

            Assert.IsTrue(maCommande.Items.Count == maDeuxiemeCommande.Items.Count);
        }

    }
}