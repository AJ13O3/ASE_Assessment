// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 11-08-2023
//
// Last Modified By : amanj
// Last Modified On : 11-08-2023
// ***********************************************************************
// <copyright file="Program.cs" company="ASE_Assessment">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ASE_Assessment
{
    /// <summary>
    /// Class Program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new GraphicsLanguage());
        }
    }
}