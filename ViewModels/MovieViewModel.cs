using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels
{
        public class MovieViewModel
    {
        public ObservableCollection<Movie> Movies { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public Movie SelectedMovie { get; set; }
        public object TitleOfNewMovie { get; private set; }

        public RelayCommand AddMovieCommand { get; set; }
        public RelayCommand DeleteMovieCommand { get; set; }
        public RelayCommand MovieToJsonCommand { get; set; }
        public MovieViewModel()
        {
            Movies = new ObservableCollection<Movie>();
            AddMovieCommand = new RelayCommand(AddMovie);
            DeleteMovieCommand = new RelayCommand(DeleteMovie);
            MovieToJsonCommand = new RelayCommand(MovieToJson);
            MovieLoadJson();
        }

    

        private void AddMovie()
        {
            Movie NewMovie = new Movie
            {
                Title = Title,
                Genre = Genre,
            };
            Movies.Add(NewMovie);
        }
        private void DeleteMovie()
        {
            Movies.Remove(SelectedMovie);
        }



        private void MovieToJson()
        {

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Movie[]));

            using (FileStream fs = new FileStream("movie.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, Movies.ToArray());
            }
        }
        private void MovieLoadJson()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Movie[]));
            using (FileStream fs = new FileStream("movie.json", FileMode.OpenOrCreate))
            {
                Movie[] newmovie = (Movie[])jsonFormatter.ReadObject(fs);

                foreach (Movie p in newmovie)
                {
                    Movies.Add(p);
                }

            }
        }
    }
}
