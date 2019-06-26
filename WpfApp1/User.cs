using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WpfApp1
{
    public class User: Entity
    {
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
