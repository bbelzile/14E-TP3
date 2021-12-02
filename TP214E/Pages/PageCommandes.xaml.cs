﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TP214E.Data;

namespace TP214E.Pages
{
    /// <summary>
    /// Interaction logic for PageCommandes.xaml
    /// </summary>
    public partial class PageCommandes : Page
    {
        AlimentDAL _alimentDAL;
        RecetteDAL _recetteDAL;
        CommandeDAL _CommandeDAL;
        Commande _maCommande;
        List<Recette> _listeToutesRecettes;
        List<Aliment> _listeAlimentsDispo;
        Dictionary<string, int> _dictAlimentsDispo;
        Dictionary<Recette, int> _recettesPossibles;

        public PageCommandes()
        {
            InitializeComponent();
            _maCommande = new Commande();
            _alimentDAL = new AlimentDAL();
            _recetteDAL = new RecetteDAL();
            _CommandeDAL = new CommandeDAL();
            _listeToutesRecettes = _recetteDAL.RechercherToutesLesRecettes();
            _listeAlimentsDispo = _alimentDAL.RechercherTousLesAliments();
            _dictAlimentsDispo = ListeAlimentsEnDictionnaire(_listeAlimentsDispo);
            _recettesPossibles = RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
            RemplirAffichageRecette(_recettesPossibles);
        }

        private Dictionary<string, int> ListeAlimentsEnDictionnaire(List<Aliment> pListeAlimentsDispo)
        {
            Dictionary<string, int> dictAlimentsDispo = new Dictionary<string, int>();
            foreach (var item in pListeAlimentsDispo)
            {
                dictAlimentsDispo.Add(item.Nom, item.Quantite);
            }

            return dictAlimentsDispo;
        }

        private Dictionary<Recette, int> RecupererLesRecettesPossibles(Dictionary<string, int> pAliments, List<Recette> pRecettes)
        {
            Dictionary<Recette, int> recettesPossibles = new Dictionary<Recette, int>();

            double cbRecetteFaisable;
            int qtMinimum;
            foreach (var recette in pRecettes)
            {
                qtMinimum = 10000;
                foreach(var ingredient in recette.Ingredients)
                {
                    cbRecetteFaisable = Math.Floor((double)(pAliments[ingredient.Nom] / ingredient.Quantite));
                    if(cbRecetteFaisable < qtMinimum)
                    {
                        qtMinimum = (int)cbRecetteFaisable;
                    }
                }

                recettesPossibles.Add(recette, qtMinimum);
            }

            return recettesPossibles;
        }

        private void RemplirAffichageRecette(Dictionary<Recette, int> pListeRecettesPossibles)
        {
            WP.Children.Clear();
            foreach (KeyValuePair<Recette, int> item in pListeRecettesPossibles)
            {
                Button nouveauBouton = CreerNouveauBouton(item.Key, item.Value);
                WP.Children.Add(nouveauBouton);
            }
        }

        private Button CreerNouveauBouton(Recette pRecette, int quantitePossible)
        {
            Button nouveauBouton = new Button();

            nouveauBouton.Content = pRecette.Nom;
            nouveauBouton.Name = "Bouton" + pRecette.Id;
            nouveauBouton.Margin = new Thickness(3);
            nouveauBouton.Height = 40;
            nouveauBouton.Width = 100;
            nouveauBouton.Tag = pRecette;
            if (quantitePossible == 0)
            {
                nouveauBouton.Background = Brushes.Red;
                nouveauBouton.IsEnabled = false;
                nouveauBouton.Visibility = Visibility.Collapsed;
            }
            else if (quantitePossible < 6)
            {
                nouveauBouton.Background = Brushes.Yellow;
            }
            else
            {
                nouveauBouton.Background = Brushes.Green;
            }

            nouveauBouton.Click += (object sender, RoutedEventArgs e) =>
            {
                RetirerQuantiteIngredient(pRecette);

                _recettesPossibles = RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
                RemplirAffichageRecette(_recettesPossibles);

                _maCommande.AjouterItemCommande(pRecette);
                RafraichirListBoxCommande();
            };

            return nouveauBouton;
        }

        private void RetirerQuantiteIngredient(Recette pRecette)
        {
            foreach (var ingredient in pRecette.Ingredients)
            {
                _dictAlimentsDispo[ingredient.Nom] -= ingredient.Quantite;
            }
        }

        private void AjouterQuantiteIngredient(Recette pRecette)
        {
            foreach (var ingredient in pRecette.Ingredients)
            {
                _dictAlimentsDispo[ingredient.Nom] += ingredient.Quantite;
            }
        }

        private void RafraichirListBoxCommande()
        {
            lbCommande.Items.Clear();
            foreach (var item in _maCommande.Items)
            {
                lbCommande.Items.Add(item.Nom);
            }

            lblTotal.Content = _maCommande.Total.ToString("0.00") + "$";

            if (lbCommande.Items.Count > 0)
            {
                lbCommande.SelectedIndex = 0;
            }
                

        }

        private void btnRetirer_Click(object sender, RoutedEventArgs e)
        {
            if (lbCommande.Items.Count > 0)
            {
                string itemARetirer = lbCommande.SelectedItem.ToString();
                Recette recetteARetirer = _maCommande.Items.Find(x => x.Nom == itemARetirer);
                _maCommande.RetirerItemCommande(recetteARetirer);
                AjouterQuantiteIngredient(recetteARetirer);
                _recettesPossibles = RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
                RemplirAffichageRecette(_recettesPossibles);
                RafraichirListBoxCommande();
            }           
            
        }

        private void btnCommander_Click(object sender, RoutedEventArgs e)
        {
            if(_maCommande.Items.Count > 0)
            {
                _maCommande.DateHeure = DateTime.Now;
                _CommandeDAL.AjouterCommandeLog(_maCommande);

                RafraichirListeAlimentsBD();

                _maCommande = new Commande();
                RafraichirListBoxCommande();
            }           
        }

        private void RafraichirListeAlimentsBD()
        {
            foreach (var aliment in _listeAlimentsDispo)
            {
                aliment.Quantite = _dictAlimentsDispo[aliment.Nom];
                _alimentDAL.ModifierAliment(aliment);
            }
        }

        private void btnHistorique_Click(object sender, RoutedEventArgs e)
        {
            PageHistoriqueCommande frmHistorique = new PageHistoriqueCommande(_CommandeDAL);
            this.NavigationService.Navigate(frmHistorique);
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void btnRecetteInd_Click(object sender, RoutedEventArgs e)
        {
            if (btnRecetteInd.Content == "Afficher les recettes non-disponibles")
            {
                foreach (Button bouton in WP.Children)
                {
                    bouton.Visibility = Visibility.Visible;
                }
                btnRecetteInd.Content = "Masquer les recettes non-disponibles";
            }
            else
            {
                _recettesPossibles = RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
                RemplirAffichageRecette(_recettesPossibles);

                btnRecetteInd.Content = "Afficher les recettes non-disponibles";
            }
            
        }
    }
}
