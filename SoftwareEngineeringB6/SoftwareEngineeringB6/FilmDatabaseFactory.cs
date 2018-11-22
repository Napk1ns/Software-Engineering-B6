using System;
namespace SoftwareEngineeringB6
{
    public static class FilmDatabaseFactory
    {
        public static FilmDatabaseFacade Create(string db)
        {
            switch (db)
            {
                case "TMDB":
                    return new TMDB();
                    break;
                case "OMDB":
                default:
                    return new OMDB();
                    break;
            }
        }
    }
}
