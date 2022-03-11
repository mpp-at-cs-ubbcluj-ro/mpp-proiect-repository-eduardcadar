using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentiiDeTurism.src.domain
{
    public class Agency : IIdentifiable<int>
    {
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        
        public Agency(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public Agency(int id, string name, string password)
        {
            this.id = id;
            this.name = name;
            this.password = password;
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return "id=" + this.id + "; name=" + this.name + "; password=" + this.password;
        }
    }
}
