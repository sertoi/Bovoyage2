﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoVoyage.Core;
using System.IO;
using BoVoyage.Core.Services;
using BoVoyage.Core.Entity;
using BoVoyage.Core.DAL;
using BoVoyage.Core.Service;

namespace Bovoyage.AppConsole
{
    class Program
    {
        static ServiceClient serviceClient = new ServiceClient();
        static ServiceVoyage serviceVoyage = new ServiceVoyage();
        static ServiceDossier serviceDossier = new ServiceDossier();
        static ServicePersonne servicePersonne = new ServicePersonne();
        static ServiceParticipant serviceParticipant = new ServiceParticipant();

        static void Main(string[] args)
        {
            bool continuer = true;
            while (continuer)
            {
                var choix = AfficherMenu();
                switch (choix)
                {
                    case "1":
                        continuer = GererMenuClient();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "q":
                    case "Q":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide, l'application va fermer...");
                        continuer = false;
                        break;
                }
            }

            Console.WriteLine("Au Revoir !");
        }


        static bool GererMenuClient()
        {
            var choix = AfficherMenuClient();
            switch (choix)
            {
                case "1":
                    ListerClients();
                    return true;
                case "2":
                    return true;
                case "r":
                case "R":
                    return true;
                case "q":
                case "Q":
                    return false;
                default:
                    Console.WriteLine("Choix invalide, l'application va fermer...");
                    return false;
            }

        }
        static string AfficherMenu()
        {
            Console.Clear();
            Console.WriteLine("MENU\n");
            Console.WriteLine("1. Menu Client");
            Console.WriteLine("2. Menu Voyage");
            Console.WriteLine("3. Menu Reservation");
            Console.WriteLine("Q. Quitter");
            Console.Write("\nVotre choix: ");
            return Console.ReadLine();
        }


        static string AfficherMenuClient()
        {
            Console.Clear();
            Console.WriteLine("MENU CLIENT\n");
            Console.WriteLine("1. Lister client");
            Console.WriteLine("2. Ajouter client");
            Console.WriteLine("R. Retour");
            Console.WriteLine("Q. Quitter");
            Console.Write("\nVotre choix: ");
            return Console.ReadLine();
        }

        static string AfficherMenuVoyage()
        {
            Console.Clear();
            Console.WriteLine("MENU VOYAGE\n");
            Console.WriteLine("1. Lister voyage");
            Console.WriteLine("2. Ajouter voyage");
            Console.WriteLine("R. Retour");
            Console.WriteLine("Q. Quitter");
            Console.Write("\nVotre choix: ");
            return Console.ReadLine();
        }

        static string AfficherMenuReservation()
        {
            Console.Clear();
            Console.WriteLine("MENU RESERVATION\n");
            Console.WriteLine("1. Lister destination");
            Console.WriteLine("2. Ajouter destination");
            Console.WriteLine("R. Retour");
            Console.WriteLine("Q. Quitter");
            Console.Write("\nVotre choix: ");
            return Console.ReadLine();
        }

        static void CreerClient()
        {
            Console.Clear();
            Console.WriteLine("AJOUT D'UN CLIENT\n");

            var client = new Client();

            Console.WriteLine("Civilité:");

            client.Email = OutilsConsole.SaisirChaineObligatoire("Email:");
            client.Nom = OutilsConsole.SaisirChaineObligatoire("Nom:");
            client.Prenom = OutilsConsole.SaisirChaineObligatoire("Prénom:");
            client.Telephone = OutilsConsole.SaisirChaineObligatoire("Prénom:");
            client.DateNaissance = OutilsConsole.SaisirDateObligatoire("Date de naissance:");

            serviceClient.Ajouter(client);

            OutilsConsole.AfficherMessage("Contact ajouté !", ConsoleColor.Green);
        }


