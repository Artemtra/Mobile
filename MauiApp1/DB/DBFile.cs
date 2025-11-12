

using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace MauiApp1.DB
{
    public class DBFile
    {
        private List<Author> authorList = new List<Author>();
        private List<Movie> moviesList = new List<Movie>();
        private List<MoviesAuthors> listMovies = new List<MoviesAuthors>();
        private List<int> ints = new List<int> { 0, 0, 0 };


        public DBFile()
        {
            LoadDis();

        }



        public async Task ListMoviesAdd(int idAuthor, int idMovies)
        {
            MoviesAuthors movies = new MoviesAuthors();
            movies.Id = ints[2];
            movies.IdAuthor = idAuthor;
            movies.IdMovie = idMovies;

            listMovies.Add(movies);

            ints[2] = ints[2] + 1;

            await SaveFileListMovie();
            await SaveFileDiscriminant();
            //ListMovies movies = new ListMovies();
            //movies.Id = ints[2];
            //movies.IdAuthor = idAuthor;
            //movies.IdMovies = idMovies;

            //ints[2] = ints[2] + 1;
            //listMovies.Add(movies);
            //await SaveFileListMovie();
            //await SaveFileDiscriminant();

        }

        public async Task ListMoviesChange(int id, int idAuthor, int idMovies)
        {
            MoviesAuthors movies = new MoviesAuthors();
            movies.Id = id;
            movies.IdAuthor = idAuthor;
            movies.IdMovie = idMovies;

            listMovies.Insert(id, movies);

            await SaveFileListMovie();
            await SaveFileDiscriminant();

        }
        public async Task ListMoviesDel(int id)
        {
            Movie movies = new Movie();
            foreach (Movie author in moviesList)
            {

                if (author.Id == id)
                {

                    movies = author;
                }


            }
            moviesList.RemoveAt(id - id);
            await SaveFileListMovie();
        }
        public async Task ChangeMovie(int id, string name, string description, DateTime date, double ocenochka, string genre, double minutes)
        {
            Movie movies = new Movie();
            movies.Id = id;
            movies.Name = name;
            movies.Description = description;
            movies.Date = date;
            movies.Genre = genre;
            movies.Minutes = minutes;
            movies.Ocenochka = ocenochka;

            int a = 0;
            int b = 0;
            foreach (Movie author in moviesList)
            {

                if (author.Id == id)
                {

                    b = a;
                }
                a++;

            }
            moviesList[b] = movies;
            await SaveFileMovie();

        }
        public async Task ChangeAuthor(int id, string name, string secondName, string thrityName, DateTime birthDay, string gender, double ocenochka,bool isalive)
        {
            int a = 0;
            int b = 0;
            Author authors = new Author();
            authors.Id = id;
            authors.Name = name;
            authors.SecondName = secondName;
            authors.BirthDay = birthDay;
            authors.ThrityName = thrityName;
            authors.Gender = gender;
            authors.Ocenochka = ocenochka;
            authors.IsAlive = isalive;
            foreach (Author author in authorList)
            {

                if (author.Id == id)
                {
                    b = a;
                }
                a++;

            }
            authorList[b] = authors;
            await SaveFileAuthor();
        }
        public async void LoadDis()
        {

            await LoadDiscriminant();
            await LoadFileAuthor();
            await LoadFileMovie();
            await LoadFileListMovie();

        }

        public async Task SaveFileDiscriminant()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "discriminant2.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, ints);
            }
        }
        public async Task LoadDiscriminant()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "discriminant2.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);
                ints = JsonSerializer.Deserialize<List<int>>(a);

            }

        }

        public async Task<List<MoviesAuthors>> GetMovieAuthorList()
        {
            await Task.Delay(1000);
            return listMovies.ToList();
        }
        public async Task<List<Author>> GetAuthorList()
        {
            await Task.Delay(1000);
            return authorList.ToList();
        }

        public async Task<List<Movie>> GetMovieList()
        {
            await Task.Delay(1000);
            return moviesList;
        }

        public async Task DelAuthor(int id)
        {

            authorList.RemoveAt(id);
            await SaveFileAuthor();
        }
        public async Task DelMovie(int id)
        {

            await Task.Delay(1000);
            moviesList.RemoveAt(id);
            await SaveFileMovie();
        }



        public async Task AddAuthor(string name, string secondName, string thrityName, DateTime birthDay , string gender ,double ocenochka,bool isalive)
        {
            Author author = new Author();
            author.Id = ints[0];
            author.Name = name;
            author.SecondName = secondName;
            author.ThrityName = thrityName;
            author.BirthDay = birthDay;
            author.Gender = gender;
            author.Ocenochka = ocenochka;
            author.IsAlive = isalive;
            authorList.Add(author);
            ints[0] = ints[0] + 1;
            await SaveFileDiscriminant();
            await SaveFileAuthor();
        }
        public async Task AddMovies(string name, string description, DateTime date, double ocenochka, string genre,double minutes)
        {
            Movie movies = new Movie();
            movies.Id = ints[1];
            movies.Name = name;
            movies.Description = description;
            movies.Date = date;
            movies.Ocenochka = ocenochka;
            movies.Genre = genre;
            movies.Minutes = minutes;
            moviesList.Add(movies);
            ints[1] = ints[1] + 1;
            await SaveFileDiscriminant();
            await SaveFileMovie();
        }
        public async Task AddMoviesList(int IdAuthor, int IdMovies, string Title, string FirstName, string LastName, string SecondName)
        {

            MoviesAuthors listMoviesAdd = new MoviesAuthors();


            listMovies.Add(listMoviesAdd);
            ints[2] = ints[2] + 1;
            await SaveFileDiscriminant();
            await SaveFileListMovie();


        }

        public async Task SaveFileMovie()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "movie2.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, moviesList);
            }
        }

        public async Task LoadFileMovie()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "movie2.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);
                moviesList = JsonSerializer.Deserialize<List<Movie>>(a);

            }

        }
        public async Task SaveFileListMovie()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "listmovie2.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, listMovies);
            }
        }

        public async Task LoadFileListMovie()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "listmovie2.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);
                listMovies = JsonSerializer.Deserialize<List<MoviesAuthors>>(a);

            }

        }
        public async Task SaveFileAuthor()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "author3.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, authorList);
            }
            LoadDis();
        }
        public async Task LoadFileAuthor()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "author3.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);

                authorList = JsonSerializer.Deserialize<List<Author>>(a);

            }

        }


    }
}




