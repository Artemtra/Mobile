using MauiApp1.DB;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace MauiApp1.Pages;
public partial class NewPage2 : ContentPage
{
    public ObservableCollection<MoviesAuthors> MainTablichka { get; set; } = new ObservableCollection<MoviesAuthors>();
    DBFile db;
    private MoviesAuthors _selectedMovieAuthor;
    private Author _selectedAuthor;
    private Movie _selectedMovie;

    public NewPage2(DBFile db)
    {
        InitializeComponent();
        this.db = db;
        Tablichka();
        BindingContext = this;
    }


    public List<MoviesAuthors> ListMoviess { get; set; }
    public List<Author> AuthorList { get; set; }
    public List<Movie> MovieList { get; set; }
    public void CraftTablichka()
    {     
        MoviesAuthors listMovies = new MoviesAuthors();
        if (ListMoviess == null || ListMoviess.Count == 0)
        {
            return;
        }
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
        MainTablichka.Add(listMovies);
        OnPropertyChanged(nameof(MainTablichka));     

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
            var getMovewAuthorListTask = db.GetMovieAuthorList();
            var getMoviesTask = db.GetMovieList();
            var getAuthorsTask = db.GetAuthorList();
            AuthorList = await getAuthorsTask;
            MovieList = await getMoviesTask;
            ListMoviess = await getMovewAuthorListTask;
            PickerAuthor.ItemsSource = AuthorList;
            PickerMovie.ItemsSource = MovieList;
            CraftTablichka();
            OnPropertyChanged(nameof(AuthorList));
            OnPropertyChanged(nameof(MovieList));
            OnPropertyChanged(nameof(ListMoviess));
            OnPropertyChanged(nameof(MainTablichka));

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