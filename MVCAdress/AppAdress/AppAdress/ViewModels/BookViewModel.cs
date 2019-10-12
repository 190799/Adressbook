using AppAdress.Models;
using AppAdress.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace AppAdress.ViewModels
{
    public class BookViewModel:BaseViewModel
    {
        #region Atttributes
        private ApiService apiservice;
        private ObservableCollection<Book> books;
        #endregion

        #region Properties
        public ObservableCollection<Book> Books
        {
            get { return this.books; }
            set { SetValue(ref this.books, value); }
        }
        #endregion

        #region Constructor
        public BookViewModel()
        {
            this.apiservice = new ApiService();
            this.cargar();
        }

        private async void cargar()
        {
            
        }
        #endregion

        #region Method
        private async void LoadBooks()
        {
            var connection = await apiservice.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                 "Error en Internet",
                 connection.Message,
                 "aceptar"
                    );
                return;
            }
            var response = await apiservice.GetList<Book>(
                "http://localhost:49318/",
                "api/",
                "Books"
                );
            if(!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Service Book Error",
                     response.Message,
                     "accept");
                return;
            }
            MainViewModel mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ListBook = (List<Book>)response.Result;
            this.Books = new ObservableCollection<Book>(this.ToBookCollect());
        }

        private IEnumerable<Book> ToBookCollect()
        {
            ObservableCollection<Book> collect = new ObservableCollection<Book>();
            MainViewModel main = MainViewModel.GetInstance();
            foreach(var lista in main.ListBook)
            {
                Book book = new Book();
                book.BookID = lista.BookID;
                book.Name = lista.Name;
                book.Contact = lista.Contact;
                collect.Add(book);
        
            }
            return (collect);
        }
        #endregion

    }
}