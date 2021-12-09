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
using System.Linq;
using TP214E.Data.Utilitaire;

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
        bool _afficher = false;

        public PageCommandes()
        {
            InitializeComponent();

            _maCommande = new Commande();
            _alimentDAL = new AlimentDAL();
            _recetteDAL = new RecetteDAL();
            _CommandeDAL = new CommandeDAL();

            ChargerRecettes();
            ChargerCategories();

            optAucunFiltre.IsChecked = true;
        }

        public void ChargerCategories()
        {
            InstancierCheminValeurCboCategorie();

            foreach (int valeurCategorie in Enum.GetValues(typeof(Categories)))
            {
                int valeurDeLaCategorie = valeurCategorie;
                string nomDeLaCategorie = Enum.GetName(typeof(Categories), valeurCategorie);

                KeyValuePair<int, string> clefValeurCategorie =
                    new KeyValuePair<int, string>(valeurDeLaCategorie, nomDeLaCategorie);

                this.cboCategorie.Items.Add(clefValeurCategorie);
            }

            KeyValuePair<int, string> clefValeurParDefaut =
                    new KeyValuePair<int, string>(-1, "Aucune Categorie");

            this.cboCategorie.Items.Add(clefValeurParDefaut);

            this.cboCategorie.SelectedItem = clefValeurParDefaut;
        }

        public void InstancierCheminValeurCboCategorie()
        {
            this.cboCategorie.SelectedValuePath = "Key";
            this.cboCategorie.DisplayMemberPath = "Value";
        }

        public void ChargerRecettes()
        {
            _listeToutesRecettes = _recetteDAL.RechercherToutesLesRecettes();
            _listeAlimentsDispo = _alimentDAL.RechercherTousLesAliments();
            _dictAlimentsDispo = UtilitairePageCommande.ListeAlimentsEnDictionnaire(_listeAlimentsDispo);
            _recettesPossibles = UtilitairePageCommande.RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
            RemplirAffichageRecette(_recettesPossibles);
            RafraichirComboBoxMulti();
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
                nouveauBouton.Style = (Style) FindResource("survolBoutonPrincipalRouge");
                nouveauBouton.IsEnabled = false;
                nouveauBouton.Visibility = Visibility.Collapsed;
            }
            else if (quantitePossible < 6)
            {
                nouveauBouton.Style = (Style)FindResource("survolBoutonPrincipalJaune");
            }
            else
            {
                nouveauBouton.Style = (Style)FindResource("survolBoutonPrincipal");
            }

            nouveauBouton.Click += (object sender, RoutedEventArgs e) =>
            {
                RetirerQuantiteIngredient(pRecette);

                _recettesPossibles = UtilitairePageCommande.RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
                RemplirAffichageRecette(_recettesPossibles);

                _maCommande.AjouterItemCommande(pRecette);
                RafraichirListBoxCommande();
                RafraichirComboBoxMulti();


                if (_afficher)
                {
                    AfficherRecettesIndispo();
                }
            };

            return nouveauBouton;
        }

        private void RafraichirComboBoxMulti()
        {
            cbbMulti.Items.Clear();
            if(_maCommande.Items.Count == 0)
            {
                cbbMulti.Items.Add("La commande est vide");
                btnMulti.IsEnabled = false;
                cbbMulti.SelectedIndex = 0;
            }
            else
            {
                List<int> listePossibliteMultiplication = TrouverPossibiliteMultiplication();
                if (listePossibliteMultiplication.Count > 0)
                {
                    foreach (int possibilite in listePossibliteMultiplication)
                    {
                        cbbMulti.Items.Add(possibilite);
                    }
                    btnMulti.IsEnabled = true;
                    cbbMulti.SelectedIndex = 0;
                }
                else
                {
                    cbbMulti.Items.Add("La commande est vide");
                    btnMulti.IsEnabled = false;
                    cbbMulti.SelectedIndex = 0;
                }
                
            }
        }

        private List<int> TrouverPossibiliteMultiplication()
        {
            List<int> listePossibiliteMultiplication = new List<int>();
            Dictionary<Recette, int> itemsCommande = GrouperItemsCommande(_maCommande);
            int min = 10;
            foreach (var paire in _recettesPossibles)
            {
                if (_maCommande.Items.Contains(paire.Key))
                {
                    int possibilite = (itemsCommande[paire.Key]+paire.Value)/itemsCommande[paire.Key];
                    if (possibilite < min)
                    {
                        min = possibilite;
                    }
                }
                
            }
            for (int i = 2; i <= min; i++)
            {
                listePossibiliteMultiplication.Add(i);
            }

            return listePossibiliteMultiplication;
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
                _recettesPossibles = UtilitairePageCommande.RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
                RemplirAffichageRecette(_recettesPossibles);
                RafraichirListBoxCommande();
                RafraichirComboBoxMulti();
                RafraichirComboBoxMulti();
            }           
            
        }

        private void TrierRecette(object sender, RoutedEventArgs e)
        {
            Dictionary<Recette, int> recetteTriees = new Dictionary<Recette, int>();

            _recettesPossibles = UtilitairePageCommande.RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);

            recetteTriees = SelectionTrieRecette();

            _recettesPossibles = recetteTriees;
            RemplirAffichageRecette(_recettesPossibles);
        }

        private Dictionary<Recette, int> SelectionTrieRecette()
        {
            Dictionary<Recette, int> recetteTriees = new Dictionary<Recette, int>();

            if (!(bool)optAucunFiltre.IsChecked)
            {
                if ((bool)optNom.IsChecked)
                {
                    recetteTriees = UtilitairePageCommande.TrierRecettesParNom(_dictAlimentsDispo,_recetteDAL);
                }
                else
                {
                    recetteTriees = UtilitairePageCommande.TrierRecetteParQuantite(_recettesPossibles);
                }
            }
            else
            {
                recetteTriees = _recettesPossibles;
            }

            KeyValuePair<int, string> paireClefValeur = (KeyValuePair<int, string>)cboCategorie.SelectedItem;
            int categorie = paireClefValeur.Key;

            recetteTriees = UtilitairePageCommande.TrierRecettesParCategorie(recetteTriees, categorie);

            return recetteTriees;
        }


        private void btnCommander_Click(object sender, RoutedEventArgs e)
        {
            if(_maCommande.Items.Count > 0)
            {
                string message = CommandeToString(_maCommande);
                string titre = "Facture";
                MessageBoxButton boutons = MessageBoxButton.OKCancel;
                MessageBoxResult result = MessageBox.Show(message, titre, boutons);
                if(result == MessageBoxResult.OK)
                {
                    _maCommande.DateHeure = DateTime.Now;
                    _CommandeDAL.AjouterCommandeLog(_maCommande);

                    RafraichirListeAlimentsBD();



                    _maCommande = new Commande();
                    RafraichirListBoxCommande();
                }               
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
            if (!_afficher)
            {
                AfficherRecettesIndispo();
                btnRecetteInd.Content = "Masquer les recettes non-disponibles";
                _afficher = true;
            }
            else
            {
                _recettesPossibles = UtilitairePageCommande.RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
                RemplirAffichageRecette(_recettesPossibles);

                btnRecetteInd.Content = "Afficher les recettes non-disponibles";
                _afficher = false;
            }
            
        }

        private void AfficherRecettesIndispo()
        {
            foreach (Button bouton in WP.Children)
            {
                bouton.Visibility = Visibility.Visible;
            }
        }

        private string CommandeToString(Commande pCommande)
        {
            Dictionary<Recette, int> dictItemsGroupes = GrouperItemsCommande(pCommande);
            decimal sousTotal = 0;

            string str = "Votre commande : \n\n";           

            foreach (KeyValuePair<Recette, int> item in dictItemsGroupes)
            {
                str += $"{item.Value}  {item.Key.Nom}:";
                str += TrouverNombreTabulation(item.Key.Nom);
                str += $"{Math.Round(item.Key.Prix * item.Value, 2)}$\n";
                sousTotal += item.Key.Prix * item.Value;
            }

            str += $"Sous-Total:\t{Math.Round(pCommande.Total, 2)}$\n";

            str += $"\nTPS:\t\t{pCommande.Total * (decimal)0.05}$";
            str += $"\nTVQ:\t\t{Math.Round(pCommande.Total * (decimal)0.09975, 2)}$";

            str += $"\n\nTotal:\t\t{Math.Round(pCommande.Total * (decimal)1.14975, 2)}$";

            return str;
        }

        private string TrouverNombreTabulation(string pNom)
        {
            string str = "";
            int nb = 3 - (pNom.Length+1) / 4;
            for (int i = 0; i < nb; i++)
            {
                str += "\t";
            }

            return str;    
        }

        private Dictionary<Recette, int> GrouperItemsCommande(Commande pCommande)
        {
            Dictionary<Recette, int> dictRecetteCommande = new Dictionary<Recette, int>();
            foreach (var item in pCommande.Items)
            {
                if (!dictRecetteCommande.ContainsKey(item))
                {
                    dictRecetteCommande.Add(item, 1);
                }
                else
                {
                    dictRecetteCommande[item] += 1;
                }
            }

           return dictRecetteCommande;
        }

        private void btnMulti_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<Recette, int> itemsCommande = GrouperItemsCommande(_maCommande);
            int multi = (int)cbbMulti.SelectedItem - 1;
            foreach (var recetteInt in itemsCommande)
            {
                for (int i = 0; i < (multi*recetteInt.Value); i++)
                {
                    RetirerQuantiteIngredient(recetteInt.Key);
                    _maCommande.AjouterItemCommande(recetteInt.Key);
                    
                }
            }
            _recettesPossibles = UtilitairePageCommande.RecupererLesRecettesPossibles(_dictAlimentsDispo, _listeToutesRecettes);
            RemplirAffichageRecette(_recettesPossibles);
            RafraichirListBoxCommande();
            RafraichirComboBoxMulti();

        }
    }
}
