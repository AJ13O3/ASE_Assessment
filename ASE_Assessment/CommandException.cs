﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    public class CommandException : Exception
    {
        public CommandException(string message) : base(message)
        {

        }

        // You can add additional constructors or properties if needed
    }
}
