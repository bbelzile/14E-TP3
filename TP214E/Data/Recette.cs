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

        public Recette(ObjectId pId, string pNom, Dictionary<string, int> pDictAliments, decimal pPrix)
        {
            Id = pId;
            Nom = pNom;
            AlimentsQuantites = pDictAliments;
            Prix = pPrix;
        }

        public Recette(string pNom, Dictionary<string, int> pDictAliments, decimal pPrix)
        {
            Nom = pNom;
            AlimentsQuantites = pDictAliments;
            Prix = pPrix;
        }
    }
}
