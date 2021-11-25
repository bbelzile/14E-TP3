using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class UtilitaireVerificationFormulaireTests
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
    }
}