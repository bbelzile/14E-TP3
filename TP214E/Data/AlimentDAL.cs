using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Windows;

namespace TP214E.Data
{
    public class AlimentDAL : DAL
    {
        const string NOM_DE_LA_COLLECTION_ALIMENT = "Aliments";

        IMongoCollection<Aliment> _collectionAliment;

        public AlimentDAL() : base()
        {
            IMongoDatabase db = mongoDBClient.GetDatabase(NOM_DE_LA_BD);
            _collectionAliment = db.GetCollection<Aliment>(NOM_DE_LA_COLLECTION_ALIMENT);

        }

        public List<Aliment> RechercherTousLesAliments()
        {
            var aliments = new List<Aliment>();
            try
            {
                IMongoDatabase db = mongoDBClient.GetDatabase(NOM_DE_LA_BD);
                aliments = db.GetCollection<Aliment>(NOM_DE_LA_COLLECTION_ALIMENT).Aggregate().ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return aliments;
        }

        public bool CreerAliment(Aliment alimentACreer)
        {
            try
            {
                _collectionAliment.InsertOne(alimentACreer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public bool ModifierAliment(Aliment alimentAModifier)
        {
            try
            {
                var filtre = Builders<Aliment>.Filter.Eq(s => s.Id, alimentAModifier.Id);
                _collectionAliment.ReplaceOne(filtre, alimentAModifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public bool SupprimerAliment(Aliment alimentASupprimer)
        {
            try
            {
                var filtre = Builders<Aliment>.Filter.Eq(s => s.Id, alimentASupprimer.Id);
                _collectionAliment.DeleteOne(filtre);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
