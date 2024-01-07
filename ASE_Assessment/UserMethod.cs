using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    public class UserMethod
    {
        public List<string> Parameters { get; set; }
        public List<string> Commands { get; set; }

        public UserMethod()
        {
            Parameters = new List<string>();
            Commands = new List<string>();
        }

    }
}
