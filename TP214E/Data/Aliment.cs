using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TP214E.Data
{
    public class Aliment
    {
        private static readonly Regex _regexChiffre = new Regex("^[0-9]+$");


        private ObjectId _id;
        private string _nom;
        private int _quantite;
        private string _unite;
        private DateTime  _expireLe;

        public Aliment()
        {

        }

        public Aliment(ObjectId id, string nom, int quantite, string unite, DateTime expireLe)
        {
            Id = id;
            Nom = nom;
            Quantite = quantite;
            Unite = unite;
            ExpireLe = expireLe;
        }

        public Aliment(string nom, string quantite, string unite, string expireLe)
        {
            VerificationValeurNom(nom);
            VerificationValeurQuantite(quantite);
            VerificationValeurUnite(unite);
            VerificationValeurDate(expireLe);
        }

        public ObjectId Id {
            get { return _id; }
            set { _id = value; }
        }
        public string Nom {
            get { return _nom; }
            set { _nom = value; }
        }
        public int Quantite {
            get { return _quantite; }
            set { _quantite = value; }
        }
        public string Unite {
            get { return _unite; }
            set { _unite = value; }
        }
        public DateTime ExpireLe {
            get { return _expireLe; }
            set { _expireLe = value; }
        }

        public void VerificationValeurDate(string valeurAVerifier)
        {
            if (VerificationSiTextPasVide(valeurAVerifier))
            {
                if (DateTime.Parse(valeurAVerifier) > DateTime.Today)
                {
                    _expireLe = DateTime.Parse(valeurAVerifier);
                }
                else
                {
                    throw new ArgumentException("La date est invalide.");
                }
            }
            else
            {
                throw new ArgumentException("Le champ date est vide.");
            }
        }

        public void VerificationValeurQuantite(string valeurAVerifier)
        {
            if (VerificationSiTextPasVide(valeurAVerifier))
            {
                if ( Aliment.TextContienQueDesChiffre(valeurAVerifier))
                {
                    _quantite = int.Parse(valeurAVerifier);
                }
                else
                {
                    throw new ArgumentException("Le champ quantite doit comporter que des nombres.");
                }
            }
            else
            {
                throw new ArgumentException("Le champ quantite est vide.");
            }
        }

        public void VerificationValeurNom(string valeurAVerifier)
        {
            if (VerificationSiTextPasVide(valeurAVerifier))
            {
                _nom = valeurAVerifier;
            }
            else
            {
                throw new ArgumentException("Le champ nom est vide.");
            }
        }

        public void VerificationValeurUnite(string valeurAVerifier)
        {
            if (VerificationSiTextPasVide(valeurAVerifier))
            {
                _unite = valeurAVerifier;
            }
            else
            {
                throw new ArgumentException("Le champ unite est vide.");
            }
        }

        public static bool VerificationSiTextPasVide(string valeurAVerifier)
        {
            return (valeurAVerifier != "");
        }

        public static bool TextContienQueDesChiffre(string text)
        {
            bool estUnNombre = _regexChiffre.IsMatch(text);
            return estUnNombre;
        }
    }
}
