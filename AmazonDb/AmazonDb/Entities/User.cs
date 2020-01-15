using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonDb.Entities
{
    public class User
    {
        public string ID { get; set; }

        public User(string iD)
        {
            ID = iD;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   ID == user.ID;
        }
    }
}
