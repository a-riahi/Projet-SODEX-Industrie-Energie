using BddprojetContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllprojet
{
    public class NumeroPlanbdd : connexion
    {
        public NumeroPlanbdd(string serveurIp, string port, string user, string mdp, string nom_bb)
            : base(serveurIp, port, user, mdp, nom_bb)
        {
        }

        public List<Numeroplan> getallnumeroplan()
        {
            try
            {
                return bdd.Numeroplans.ToList();  // Retourne la liste des éléments présents dans la table 'NumeroPlans'
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des Numéros de plan : " + ex.Message);
            }
        }

        public bool ajoutNumeroplan( int numero)
        {
            bool result;
            try
            {
                Numeroplan numPlan = new Numeroplan  // numPlan = type de numéro de plan, on créer un objet de type Numeroplan
                {
                   
                    Numero = numero
                };
                bdd.Numeroplans.InsertOnSubmit(numPlan);     // ajoute a la table le numéro de plan que l'on a décidé de rajouter à la liste
                bdd.SubmitChanges();                    // on mets a jour la bdd
                result = true;                          // on retourne True si l'ajout est réussi
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception($"Erreur lors de l'ajout du numéro de plan : {ex.Message}");
            }
            return result;
        }

        public bool supNumeroplan(int numero)
        {
            bool result = false;
            try
            {
                List<Numeroplan> n = getallnumeroplan(); // Récupère la liste de tous les numéros de plan depuis la base de données
                Numeroplan numPlan = n.FirstOrDefault(t => t.Numero == numero); // Recherche le premier numéro de plan dont le numéro correspond à 'numero'
                if (numPlan != null)
                {
                    bdd.Numeroplans.DeleteOnSubmit(numPlan); // Supprime le numéro de plan trouvé de la base de données
                    bdd.SubmitChanges(); // Applique les modifications à la base de données
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception($"Erreur lors de la suppression du numéro de plan : {ex.Message}");
            }
            return result;
        }

        public bool modifNumeroplan(int numero, int id)
        {
            bool result = false;
            try
            {
                List<Numeroplan> n = getallnumeroplan(); // Récupère la liste de tous les numéros de plan depuis la base de données
                Numeroplan numPlan = n.FirstOrDefault(t => t.Numero == numero); // Recherche le premier numéro de plan dont le numéro correspond à 'numero'
                if (numPlan != null)
                {
                    numPlan.Numero = id; // Modifie le numéro de plan trouvé
                    bdd.SubmitChanges(); // Applique les modifications à la base de données
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception($"Erreur lors de la modification du numéro de plan : {ex.Message}");
            }
            return result;
        }
    }
}
