using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIManagement.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
            Pages.Add(new PageAanmeldenVM());
            Pages.Add(new PageMedewerkersVM());
            pages.Add(new PageKlantenVM());
            pages.Add(new PageKassaVM());
            pages.Add(new PageProductVM());
            pages.Add(new PageStatistiekVM());
            CurrentPage = Pages[0];
        }

        private object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
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

        private void ChangePage(IPage page)
        {
            CurrentPage = page;
        }   
        
        //public ApplicationVM()
        //{
        //    Pages.Add(new PageKassaVM());
        //    Pages.Add(new PageKlantenVM());
        //    Pages.Add(new PageMedewerkersVM());
        //    Pages.Add(new PageProductVM());
        //    Pages.Add(new PageStatistiekVM());
        //    // Add other pages

        //    CurrentPage = Pages[0];
        //}

        //private IPage currentPage;
        //public IPage CurrentPage
        //{
        //    get { return currentPage; }
        //    set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        //}

        //private List<IPage> pages;
        //public List<IPage> Pages
        //{
        //    get
        //    {
        //        if (pages == null)
        //            pages = new List<IPage>();
        //        return pages;
        //    }
        //}

        //public ICommand ChangePageCommand
        //{
        //    get { return new RelayCommand<IPage>(ChangePage); }
        //}

        //private void ChangePage(IPage page)
        //{
        //    CurrentPage = page;
        //}
    }
}
