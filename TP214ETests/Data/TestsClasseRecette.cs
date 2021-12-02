using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class TestsClasseRecette
    {
        private Recette recetteDeTest;
        private const string chainDeTestTresLongue = "123456789101112131415161718192021" +
                "2223242526272829303132333435363738394041" +
                "424344454647484950515253545556575859606162636" +
                "46566676869707172737475767778798081828384858687" +
                "888990919293949596979899100";

        private void InitialisterVariable(int quantiteIngredientTest = 1)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            Ingredient ingredientTest = new Ingredient("tomate",1);

            ingredients.Add(ingredientTest);

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

            recetteDeTest.VerifierValeurNom(chainDeTestTresLongue);

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
            Ingredient nouvelIngredientTest = new Ingredient("tomate",1);
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.Add(nouvelIngredientTest);
            ingredients.Add(nouvelIngredientTest);

            recetteDeTest.VerifierValeurAlimentsQuantites(ingredients);

            Assert.AreEqual(recetteDeTest.Ingredients.Count,2);

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