        static void CreerVoyage()
        {
            Console.Clear();
            Console.WriteLine("AJOUT D'UN VOYAGE\n");

            var voyage = new Voyage();
            var destination = new Destination();

            voyage.DateAller = OutilsConsole.SaisirDateObligatoire("Date d'aller: ");
            voyage.DateRetour = OutilsConsole.SaisirDateObligatoire("Date de retour: ");
            voyage.PlacesDisponible = OutilsConsole.SaisirEntierObligatoire("Places Disponible: ");
            voyage.PrixParPersonne = OutilsConsole.SaisirEntierObligatoire("Prix par personne: ");

            destination.Id = OutilsConsole.SaisirEntierObligatoire("Id: ");
            destination.Continent = OutilsConsole.SaisirChaineObligatoire("Continent: ");
            destination.Pay = OutilsConsole.SaisirChaineObligatoire("Pays: ");
            destination.Region = OutilsConsole.SaisirChaineObligatoire("Region: ");
            destination.Description = OutilsConsole.SaisirChaineObligatoire("Description: ");

            serviceVoyage.CreerVoyage(voyage);
            OutilsConsole.AfficherMessage("Contact ajouté !", ConsoleColor.Green);
        }


        static void CreerDossierReservation()
        {
            Console.Clear();
            Console.WriteLine("AJOUT D'UNE RESERVATION\n");

            var reservation = new DossierReservation();
            List<Voyage> voyages = new List<Voyage>();
            List<Personne> personnes = new List<Personne>();
            voyages = serviceVoyage.GetList();
            List<Client> clients = new List<Client>();
            clients = serviceClient.GetList();

            personnes = servicePersonne.GetList();

            serviceVoyage.GetVoyage(OutilsConsole.SaisirEntierObligatoire("ID voyage "));
            serviceClient.GetClient(OutilsConsole.SaisirEntierObligatoire("ID client "));
            serviceParticipant.GetParticipant(OutilsConsole.SaisirEntierObligatoire("ID participant "));
            serviceDossier.CreerDossierReservation(reservation);
            OutilsConsole.AfficherMessage("Contact ajouté !", ConsoleColor.Green);
        }




        static void ListerClients()
        {

            Console.Clear();
            Console.WriteLine("LISTE DES CLIENTS\n");

            List<Client> listeClients = serviceClient.GetList().ToList();

            for (int i = 0; i < listeClients.Count; i++)
            {
                Client client = listeClients[i];
                Console.WriteLine((i + 1) + ". " + client.Nom);
            }

            var numeroClient = int.Parse(Console.ReadLine());

            OptionClient(listeClients[numeroClient]);
        }

        static void ListerVoyages()
        {
            Console.Clear();
            Console.WriteLine("LISTE DES VOYAGES\n");

            List<Voyage> listeVoyages = serviceVoyage.GetList().ToList();

            for (int i = 0; i < listeVoyages.Count; i++)
            {
                Voyage voyage = listeVoyages[i];
                Console.WriteLine((i + 1) + ". " + voyage.Destination);
            }

            var numeroVoyage = int.Parse(Console.ReadLine());

            OptionVoyage(listeVoyages[numeroVoyage]);
        }

        static void ListerReservations()
        {

            Console.Clear();
            Console.WriteLine("LISTE DES RESERVATIONS\n");

            List<DossierReservation> listeReservations = serviceDossier.GetList().ToList();

            for (int i = 0; i < listeReservations.Count; i++)
            {
                DossierReservation reservation = listeReservations[i];
                Console.WriteLine((i + 1) + ". " + reservation.Id);
            }

            var numeroReservation = int.Parse(Console.ReadLine());
            OptionReservation(listeReservations[numeroReservation]);
        }

        static void OptionClient(Client client)
        {
            OutilsConsole.AfficherChamp(client.Nom, 10);
            OutilsConsole.AfficherChamp(client.Prenom, 10);
            //OutilsConsole.AfficherChamp(client.Civilite, 10);
            OutilsConsole.AfficherChamp(Personne.Id, 10);
            OutilsConsole.AfficherChamp(client.Email, 20);
            OutilsConsole.AfficherChamp(client.Telephone, 15);
            OutilsConsole.AfficherChamp(client.DateNaissance?.ToShortDateString(), 10);
            Console.WriteLine();
            Console.WriteLine("1. Modifier");
            Console.WriteLine("2. Supprimer");

            var option = Console.ReadLine();
            if (option == "1")
            {
                var modifOption = AfficherMenuModifierClient();
                switch (modifOption)
                {
                    case "1":
                        var nom = OutilsConsole.SaisirChaineObligatoire("Nom:");
                        client.setNom(nom);
                        break;
                    case "2":
                        var prenom = OutilsConsole.SaisirChaineObligatoire("Prenom:");
                        client.setPrenom(prenom);
                        break;
                    case "3":
                        var email = Console.ReadLine();
                        client.setemail(email);
                        break;
                    case "4":
                        var tel = Console.ReadLine();
                        client.setTelephone(tel);
                        break;
                    default:
                        return;
                }
                serviceClient.ModifierClient(client);
            }
            if (option == "2")
            {
                serviceClient.SupprimerClient(client);
            }
            return;
        }



