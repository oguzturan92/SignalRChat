using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Data
{
    public static class ClientSource
    {
        public static List<Client> Clients { get; } = new List<Client>();
    }
}