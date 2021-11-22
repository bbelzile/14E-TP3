using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace TP214E.Data
{
    public class Recette
    {
        private ObjectId _id;
        private string _nom;
        private Dictionary<string, int> _alimentsQuantites;
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
        public Dictionary<string, int> AlimentsQuantites
        {
            get { return _alimentsQuantites; }
            set { _alimentsQuantites = value; }
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

        public Recette(ObjectId pId, string pNom, Dictionary<string, int> pDictAliments, decimal pPrix, Categories categorie)
        {
            Id = pId;
            Nom = pNom;
            AlimentsQuantites = pDictAliments;
            Prix = pPrix;
            Categorie = categorie;
        }

        public Recette(string pNom, Dictionary<string, int> pDictAliments, string pPrix, int pCategorie)
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
                _nom = valeurAVerifier;
            }
            else
            {
                throw new ArgumentException("Le champ nom est vide.");
            }
        }

        public void VerifierValeurAlimentsQuantites(Dictionary<string, int> alimentsQuantites)
        {
            if(alimentsQuantites.Count > 0)
            {
                if (VerifierSiQuatitesSontPositives(alimentsQuantites))
                {
                    _alimentsQuantites = alimentsQuantites;
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

        public static bool VerifierSiQuatitesSontPositives(Dictionary<string, int> alimentsQuantites)
        {
            bool aUneMauvaiseQuantite = true;

            foreach (KeyValuePair<string, int> unElementDuDictionnaire in alimentsQuantites)
            {
                if(unElementDuDictionnaire.Value <= 0)
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
                if (UtilitaireVerificationFormulaire.TextContienQueDesChiffre(valeurAVerifier))
                {
                    _prix = int.Parse(valeurAVerifier);
                }
                else
                {
                    throw new ArgumentException("Le champ prix doit comporter que des nombres.");
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

            } catch (Exception erreur)
            {
                throw new ArgumentException("La categorie sélectionnée est invalide.");
            }
            
        }
    }
}
