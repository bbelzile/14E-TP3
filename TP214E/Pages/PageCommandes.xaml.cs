using System;
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
        List<Recette> _recettesPossibles;
        public PageCommandes()
        {
            InitializeComponent();
            _maCommande = new Commande();
            _alimentDAL = new AlimentDAL();
            _recetteDAL = new RecetteDAL();
            _CommandeDAL = new CommandeDAL();
            RecuperationToutesRecettesPossibles();
            RemplirAffichageRecette(_recettesPossibles);
        }

        private void RecuperationToutesRecettesPossibles()
        {
            _recettesPossibles = new List<Recette>();
            List<Recette> recettesExistantes = _recetteDAL.RechercherToutesLesRecettes();
            Dictionary<string, int> alimentsDansInventaire = ListeAlimentsEnDictionnaire();

            bool alimentDisponible;
            foreach (Recette recette in recettesExistantes)
            {
                alimentDisponible = true;
                foreach (var aliment in recette.AlimentsQuantites)
                {
                    if (aliment.Value >= alimentsDansInventaire[aliment.Key])
                    {
                        alimentDisponible = false;
                    }

                }
                if (alimentDisponible)
                {
                    _recettesPossibles.Add(recette);
                }
            }
        }

        private Dictionary<string, int> ListeAlimentsEnDictionnaire()
        {
            List<Aliment> listeAliments = _alimentDAL.RechercherTousLesAliments();
            Dictionary<string, int> dictAliments = new Dictionary<string, int>();

            foreach (var aliment in listeAliments)
            {
                dictAliments.Add(aliment.Nom, aliment.Quantite);
            }
            return dictAliments;
        }

        private void RemplirAffichageRecette(List<Recette> pListeRecettesPossibles)
        {
            for (int i = 0; i < pListeRecettesPossibles.Count; i++)
            {
                Button nouveauBouton = CreerNouveauBouton(pListeRecettesPossibles[i]);
                if(i < 4)
                {
                    sp1.Children.Add(nouveauBouton);
                }
                else if(i < 8)
                {
                    sp2.Children.Add(nouveauBouton);
                }
                else if(i < 12)
                {
                    sp3.Children.Add(nouveauBouton);
                }
                else if(i < 16)
                {
                    sp4.Children.Add(nouveauBouton);
                }
            }
        }

        private Button CreerNouveauBouton(Recette pRecette)
        {
            Button nouveauBouton = new Button();

            nouveauBouton.Content = pRecette.Nom;
            nouveauBouton.Name = "Bouton" + pRecette.Nom;
            nouveauBouton.Margin = new Thickness(3);
            nouveauBouton.HorizontalAlignment = HorizontalAlignment.Stretch;
            nouveauBouton.Style = Resources["survolBoutonPrincipal"] as Style;
            nouveauBouton.Tag = pRecette;

            nouveauBouton.Click += (object sender, RoutedEventArgs e) =>
            {
                _maCommande.AjouterItemCommande(pRecette);
                RafraichirListBoxCommande();
            };

            return nouveauBouton;
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
                RafraichirListBoxCommande();
            }           
            
        }

        private void btnCommander_Click(object sender, RoutedEventArgs e)
        {
            if(_maCommande.Items.Count > 0)
            {
                _maCommande.DateHeure = DateTime.Now;
                _CommandeDAL.AjouterCommandeLog(_maCommande);

                RetirerAlimentsUtilisesInventaire();

                _maCommande = new Commande();
                RafraichirListBoxCommande();
            }           
        }

        private void RetirerAlimentsUtilisesInventaire()
        {
            List<Aliment> ListeTousLesAliments = _alimentDAL.RechercherTousLesAliments();
            foreach (Recette item in _maCommande.Items)
            {
                foreach (var aliment in item.AlimentsQuantites)
                {
                    Aliment alimentAModifier = ListeTousLesAliments.Find(x => x.Nom == aliment.Key);
                    alimentAModifier.Quantite -= aliment.Value;
                    _alimentDAL.ModifierAliment(alimentAModifier);
                }
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
    }
}
