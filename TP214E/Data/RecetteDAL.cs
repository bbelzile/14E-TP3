using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TP214E.Data
{
    public class RecetteDAL : DAL
    {
        const string NOM_DE_LA_COLLECTION_RECETTE = "Recettes";

        IMongoCollection<Recette> _collectionRecette;

        public RecetteDAL() : base()
        {
            IMongoDatabase db = mongoDBClient.GetDatabase(NOM_DE_LA_BD);
            _collectionRecette = db.GetCollection<Recette>(NOM_DE_LA_COLLECTION_RECETTE);

        }

        public List<Recette> RechercherToutesLesRecettes()
        {
            var recettes = new List<Recette>();
            try
            {
                IMongoDatabase db = mongoDBClient.GetDatabase(NOM_DE_LA_BD);
                recettes = db.GetCollection<Recette>(NOM_DE_LA_COLLECTION_RECETTE).Aggregate().ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return recettes;
        }
    }
}