//1. Придумать тему вашего приложения и выделить минимум 2 объекта связанных между собой, с которыми будет работать приложение(Пример: книга, автор).
//2. Разработать класс, имитирующий работу с базой данных.
//	2.1. Для каждого объекта должны быть следующие методы:
//		1) Получения списка элементов.
//		2) Получение элемента по id.
//		3) Добавление элемента.
//		4) Редактирование элементе.
//		5) Удаление элемента по id.
//	2.2. Все методы в классе должны иметь возможность работать асинхронно.
//	2.3. Данные сохранять в файл (файлы).
//3. Сделать вывод данных используя все 3 вида выводов списка (ListView, CollectionView, CarouselView).
//4. Сделать добавление, редактирование, удаление для всех объектов.
//	4.1. При добавлении и редактировании объектов должны использоваться в сумме как минимум 5 разных видов элементов ввода (Stepper, Switch, Slider и т.д.).
//	4.2. Должна быть возможность отмены редактирования.При отмене, значения элемента должны вернуться к исходным.
//	4.3. Должно быть запрещено удаление связанных друг с другом элементов.
//Задание делать в копии своего приложения с простой навигацией.
//1. Перестроить всю навигацию в приложении на Shell.
//1.1. Использовать все три элемента для навигации в Shell: FlyoutItem, Tab, TabBar
//1.2. Переходы на страницы с передачей данных сделать разными способами.
//2. Стилизовать FlyoutItem, Tab, TabBar (цвет, шрифт и т.д.).
//3. Создать свои ContentView и найти им применение в приложении.
//4. У всплывающего меню сделать в AppShell свои Header и Footer.
//5. Сделать страницы авторизации и регистрации.
//5.1. При запуске приложения первой страницей открывается авторизация.
//5.2. Находясь на этих страницах не должны отображаться всплывающее меню.
//6. Сделать загрузку пользователем файлов (изображений, документов и т.д.).

