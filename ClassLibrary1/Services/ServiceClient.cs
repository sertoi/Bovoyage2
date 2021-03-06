﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage.Core.Entity;
using BoVoyage.Core.DAL;


namespace BoVoyage.Core.Services
{
    public class ServiceClient
    {
        public Client Ajouter(Client client)
        {
            var cli = new ClientSQL();
            cli.Ajouter(client);
            return client;
        }

        public Client GetClient(int id)
        {
            var service = new ClientSQL();
            return service.GetList()[id];
        }

        public void CreerClient(Client client)
        {
            var cli = new ClientSQL();
            cli.CreerClient(client);        
        }
        public List<Client> GetList()
        {
            var serviceClient = new ClientSQL();
            return serviceClient.GetList();
        }

        public void SupprimerClient(int id)
        {
            var cli = new ClientSQL();
            cli.SupprimerClient(id);
        }
        public void ModifierClient(Client client)
        {
            var cli = new ClientSQL();
            cli.ModifierClient(client);
        }
    }
}
