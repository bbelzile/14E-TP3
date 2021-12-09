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
        List<Ingredient> _ingredients;
        
        AlimentDAL _dalAliment;
        RecetteDAL _dalRecette;

        public PageRecette()
        {
            _dalAliment = new AlimentDAL();
            _dalRecette = new RecetteDAL();
            _ingredients = new List<Ingredient>();

            InitializeComponent();
            InstancierCheminValeurCboCategorie();
            ChargerDonnees();

        }

        public void ChargerDonnees()
        {
            ChargerCategories();
            ChargerAliments();
            RafraichirDonnees();
        }

        public void ChargerCategories()
        {
           foreach (int valeurCategorie in Enum.GetValues(typeof(Categories)))
            {
                int valeurDeLaCategorie = valeurCategorie;
                string nomDeLaCategorie = Enum.GetName(typeof(Categories),valeurCategorie);
                
                KeyValuePair<int, string> clefValeurCategorie =
                    new KeyValuePair<int, string>(valeurDeLaCategorie,nomDeLaCategorie);
                
                this.cboCategorie.Items.Add(clefValeurCategorie);
            } 
        }

        public void InstancierCheminValeurCboCategorie()
        {
            this.cboCategorie.SelectedValuePath = "Key";
            this.cboCategorie.DisplayMemberPath = "Value";
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
            try
            {
                Recette nouvelleRecette = ChercherInformationFormulaire();

                _dalRecette.CreerRecette(nouvelleRecette);

                FermerPage(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Recette ChercherInformationFormulaire()
        {
            Recette nouvelleRecette = new Recette();

            string nom = txtNom.Text;
            string prix = iudPrix.Text;

            if (UtilitaireVerificationFormulaire.VerificationSiValeurPasNulle(cboCategorie.SelectedItem))
            {
                KeyValuePair<int, string> paireClefValeur = (KeyValuePair<int, string>)cboCategorie.SelectedItem;
                int categorie = paireClefValeur.Key;

                nouvelleRecette = new Recette(nom, _ingredients, prix, categorie);
            }


            return nouvelleRecette;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var elementDuMenu = (MenuItem)e.OriginalSource;
            Ingredient ingredientASupprimer = (Ingredient)elementDuMenu.CommandParameter;
            RetirerIngredient(ingredientASupprimer);
        }

        public void RetirerIngredient(Ingredient ingredientASupprimer)
        {
            _ingredients.Remove(ingredientASupprimer);
            RafraichirDonnees();
        }

        public void RafraichirDonnees()
        {
            lvIngredients.ItemsSource = _ingredients;
            DataContext = this;
            lvIngredients.Items.Refresh();
        }

        public void AjouterIngredient(object sender, RoutedEventArgs e)
        {
            string nomIngredient = cboAliment.SelectedItem as string;
            string quantite = txtQuantite.Text;

            try
            {
                Ingredient nouvelIngredient = new Ingredient(nomIngredient, quantite);

                _ingredients.Add(nouvelIngredient);
              
                ViderChampsIngredient();
                RetirerAlimentDesChoix(nouvelIngredient.Nom);
                RafraichirDonnees();
            }
            catch(Exception ex)
            {
                AfficherErreurAuxChampsIngredients(ex.Message);
            }
        }

        public void RetirerAlimentDesChoix(string nomAlimentASupprimer)
        {
            this.cboAliment.Items.Remove(nomAlimentASupprimer);
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
