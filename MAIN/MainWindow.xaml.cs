using BiblioProjet;
using System;
using System.Collections.Generic;
using System.Windows;
using BddprojetContext;

namespace MAIN
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ret;
        Client tour = new Client();
        Bdd database;
        List<Employé> ListeE;
        List<Client> ListeC;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'initialisation de la fenêtre principale :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuConn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ip = Properties.Settings.Default.AdresseIP;
                string port = Properties.Settings.Default.Port;
                string username = Properties.Settings.Default.Username;
                string password = Properties.Settings.Default.Password;
                string nombdd = Properties.Settings.Default.bdd;

                database = new Bdd(ip, port, username, password, nombdd);

                if (database.TesterConnexion())
                {
                    MenuGestC.IsEnabled = true;
                    MenuGestE.IsEnabled = true;
                    MessageBox.Show("Connexion Reussi.", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Connexion échouée. Vérifiez les paramètres.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void MenuGestE_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string ip = Properties.Settings.Default.AdresseIP;
                string port = Properties.Settings.Default.Port;
                string username = Properties.Settings.Default.Username;
                string password = Properties.Settings.Default.Password;
                string nombdd = Properties.Settings.Default.bdd;

                EmployE.MainWindow E = new EmployE.MainWindow(ip, port, username, password, nombdd);
                E.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture de la fenêtre de gestion des employés :\n{ex.Message}", "Gestion Employé", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuGestC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ip = Properties.Settings.Default.AdresseIP;
                string port = Properties.Settings.Default.Port;
                string username = Properties.Settings.Default.Username;
                string password = Properties.Settings.Default.Password;
                string nombdd = Properties.Settings.Default.bdd;

                Gestion_Client.MainWindow T = new Gestion_Client.MainWindow(ip, port, username, password, nombdd);
                T.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture de la fenêtre de gestion des clients :\n{ex.Message}", "Gestion Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuPara_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings.MainWindow paramBDD = new Settings.MainWindow();

                if (paramBDD.TxtBoxAdresseIP != null && paramBDD.TxtBoxPort != null &&
                    paramBDD.TxtBoxUsername != null && paramBDD.PasswordBox != null &&
                    paramBDD.TxtBoxbdd != null)
                {
                    paramBDD.TxtBoxAdresseIP.Text = Properties.Settings.Default.AdresseIP;
                    paramBDD.TxtBoxPort.Text = Properties.Settings.Default.Port;
                    paramBDD.TxtBoxUsername.Text = Properties.Settings.Default.Username;
                    paramBDD.PasswordBox.Password = Properties.Settings.Default.Password;
                    paramBDD.TxtBoxbdd.Text = Properties.Settings.Default.bdd;
                }

                if (paramBDD.ShowDialog() == true)
                {
                    Properties.Settings.Default.AdresseIP = paramBDD.TxtBoxAdresseIP.Text;
                    Properties.Settings.Default.Port = paramBDD.TxtBoxPort.Text;
                    Properties.Settings.Default.Username = paramBDD.TxtBoxUsername.Text;
                    Properties.Settings.Default.Password = paramBDD.PasswordBox.Password;
                    Properties.Settings.Default.bdd = paramBDD.TxtBoxbdd.Text;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture ou de la sauvegarde des paramètres :\n{ex.Message}", "Paramètres", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Lst_Clients_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
