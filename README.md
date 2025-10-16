<div align="center">
  <img src="Sodex_pic.png" alt="Logo Sodex" width="200"/>
  <h1>PROJET SODEX INDUSTRIE ENERGIE</h1>
  <p>
    Application de gestion pour une entreprise spécialisée dans la fabrication de matelas isolants sur-mesure.
  </p>
</div>

---

## 🚀 Description du Projet

[cite_start]Ce projet a pour but de résoudre l'augmentation exponentielle de la charge de travail et des commandes de l'entreprise Sodex Industrie Énergie[cite: 25, 28].

[cite_start]La solution proposée est un système modulaire basé sur une base de données centralisée, permettant d'optimiser la prise de mesures sur site et le traitement des commandes[cite: 29, 30].

[cite_start]Le système se compose de deux applications principales[cite: 33, 34]:
* [cite_start]**Application Windows (WPF)** : Application de bureau pour l'administrateur et les employés, utilisée pour la gestion de la base de données et la création de plans[cite: 33, 96].
* [cite_start]**Application Mobile (Web)** : Application pour les employés sur site afin d'effectuer la prise de mesures chez le client[cite: 34, 41].

## 🛠️ Stack Technique et Environnement

Le projet est développé en utilisant les technologies suivantes :

| Catégorie | Technologie | Rôle dans le projet | Source |
| :--- | :--- | :--- | :--- |
| **Frontend/Desktop** | C# .NET / WPF (Windows Presentation Foundation) | [cite_start]Développement de l'application Windows [cite: 72, 73, 179, 188] | [cite_start]C# [cite: 72, 179] [cite_start]/ WPF [cite: 73, 188] |
| **Backend/Base de Données** | MySQL | [cite_start]Gestion et stockage des données (Clients, Employés, Matériaux, Plans, etc.) [cite: 78, 175, 180] | [cite_start]MySQL [cite: 180] [cite_start]/ SQL [cite: 78] |
| **Accès BDD** | DLL (Bibliothèque de classes) | [cite_start]Module de connexion partagé et gestion des tables [cite: 181, 713] | [cite_start]DLL [cite: 181, 713] |
| **Web** | HTML / CSS / PHP | [cite_start]Développement de l'application mobile de prise de mesures [cite: 71, 75, 77] | [cite_start]HTML, CSS, PHP [cite: 71, 75, 77] |
| **Outils** | Visual Studio 2022, Paradigm | [cite_start]Environnement de développement et modélisation UML [cite: 62, 63, 178, 182] | [cite_start]Visual Studio [cite: 178] [cite_start]/ Paradigm [cite: 63, 182] |

## 👤 Ma Contribution (RIAHI Ayoub)

[cite_start]En tant qu'**Étudiant 3**, ma responsabilité principale était de développer une partie essentielle de l'application Windows[cite: 87, 165].

| Module | Tâches spécifiques |
| :--- | :--- |
| **Gestion Clients** | [cite_start]Création de la fenêtre `Gestion_Client` et du module CRUD (Ajout, Modification, Suppression) pour la table `Client`[cite: 167, 197]. |
| **Gestion Employés** | [cite_start]Création de la fenêtre `Gestion_Employe` et du module CRUD (Ajout, Modification, Suppression) pour la table `Employé`[cite: 167, 198]. |
| **Paramètres** | [cite_start]Création de la fenêtre `Settings` pour la configuration de la chaîne de connexion à la base de données (Host, Port, User, Password, DataBase)[cite: 168, 372]. |
| **Intégration** | [cite_start]Utilisation de la DLL fournie par l'Étudiant 1 pour la connexion à la BDD[cite: 170, 713]. |

### Détails d'Implémentation
* [cite_start]Les interfaces `Gestion_Client` et `Gestion_Employe` sont basées sur un formulaire à onglets, avec Data Binding pour la validation en temps réel et la mise à jour dynamique des listes[cite: 311, 312].
* [cite_start]Les opérations CRUD sont implémentées via une classe `Gestion` qui utilise la DLL de connexion[cite: 383, 384].

## 👥 Équipe de Projet

[cite_start]Le projet a été réalisé en équipe avec la répartition des tâches suivante[cite: 87]:

* **Étudiant 1 (PINEL Simon)** : Installation/Configuration Serveur MySQL, création de la BDD, installation du réseau distant, gestion des tables Plan/Matériau/Commandes.
* **Étudiant 2 (Auvray Tom)** : Fenêtres de gestion des matériaux et plans de fabrication, création d'un plan de fabrication.
* **Étudiant 3 (RIAHI Ayoub)** : **Gestion des Clients et Employés, Fenêtre de Paramètres (Settings)**.
* **Étudiant 4 (AHN Argan)** : Création des vues pour l'acquisition des mesures (Web et WPF), accès BDD pour l'enregistrement des mesures.

## 🔗 Architecture et Déploiement

[cite_start]Le système suit une architecture client-serveur[cite: 39]:

1.  [cite_start]**Serveur BDD** : Héberge la base de données (BDD) et est accessible via le réseau local[cite: 45, 47, 135].
2.  [cite_start]**Application Windows** : Accède au Serveur BDD via le réseau local (Ethernet)[cite: 47, 131, 133].
3.  [cite_start]**Application Mobile** : Accède au Serveur BDD via internet (WWW) pour la prise de mesures sur site[cite: 40, 41, 132, 137].

| Composant | Environnement d'exécution |
| :--- | :--- |
| Application Windows | [cite_start]Windows 10 minimum [cite: 134] |
| Serveur BDD | [cite_start]Windows [cite: 136] |
| Application Mobile | [cite_start]Android & Appli Web [cite: 138] |
