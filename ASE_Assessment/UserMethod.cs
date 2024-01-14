// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 01-07-2024
//
// Last Modified By : amanj
// Last Modified On : 01-10-2024
// ***********************************************************************
// <copyright file="UserMethod.cs" company="ASE_Assessment">
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
    /// Class UserMethod.
    /// </summary>
    public class UserMethod
    {
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public List<string> Parameters { get; set; }
        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>The commands.</value>
         public List<string> Commands { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMethod"/> class.
        /// </summary>
        public UserMethod()
        {
            Parameters = new List<string>();
            Commands = new List<string>();
        }

    }
}
