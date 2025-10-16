using System;
using System.Collections.Generic;
using System.Linq;
using BddprojetContext;
namespace dllprojet
{
    public class materiauxbdd : connexion 
    {
        public materiauxbdd(string serveurIp, string port, string user, string mdp, string nom_bb)
            : base(serveurIp, port, user, mdp, nom_bb)
        {
        }

        public List<Matériaux> getallmateriaux()
        {
            try
            {
                return bdd.Matériauxes.ToList();    // return la liste des éléments présent dans la table matériaux
            }
            catch { throw; }
        }

        public bool ajoutMatériaux(string nom)
        {
            bool result = false;
            try
            {
                Matériaux tp = new Matériaux  // tp = type de matériaux, on créer un objet de type Matériaux
                {
                    TypeMateriau = nom

                };

                bdd.Matériauxes.InsertOnSubmit(tp);     // ajoute a la table le matériaux que l'on a decider de rajouter à la liste
                bdd.SubmitChanges();                    // on mets a jour la bdd
                result = true;                          // on retourne True si l'ajout est réussi
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception($"Erreur lors de l'ajout du matériaux : {ex.Message}");

            }
            return result;
        }

        public bool supMateriaux(string mat)
        {
            bool result = false;
            try
            {
                List<Matériaux> m = getallmateriaux(); // Récupère la liste de tous les matériaux depuis la base de données

                Matériaux matsup = m.FirstOrDefault(t => t.TypeMateriau == mat); // Recherche le premier matériau dont le type correspond à 'mat'

                if (matsup != null)
                {
                    bdd.Matériauxes.DeleteOnSubmit(matsup); // Supprime le matériau trouvé de la base de données
                    bdd.SubmitChanges(); // Applique les modifications à la base de données
                    result = true;
                }
                else
                {
                    throw new Exception("Matériaux introuvable.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du matériaux : " + ex.Message);
            }

            return result;
        }

        public bool modifmater(int idmat, string nouveauMat)
        {
            try
            {
                var trn = bdd.Matériauxes.SingleOrDefault(s => s.Id == idmat);

                if (trn != null)
                {
                    trn.TypeMateriau = nouveauMat;
                    bdd.SubmitChanges();
                    return true;
                }
                else
                {  
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Erreur lors de la modification du matériaux : " + ex.Message);
            }
        }
    }
 
}
