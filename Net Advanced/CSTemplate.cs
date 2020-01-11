Binding{
	ComboBox{
		//Stel itemsSource in op een List of Collection
		
		//Kies 1
		
		DisplayMember/SelectedValuePath{
			//
			<ComboBox x:Name="comboBox" 
			DisplayMemberPath="Name" SelectedValuePath="GenreId"/>
			//
		}
        
		//OF 
		
		ItemTemplate{
			//
			<ComboBox SelectionChanged="gamesCombobox_SelectionChanged">
				<ComboBox.ItemTemplate>
					<DataTemplate>
						<StackPanel Orientation="Horizontal">
							<TextBlock Text="{Binding Path=GameId, Mode=OneWay}"/>
							<TextBlock Text=" - "/>
							<TextBlock Text="{Binding Path=Name, Mode=OneWay}"/>
						</StackPanel>
					</DataTemplate>
				</ComboBox.ItemTemplate>
			</ComboBox>
			//
		}
			
	}
	
	
	DataGrid{
		//ItemsSource geven..
		//Xaml:
		//
		<DataGrid ItemsSource="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type Window}}, Path=summary}">
            <DataGrid.Columns>
                <DataGridTextColumn Header="Title" Width="150" Binding="{Binding Path=Title}"/>
                <DataGridTextColumn Header="Artist" Width="200" Binding="{Binding Path=ArtistName}"/>
                <DataGridTextColumn Header="Price" Width="100" Binding="{Binding Path=Price}"/>
            </DataGrid.Columns>
        </DataGrid>
		//
		//CS:
		    CustomersDataGrid.ItemsSource = _customers;
            CityComboBoxColumn.ItemsSource = _cityRepository.GetAll();
	}
	
	
	INotifyPropertyChanged{
	//In de klasse die een property heeft waarop dit van toepassing hoort te zijn:
	//Stap 1: ": INotifyPropertyChanged achter de klasse naam"
	//Stap 2: In deze property setter:
		 public class Game : INotifyPropertyChanged{
			public double Rating { 
				get=>rating; 
				set {
                rating = value;
                OnPropertyChanged(nameof(Rating));
				}
			}
		}
		
	//Stap 3: Copy pasta my shit ;)
		//
		public event PropertyChangedEventHandler PropertyChanged;
		
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
		//
}


Converter{
	// Add --> new folder --> Converters
	// Add --> new class --> [relevantName].cs
	//Xaml:
		//
		<Window.Resources>
			<c:RatingConverter x:Key="ratingConverter"/>
		</Window.Resources>
		//
		
		//Element dat geconvert moet worden, bv:
			//
			<TextBox Text="{Binding Mode=TwoWay, Path=Rating, Converter={StaticResource ratingConverter}}"/>
			//
	//CS:
		//In Convert schrijf je de functie om te converteren.
		//In Convertback het omgekeerde. ConvertBack hoef je niet altijd in te vullen. Alleen als nodig.
	    //
		public class RatingConverter : IValueConverter{
			
			public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			{
				double rating = (double)value;
				return rating * 10;
			}
			
			public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			{
				int rating = (int)value;
				return rating / 10.0;
			}
		}
		//
}

}



