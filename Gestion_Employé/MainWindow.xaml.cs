using BddprojetContext;
using BiblioProjet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace EmployE
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bdd database; // Instance de la connexion
        List<Employé> ListeE;// ObservableCollection pour la mise à jour automatique

        public MainWindow(string ip, string port, string username, string password, string nombdd)
        {
            InitializeComponent();
            database = new Bdd(ip, port, username, password, nombdd);

            if (database != null)
            {
                ListeE = database.GetAllEmployé();
                DataContext = ListeE;
            }
            else
            {
                MessageBox.Show("Erreur : La connexion à la base de données a échoué.");
            }


        }

        private void EmployeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeListBox.SelectedItem != null)
            {
                Employé emp = (Employé)EmployeListBox.SelectedItem;

                // Affichage du mot de passe dans le PasswordBox
                txtboxmdp.Password = emp.Mdp;
                affich_btn.IsEnabled = false;
                btnAjouter.IsEnabled = false;
                btnModifier.IsEnabled = true;
                btnSupprimer.IsEnabled = true;
            }
            else
            {
                // Réinitialiser le PasswordBox
                txtboxmdp.Password = string.Empty;

                btnAjouter.IsEnabled = true;
                btnModifier.IsEnabled = false;
                btnSupprimer.IsEnabled = false;
            }
        }



        private void Deselc_btn(object sender, RoutedEventArgs e)
        {
            EmployeListBox.SelectedIndex = -1;
            affich_btn.IsEnabled = true;
        }

        private void ajt_btn_Click(object sender, RoutedEventArgs e)
        {
            string login = txtboxlogin.Text;

            Employé existant = database.GetEmployeParLogin(login);

            if (existant != null)
            {
                string nom = existant.Nom;
                string prenom = existant.Prenom;

                MessageBox.Show($"Ce login est déjà utilisé par {prenom} {nom}.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string motDePasse = txtboxmdp.Password;

            // Sinon, on ajoute l'employé
            database.ajoutEmployé(txtboxnom.Text, txtboxprenom.Text, login, Bdd.HashSHA256(motDePasse), ComboBoxDroits.SelectedValue.ToString());

            MessageBox.Show("Employé ajouté avec succès !");

            ResetChamps();

            if (database != null)
            {
                ListeE = database.GetAllEmployé();
                EmployeListBox.ItemsSource = ListeE;
            }
            else
            {
                MessageBox.Show("Erreur : La connexion à la base de données n'est pas établie.");
            }
        }



        private void sup_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxDroits.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner un droit dans la liste.");
                return;
            }

            try
            {
                string motDePasse = txtboxmdp.Password;

                database.supEmployé(txtboxnom.Text, txtboxprenom.Text, txtboxlogin.Text, motDePasse, ComboBoxDroits.SelectedValue.ToString());

                MessageBox.Show("Employé supprimé avec succès !");

                ResetChamps();

                if (database != null)
                {
                    ListeE = database.GetAllEmployé();
                    EmployeListBox.ItemsSource = ListeE;
                }
                else
                {
                    MessageBox.Show("Erreur : La connexion à la base de données n'est pas établie.");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Code SQL Server pour violation de contrainte (clé étrangère)
                {
                    MessageBox.Show("Impossible de supprimer cet employé car il est lié à d'autres données (ex : commandes, projets, etc.).");
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur inattendue est survenue : " + ex.Message);
            }
        }


        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Employé employeSelectionne = EmployeListBox.SelectedItem as Employé;

            if (employeSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un employé à modifier.");
                return;
            }

            try
            {
                string motDePasse = txtboxmdp.Password;

                bool modifié = database.modifEmployé(
                    employeSelectionne.Id, // on récupère l'ID ici
                    txtboxnom.Text,
                    txtboxprenom.Text,
                    txtboxlogin.Text,
                    Bdd.HashSHA256(motDePasse),
                    ComboBoxDroits.SelectedValue?.ToString() ?? ""
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
                    ListeE = database.GetAllEmployé();
                    EmployeListBox.ItemsSource = null; // Réinitialise la source pour rafraîchir
                    EmployeListBox.ItemsSource = ListeE;
                    ResetChamps();
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
            if (ListeE == null) return;

            string filterText = SearchBox.Text?.ToLower().Trim() ?? "";

            var ListF = ListeE
                .Where(p =>
                    (!string.IsNullOrEmpty(p.Nom) && p.Nom.ToLower().Contains(filterText)) ||
                    (!string.IsNullOrEmpty(p.Prenom) && p.Prenom.ToLower().Contains(filterText))
                )
                .ToList();

            EmployeListBox.ItemsSource = ListF;
        }

        private void ResetChamps()
        {
            txtboxnom.Clear();
            txtboxprenom.Clear();
            txtboxlogin.Clear();
            txtboxmdp.Clear();
            ComboBoxDroits.SelectedIndex = -1; // Désélectionner
        }

        private bool isPasswordVisible = false;

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible)
            {
                // Repasser en mode masqué
                txtboxmdpVisible.Visibility = Visibility.Collapsed;
                txtboxmdp.Visibility = Visibility.Visible;
                txtboxmdp.Password = txtboxmdpVisible.Text;
                isPasswordVisible = false;
            }
            else
            {
                // Afficher le mdp en clair
                txtboxmdpVisible.Visibility = Visibility.Visible;
                txtboxmdp.Visibility = Visibility.Collapsed;
                txtboxmdpVisible.Text = txtboxmdp.Password;
                isPasswordVisible = true;
            }
        }




    }

}
