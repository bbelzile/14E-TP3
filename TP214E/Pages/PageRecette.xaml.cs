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

        public PageRecette()
        {
            InitializeComponent();
            ChargerCategories();
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

        public void PreviewQuatiteTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !UtilitaireVerificationFormulaire.TextContienQueDesChiffre(e.Text);
        }
    }
}
