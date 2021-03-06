// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeManager.cs" company="SmokeLounge">
//   Copyright � 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ThemeManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AOWorkbench.Controls
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics.Contracts;
    using System.Windows;

    [Export(typeof(IThemeManager))]
    public class ThemeManager : IThemeManager
    {
        #region Fields

        private readonly ResourceDictionary themeResources;

        #endregion

        #region Constructors and Destructors

        public ThemeManager()
        {
            this.themeResources = new ResourceDictionary
                                      {
                                          Source =
                                              new Uri(
                                              "pack://application:,,,/Resources/Generic.xaml")
                                      };
        }

        #endregion

        #region Public Methods and Operators

        public ResourceDictionary GetThemeResources()
        {
            return this.themeResources;
        }

        #endregion

        #region Methods

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.themeResources != null);
        }

        #endregion
    }
}