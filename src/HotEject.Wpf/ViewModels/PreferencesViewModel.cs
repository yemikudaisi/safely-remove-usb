﻿using Caliburn.Micro;
using HotEject.Wpf.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotEject.Wpf.ViewModels
{
    public class PreferencesViewModel : Conductor<object>
    {
        readonly IList<Screen> _screenCollection;
        Screen _selectedScreen;

        public PreferencesViewModel()
        {
            DisplayName = Properties.Resources.Preferences;
            _screenCollection = new List<Screen>();
            _screenCollection.Add(new GeneralPreferencesViewModel());
            _screenCollection.Add(new HotKeysPreferencesViewModel());
            SelectedScreen = _screenCollection[0];
            NotifyOfPropertyChange(() => ScreenCollection );
            Deactivated += (s, e) =>
            {
                Properties.Settings.Default.Save();
                IoC.Get<IEventAggregator>().PublishOnUIThread(new PreferenceChangeEvent());
            };
        }

        public IList<Screen> ScreenCollection
        {
            get
            {
                return _screenCollection;
            }
        }

        public Screen SelectedScreen
        {
            get
            {
                return _selectedScreen;
            }

            set
            {
                _selectedScreen = value;
                ActivateItem(_selectedScreen);
                NotifyOfPropertyChange(() => SelectedScreen);
            }
        }
 
    }
}
