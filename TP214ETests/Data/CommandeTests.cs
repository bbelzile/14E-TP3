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
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.Add(new Ingredient("tomate", 1));
            Recette recetteTest = new Recette("tomates en dés", ingredients, "2",1);
            Commande maCommande = new Commande();
            Commande maDeuxiemeCommande = new Commande();

            maCommande.AjouterItemCommande(recetteTest);

            Assert.IsTrue(maCommande.Total > maDeuxiemeCommande.Total);
        }

        [TestMethod()]
        public void VerifierCompteAjouterALaCommandeTest()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.Add(new Ingredient("tomate", 1));
            Recette recetteTest = new Recette("tomates en dés", ingredients, "2", 1);
            Commande maCommande = new Commande();
            Commande maDeuxiemeCommande = new Commande();

            maCommande.AjouterItemCommande(recetteTest);

            Assert.IsTrue(maCommande.Items.Count > maDeuxiemeCommande.Items.Count);

        }

        [TestMethod()]
        public void VerifierPrixRetirerDeLaCommandeTest()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.Add(new Ingredient("tomate", 1));
            Recette recetteTest = new Recette("tomates en dés", ingredients, "2", 1);
            Commande maCommande = new Commande();
            Commande maDeuxiemeCommande = new Commande();

            maCommande.AjouterItemCommande(recetteTest);
            maCommande.RetirerItemCommande(recetteTest);

            Assert.IsTrue(maCommande.Total == maDeuxiemeCommande.Total);
        }

        [TestMethod()]
        public void VerifierCompteRetirerDeLaCommandeTest()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.Add(new Ingredient("tomate", 1));
            Recette recetteTest = new Recette("tomates en dés", ingredients, "2", 1);
            Commande maCommande = new Commande();
            Commande maDeuxiemeCommande = new Commande();
            maCommande.AjouterItemCommande(recetteTest);
            maCommande.RetirerItemCommande(recetteTest);

            Assert.IsTrue(maCommande.Items.Count == maDeuxiemeCommande.Items.Count);
        }

    }
}