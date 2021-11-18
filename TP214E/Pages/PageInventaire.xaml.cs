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
using TP214E.Pages;


namespace TP214E
{
    /// <summary>
    /// Logique d'interaction pour Inventaire.xaml
    /// </summary>
    public partial class PageInventaire : Page
    {
        private List<Aliment> _aliments;
        AlimentDAL _dal;

        public PageInventaire()
        {
            _dal = new AlimentDAL();
            _aliments = new List<Aliment>();


            InitializeComponent();
        }

        public void AuChargement(object sender, RoutedEventArgs e)
        {
            ChargerLesAliments();
        }

        private void ChargerLesAliments()
        {
            _aliments = _dal.RechercherTousLesAliments();
            lvAliments.ItemsSource = _aliments;
            DataContext = this;
        }

        

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            ListViewItem elemetDeLaliste = sender as ListViewItem;
            if (elemetDeLaliste != null && elemetDeLaliste.IsSelected)
            {
                Aliment alimentSelectionne = (Aliment)elemetDeLaliste.DataContext;
                PageAliment frmAliment = new PageAliment(_dal, alimentSelectionne);

                this.NavigationService.Navigate(frmAliment);
            }
        }

        public void SupprimerAliment(Aliment alimentASupprimer)
        {
            bool requeteReussi = _dal.SupprimerAliment(alimentASupprimer);
            VerifierReussiteRequete(requeteReussi);
        }

        public void VerifierReussiteRequete(bool requeteRéussi)
        {
            if (requeteRéussi)
            {
                ChargerLesAliments();
            }
            else
            {
                MessageBox.Show("Il y a eu une erreur lors de la suppression");
            }
        }

        private void btnAjouterAliment_Click(object sender, RoutedEventArgs e)
        {
            PageAliment frmAliment = new PageAliment(_dal, null);
            this.NavigationService.Navigate(frmAliment);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var elementDuMenu = (MenuItem)e.OriginalSource;
            Aliment alimentASupprimer = (Aliment)elementDuMenu.CommandParameter;
            SupprimerAliment(alimentASupprimer);
        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            RetournerAuMenu();
        }

        private void RetournerAuMenu()
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}
