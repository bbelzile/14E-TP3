using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TP214E.Data
{
    public static class UtilitaireVerificationFormulaire
    {
        private static readonly Regex _regexChiffre = new Regex("^[0-9]+$");
        public const int LONGUEUR_MAXIMUM_CHAINE = 100;
        public const int LONGUEUR_MAXIMUM_NOMBRE = 4;

        public static bool VerificationSiTextPasVide(string valeurAVerifier)
        {
            return (valeurAVerifier != "");
        }

        public static bool TextContienQueDesChiffre(string text)
        {
            bool estUnNombre = _regexChiffre.IsMatch(text);
            return estUnNombre;
        }

        public static bool VerificationLongueurChaine(string chaineAVerifier)
        {

            if (chaineAVerifier != null && chaineAVerifier.Length > LONGUEUR_MAXIMUM_CHAINE)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool VerificationLongueureNombre(string nombreAVerifier)
        {

            if(nombreAVerifier.Length > LONGUEUR_MAXIMUM_NOMBRE)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool VerificationNombreEstPositif(int nombreAVerifier)
        {
            if (nombreAVerifier > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool VerificationValeurEstDansEnumCategorie(int valeurAVerifier)
        {
            return Enum.IsDefined(typeof(Categories), valeurAVerifier);
        }

        public static bool VerificationSiValeurPasNulle(Object valeurAVerifier)
        {
            if(valeurAVerifier != null)
            {
                return true;
            }
            else
            {
                throw new Exception("Un champ du formulaire est vide.");
            }
        }
    }
}
