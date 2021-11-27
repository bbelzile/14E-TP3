using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using TP214E.Pages;
using TP214E.Data.interfaces;

namespace TP214E.Data
{
    public class Recette: iRecette
    {
        private ObjectId _id;
        private string _nom;
        private List<Ingredient> _ingredients;
        private decimal _prix;
        private Categories _categorie;

        public ObjectId Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; }
        }
        public decimal Prix
        {
            get { return _prix; }
            set { _prix = value; }
        }

        public Categories Categorie
        {
            get { return _categorie; }
            set { _categorie = value; }
        }

        List<Ingredient> iRecette.Ingredients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Recette() { }
        public Recette(ObjectId pId, string pNom, List<Ingredient> pDictAliments, decimal pPrix, Categories categorie)
        {
            Id = pId;
            Nom = pNom;
            Ingredients = pDictAliments;
            Prix = pPrix;
            Categorie = categorie;
        }

        public Recette(string pNom, List<Ingredient> pDictAliments, string pPrix, int pCategorie)
        {
            VerifierValeurNom(pNom);
            VerifierValeurAlimentsQuantites(pDictAliments);
            VerifierValeurPrix(pPrix);
            VerifierValeurCategorie(pCategorie);
        }


        public void VerifierValeurNom(string valeurAVerifier)
        {
            if (UtilitaireVerificationFormulaire.VerificationSiTextPasVide(valeurAVerifier))
            {
                if (UtilitaireVerificationFormulaire.VerificationLongueurChaine(valeurAVerifier))
                {
                    _nom = valeurAVerifier;
                }
                else
                {
                    throw new ArgumentException("Le champ nom doit contenir un maximum de " + UtilitaireVerificationFormulaire.LONGUEUR_MAXIMUM_CHAINE +
                        " caractère.");
                }
            }
            else
            {
                throw new ArgumentException("Le champ nom est vide.");
            }
        }

        public void VerifierValeurAlimentsQuantites(List<Ingredient> ingredients)
        {
            if(UtilitaireVerificationFormulaire.VerificationNombreEstPositif(ingredients.Count))
            {
                if (VerifierSiQuatitesSontPositives(ingredients))
                {
                    _ingredients = ingredients;
                    
                }
                else
                {
                    throw new ArgumentException("L'une des quantité entrée est négative ou égale à zero.");
                }
            }
            else
            {
                throw new ArgumentException("Vous devez ajouter au moins un aliment à la recette.");
            }
        }

        private bool VerifierSiQuatitesSontPositives(List<Ingredient> alimentsQuantites)
        {
            bool aUneMauvaiseQuantite = true;

            foreach (Ingredient unIngredient in alimentsQuantites)
            {
                if(!UtilitaireVerificationFormulaire.VerificationNombreEstPositif(unIngredient.Quantite))
                {
                    aUneMauvaiseQuantite = false;
                    break;
                }
            }

            return aUneMauvaiseQuantite;

        }

        public void VerifierValeurPrix(string valeurAVerifier)
        {
            if (UtilitaireVerificationFormulaire.VerificationSiTextPasVide(valeurAVerifier))
            {
                if (UtilitaireVerificationFormulaire.VerificationLongueureNombre(valeurAVerifier))
                {
                    try
                    {
                        _prix = decimal.Parse(valeurAVerifier);
                    }
                    catch
                    {
                        throw new ArgumentException("Le champ prix doit comporter que des nombres.");

                    }
                }
                else
                {
                    throw new ArgumentException("Le champ prix doit contenir un maximum de " + UtilitaireVerificationFormulaire.LONGUEUR_MAXIMUM_NOMBRE +
                        " caractère.");
                }
            
            }
            else
            {
                throw new ArgumentException("Le champ prix est vide.");
            }
        }

        public void VerifierValeurCategorie(int valeurAVerifier)
        {
            try
            {
                Categories categorie;
                categorie = (Categories) valeurAVerifier;

                _categorie = categorie;

            } catch
            {
                throw new ArgumentException("La categorie sélectionnée est invalide.");
            }
            
        }

    }
}
