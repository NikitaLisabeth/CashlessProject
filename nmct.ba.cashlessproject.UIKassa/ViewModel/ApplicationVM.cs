﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.UIKassa.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        //public static TokenResponse token = null;

        public ApplicationVM()
        {
            Pages.Add(new InloggenVM() );
            // Add other pages

            CurrentPage = Pages[0];
        }

        private object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }
        private int _activeUserId;

        public int ActiveUserId
        {
            get { return _activeUserId; }
            set { _activeUserId = value; OnPropertyChanged("ActiveUserId");}
        }
       
        private List<IPage> pages;
        public List<IPage> Pages
        {
            get
            {
                if (pages == null)
                    pages = new List<IPage>();
                return pages;
            }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        public void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }
}
