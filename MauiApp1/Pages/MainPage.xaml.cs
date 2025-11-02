using MauiApp1.DB;
using MauiApp1.Models;
using MauiApp1.Pages;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        DBFile db = new DBFile();
        public Movies SelectedMovie { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            db.LoadFileMovie();
            Tablichka();
        }
        public void SaveMovie()
        {
            db.AddMovies(TitleText.Text, DiscriptionText.Text, DiscriptionDate.Date);
            Tablichka();
            
        }
  
        public  async void Tablichka()
        {
            
            MovieListTablichka.ItemsSource = await db.GetMovieList();
            OnPropertyChanged(nameof(db.GetMovieList));
        }
        private void Button_Clicked_Movie(object sender, EventArgs e)
        {
            SaveMovie();
        }
        private async void OnChangeClicked(object sender, EventArgs e)
        {
           
            if (SelectedMovie != null)
            {
                await db.ChangeMovie(SelectedMovie.Id, TitleText.Text, DiscriptionText.Text, DiscriptionDate.Date);
            }
            else
            {
                await DisplayAlert("ОШИБКА МОЛОДОСТИ", "Не выбран айтем", "Емае");
            }
            Tablichka();
        }
        private async void OnDeleteClicked(object sender, EventArgs e)
        {

            Movies model = null;

            // Определяем, откуда пришел вызов
            if (sender is Button button)
            {
                // Если вызвано кнопкой - берем текущий элемент CarouselView
                model = MovieListTablichka.ItemsSource as Movies;
            }
            else if (sender is Label label)
            {
                // Если вызвано кликом на Label
                model = label.BindingContext as Movies;
            }

            if (model != null)
            {
                bool result = await DisplayAlert("Удаление",
                    $"Вы уверены, что хотите удалить ?", "Да", "Нет");

                if (result)
                {
                    await db.DelAuthor(model.Id - model.Id);
                    Tablichka(); // Обновляем данные
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Не выбран автор для удаления", "OK");
            }
        }
        public async void Button_Clicked_To_Page2(object sender, EventArgs e)
        {   
            await Navigation.PushModalAsync(new NewPage1());
        }
        public async void Button_Clicked_To_Page3(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewPage2());
        }
    }

    }

