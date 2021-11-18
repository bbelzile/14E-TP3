using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace TP214E.Data
{
    public class DAL
    {
        
        private const string URL_DE_LA_BD = "mongodb://localhost:27017/TP2DB";
        public const string NOM_DE_LA_BD = "TP2DB";

        public MongoClient mongoDBClient;

        public DAL()
        {
            mongoDBClient = OuvrirConnexion();
        }

        private MongoClient OuvrirConnexion()
        {
            MongoClient dbClient = null;
            try{
                dbClient = new MongoClient(URL_DE_LA_BD);
            }catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return dbClient;
        }

    }
}
