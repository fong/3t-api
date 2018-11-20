using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace t3.Models
{
    public class Auth
    {
        [Key]
        public string playerID { get; set; }
        public string playerName { get; set; }
        public string passcode { get; set; }
    }
}