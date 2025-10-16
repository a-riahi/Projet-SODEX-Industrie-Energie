using System;
using System.Collections.Generic;
using System.Linq;
using BddprojetContext;

namespace dllprojet
{
    public class planbdd : connexion
    {
       
        public planbdd(string serveurIp, string port, string user, string mdp, string nom_bb)
            : base(serveurIp, port, user, mdp, nom_bb)
        {
        }

        public List<Plan> getallplan()
        {
            try
            {
                return bdd.Plans.ToList();  // Retourne la liste des éléments présents dans la table 'Plans'
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des plans : " + ex.Message);
            }
        }

        public int CompterPlansParCommande(int idCommande)
        {
            int nombrePlans = 0;
            try
            {
                // Compte le nombre de plans associés à une commande donnée
                nombrePlans = bdd.Plans.Count(p => p.IdCommande == idCommande);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors du comptage des plans pour la commande {idCommande} : {ex.Message}");
            }
            return nombrePlans;
        }

        public bool ajoutPlan(int numero, string lien, int idFermeture, int idMateriaux, int idCommande)
        {
            bool result;
            try
            {
                Plan pl = new Plan  // pl = type de plan, on créer un objet de type Plan
                {
                    IdNumero = numero,
                    LienFichierPlan = lien,
                    IdFermeture = idFermeture,
                    IdMateriaux = idMateriaux,
                    IdCommande = idCommande


                };

                bdd.Plans.InsertOnSubmit(pl);     // ajoute a la table le Plan que l'on a decider de rajouter à la liste
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
        public bool supPlan(int nb)
        {
            bool result = false;
            try
            {
                List<Plan> p = getallplan(); // Récupère la liste de tous les plan depuis la base de données

                Plan pl = p.FirstOrDefault(t => t.Id == nb ); // Recherche le premier plan dont le type correspond à 'pl'

                if (pl != null)
                {
                    bdd.Plans.DeleteOnSubmit(pl); // Supprime le plan trouvé de la base de données
                    bdd.SubmitChanges(); // Applique les modifications à la base de données
                    result = true;
                }
                else
                {
                    throw new Exception("Plan introuvable.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du Plan : " + ex.Message);
            }

            return result;
        }

        public bool modifPlan(int idPlan, int nouveauNumero, string nouveauLien, int nouvelleFermeture, int nouveauMateriau, int nouvelleCommande)
        {
            try
            {
                var trn = bdd.Plans.SingleOrDefault(s => s.Id == idPlan); // cherche le plan correspondant a l'id entrer en parametre 

                if (trn != null)
                {
                    trn.LienFichierPlan = nouveauLien;           //change le lien du fichier
                    trn.IdNumero = nouveauNumero;
                    trn.IdFermeture = nouvelleFermeture;
                    trn.IdMateriaux = nouveauMateriau;
                    trn.IdCommande=nouvelleCommande;//change le numero du plan 
                    bdd.SubmitChanges();                //applique les changements dans la bdd
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
                throw new Exception("Erreur lors de la modification du plan : " + ex.Message);
            }
        }

        public string Nomfermeture(int idferm)
        {
            try
            {
                var fermeture = bdd.Fermetures.FirstOrDefault(c => c.Id == idferm);
                if (fermeture == null)
                    throw new Exception("Fermeture introuvable.");

                return fermeture.TypeFermeture; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération du nom de la fermeture : {ex.Message}");
            }
        }

        

        public string Nom_client(int idclient)
        {
            try
            {
                var nom_client = bdd.Clients.FirstOrDefault(c => c.Id == idclient);
                if (nom_client == null)
                    throw new Exception("Fermeture introuvable.");

                return nom_client.NomE; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération du nom de la fermeture : {ex.Message}");
            }
        }

        public string NomMatériaux(int idmat)
        {
            try
            {
                var mat = bdd.Matériauxes.FirstOrDefault(c => c.Id == idmat);
                if (mat == null)
                    throw new Exception("Fermeture introuvable.");

                return mat.TypeMateriau; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération du nom de la fermeture : {ex.Message}");
            }
        }


    }
}