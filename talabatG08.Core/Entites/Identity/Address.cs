using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatG08.Core.Entites.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Street { get; set; }  
        public string City { get; set; }    
        public string Country { get; set; }
        
        public string AppUserId { get; set; } //Forign key
        public AppUser User { get; set; }   //Navigational prop => one
    }
}
