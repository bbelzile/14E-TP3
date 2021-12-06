using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP214E.Data.Utilitaire
{
    public static class UtilitairePageCommande
    {
        public static Dictionary<Recette, int> TrierRecettesParNom(Dictionary<string, int> aliments, IRecetteDAL recetteDAL)
        {
            List<Recette> recettesTrieesParNom = recetteDAL.RechercherTLesRecettesParNom();

            return RecupererLesRecettesPossibles(aliments, recettesTrieesParNom);
        }

        public static Dictionary<Recette, int> TrierRecettesParCategorie(Dictionary<Recette, int> recetteATrier, int categorie)
        {
            Dictionary<Recette, int> recetteTriees = new Dictionary<Recette, int>();

            if (UtilitaireVerificationFormulaire.VerificationValeurEstDansEnumCategorie(categorie))
            {
                foreach (KeyValuePair<Recette, int> uneRecette in recetteATrier)
                {
                    if (uneRecette.Key.Categorie == (Categories)categorie)
                    {
                        recetteTriees.Add(uneRecette.Key, uneRecette.Value);
                    }
                }
            }
            else
            {
                return recetteATrier;
            }

            return recetteTriees;
        }

        public static Dictionary<Recette, int> TrierRecetteParQuantite(Dictionary<Recette, int> recettes)
        {
            return recettes.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
        }

        public static Dictionary<Recette, int> RecupererLesRecettesPossibles(Dictionary<string, int> pAliments, List<Recette> pRecettes)
        {
            Dictionary<Recette, int> recettesPossibles = new Dictionary<Recette, int>();

            double cbRecetteFaisable;
            int qtMinimum;
            foreach (var recette in pRecettes)
            {
                qtMinimum = 10000;
                foreach (var ingredient in recette.Ingredients)
                {
                    cbRecetteFaisable = Math.Floor((double)(pAliments[ingredient.Nom] / ingredient.Quantite));
                    if (cbRecetteFaisable < qtMinimum)
                    {
                        qtMinimum = (int)cbRecetteFaisable;
                    }
                }

                recettesPossibles.Add(recette, qtMinimum);
            }

            return recettesPossibles;
        }

        public static Dictionary<string, int> ListeAlimentsEnDictionnaire(List<Aliment> pListeAlimentsDispo)
        {
            Dictionary<string, int> dictAlimentsDispo = new Dictionary<string, int>();
            foreach (var item in pListeAlimentsDispo)
            {
                dictAlimentsDispo.Add(item.Nom, item.Quantite);
            }

            return dictAlimentsDispo;
        }
    }
}