        static void OptionVoyage(Voyage voyage)
        {
            OutilsConsole.AfficherChamp(voyage.Destination, 20);
            OutilsConsole.AfficherChamp(voyage.PlacesDisponible, 2);
            OutilsConsole.AfficherChamp(voyage.DateAller.ToString(), 10);
            OutilsConsole.AfficherChamp(voyage.DateRetour.ToString(), 10);
            OutilsConsole.AfficherChamp(voyage.setPrixParPersonne.ToString(), 4);
            //OutilsConsole.AfficherChamp(voyage.PrixParPersonne, 10);
            Console.WriteLine();
            Console.WriteLine("1. Modifier");
            Console.WriteLine("2. Supprimer");

            var option = Console.ReadLine();
            if (option == "1")
            {
                var modifOption = AfficherMenuModifierVoyage();
                switch (modifOption)
                {
                    case "1":
                        var destination = OutilsConsole.SaisirChaineObligatoire("Destination:");
                        voyage.setDestination(destination);
                        break;
                    case "2":
                        var placesdisponible = OutilsConsole.SaisirChaineObligatoire("Places Disponibles:");
                        voyage.setPlacesDisponible(placesdisponible);
                        break;
                    case "3":
                        var datealler = OutilsConsole.SaisirDateObligatoire("Date d'aller");
                        voyage.setDateAller(datealler);
                        break;
                    case "4":
                        var dateretour = OutilsConsole.SaisirDateObligatoire("Date de retour");
                        voyage.setDateRetour(dateretour);
                        break;
                    case "5":
                        var PrixParPersonne = OutilsConsole.SaisirEntierObligatoire("Prix par personne");
                        client.setPrixParPersonne(PrixParPersonne);
                        break;

                    //case "3":
                    //var prix = Console.ReadLine();
                    // client.setPrixParPersonne = prix;
                    //break;
                    default:
                        return;
                }
                serviceVoyage.ModifierVoyage(voyage);
            }
            if (option == "2")
            {
                serviceVoyage.SupprimerVoyage(voyage);
            }
            return;
        }



        static void OptionReservation(Reservation reservation)
        {
            OutilsConsole.AfficherChamp(client.Nom, 10);
            OutilsConsole.AfficherChamp(client.Prenom, 10);
            //OutilsConsole.AfficherChamp(client.Civilite, 10);
            OutilsConsole.AfficherChamp(client.Id, 10);
            OutilsConsole.AfficherChamp(client.Email, 20);
            OutilsConsole.AfficherChamp(client.Telephone, 15);
            OutilsConsole.AfficherChamp(client.DateNaissance?.ToShortDateString(), 10);
            Console.WriteLine();
            Console.WriteLine("1. Modifier");
            Console.WriteLine("2. Supprimer");

            var option = Console.ReadLine();
            if (option == "1")
            {
                var modifOption = AfficherMenuModifierClient();
                switch (modifOption)
                {
                    case "1":
                        var nom = OutilsConsole.SaisirChaineObligatoire("Nom:");
                        client.setNom(nom);


                        static string AfficherMenuModifierClient()
                        {
                            Console.Clear();
                            Console.WriteLine("MENU MODIFIER\n");
                            Console.WriteLine("1. Modifier Nom");
                            Console.WriteLine("2. Modifier Prenom");
                            Console.WriteLine("3. Modifer Email");
                            Console.WriteLine("4. Modifer Telephone");
                            Console.Write("\nVotre choix: ");
                            return Console.ReadLine();
                        }


                        static string AfficherMenuModifierVoyage()
                        {
                            Console.Clear();
                            Console.WriteLine("MENU MODIFIER\n");
                            Console.WriteLine("1. Modifier Destination");
                            Console.WriteLine("2. Modifier Places Disponibles");
                            Console.WriteLine("3. Modifer Date Aller");
                            Console.WriteLine("4. Modifer Date Retour");
                            Console.WriteLine("4. Modifer Prix Par Personne");
                            Console.Write("\nVotre choix: ");
                            return Console.ReadLine();
                        }


                }
            }
