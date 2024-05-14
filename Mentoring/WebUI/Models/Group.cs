using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class Group
    {
        public string GroupName { get; set; }
        public List<Client> Clients { get; } = new  List<Client>();
    }
}