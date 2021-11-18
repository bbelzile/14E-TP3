using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TP214E.Data
{
    public class CommandeDAL : DAL
    {
        const string NOM_DE_LA_COLLECTION_COMMANDE = "Commandes";

        IMongoCollection<Commande> _collectionCommande;

        public CommandeDAL() : base()
        {
            IMongoDatabase db = mongoDBClient.GetDatabase(NOM_DE_LA_BD);
            _collectionCommande = db.GetCollection<Commande>(NOM_DE_LA_COLLECTION_COMMANDE);

        }

        public List<Commande> RechercherToutesLesCommandes()
        {
            var commandes = new List<Commande>();
            try
            {
                IMongoDatabase db = mongoDBClient.GetDatabase(NOM_DE_LA_BD);
                commandes = db.GetCollection<Commande>(NOM_DE_LA_COLLECTION_COMMANDE).Aggregate().ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return commandes;
        }

        public bool AjouterCommandeLog(Commande pCommandeAAjouter)
        {
            try
            {
                _collectionCommande.InsertOne(pCommandeAAjouter);
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
