using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoftwareEngineeringB6
{
	interface FilmDatabaseFacade
	{
		Task<Film> GetFilmAsync(string title);

		Task<Film> GetRandomFilmAsync();
	}
}
