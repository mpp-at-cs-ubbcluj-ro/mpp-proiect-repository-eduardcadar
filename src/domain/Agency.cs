using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentiiDeTurism.src.domain
{
    public class Agency : IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        public Agency(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }

        public Agency(int id, string name, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
        }

        public int getId()
        {
            return this.Id;
        }

        public void setId(int id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
