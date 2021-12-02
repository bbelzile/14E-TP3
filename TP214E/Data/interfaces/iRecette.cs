using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP214E.Data.interfaces
{
    public interface iRecette
    {
      

        public ObjectId Id
        {
            get;
            set;
        }

        public string Nom
        {
            get;
            set;
        }
        public List<Ingredient> Ingredients
        {
            get;
            set;
        }
        public decimal Prix
        {
            get;
            set;
        }

        public Categories Categorie
        {
            get;
            set;
        }

        public void VerifierValeurNom(string valeurAVerifier);
        

        public void VerifierValeurAlimentsQuantites(List<Ingredient> ingredients);
        

        public void VerifierValeurPrix(string valeurAVerifier);


        public void VerifierValeurCategorie(int valeurAVerifier);
        
    
}
}
