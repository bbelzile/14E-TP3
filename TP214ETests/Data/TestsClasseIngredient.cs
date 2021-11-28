using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TP214E.Data;

namespace TP214ETests.Data
{
    internal class TestsClasseIngredient
    {
        private const string chainDeTestTresLongue = "123456789101112131415161718192021" +
                "2223242526272829303132333435363738394041" +
                "424344454647484950515253545556575859606162636" +
                "46566676869707172737475767778798081828384858687" +
                "888990919293949596979899100";

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ nom est vide.")]
        public void VerifierValeurNomEchouSiValeurEstVide()
        {
            Ingredient ingredintTest = new Ingredient();
            string nom = "";

            ingredintTest.VerifierValeurNom(nom);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ nom doit contenir un maximum de 100 caractères.")]
        public void VerifierValeurNomEchouSiValeurTropLongue()
        {
            Ingredient ingredintTest = new Ingredient();

            ingredintTest.VerifierValeurNom(chainDeTestTresLongue);

        }

        [TestMethod()]
        public void VerifierValeurNomDonneValeurNomAObjetIngredient()
        {
            Ingredient ingredintTest = new Ingredient();
            string nom = "tomate";

            ingredintTest.VerifierValeurNom(nom);

            Assert.AreEqual(ingredintTest.Nom, nom);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ quantite est vide.")]
        public void VerifierValeurQuantiteEchouSiValeurEstVide()
        {
            Ingredient ingredintTest = new Ingredient();
            string nom = "";

            ingredintTest.VerifierValeurQuantite(nom);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ quantite doit contenir un maximum de 4 nombres.")]
        public void VerifierValeurQuantiteEchouSiValeurTropLongue()
        {
            Ingredient ingredintTest = new Ingredient();

            ingredintTest.VerifierValeurQuantite(chainDeTestTresLongue);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ quantite doit contenir que des chiffres.")]
        public void VerifierValeurQuantiteEchouSiValeurContientLettres()
        {
            Ingredient ingredintTest = new Ingredient();
            string quantiteContenantAutreChoseQueDesChiffre = "weqweqw";

            ingredintTest.VerifierValeurQuantite(quantiteContenantAutreChoseQueDesChiffre);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ quantite doit supérieur à 0.")]
        public void VerifierValeurQuantiteEchouSiValeurInferieurAZero()
        {
            Ingredient ingredintTest = new Ingredient();
            string quantiteInferieurAZero = "-1";

            ingredintTest.VerifierValeurQuantite(quantiteInferieurAZero);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ quantite doit supérieur à 0.")]
        public void VerifierValeurQuantiteEchouSiValeurEgaleAZero()
        {
            Ingredient ingredintTest = new Ingredient();
            string quantiteInferieurAZero = "0";

            ingredintTest.VerifierValeurQuantite(quantiteInferieurAZero);

        }

        [TestMethod()]
        public void VerifierValeurQuantiteDonneValeurQuantiteAObjetIngredient()
        {
            Ingredient ingredintTest = new Ingredient();
            string quantiteInferieurAZero = "5";

            ingredintTest.VerifierValeurQuantite(quantiteInferieurAZero);

        }

    }
}
