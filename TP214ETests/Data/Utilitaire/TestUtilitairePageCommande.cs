using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data.Utilitaire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;

namespace TP214E.Data.Utilitaire.Tests
{
    [TestClass()]
    public class TestUtilitairePageCommande
    {
        List<Recette> listeRecettesTest = new List<Recette>();
        List<Aliment> listeAlimentTest = new List<Aliment>();

        Dictionary<string,int> dictAlimentsTest = new Dictionary<string,int>();
        Dictionary<Recette,int> dictRecetteTest = new Dictionary<Recette,int>();

        public void InitialiserVariable()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            List<Ingredient> ingredients2 = new List<Ingredient>();

            Ingredient ingredientTest = new Ingredient("tomate", 1);
            Ingredient ingredientTest2 = new Ingredient("patate", 1);

            ingredients.Add(ingredientTest);
            ingredients2.Add(ingredientTest2);

            Recette recetteTest1 = new Recette("tomate en des",ingredients,"3",2);
            Recette recetteTest2 = new Recette("salade", ingredients, "3", 3);
            Recette recetteTest3 = new Recette("salade gourmande", ingredients2, "3", 3);

            listeRecettesTest.Add(recetteTest1);
            listeRecettesTest.Add(recetteTest2);
            listeRecettesTest.Add(recetteTest3);

            dictRecetteTest.Add(recetteTest1, 1);
            dictRecetteTest.Add(recetteTest2, 3);
            dictRecetteTest.Add(recetteTest3, 2);

            dictAlimentsTest.Add("tomate",1);
            dictAlimentsTest.Add("patate", 0);

            Aliment alimentTest1 = new Aliment();
            alimentTest1.Nom = "tomate";
            alimentTest1.Quantite = 1;

            Aliment alimentTest2 = new Aliment();
            alimentTest2.Nom = "patate";
            alimentTest2.Quantite = 1;

            listeAlimentTest.Add(alimentTest1);
            listeAlimentTest.Add(alimentTest2);
        }

        [TestMethod()]
        public void TrierRecettesParNomAppelLeDAL()
        {
            InitialiserVariable();  
            Mock<IRecetteDAL> RecetteDALMock = new Mock<IRecetteDAL>();
            RecetteDALMock.Setup(x => x.RechercherTLesRecettesParNom()).Returns(listeRecettesTest);
            Dictionary<Recette,int> recettePossible = new Dictionary<Recette,int>();

            recettePossible = UtilitairePageCommande.TrierRecettesParNom(dictAlimentsTest,RecetteDALMock.Object);

            Assert.AreEqual(recettePossible.Count, 3);
        }

        [TestMethod()]
        public void TrierRecettesParCategorieRetournUneRecetteCarUneSeulRecetteAvecCategorie2()
        {
            InitialiserVariable();
            Dictionary<Recette, int> recettePossible = new Dictionary<Recette, int>();

            recettePossible = UtilitairePageCommande.TrierRecettesParCategorie(dictRecetteTest,2);
            
            Assert.AreEqual(recettePossible.Count, 1);
        }

        [TestMethod()]
        public void TrierRecettesParCategorieRetournDeuxRecetteCarDeuxRecetteAvecCategorie3()
        {
            InitialiserVariable();
            Dictionary<Recette, int> recettePossible = new Dictionary<Recette, int>();

            recettePossible = UtilitairePageCommande.TrierRecettesParCategorie(dictRecetteTest, 3);

            Assert.AreEqual(recettePossible.Count, 2);
        }

        [TestMethod()]
        public void TrierRecettesParCategorieRetournToutesLesRecettesCarMauvaiseCategorieEnParametre()
        {
            InitialiserVariable();
            Dictionary<Recette, int> recettePossible = new Dictionary<Recette, int>();

            recettePossible = UtilitairePageCommande.TrierRecettesParCategorie(dictRecetteTest, -1);

            Assert.AreEqual(recettePossible.Count, 3);
        }

        [TestMethod()]
        public void TrierRecetteParQuantitePremiereRecetteEstSalade()
        {
            InitialiserVariable();
            Dictionary<Recette, int> recettePossible = new Dictionary<Recette, int>();

            recettePossible = UtilitairePageCommande.TrierRecetteParQuantite(dictRecetteTest);

            Assert.AreEqual(recettePossible.First().Key.Nom, "salade");
        }

        [TestMethod()]
        public void TrierRecetteParQuantiteDerniereRecetteEstTomateEnDes()
        {
            InitialiserVariable();
            Dictionary<Recette, int> recettePossible = new Dictionary<Recette, int>();

            recettePossible = UtilitairePageCommande.TrierRecetteParQuantite(dictRecetteTest);

            Assert.AreEqual(recettePossible.Last().Key.Nom, "tomate en des");
        }

        [TestMethod()]
        public void RecupererLesRecettesPossiblesDonneZeroALaRecetteDemandantDesPatates()
        {
            InitialiserVariable();
            Dictionary<Recette, int> recettePossible = new Dictionary<Recette, int>();

            recettePossible = UtilitairePageCommande.RecupererLesRecettesPossibles(dictAlimentsTest, listeRecettesTest);

            Assert.AreEqual(recettePossible.Last().Value, 0);
        }

        [TestMethod()]
        public void RecupererLesRecettesPossiblesDonneUnALaRecetteDemandantDesTomates()
        {
            InitialiserVariable();
            Dictionary<Recette, int> recettePossible = new Dictionary<Recette, int>();

            recettePossible = UtilitairePageCommande.RecupererLesRecettesPossibles(dictAlimentsTest, listeRecettesTest);

            Assert.AreEqual(recettePossible.First().Value, 1);
        }

        [TestMethod()]
        public void ListeAlimentsEnDictionnaireTest()
        {
            InitialiserVariable();
            Dictionary<string, int> aliments = new Dictionary<string, int>();

            aliments = UtilitairePageCommande.ListeAlimentsEnDictionnaire(listeAlimentTest);

            Assert.AreEqual(aliments.Count,2);
        }
    }
}