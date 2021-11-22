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
    /// Logique d'interaction pour PageRecette.xaml
    /// </summary>
    public partial class PageRecette : Page
    {
        Dictionary<string, int> _ingredients;
        
        AlimentDAL _dalAliment;
        RecetteDAL _dalRecette;

        public PageRecette()
        {
            _dalAliment = new AlimentDAL();
            _dalRecette = new RecetteDAL();
            _ingredients = new Dictionary<string, int>();

            InitializeComponent();
            ChargerCategories();
            ChargerAliments();
            RafraichirDonnees();
            
        }

        public void ChargerCategories()
        {
            this.cboCategorie.SelectedValuePath = "Key";
            this.cboCategorie.DisplayMemberPath = "Value";

           foreach (int valeurCategorie in Enum.GetValues(typeof(Categories)))
            {
                int valeurDeLaCategorie = valeurCategorie;
                string nomDeLaCategorie = Enum.GetName(typeof(Categories),valeurCategorie);
                
                KeyValuePair<int, string> clefValeurCategorie =
                    new KeyValuePair<int, string>(valeurDeLaCategorie,nomDeLaCategorie);
                
                this.cboCategorie.Items.Add(clefValeurCategorie);
            } 
        }

        public void ChargerAliments()
        {
            List<Aliment> aliments = _dalAliment.RechercherTousLesAliments();
            foreach (Aliment aliment in aliments)
            {
                this.cboAliment.Items.Add(aliment.Nom);
            }
        }

        public void EnvoyerInformationsRecette(object sender, RoutedEventArgs e)
        {
            Recette nouvelleRecette;

            string nom = txtNom.Text;
            string prix = iudPrix.Text;
            KeyValuePair<int, string> paireClefValeur = (KeyValuePair<int, string>) cboCategorie.SelectedItem;
            int categorie = paireClefValeur.Key;

            try
            {
                nouvelleRecette = new Recette(nom, _ingredients, prix, categorie);
                _dalRecette.CreerRecette(nouvelleRecette);

                FermerPage(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var elementDuMenu = (MenuItem)e.OriginalSource;
            KeyValuePair<string, int> ingredientASupprimer = (KeyValuePair<string, int>)elementDuMenu.CommandParameter;
            RetirerIngredient(ingredientASupprimer);
        }

        public void RetirerIngredient(KeyValuePair<string, int> ingredientASupprimer)
        {
            _ingredients.Remove(ingredientASupprimer.Key);

            RafraichirDonnees();
        }

        public void RafraichirDonnees()
        {
            lvIngredients.ItemsSource = _ingredients;
            DataContext = this;
        }

        public void AjouterIngredient(object sender, RoutedEventArgs e)
        {
            string ingredient = cboAliment.SelectedItem as string;
            string quantite = txtQuantite.Text;

            if (VerifierValeursAjoutIngredient(ingredient,quantite) == "")
            {
                _ingredients.Add(ingredient, int.Parse(quantite));
                ViderChampsIngredient();
            }
            else
            {
                AfficherErreurAuxChampsIngredients("Valeur invalide");
            }

            RafraichirDonnees();
        }

        public static string VerifierValeursAjoutIngredient(string ingredient, string quantite)
        {
            string messageErreur = "";
            if(UtilitaireVerificationFormulaire.VerificationSiTextPasVide(ingredient) &&
                UtilitaireVerificationFormulaire.VerificationSiTextPasVide(quantite))
            {

                if (!UtilitaireVerificationFormulaire.TextContienQueDesChiffre(quantite))
                {
                    messageErreur = "Le champ quantite doit contenir que des chiffres";
                }
            }
            else
            {
                messageErreur = "Il y a un champ vide";
            }
            return messageErreur;
        }

        public void AfficherErreurAuxChampsIngredients(string message)
        {
            gboChampIngredient.BorderBrush = Brushes.Red;
            gboChampIngredient.Foreground = Brushes.Red;
            gboChampIngredient.Header = message;
        }

        public void ViderChampsIngredient()
        {
            cboAliment.SelectedIndex = -1;
            txtQuantite.Text = "0";

            gboChampIngredient.Header = "";
            gboChampIngredient.BorderBrush = Brushes.White;
            gboChampIngredient.Foreground = Brushes.White;
        }

        

        public void PreviewQuatiteTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !UtilitaireVerificationFormulaire.TextContienQueDesChiffre(e.Text);
        }

        private void FermerPage(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

    }
}
