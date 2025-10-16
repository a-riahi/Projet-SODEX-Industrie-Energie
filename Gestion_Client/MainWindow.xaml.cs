using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using BddprojetContext;
using BiblioProjet;



namespace Gestion_Client
{
    public partial class MainWindow : Window
    {
        private Bdd database; // Instance de la connexion
        List<Client> ListeC;// ObservableCollection pour la mise à jour automatique

        public MainWindow(string ip, string port, string username, string password, string nombdd)
        {
            InitializeComponent();
            database = new Bdd(ip, port, username, password, nombdd);

            if (database != null)
            {
                ListeC = database.GetAllClient();
                DataContext = ListeC;
            }
            else
            {
                MessageBox.Show("Erreur : La connexion à la base de données a échoué.");
            }


        }






        private void ClientListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientListBox.SelectedItem != null)
            {
                btnAjouter.IsEnabled = false;
                btnModifier.IsEnabled = true;
                btnSupprimer.IsEnabled = true;
            }
            else
            {
                btnAjouter.IsEnabled = true;
                btnModifier.IsEnabled = false;
                btnSupprimer.IsEnabled = false;
            }
        }


        private void Deselc_btn(object sender, RoutedEventArgs e)
        {
            ClientListBox.SelectedIndex = -1;
        }

        private void ajt_btn_Click(object sender, RoutedEventArgs e)
        {
            string nomC = txtboxnomc.Text;
            string prenomC = txtboxprenomc.Text;
            string nomE = txtboxnome.Text;

            try
            {
                Client existant = database.GetClientParInfos(nomC, prenomC, nomE);

                if (existant != null)
                {
                    MessageBox.Show($"Ce client existe déjà : {existant.PrenomC} {existant.NomC} de l'entreprise {existant.NomE}.",
                                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Sinon, on ajoute le client
                database.ajoutClient(txtboxnome.Text, txtboxnomc.Text, txtboxprenomc.Text, txtboxinfo.Text, Convert.ToInt32( txtboxnumrue.Text), txtboxnomrue.Text, Convert.ToInt32( txtboxcp.Text)); // ⚠️ À adapter si ta méthode d’ajout prend plus que juste le nom

                MessageBox.Show("Client ajouté avec succès !");

                if (database != null)
                {
                    ListeC = database.GetAllClient();
                    ClientListBox.ItemsSource = null; // Rafraîchit la liste
                    ClientListBox.ItemsSource = ListeC;
                }
                else
                {
                    MessageBox.Show("Erreur : La connexion à la base de données n'est pas établie.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du client : " + ex.Message);
            }
        }




        private void sup_btn_Click(object sender, RoutedEventArgs e)
        {
            database.supClient(txtboxnome.Text, txtboxnomc.Text, txtboxprenomc.Text, txtboxinfo.Text,
                Convert.ToInt32(txtboxnumrue.Text), txtboxnomrue.Text, Convert.ToInt32(txtboxcp.Text));

            MessageBox.Show("Employé supprimé avec succès !");

            if (database != null)
            {
                ListeC = database.GetAllClient();
                ClientListBox.ItemsSource = ListeC;
            }
            else
            {
                MessageBox.Show("Erreur : La connexion à la base de données n'est pas établie.");
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Client ClientSelectionne = ClientListBox.SelectedItem as Client;

            if (ClientSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un employé à modifier.");
                return;
            }

            try
            {
                bool modifié = database.modifClient(
                    ClientSelectionne.Id, // on récupère l'ID ici
                    txtboxnome.Text,
                    txtboxnomc.Text,
                    txtboxprenomc.Text,
                    txtboxinfo.Text,
                    Convert.ToInt32(txtboxnumrue.Text),
                    txtboxnomrue.Text,
                    Convert.ToInt32(txtboxcp.Text)
                );

                if (modifié)
                {
                    MessageBox.Show("Employé modifié avec succès !");
                }
                else
                {
                    MessageBox.Show("La modification a échoué.");
                }

                if (database != null)
                {
                    ListeC = database.GetAllClient();
                    ClientListBox.ItemsSource = null; // Réinitialise la source pour rafraîchir
                    ClientListBox.ItemsSource = ListeC;
                }
                else
                {
                    MessageBox.Show("Erreur : La connexion à la base de données n'est pas établie.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message);
            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListeC == null) return;

            string filterText = SearchBox.Text?.ToLower().Trim() ?? "";

            var ListF = ListeC.Where(p =>
                                  (!string.IsNullOrEmpty(p.NomE) && p.NomE.ToLower().Contains(filterText)) 
                )
                .ToList();

            ClientListBox.ItemsSource = ListF;
        }

    }
}
