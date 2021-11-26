using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class RecetteTests
    {
        private Recette recetteDeTest;
        private const string chainDeTesteTresLongue = "12345678910111213141516171819" +
            "20212223242526272829303132333435363738394041424344454647484950515253545" +
            "5565758596061626364656667686970";

        private void InitialisterVariable(int quantiteIngredientTest = 1)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            Mock<IIngredient> ingredientTest = new Mock<IIngredient>();
            ingredientTest.Setup(x => x.Quantite).Returns(quantiteIngredientTest);
            ingredients.Add((Ingredient)ingredientTest.Object);
            recetteDeTest = new Recette("tomates en dés",ingredients,"2",1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ nom est vide.")]
        public void VerifierValeurNomLanceErreurCarChaineVide()
        {
            InitialisterVariable();

            recetteDeTest.VerifierValeurNom("");

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ nom doit contenir un maximum de 100 caractère.")]
        public void VerifierValeurNomLanceErreurCarChaineTropLongue()
        {
            InitialisterVariable();

            recetteDeTest.VerifierValeurNom(chainDeTesteTresLongue);

        }

        [TestMethod()]
        public void VerifierValeurNomDonneValeurARecette()
        {
            InitialisterVariable();
            string nomRecette = "nomRecette";

            recetteDeTest.VerifierValeurNom(nomRecette);

            Assert.AreEqual(recetteDeTest.Nom, nomRecette);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Vous devez ajouter au moins un aliment à la recette.")]
        
        public void VerifierValeurAlimentLanceErreurCarAucuneValeurDansListe()
        {
            InitialisterVariable();
            List< Ingredient > listeVide = new List<Ingredient>();

            recetteDeTest.VerifierValeurAlimentsQuantites(listeVide);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "L'une des quantité entrée est négative ou égale à zero.")]

        public void VerifierValeurAlimentLanceErreurCarQuantiteNegativeDansListe()
        {
            InitialisterVariable(-1);
            List<Ingredient> listeVide = new List<Ingredient>();

            recetteDeTest.VerifierValeurAlimentsQuantites(listeVide);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "L'une des quantité entrée est négative ou égale à zero.")]

        public void VerifierValeurAlimentLanceErreurCarQuantiteEgaleAZeroDansListe()
        {
            InitialisterVariable(0);
            List<Ingredient> listeVide = new List<Ingredient>();

            recetteDeTest.VerifierValeurAlimentsQuantites(listeVide);

        }

        [TestMethod()]
        public void VerifierValeurAlimentDonneValeurARecette()
        {
            InitialisterVariable();
            List<Ingredient> listeVide = new List<Ingredient>();

            recetteDeTest.VerifierValeurAlimentsQuantites(listeVide);

            Assert.AreEqual(recetteDeTest.Ingredients.Count,1);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ prix est vide.")]

        public void VerifierValeurPrixLanceErreurCarValeurVide()
        {
            InitialisterVariable();


            recetteDeTest.VerifierValeurPrix("");

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ prix doit contenir un maximum de 4 caractère.")]

        public void VerifierValeurPrixLanceErreurCarValeurTrogLongue()
        {
            InitialisterVariable();


            recetteDeTest.VerifierValeurPrix("1234567");

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Le champ prix doit comporter que des nombres.")]

        public void VerifierValeurPrixLanceErreurCarValeurContientPasJusteDesChiffres()
        {
            InitialisterVariable();


            recetteDeTest.VerifierValeurPrix("1#3d56b");

        }

        [TestMethod()]
        public void VerifierValeurPrixDonneValeurARecette()
        {
            InitialisterVariable();

            recetteDeTest.VerifierValeurPrix("44");

            Assert.AreEqual(recetteDeTest.Prix, 44);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "La categorie sélectionnée est invalide.")]

        public void VerifierValeurCategorieLanceErreurCarValeurCategoriePasDansEnumCategories()
        {
            InitialisterVariable();

            recetteDeTest.VerifierValeurCategorie(100);

        }

        [TestMethod()]

        public void VerifierValeurCategorieDonneValeurARecette()
        {
            InitialisterVariable();

            recetteDeTest.VerifierValeurCategorie(1);

            Assert.AreEqual(recetteDeTest.Categorie, Categories.vegetalien);

        }
    }
}