using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineeringB6
{
	[DataContract]
	public class Film
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
}
