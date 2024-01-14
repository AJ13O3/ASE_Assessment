// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 01-12-2024
//
// Last Modified By : amanj
// Last Modified On : 01-14-2024
// ***********************************************************************
// <copyright file="CommandException.cs" company="ASE_Assessment">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    /// <summary>
    /// Class CommandException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    public class CommandException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CommandException (string message) : base(message)
        {

        }
    }
}
