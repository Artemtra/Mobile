using MauiApp1.DB;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace MauiApp1.Pages;
public partial class NewPage2 : ContentPage
{
    public List<MoviesAuthors> mainTablichka { get; set; }
    DBFile db = new DBFile();
    private MoviesAuthors _selectedMovieAuthor;
    private Author _selectedAuthor;
    private Movie _selectedMovie;

    public NewPage2()
    {
        InitializeComponent();
        BindingContext = this;
        Tablichka();
    }

   
    public List<MoviesAuthors> ListMoviess { get;  set; } 
    public List<Author> AuthorList { get;  set; }
    public List<Movie> MovieList { get;  set; }
    public async void  CraftTablichka()
    {
        MoviesAuthors listMovies = new MoviesAuthors();
        if(ListMoviess != null) 
        { 
        foreach (var item in ListMoviess)
        {

            foreach (var author in AuthorList)
            {
                if (author.Id == item.IdAuthor)
                {
                    listMovies.Author = author;
                }
            }
            foreach (var author in MovieList)
            {
                if (author.Id == item.IdMovie)
                {
                    listMovies.Movie = author;
                }
            }
        }







        }
        mainTablichka.Add(listMovies);
    }
   
    public MoviesAuthors SelectedMovieAuthor
    {
        get => _selectedMovieAuthor;
        set
        {
            _selectedMovieAuthor = value;
            OnPropertyChanged();
        }
    }

    public Author SelectedAuthor
    {
        get => _selectedAuthor;
        set
        {
            if (_selectedAuthor != value)
            {
                _selectedAuthor = value;
                OnPropertyChanged(nameof(SelectedAuthor));
            }
        }
    }

    public Movie SelectedMovie
    {
        get => _selectedMovie;
        set
        {
            if (_selectedMovie != value)
            {
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }
    }

    public async void SaveAuthor()
    {
        if (SelectedAuthor == null || SelectedMovie == null)
        {
            await DisplayAlert("Ошибка", "Выберите автора и фильм", "OK");
            return;
        }

        await db.ListMoviesAdd(SelectedAuthor.Id, SelectedMovie.Id);
        
       
        SelectedAuthor = null;
        SelectedMovie = null;
        Tablichka();
    }

    public async void Tablichka()
    {
        try
        {
            AuthorList = await db.GetAuthorList();
            MovieList = await db.GetMovieList();
            ListMoviess = await db.GetMovieAuthorList();
            PickerAuthor.ItemsSource = AuthorList;
            PickerMovie.ItemsSource = MovieList;
            CraftTablichka();
            OnPropertyChanged(nameof(AuthorList));
            OnPropertyChanged(nameof(MovieList));
            OnPropertyChanged(nameof(ListMoviess));
            OnPropertyChanged(nameof(mainTablichka));
          
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка загрузки данных: {ex.Message}", "OK");
        }
    }

    private void Button_Clicked_Save(object sender, EventArgs e)
    {
        SaveAuthor();
    }

    private async void OnChangeClicked(object sender, EventArgs e)
    {
        if (SelectedAuthor == null || SelectedMovie == null)
        {

        }
        else
        {
            await DisplayAlert("Ошибка", "Не выбран элемент", "OK");
        }
         Tablichka();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (SelectedMovieAuthor == null)
        {
            await DisplayAlert("Ошибка", "Не выбран элемент", "OK");
        }
        else
        {

        }
        Tablichka();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MainPage());
    }
}