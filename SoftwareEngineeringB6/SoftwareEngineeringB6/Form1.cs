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

		[DataContract]
		internal class Film
		{
			[DataMember]
			internal string Title;

			[DataMember]
			internal string Year;

			[DataMember]
			internal string Rated;

			[DataMember]
			internal string Released;

			[DataMember]
			internal string Runtime;

			[DataMember]
			internal string Genre;

			[DataMember]
			internal string Director;

			[DataMember]
			internal string Writer;

			[DataMember]
			internal string Actors;

			[DataMember]
			internal string Plot;

			[DataMember]
			internal string Language;

			[DataMember]
			internal string Country;

			[DataMember]
			internal string Awards;

			[DataMember]
			internal string Poster;

			// [DataMember]
			// internal string Ratings;

			[DataMember]
			internal string Metascore;

			[DataMember]
			internal string imdbRating;

			[DataMember]
			internal string imdbVotes;

			[DataMember]
			internal string imdbID;

			[DataMember]
			internal string Type;

			[DataMember]
			internal string DVD;

			[DataMember]
			internal string BoxOffice;

			[DataMember]
			internal string Production;

			[DataMember]
			internal string Website;

			[DataMember]
			internal string Response;
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
			Film film = new Film();
			Task.Run(() => {
				if (title == "random")
				{
					film = GetRandomFilmAsync().Result;
				}
				else
				{
					film = GetFilmAsync(title, db).Result;
				}
			}).Wait();
			textBox1.Text = string.Format("Title: {0}, \r\nYear: {1}, \r\nRating: {2}, \r\nReleased: {3}, \r\nRuntime: {4}, \r\nGenre: {5}, \r\nDirector: {6}, \r\nWriter: {7}, \r\nActor: {8}, \r\nPlot: {9}, \r\nLanguage: {10}, \r\nCountry: {11}, \r\nAwards: {12}, \r\nPoster: {13}, \r\nMetascore: {14}, \r\nIMDB Rating: {15}, \r\nIMDB Votes: {16}, \r\nIMDB ID: {17}, \r\nType: {18}, \r\nDVD Release date: {19}, \r\nBox Office: {20}, \r\nProduction: {21}, \r\nWebsite: {22}, \r\nResponse: {23}", film.Title, film.Year, film.Rated, film.Released, film.Runtime, film.Genre, film.Director, film.Writer, film.Actors, film.Plot, film.Language, film.Country, film.Awards, film.Poster, film.Metascore, film.imdbRating, film.imdbVotes, film.imdbID, film.Type, film.DVD, film.BoxOffice, film.Production, film.Website, film.Response);
		}

		static async Task<Film> GetFilmAsync(string title, string db)
		{
			string url;
			switch (db)
			{
				case "TMDB":
					url = "https://api.themoviedb.org/3/search/movie?api_key=<<api_key>>&query={0}";
					break;
				default:
				case "OMDB":
					url = "http://www.omdbapi.com/?t={0}&apikey=b413c0e5";
					break;
			}
			HttpResponseMessage response = await client.GetAsync(string.Format(url, title));

			Film film = new Film();

			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
				DataContractJsonSerializer ser = new DataContractJsonSerializer(film.GetType());
				film = ser.ReadObject(ms) as Film;
				return film;
			}
			return film;
		}
		
		static async Task<Film> GetRandomFilmAsync()
		{
			Random r = new Random();
			string imdbID = "tt" + r.Next(10000000).ToString("D7");
			
			string url = "http://www.omdbapi.com/?i={0}&apikey=b413c0e5";
			
			HttpResponseMessage response = await client.GetAsync(string.Format(url, imdbID));
 			Film film = new Film();
 			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
				DataContractJsonSerializer ser = new DataContractJsonSerializer(film.GetType());
				film = ser.ReadObject(ms) as Film;
				return film;
			}
			return film;
		}
	}
}
