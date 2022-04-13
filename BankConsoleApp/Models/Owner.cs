using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Models
{
    public class Owner
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public Owner(long id, string name)
        {
            if (string.IsNullOrWhiteSpace(name) || id < 1)
                throw new Exception("Invalid Owner Info");
            Name = name;
            Id = id;
        }

    }
}