Databases{
	
	
	Aanmaak{
	//New project -->  WPF project.
		//In deze solution , Add --> new project --> [Projectname].Data
		//Deze is een class library
	//In de .Data:
		//ConnectionFactory.cs
		//Repository (Bv. InvoiceRepository.cs)
		//het ding zelf (Bv. Invoice.cs)
	}



	ConnectionString{
//Connectionstring maken in App.config (Van de WPF solution):
	//ConnectionFactory:
		//App.config manier):
		//In de App.config van de WPF:
			//
			<configuration>
			  <connectionStrings>
				<add name="PayablesConnectionString" connectionString="Data Source=(localdb)\MSSqlLocalDB;Initial Catalog=Payables;Integrated Security=true"/>
			  </connectionStrings>
			</configuration>
			//

		//In de ConnectionFactory.cs:
			
			//
			internal static class ConnectionFactory
			{
				public static SqlConnection CreateSqlConnection()
				{
					string connectionString = ConfigurationManager.ConnectionStrings["PayablesConnectionString"].ConnectionString;
					return new SqlConnection(connectionString);
				}
			}
			//
			
		//zonder App.config:
			//
			string connectionString =
				"Data Source={localdb}" +
				"\\MSSqllocalDb;" +
				"Initial Catalog=Payables;" +
				"Integrated Security=True";
			return new SqlConnection(connectionString);
			//
			
			// OF (Kies 1)
			
			//
			SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder()
			{
				DataSource = "(localdb)\\MSSqllocalDb",
				InitialCatalog = "Payables",
				IntegratedSecurity = true
			};
			return new SqlConnection(connBuilder.ConnectionString);
			//
	}
	
	
	
	Repository{
	//repository (Dient om gegevens uit de database te halen met query en terug te geven):
		
		Get{
			//
			public static ObservableCollection<Album> GetAlbumsByGenre(int id){
				SqlDataReader reader = null;
				ObservableCollection<Album> albumList = new ObservableCollection<Album>();
				SqlConnection connection = ConnectionFactory.CreateSqlConnection();
				SqlParameter test = new SqlParameter();
				test.parameterName = "@id";
				test.value = id;


            string selectStatement =
				"select albumid, title, genreid, artistid, price " +
				"From album " +
				"where genreId = @id";

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
				//
			}
		
		
		Add{
			public void Add(int lotteryGameId, IList<int> numbers)
			{
				SqlConnection connection = _connectionFactory.CreateSqlConnection();
				SqlTransaction lotteryTransaction = connection.BeginTransaction();

				connection.Open();
				SqlCommand insertDrawCommand = new SqlCommand();
				insertDrawCommand.Connection = connection;
				insertDrawCommand.Transaction = lotteryTransaction;
				lotteryTransaction.Commit();
				connection.Close();
			}			

		}
		
	}



	Linq{
		
		Join{
			//voegt een list samen met komma's tussen elk element
			string.Join(",", list)
		}
		
		OrderBy{
			//sorteert een list op wat je wil..
			list.OrderBy(a => a.[PropertyNaam]) //sorteert op die property.	
		}
		
		Select{
			//selecteert alleen een bepaalde property uit elk element van de list
			list.Select(n=>n.Number) //Pakt alleen het "Number"
		}
	}

}




EntityFramework{
	//nieuwe solution? Voeg projecten toe: (
	//[Naam].Data (CLASS LIBRARY - NET FRAMEWORK)
	//[Naam].Domain (CLASS LIBRARY - NET FRAMEWORK)
	//UI (WPF - NET FRAMEWORK)
	//rightclick .Data-->manage nuGet --> microsoft.entityframeworkcore --> .Sqlserver (VERSIE 2.2.6!!) --> install
	
	Context{
		//In de .Data zal moet de context klasse staan. Deze extend "DbContext"
		//wat doen?
		//1. alle DbSets aanmaken als properties (tip: prop tab tab)
		//2. override OnConfiguring
		public class SamuraiContext:DbContext
		{
			public DbSet<Samurai> Samurais { get; set; }
			public DbSet <Battle> Battles { get; set; }
			public DbSet<Quote> Quotes { get; set; }
		
			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SamuraiAppData;Integrated Security=True";
				optionsBuilder.UseSqlServer(connectionString);
				
			}
		}
	
	}
	
	Migrations{
		//Volg deze stappen:
		//Om migrations te kunnen doen --> .Data --> Manage nuGet --> microsoft.entityframeworkcore.tools (VERSIE 2.2.6)!!-->install
		//UI --> add reference-->projects--> .Data en .Domain
		//UI --> rightclick --> manage nuGet --> microsoft.entityframeworkcore.design --> install
		//UI --> rightclick --> set as startup project
		//Tip: Voor packet manager console: View-->other windows --> packet manager console
		//Tip: in de packet manager console: get-help entityframeworkcore
		//in de console --> Default project: .Data
	
		Add-Migration{
			//in console
			add-migration initial
		}
		
		Script-Migration{
			//in console:
			script-migration
			//Dit maakt een .sql file met de hele sql in.
			//Waarschijnlijk niet nodig. Dit is om te delen met andere mensen in een project ofzo.
		}
		
		Update-Database{
			//in console:
			update-database
			//als de database nog niet bestaat zal het deze ook aanmaken. 
		}
		
	}


	Relationships{
		//one to one, many to many, ...

		One to one{
			//nieuwe klasse om in dit geval de echte naam van een samurai bij te houden.
			//deze heeft een PK "Id" en samuraiId en samuraiNaam.
			public class SecretIdentity
			{
				public int Id { get; set; }
				public string RealName { get; set; }
				public int SamuraiId { get; set; }
			}
			
			//Aan de Samurai klasse voegen we ook een property toe:
			public SecretIdentity SecretIdentity{get; set; }
			//nu kan EFCore er aan uit. In orde.
			
		}
			
		
		One to many{
			//Extra property maken in de domain klassen
			//In de "one" klasse --> [Object] Object:
			public class Post
			{
				public int PostId { get; set; }
				public string Title { get; set; }
				public string Content { get; set; }

				public int BlogId { get; set; }
				public Blog Blog { get; set; }
			}
			//In degene met many --> ICollection<[WatHijVeelHeeft]>:
			public class Blog
			{
				public int BlogId { get; set; }
				public string Url { get; set; }

				public ICollection<Post> Posts { get; set; }
			}


			//Dan kunnen we in de OnModelCreating klasse:
			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.Entity<Post>()
					.HasOne(a => a.Blog)
					.WithMany(b => b.Posts)
					.HasForeignKey(c => c.BlogId)
					.HasPrincipalKey(d => d.Url);
			}
		
		}
		
		
		Many to many{
			//Voor een many to many relationship (bv Samurais meerdere battles, battles meerdere samurais)
			//Moet er een join entity aangemaakt worden. Dit is een nieuwe klasse in .Domain
			//Deze heeft de primary keys van beide (Id)
			//
			public class SamuraiBattle
			{
				public int SamuraiId { get; set; }
				public Samurai Samurai { get; set; }
				public int BattleId { get; set; }
				public Battle Battle { get; set; }
			}
			
			//Dan pas je in Battle klasse en Samurai klasse de properties aan:
			//In Samurai:
			public class Samurai
			{
				//public int BattleId { get; set; } DEZE GAAT IN COMMENT
				//in de plaats komt deze:
				public List<SamuraiBattle> SamuraiBatttles {get; set; }
			}
			//in Battle:
			public class Battle
			{
				//public List<Samurai> Samurais { get; set; } DEZE GAAT IN COMMENT
				//in de plaats komt deze:
				public List<SamuraiBattle> SamuraiBatttles { get; set; }
			}
			
			//Dan naar de Context klasse:
			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.Entity<SamuraiBattle>().HasKey(s => new { s.SamuraiId, s.BattleId }); 
			}
		}

	
}
		
	
	modelBuilder (Required, PK, ...){
		//Om een property required te maken:
		modelBuilder.Entity<Person>().Property(p => p.Name).IsRequired();

		
		//Om aan EFCore duidelijk te maken wat de PK is --> in de context klasse:
		protected override void OnModelCreating(ModelBuilder modelBuilder){
			modelBuilder.Entity<Blog>().HasKey(b => b.Url);
		}
	}
}






























