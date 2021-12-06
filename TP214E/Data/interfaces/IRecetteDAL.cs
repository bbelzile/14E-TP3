using System.Collections.Generic;

namespace TP214E.Data
{
    public interface IRecetteDAL
    {
        bool CreerRecette(Recette recetteACreer);
        List<Recette> RechercherTLesRecettesParNom();
        List<Recette> RechercherToutesLesRecettes();
    }
}