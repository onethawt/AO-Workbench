﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoteProcessViewModel.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the RemoteProcessViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AOWorkbench.ViewModels.Domain
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;

    using Caliburn.Micro;

    using SmokeLounge.AOtomation.AutoFactory;
    using SmokeLounge.AOtomation.Bus;
    using SmokeLounge.AOWorkbench.Components.Events;
    using SmokeLounge.AOWorkbench.Models.Domain;

    public class RemoteProcessViewModel : PropertyChangedBase, IRemoteProcess
    {
        #region Fields

        private readonly IBus bus;

        private readonly Guid id;

        private readonly int remoteId;

        private readonly IMiniIoC serviceLocator;

        private Guid clientId;

        private IPlayer player;

        #endregion

        #region Constructors and Destructors

        public RemoteProcessViewModel(Guid id, int remoteId, IPlayer player, IBus bus)
        {
            Contract.Requires<ArgumentNullException>(bus != null);

            this.id = id;
            this.remoteId = remoteId;
            this.player = player;
            this.bus = bus;

            this.serviceLocator = new MiniIoC();
        }

        #endregion

        #region Public Properties

        public Guid ClientId
        {
            get
            {
                return this.clientId;
            }

            set
            {
                if (value.Equals(this.clientId))
                {
                    return;
                }

                this.clientId = value;
                this.NotifyOfPropertyChange();
                this.NotifyOfPropertyChange(() => this.IsAttached);
                this.bus.Publish(new ProcessLoadedEvent(this.id));
            }
        }

        public string DisplayName
        {
            get
            {
                return this.remoteId.ToString(CultureInfo.InvariantCulture);
            }
        }

        public Guid Id
        {
            get
            {
                return this.id;
            }
        }

        public bool IsAttached
        {
            get
            {
                return this.clientId != Guid.Empty;
            }
        }

        public IPlayer Player
        {
            get
            {
                return this.player;
            }

            set
            {
                if (value == this.player)
                {
                    return;
                }

                this.player = value;
                this.NotifyOfPropertyChange();
                this.NotifyOfPropertyChange(() => this.DisplayName);
            }
        }

        public int RemoteId
        {
            get
            {
                return this.remoteId;
            }
        }

        public IMiniIoC ServiceLocator
        {
            get
            {
                return this.serviceLocator;
            }
        }

        #endregion

        #region Methods

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.bus != null);
            Contract.Invariant(this.serviceLocator != null);
        }

        #endregion
    }
}