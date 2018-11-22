using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace SoftwareEngineeringB6
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		
		static HttpClient client = new HttpClient();

		private void addButton_Click(object sender, EventArgs e)
		{
			listBox1.Items.Add(textBox1.Text);
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			listBox1.Items.Remove(listBox1.SelectedItem);
		}

		private void searchButton_Click(object sender, EventArgs e)
		{
			string title = searchText.Text;
			string db = comboBox1.Text;
			FilmDatabaseFacade filmDb;
			switch (db)
			{
				case "TMDB":
					filmDb = new TMDB();
					break;
				case "OMDB":
				default:
					filmDb = new OMDB();
					break;
			}
			Film film = new Film();
			Task.Run(() => {
				if (title == "random")
				{
					film = filmDb.GetRandomFilmAsync().Result;
				}
				else
				{
					film = filmDb.GetFilmAsync(title).Result;
				}
			}).Wait();
			textBox1.Text = string.Format("Title: {0}, \r\nYear: {1}, \r\nRating: {2}, \r\nReleased: {3}, \r\nRuntime: {4}, \r\nGenre: {5}, \r\nDirector: {6}, \r\nWriter: {7}, \r\nActor: {8}, \r\nPlot: {9}, \r\nLanguage: {10}, \r\nCountry: {11}, \r\nAwards: {12}, \r\nPoster: {13}, \r\nMetascore: {14}, \r\nIMDB Rating: {15}, \r\nIMDB Votes: {16}, \r\nIMDB ID: {17}, \r\nType: {18}, \r\nDVD Release date: {19}, \r\nBox Office: {20}, \r\nProduction: {21}, \r\nWebsite: {22}, \r\nResponse: {23}", film.Title, film.Year, film.Rated, film.Released, film.Runtime, film.Genre, film.Director, film.Writer, film.Actors, film.Plot, film.Language, film.Country, film.Awards, film.Poster, film.Metascore, film.imdbRating, film.imdbVotes, film.imdbID, film.Type, film.DVD, film.BoxOffice, film.Production, film.Website, film.Response);
		}
	
	}
}
