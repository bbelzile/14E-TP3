using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class TestsUtilitaireVerificationFormulaire
    {

        [TestMethod()]
        public void SontDesChiffreTestRetournTrueSiEnvoie3()
        {
            bool resultat = UtilitaireVerificationFormulaire.TextContienQueDesChiffre("3");


            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void SontDesChiffreTestRetournTrueSiEnvoieLongNombre()
        {
            bool resultat = UtilitaireVerificationFormulaire.TextContienQueDesChiffre("312312312");

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void SontDesChiffreTestRetournFalseSiEnvoieDesLettres()
        {
            bool resultat = UtilitaireVerificationFormulaire.TextContienQueDesChiffre("asdasd");

            Assert.IsFalse(resultat);
        }

        [TestMethod()]
        public void VerificationSiPasTextVideRetournTrueSiEnvoieChaineNormale()
        {
            bool resultat = UtilitaireVerificationFormulaire.VerificationSiTextPasVide("asdasd");

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void VerificationSiTextPasVideRetournFalseSiEnvoieChaineVide()
        {
            bool resultat = UtilitaireVerificationFormulaire.VerificationSiTextPasVide("");

            Assert.IsFalse(resultat);
        }

        [TestMethod()]
        public void VerificationLongueurChaineRetourneFalseSiValeurTropLongue()
        {
            string chaineDePlusDe100Caractere = "123456789101112131415161718192021" +
                "2223242526272829303132333435363738394041" +
                "424344454647484950515253545556575859606162636" +
                "46566676869707172737475767778798081828384858687" +
                "888990919293949596979899100";

            bool resultat = UtilitaireVerificationFormulaire.VerificationLongueurChaine(chaineDePlusDe100Caractere);

            Assert.IsFalse(resultat);
        }

        [TestMethod()]
        public void VerificationLongueurChaineRetourneTrueSiValeurBonneLongueur()
        {
            string chaineDeMoinsDe100Caractere = "123456789100";

            bool resultat = UtilitaireVerificationFormulaire.VerificationLongueurChaine(chaineDeMoinsDe100Caractere);

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void VerificationLongueureNombreRetourneFalseSiValeurTropLongue()
        {
            string chaineDePlusDe4Caractere = "123412";

            bool resultat = UtilitaireVerificationFormulaire.VerificationLongueureNombre(chaineDePlusDe4Caractere);

            Assert.IsFalse(resultat);
        }

        [TestMethod()]
        public void VerificationLongueureNombreRetourneTrueSiValeurLongueureMaximal()
        {
            string chaineDe4Caractere = "1234";

            bool resultat = UtilitaireVerificationFormulaire.VerificationLongueureNombre(chaineDe4Caractere);

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void VerificationLongueureNombreRetourneTrueSiValeurBonneLongueur()
        {
            string chaineDeMoinsDe4Caractere = "12";

            bool resultat = UtilitaireVerificationFormulaire.VerificationLongueureNombre(chaineDeMoinsDe4Caractere);

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void VerificationNombreEstPositifRetourneFalseSiValeurNegative()
        {
            int entierNegatif = -1;

            bool resultat = UtilitaireVerificationFormulaire.VerificationNombreEstPositif(entierNegatif);

            Assert.IsFalse(resultat);
        }

        [TestMethod()]
        public void VerificationNombreEstPositifRetourneFalseSiValeurEgalAZero()
        {
            int entierEgalAZero = 0;

            bool resultat = UtilitaireVerificationFormulaire.VerificationNombreEstPositif(entierEgalAZero);

            Assert.IsFalse(resultat);
        }

        [TestMethod()]
        public void VerificationNombreEstPositifRetourneTrueSiValeurPositive()
        {
            int entierPositif = 5;

            bool resultat = UtilitaireVerificationFormulaire.VerificationNombreEstPositif(entierPositif);

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void VerificationValeurEstDansEnumCategorieRetourneTrueSiValeurEstDansEnumCategorie()
        {
            int entierPositif = 3;

            bool resultat = UtilitaireVerificationFormulaire.VerificationValeurEstDansEnumCategorie(entierPositif);

            Assert.IsTrue(resultat);
        }

        [TestMethod()]
        public void VerificationValeurEstDansEnumCategorieRetourneFalseSiValeurEstPasDansEnumCategorie()
        {
            int entierPositif = -1;

            bool resultat = UtilitaireVerificationFormulaire.VerificationValeurEstDansEnumCategorie(entierPositif);

            Assert.IsFalse(resultat);
        }
    }
}