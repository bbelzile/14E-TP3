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
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace TP214E.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PageAliment : Page
    {
        AlimentDAL _dal;
        Aliment _aliment;

        public PageAliment(AlimentDAL dal, Aliment aliment)
        {
            _dal = dal;
            _aliment = aliment;
            InitializeComponent();
            RemplissageFormulaire();
        }

        public void FermerPage(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        public void EnvoyerInformationsAliment(object sender, RoutedEventArgs e)
        {
            Aliment aliment = ObtenirInformationDuFormulaire();
            if (aliment != null)
            {
                if (VerificationCasEnvoie(_aliment))
                {
                    CreerAliment(aliment, _dal);
                }
                else
                {
                    aliment.Id = ObjectId.Parse(lblId.Text);
                    ModifierAliment(aliment, _dal);
                }
            }
            
        }

        public static bool VerificationCasEnvoie(Aliment aliment)
        {
            return (aliment == null);
        }

        public void CreerAliment(Aliment alimentACree, AlimentDAL dal)
        {
            bool requeteReussi = dal.CreerAliment(alimentACree);
            VerifierReussiteRequete(requeteReussi);
        }

        public void ModifierAliment(Aliment alimentAModifier, AlimentDAL dal)
        {
            bool requeteReussi = dal.ModifierAliment(alimentAModifier);
            VerifierReussiteRequete(requeteReussi);
        }

        public void VerifierReussiteRequete(bool requeteReussi)
        {
            if (requeteReussi)
            {
                FermerPage(null, null);
            }
            else
            {
                MessageBox.Show("Il y a eu une erreur lors de la connection au server.");
            }
        }

        private Aliment ObtenirInformationDuFormulaire()
        {
            try
            {
                Aliment nouvelAliment = new Aliment(
                txtNom.Text,
                txtQuatite.Text,
                txtUnite.Text,
                dpkDate.Text);

                return nouvelAliment;
            }
            catch(ArgumentException erreur)
            {
                MessageBox.Show(erreur.Message);
                return null;
            }
            
        }

        private void RemplissageFormulaire()
        {
            if (_aliment != null)
            {
                lblId.Text = _aliment.Id.ToString();
                txtNom.Text = _aliment.Nom;
                txtQuatite.Text = _aliment.Quantite.ToString();
                txtUnite.Text = _aliment.Unite;
                dpkDate.Text = _aliment.ExpireLe.ToString();
            }
        }

        public void PreviewQuatiteTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Aliment.TextContienQueDesChiffre(e.Text);
        }

    }
    
}
