using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoftwareEngineeringB6
{
	public abstract class FilmDatabaseFacade
	{
		public abstract Task<Film> GetFilmAsync(string title);
		public abstract Task<Film> GetRandomFilmAsync();
	}
}
