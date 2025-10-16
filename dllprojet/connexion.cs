using BddprojetContext;
using System;

namespace dllprojet
{
    public class connexion
    {
        protected BddprojetDataContext bdd = null;

        public connexion(string serveurIp, string port, string user, string mdp, string nom_bb)
        {
            try
            {
                
                string connectionString = $"User Id={user};Password={mdp};Host={serveurIp};Port={port};Database={nom_bb};Persist Security Info=True";

                bdd = new BddprojetDataContext(connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la connexion à la base de données : {ex.Message}", ex);
            }
        }
    }
}