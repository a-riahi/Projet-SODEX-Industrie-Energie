using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BddprojetContext;
using dllprojet;
namespace BiblioProjet
{
    public class Bdd : connexion
    {
        //private connexion bddproj;

        public Bdd(string serveurIp, string port, string user, string mdp, string nom_bb) : base(serveurIp, port, user, mdp, nom_bb)
        {

        }

        public bool TesterConnexion()
        {
            try
            {
                // Code réel pour tester la connexion à la base (ex: ouvrir une connexion)
                bdd.Connection.Open();
                bdd.Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<Employé> GetAllEmployé()
        {
            try
            {
                return bdd.Employés.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("ERREUR dans GetAllEmploye!! : " + ex.Message);
            }
        }

        public List<Client> GetAllClient()
        {
            try
            {
                return bdd.Clients.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("ERREUR dans GetAllClient!! : " + ex.Message);
            }
        }

        public bool ajoutClient(string nomE, string nomC, string prenomC, string contact, int numRue, string nomRue, int cp)
        {
            bool flag = false;

            try
            {
                Client entity = new Client
                {
                    NomC = nomC,
                    PrenomC = prenomC,
                    NomE = nomE,
                    Contact = contact,
                    NumeroRue = numRue,
                    NomRue = nomRue,
                    Cp = cp


                };
                bdd.Clients.InsertOnSubmit(entity);
                bdd.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                flag = false;
                throw new Exception("Erreur lors de l'ajout d'employé : " + ex.Message);
            }
        }

        public bool supClient(string nomE, string nomC, string prenomC, string contact, int numRue, string nomRue, int cp)
        {
            try
            {
                Client client = bdd.Clients.FirstOrDefault(c =>
                    c.NomE == nomE &&
                    c.NomC == nomC &&
                    c.PrenomC == prenomC &&
                    c.Contact == contact &&
                    c.NumeroRue == numRue &&
                    c.NomRue == nomRue &&
                    c.Cp == cp
                );

                if (client != null)
                {
                    bdd.Clients.DeleteOnSubmit(client);
                    bdd.SubmitChanges();
                    return true;
                }

                throw new Exception("Client introuvable ou les informations ne correspondent pas.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du client : " + ex.Message);
            }
        }


        public bool modifClient(int idClient, string nomE, string nomC, string prenomC, string contact, int numRue, string nomRue, int cp)
        {
            try
            {
                Client client = bdd.Clients.SingleOrDefault(c => c.Id == idClient);
                if (client != null)
                {
                    client.NomE = nomE;
                    client.NomC = nomC;
                    client.PrenomC = prenomC;
                    client.Contact = contact;
                    client.NumeroRue = numRue;
                    client.NomRue = nomRue;
                    client.Cp = cp;

                    bdd.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification du client : " + ex.Message);
            }
        }

        public Client GetClientParInfos(string nomC, string prenomC, string nomE)
        {
            try
            {
                return bdd.Clients.FirstOrDefault(c =>
                    c.NomC == nomC &&
                    c.PrenomC == prenomC &&
                    c.NomE == nomE);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la vérification de l'existence du client : " + ex.Message);
            }
        }


        public bool ajoutEmployé(string nom, string prenom, string login, string mdp, string droits)
        {
            bool flag = false;

            try
            {
                Employé entity = new Employé
                {
                    Nom = nom,
                    Prenom = prenom,
                    Login = login,
                    Mdp = mdp,
                    Droit = droits


                };
                bdd.Employés.InsertOnSubmit(entity);
                bdd.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                flag = false;
                throw new Exception("Erreur lors de l'ajout d'employé : " + ex.Message);
            }
        }
        public Employé GetEmployeParLogin(string login)
        {
            try
            {
                return bdd.Employés.FirstOrDefault(e => e.Login == login);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la vérification de l'existence de l'employé : " + ex.Message);
            }
        }
        public bool supEmployé(string nom, string prenom, string login, string mdp, string droits)
        {
            bool flag = false;
            try
            {
                List<Employé> source = GetAllEmployé();
                Employé employe = source.FirstOrDefault((Employé t) => t.Nom == nom);
                if (employe != null)
                {
                    bdd.Employés.DeleteOnSubmit(employe);
                    bdd.SubmitChanges();
                    return true;
                }

                throw new Exception("Employé introuvable.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du matériaux : " + ex.Message);
            }
        }

        public bool modifEmployé(int idEmployé, string nom, string prenom, string login, string mdp, string droit)
        {
            try
            {
                Employé employe = bdd.Employés.SingleOrDefault(e => e.Id == idEmployé);
                if (employe != null)
                {
                    employe.Nom = nom;
                    employe.Prenom = prenom;
                    employe.Login = login;
                    employe.Mdp = mdp;
                    employe.Droit = droit;

                    bdd.SubmitChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification de l'employe : " + ex.Message);
            }
        }


        public static string HashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convertir les bytes en chaîne hexadécimale
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

    }

}

   
