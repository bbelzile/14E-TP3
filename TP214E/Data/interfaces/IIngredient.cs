namespace TP214E.Data
{
    public interface IIngredient
    {
        string Nom { get; set; }
        int Quantite { get; set; }

        void VerifierValeurNom(string valeurAVerifier);
        void VerifierValeurQuantite(string valeurAVerifier);
    }
}