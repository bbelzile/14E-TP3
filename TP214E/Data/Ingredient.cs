using System;

namespace TP214E.Data
{
    public class Ingredient
    {
        public Ingredient(string pNom, int pQuantite)
        {
            Nom = pNom;
            Quantite = pQuantite;
        }

        public Ingredient(string pNom, string pQuantite)
        {
            VerifierValeurNom(pNom);
            VerifierValeurQuantite(pQuantite);
        }

        public string Nom { get; set; }

        public int Quantite{ get; set; } 


        public void VerifierValeurNom(string valeurAVerifier)
        {
            if (UtilitaireVerificationFormulaire.VerificationSiTextPasVide(valeurAVerifier))
            {
                Nom = valeurAVerifier;
            }
            else
            {
                throw new ArgumentException("Le champ nom est vide.");
            }
        }

        public void VerifierValeurQuantite(string valeurAVerifier)
        {
            if (UtilitaireVerificationFormulaire.VerificationSiTextPasVide(valeurAVerifier))
            {
                if (UtilitaireVerificationFormulaire.TextContienQueDesChiffre(valeurAVerifier))
                {
                        if (int.Parse(valeurAVerifier) > 0)
                        {
                            Quantite = int.Parse(valeurAVerifier);
                        }
                        else
                        {
                            throw new ArgumentException("Le champ quantite doit supérieur à 0.");
                        }
                }
                else
                {
                    throw new ArgumentException("Le champ quantite doit contenir que des chiffres.");
                }
            }
            else
            {
                throw new ArgumentException("Le champ quantite est vide.");
            }
        }
    }
}