using System;

namespace Model
{
    [Serializable]
    public class Agency : IIdentifiable<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        
        public Agency(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public override string ToString()
        {
            return Name;
        }

        public string getId()
        {
            return Name;
        }

        public void setId(string id)
        {
            Name = id;
        }
    }
}
