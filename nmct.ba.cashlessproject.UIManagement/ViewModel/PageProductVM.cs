using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.UIManagement.ViewModel
{
    class PageProductVM : ObservableObject, IPage
    {

        public string Name
        {
            get { return "Producten"; }
        }

         public PageProductVM()
        {
            if (ApplicationVM.token != null)
            {
                GetProducten();
            }

        }
         private ObservableCollection<Products> _products;

         public ObservableCollection<Products> Products
         {
             get { return _products; }
             set { _products = value; OnPropertyChanged("Products"); }
         }

         private async void GetProducten()
         {
             using (HttpClient client = new HttpClient())
             {
                 client.SetBearerToken(ApplicationVM.token.AccessToken);
                 HttpResponseMessage response = await client.GetAsync("http://localhost:1817/api/product");
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     Products = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                 }
             }
         }

         private Products _selectedProduct;
         public Products SelectedProduct
         {
             get { return _selectedProduct; }
             set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
         }

         public async void DeleteProduct()
         {
             using (HttpClient client = new HttpClient())
             {
                 client.SetBearerToken(ApplicationVM.token.AccessToken);

                 HttpResponseMessage response = await client.DeleteAsync("http://localhost:1817/api/product/" + SelectedProduct.Id);
                 if (response.IsSuccessStatusCode)
                 {
                     GetProducten();
                 }
             }
         }


         private string _status;
         public string Status
         {
             get { return _status; }
             set { _status = value; OnPropertyChanged("Status"); }
         }

         private async void UpdateProduct()
         {
             using (HttpClient client = new HttpClient())
             {
                 client.SetBearerToken(ApplicationVM.token.AccessToken);

                 Products kl = SelectedProduct;
                 string input = JsonConvert.SerializeObject(kl);
                 HttpResponseMessage response = await client.PutAsync("http://localhost:1817/api/product", new StringContent(input, Encoding.UTF8, "application/json"));
                 if (response.IsSuccessStatusCode)
                 {
                     GetProducten();
                 }
             }
         }

         public async void AddProduct()
         {
             Products newProduct = SelectedProduct;
             using (HttpClient client = new HttpClient())
             {
                 client.SetBearerToken(ApplicationVM.token.AccessToken);

                 string emp = JsonConvert.SerializeObject(newProduct);
                 HttpResponseMessage response = await client.PostAsync("http://localhost:1817/api/product", new StringContent(emp, Encoding.UTF8, "application/json"));
                 if (response.IsSuccessStatusCode)
                 {
                     GetProducten();
                 }
             }
         }

         public void SetStatusToUpdate()
         {
             Status = "Update";
         }
         public ICommand DeleteCommand
         {
             get { return new RelayCommand(DeleteProduct); }
         }
         public ICommand SetStatusToUpdateCommand
         {
             get { return new RelayCommand(SetStatusToUpdate); }
         }

         public void SetStatusToAdd()
         {
             Products product = new Products();
             Products.Add(product);
             SelectedProduct = product;

             Status = "Add";
         }

         public ICommand SetStatusToAddCommand
         {
             get { return new RelayCommand(SetStatusToAdd); }
         }

         public void ClickSave()
         {
             if (Status == "Update")
             {
                 UpdateProduct();
             }

             else if (Status == "Add")
             {
                 AddProduct();
             }
         }

         public ICommand ClickSaveCommand
         {
             get { return new RelayCommand(ClickSave); }
         }

    }
}
