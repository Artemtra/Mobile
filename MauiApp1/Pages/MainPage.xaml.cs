using MauiApp1.DB;
using MauiApp1.Models;
using MauiApp1.Pages;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;


namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public List<string> Genres { get; set; } = new List<string> { "Хоррор","Комедия","Романтика","Боевик"};
         
       public double OcenochkaReal {  get; set; }
       DBFile db = new DBFile();
        public Movie SelectedMovie { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            db.LoadFileMovie();
            Tablichka();
        }
        public async void SaveMovie()
        {
           await db.AddMovies(TitleText.Text, DiscriptionText.Text, DiscriptionDate.Date, OcenochkaReal, GenreList.SelectedItem.ToString(), SliderMinutes.Value);
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
                await db.ChangeMovie(SelectedMovie.Id, TitleText.Text, DiscriptionText.Text, DiscriptionDate.Date, OcenochkaReal, GenreList.SelectedItem.ToString(), SliderMinutes.Value);
            }
            else
            {
                await DisplayAlert("ОШИБКА МОЛОДОСТИ", "Не выбран айтем", "Емае");
            }
            Tablichka();
        }
        private async void OnDeleteClicked(object sender, EventArgs e)
        {

            Movie model = null;

            if (sender is Button button)
            {
              
                model = MovieListTablichka.ItemsSource as Movie;
            }
            else if (sender is Label label)
            {
               
                model = label.BindingContext as Movie;
            }

            if (model != null)
            {
                bool result = await DisplayAlert("Удаление",
                    $"Вы уверены, что хотите удалить ?", "Да", "Нет");

                if (result)
                {
                    await db.DelAuthor(model.Id - model.Id);
                    Tablichka(); 
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

