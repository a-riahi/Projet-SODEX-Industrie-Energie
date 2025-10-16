using BddprojetContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllprojet
{
    public class commandebdd : connexion
    {
        public commandebdd(string serveurIp, string port, string user, string mdp, string nom_bb)
            : base(serveurIp, port, user, mdp, nom_bb)
        {
        }

        public List<Commande> getallcommande()
        {
            try
            {
                return bdd.Commandes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des Commandes : " + ex.Message + " | StackTrace : " + ex.StackTrace);
            }
        }

        public bool ajoutCommande(int id_client, int nb_plan, int etat)
        {
            bool result;
            try
            {
                Commande com = new Commande
                {
                    IdClient = id_client,
                    NbPlan = nb_plan,
                    EtatCmd = etat

                };

                bdd.Commandes.InsertOnSubmit(com);
                bdd.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception($"Erreur lors de l'ajout de la commande : {ex.Message}");

            }
            return result;
        }
        public bool supcmd(int id)
        {
            bool result = false;
            try
            {
                List<Commande> c = getallcommande();

                Commande cmd = c.FirstOrDefault(t => t.Id == id);

                if (cmd != null)
                {
                    bdd.Commandes.DeleteOnSubmit(cmd);
                    bdd.SubmitChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("Commande introuvable.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de la commande : " + ex.Message);
            }

            return result;
        }

        public bool modifcmd(int idcom, int id_client, int nb_commade, int etat)
        {
            try
            {
                var com = bdd.Commandes.SingleOrDefault(s => s.Id == idcom);

                if (com != null)
                {

                    com.IdClient = id_client;
                    com.NbPlan = nb_commade;
                    com.EtatCmd = etat;

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
                throw new Exception("Erreur lors de la modification de la commande : " + ex.Message);
            }
        }


        public List<Plan> Planparcommande(int idcommande)
        {
            List<Plan> plans = new List<Plan>();
            try
            {
                plans = bdd.Plans.Where(p => p.IdCommande == idcommande).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des plans : " + ex.Message);
            }
            return plans;
        }

        public List<Commande> Clientparcommande(int idclient)
        {
            List<Commande> commandes = new List<Commande>();
            try
            {
                commandes = bdd.Commandes.Where(c => c.IdClient == idclient).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return commandes;   
        }

        public string Etat(int etatCmd)
        {
            switch (etatCmd)
            {
                case 0: return "En attente";
                case 1: return "En cours";
                case 2: return "Validée";
                default: return "État inconnu";
            }

        }
    }
}
