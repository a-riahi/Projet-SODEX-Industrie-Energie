using BddprojetContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllprojet
{
    public class fermeturebdd : connexion
    {
        public fermeturebdd(string serveurIp, string port, string user, string mdp, string nom_bb)
            : base(serveurIp, port, user, mdp, nom_bb)
        {
        }


        public List<Fermeture> getallfermeture()
        {
            try
            {
                return bdd.Fermetures.ToList();  // Retourne la liste des éléments présents dans la table 'Plans'
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des fermetures : " + ex.Message);
            }
        }

        public bool ajoutFermeture( string nom_fermeture)
        {
            bool result;
            try
            {
                Fermeture fer = new Fermeture  // pl = type de plan, on créer un objet de type Plan
                {
                    TypeFermeture = nom_fermeture,

                };

                bdd.Fermetures.InsertOnSubmit(fer);     // ajoute a la table le Plan que l'on a decider de rajouter à la liste
                bdd.SubmitChanges();                    // on mets a jour la bdd
                result = true;                          // on retourne True si l'ajout est réussi
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception($"Erreur lors de l'ajout du plan : {ex.Message}");

            }
            return result;
        }
        public bool supFerm(string mat)
        {
            bool result = false;
            try
            {
                List<Fermeture> f = getallfermeture(); // Récupère la liste de tous les matériaux depuis la base de données

                Fermeture fer = f.FirstOrDefault(t => t.TypeFermeture == mat); // Recherche le premier matériau dont le type correspond à 'mat'

                if (fer != null)
                {
                    bdd.Fermetures.DeleteOnSubmit(fer); // Supprime le matériau trouvé de la base de données
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

        public bool modifferm(int idferm, string nouveau_fermeture)
        {
            try
            {
                var ferm = bdd.Fermetures.SingleOrDefault(s => s.Id == idferm); // cherche le plan correspondant a l'id entrer en parametre 

                if (ferm != null)
                {
                              
                    ferm.TypeFermeture = nouveau_fermeture;                    
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
                throw new Exception("Erreur lors de la modification de la fermeture : " + ex.Message);
            }
        }

       



    }
}
