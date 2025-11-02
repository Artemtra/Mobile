using MauiApp1.DB;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MauiApp1.Pages;
public partial class NewPage2 : ContentPage
{
    DBFile db = new DBFile();
    private ListMovies _selectedMovieAuthor;
    private Author _selectedAuthor;
    private Movies _selectedMovie;

    public NewPage2()
    {
        InitializeComponent();
        BindingContext = this;
        Tablichka();
    }

    // Свойства для привязки - ИСПРАВЛЕНО: ListMoviess теперь заполняется
    public List<ListMovies> ListMoviess { get; private set; }
    public List<Author> AuthorList { get; private set; }
    public List<Movies> MovieList { get; private set; }

    public ListMovies SelectedMovieAuthor
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

    public Movies SelectedMovie
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
        
        // Сбрасываем выбор после сохранения
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

            OnPropertyChanged(nameof(AuthorList));
            OnPropertyChanged(nameof(MovieList));
            OnPropertyChanged(nameof(ListMoviess));

            PickerAuthor.ItemsSource = AuthorList;
            PickerMovie.ItemsSource = MovieList;
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
        if (SelectedMovieAuthor != null)
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
        if (SelectedMovieAuthor != null)
        {

        }
        else
        {
            await DisplayAlert("Ошибка", "Не выбран элемент", "OK");
        }
        Tablichka();
    }
}