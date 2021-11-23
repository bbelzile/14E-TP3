using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TP214E.Data
{
    public static class UtilitaireVerificationFormulaire
    {
        private static readonly Regex _regexChiffre = new Regex("^[0-9]+$");

        public static bool VerificationSiTextPasVide(string valeurAVerifier)
        {
            return (valeurAVerifier != "");
        }

        public static bool TextContienQueDesChiffre(string text)
        {
            bool estUnNombre = _regexChiffre.IsMatch(text);
            return estUnNombre;
        }

        public static bool VerificationLongueurChaine()
        {
            return true;
        }

        public static bool VerificationLongueureNombre()
        {
            return true;
        }
    }
}
