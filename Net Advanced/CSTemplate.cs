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
			<c:RatingConverter x:Key="ratingConverter">
				
			</c:RatingConverter>
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
	
		//
		public static ObservableCollection<Album> GetAlbumsByGenre(int id){
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
		//
		
	}

}






























