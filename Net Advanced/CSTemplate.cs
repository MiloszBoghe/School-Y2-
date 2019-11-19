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
			
	//repository:
	using System.Collections.Generic;
	using System.Data.SqlClient;

