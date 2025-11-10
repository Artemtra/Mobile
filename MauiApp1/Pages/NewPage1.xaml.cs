using MauiApp1.Models;
using MauiApp1.DB;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class NewPage1 : ContentPage
{
    DBFile db = new DBFile();
    public Author SelectedAuthor { get; set; }
    public NewPage1()
	{
		InitializeComponent();
        BindingContext = this;

        db.LoadFileAuthor();
        Tablichka();

    }
    public  async Task SaveAuthor()
    {
      await  db.AddAuthor(Name.Text, SecondName.Text, ThirtyName.Text, BirthDayText.Date);
        
        Tablichka();
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {

        Author model = null;

        if (sender is Button button)
        {
        
            model = Tablicka.CurrentItem as Author;
        }
        else if (sender is Label label)
        {
         
            model = label.BindingContext as Author;
        }

        if (model != null)
        {
            bool result = await DisplayAlert("Удаление",
                $"Вы уверены, что хотите удалить автора {model.SecondName} {model.Name}?", "Да", "Нет");

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
    private async void OnChangeClicked(object sender, EventArgs e)
    {

        if (SelectedAuthor != null)
        {
            await db.ChangeAuthor(SelectedAuthor.Id, Name.Text, SecondName.Text, ThirtyName.Text ,BirthDayText.Date);
        }
        else
        {
            await DisplayAlert("ОШИБКА МОЛОДОСТИ", "Не выбран айтем", "Емае");
        }
        Tablichka();
    }
    public async void Tablichka()
    {

        Tablicka.ItemsSource = await db.GetAuthorList();
        OnPropertyChanged(nameof(db.GetMovieList));
    }
   
    private void Button_Clicked_Author(object sender, EventArgs e)
    {
         SaveAuthor();
    }
   
    private async void Button_Clicked_Home(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MainPage());
    }
}