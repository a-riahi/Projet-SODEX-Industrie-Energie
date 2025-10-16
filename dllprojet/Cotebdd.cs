using BddprojetContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllprojet
{
    public class Cotebdd : connexion
    {
        public Cotebdd(string serveurIp, string port, string user, string mdp, string nom_bb)
            : base(serveurIp, port, user, mdp, nom_bb)
        {
        }
        public List<Cote> getallcote()
        {
            try
            {
                return bdd.Cotes.ToList();  // Retourne la liste des éléments présents dans la table 'Cotes'
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des cotes : " + ex.Message);
            }
        }
        public bool ajoutCote( float longueur, double Diametre, double deport, double axe1peD,double axe1peL, double axe1pel, double axe2peD, double axe2peL, double axe2pel, double axe1vlD, double axe1vlL, double axe1vll, double axe2vlD, double axe2vlL, double axe2vll, double hauteur,double profondeur,double epaisseur,double autre, int  id_plan)
        {
            bool result;
            try
            {
                Cote tp = new Cote  // tp = type de cote, on créer un objet de type Cote
                {
                   Longueur= longueur,
                    Diametre = Diametre,
                    Deport = deport,
                    Axe1PeDiametre = axe1peD,
                    Axe1PeLargeur = axe1pel,
                    Axe1PeLongueur = axe1peL,
                    Axe2PeDiametre = axe2peD,
                    Axe2PeLargeur = axe2pel,
                    Axe2PeLongueur = axe2peL,
                    Axe1VolantDiametre = axe1vlD,
                    Axe1VolantLargeur = axe1vll,
                    Axe1VolantLongueur = axe1vlL,
                    Axe2VolantDiametre = axe2vlD,
                    Axe2VolantLargeur = axe2vll,
                    Axe2VolantLongueur = axe2vlL,
                    Hauteur = hauteur,
                    Profondeur=profondeur,
                    Epaisseur = epaisseur,
                    Autre = autre,
                    IdPlan = id_plan,

                };
                bdd.Cotes.InsertOnSubmit(tp);     // ajoute a la table le cote que l'on a decider de rajouter à la liste
                bdd.SubmitChanges();                    // on mets a jour la bdd
                result = true;                          // on retourne True si l'ajout est réussi
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception($"Erreur lors de l'ajout du cote : {ex.Message}");
            }
            return result;
        }

        public void supCote(int id)
        {
            try
            {
                List<Cote> c = getallcote(); // Récupère la liste de tous les cotes depuis la base de données
                Cote tp = c.FirstOrDefault(t => t.Id == id); // Recherche le premier cote dont l'id correspond à 'id'
                if (tp != null)
                {
                    bdd.Cotes.DeleteOnSubmit(tp); // Supprime le cote trouvé de la base de données
                    bdd.SubmitChanges(); // Applique les modifications à la base de données
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la suppression du cote : {ex.Message}");
            }
        }

        public void modifCote(int id, float longueur, double Diametre, double deport, double axe1peD, double axe1peL, double axe1pel, double axe2peD, double axe2peL, double axe2pel, double axe1vlD, double axe1vlL, double axe1vll, double axe2vlD, double axe2vlL, double axe2vll, double hauteur, double profondeur, double epaisseur, double autre, int id_plan)
        {
            try
            {
                var trn = bdd.Cotes.SingleOrDefault(s => s.Id == id); // cherche le cote correspondant a l'id entrer en parametre 
                if (trn != null)
                {
                    trn.Longueur = longueur;
                    trn.Diametre = Diametre;
                    trn.Deport = deport;
                    trn.Axe1PeDiametre = axe1peD;
                    trn.Axe1PeLargeur = axe1pel;
                    trn.Axe1PeLongueur = axe1peL;
                    trn.Axe2PeDiametre = axe2peD;
                    trn.Axe2PeLargeur = axe2pel;
                    trn.Axe2PeLongueur = axe2peL;
                    trn.Axe1VolantDiametre = axe1vlD;
                    trn.Axe1VolantLargeur = axe1vll;
                    trn.Axe1VolantLongueur = axe1vlL;
                    trn.Axe2VolantDiametre = axe2vlD;
                    trn.Axe2VolantLargeur = axe2vll;
                    trn.Axe2VolantLongueur = axe2vlL;
                    trn.Hauteur = hauteur;
                    trn.Profondeur = profondeur;
                    trn.Epaisseur = epaisseur;
                    trn.Autre = autre;
                    trn.IdPlan = id_plan;
                    bdd.SubmitChanges(); // Applique les modifications à la base de données
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la modification du cote : {ex.Message}");
            }
        }
    }
}
