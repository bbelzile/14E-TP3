using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using TP214E.Data;

namespace TP214E.Pages.Tests
{
    [TestClass()]
    public class TestsClasseAliment
    {

        [TestMethod()]
        public void SontDesChiffreTestRetournTrueSiEnvoie3()
        {
            bool resultat = Aliment.TextContienQueDesChiffre("3");


            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void SontDesChiffreTestRetournTrueSiEnvoieLongNombre()
        {
            bool resultat = Aliment.TextContienQueDesChiffre("312312312");

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void SontDesChiffreTestRetournFalseSiEnvoieDesLettres()
        {
            bool resultat = Aliment.TextContienQueDesChiffre("asdasd");

            Assert.IsFalse(resultat);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ date est vide.")]
        public void VerificationValeurDateEchouSiValeurChampVide()
        {
            Aliment aliment = new Aliment();
            string date = "";

            aliment.VerificationValeurDate(date);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "La date est invalide.")]
        public void VerificationValeurDateEchouSiValeurEstDansLePassse()
        {
            Aliment aliment = new Aliment();
            string date = "10-12-2021";

            aliment.VerificationValeurDate(date);

        }

        [TestMethod()]
        public void VerificationValeurDateAttribueValeurAObjetAliment()
        {
            Aliment aliment = new Aliment();
            string date = "10-12-2030";

            aliment.VerificationValeurDate(date);

            Assert.AreEqual(aliment.ExpireLe,DateTime.Parse(date));

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ quantite est vide.")]
        public void VerificationValeurQuantiteEchouSiValeurEstVide()
        {
            Aliment aliment = new Aliment();
            string quantite = "";

            aliment.VerificationValeurQuantite(quantite);
            
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ quantite doit comporter que des nombres.")]
        public void VerificationValeurQuantiteEchouSiValeurContienDesLettre()
        {
            Aliment aliment = new Aliment();
            string quantite = "sdfsdf";

            aliment.VerificationValeurQuantite(quantite);

        }

        [TestMethod()]
        public void VerificationValeurQuantiteEAttribueValeurAObjetAliment()
        {
            Aliment aliment = new Aliment();
            string quantite = "123";

            aliment.VerificationValeurQuantite(quantite);

            Assert.AreEqual(123, aliment.Quantite);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ nom est vide.")]
        public void VerificationValeurNomEchouSiValeurEstVide()
        {
            Aliment aliment = new Aliment();
            string nom = "";

            aliment.VerificationValeurNom(nom);

        }

        [TestMethod()]
        public void VerificationValeurNomAttribueValeurAObjetAliment()
        {
            Aliment aliment = new Aliment();
            string nom = "Patate";

            aliment.VerificationValeurNom(nom);

            Assert.AreEqual(nom, aliment.Nom);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ unite est vide.")]
        public void VerificationValeurUniteEchouSiValeurEstVide()
        {
            Aliment aliment = new Aliment();
            string unite = "";

            aliment.VerificationValeurUnite(unite);

        }

        [TestMethod()]
        public void VerificationValeurUniteAttribueValeurAObjetAliment()
        {
            Aliment aliment = new Aliment();
            string unite = "boite";

            aliment.VerificationValeurUnite(unite);

            Assert.AreEqual(unite, aliment.Unite);

        }

        [TestMethod()]
        public void VerificationSiPasTextVideRetournTrueSiEnvoieChaineNormale()
        {
            bool resultat = Aliment.VerificationSiTextPasVide("asdasd");

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void VerificationSiTextPasVideRetournFalseSiEnvoieChaineVide()
        {
            bool resultat = Aliment.VerificationSiTextPasVide("");

            Assert.IsFalse(resultat);
        }

    }
}