//New project -->  WPF project.
//In deze solution , Add --> new project --> [Projectname].Data
//Deze is een class library
//In de .Data:

//ConnectionFactory.cs
//Repository (Bv. InvoiceRepository.cs)
//het ding zelf (Bv. Invoice.cs)

//Databases:
	//Connection string maken in App.config (Van de WPF solution):
	<configuration>
	  <connectionStrings>
		<add name="PayablesConnectionString" connectionString="Data Source=(localdb)\MSSqlLocalDB;Initial Catalog=Payables;Integrated Security=true"/>
	  </connectionStrings>
	</configuration>

	//ConnectionFactory:
		//App.config manier):
			internal static class ConnectionFactory
			{
				public static SqlConnection CreateSqlConnection()
				{
					string connectionString = ConfigurationManager.ConnectionStrings["PayablesConnectionString"].ConnectionString;
					return new SqlConnection(connectionString);
				}
			}
			
		//zonder App.config:
			string connectionString =
				"Data Source={localdb}" +
				"\\MSSqllocalDb;" +
				"Initial Catalog=Payables;" +
				"Integrated Security=True";
			return new SqlConnection(connectionString);
			
			// OF (Kies 1)
			
			SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder()
			{
				DataSource = "(localdb)\\MSSqllocalDb",
				InitialCatalog = "Payables",
				IntegratedSecurity = true
			};
			return new SqlConnection(connBuilder.ConnectionString);
			
	//repository (Dient om gegevens uit de database te halen met query en terug te geven):
	  public static ObservableCollection<Album> GetAlbumsByGenre(int id)
		{
			SqlDataReader reader = null;
			ObservableCollection<Album> albumList = new ObservableCollection<Album>();
			SqlConnection connection = ConnectionFactory.CreateSqlConnection();

			string selectStatement =
				"select albumid, title, genreid, artistid,price " +
				"From album " +
				"where genreId =  " + id;

			SqlCommand command = new SqlCommand(selectStatement, connection);
			try
			{
				connection.Open();
				reader = command.ExecuteReader();
				int genreId = reader.GetOrdinal("GenreId");
				while (reader.Read())
				{
					Album album = new Album()
					{
						GenreId = reader.GetInt32(genreId),
					};
					albumList.Add(album);
				}
			}
			finally
			{
				reader?.Close();
				connection?.Close();
			}
			return albumList;
		}

