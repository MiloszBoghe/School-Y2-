

Web Application{
	
	Welcome page{
		//Path zorgt er voor dat je dus dit moet achtervoegen in de link.
		//localhost:5000/welcome in dit geval dus.
		app.UseWelcomePage(new WelcomePageOptions
		{            
			Path = "/welcome"
		});	
	}
	
	pagina's aanmaken en gebruiken{
		//om pagina's aan te maken: in project --> new folder --> naam: wwwroot --> hierin .html files maken.
		//dan in programma boven app.UseRouting() :
		app.UseStaticFiles();

	}
	
	
	
	
	
}