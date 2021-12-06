using System.Collections.Generic;

namespace TP214E.Data
{
    public interface IAlimentDAL
    {
        bool CreerAliment(Aliment alimentACreer);
        bool ModifierAliment(Aliment alimentAModifier);
        List<Aliment> RechercherTousLesAliments();
        bool SupprimerAliment(Aliment alimentASupprimer);
    }
}