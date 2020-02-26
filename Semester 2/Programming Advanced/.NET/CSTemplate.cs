

Web Application{
	
	
	Use{
		//Dit is de belangrijkste, hiermee kan je zeggen welk path je verwacht enz verder.
		 app.Use(next =>
				{
					return async context =>
					{
						//loginformation zorgt voor informatie in je output window.
						logger.LogInformation("Request incoming");
						//onderstaande if zal alle requests opvangen met /math
						if (context.Request.Path.StartsWithSegments("/math"))
						{
							await context.Response.WriteAsync("Caught");
							logger.LogInformation("Request handled");
						}
						//anders stuurt hij ze door naar de volgende middelware.
						else
						{
							await next(context);
							logger.LogInformation("Request outgoing to next middleware");
						}
					};
				});
	}
	
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
		//dan in startup ergens:
		app.UseStaticFiles();

	}
	
	Standaard file gebruiken{
		//Dit zal de file index.html laden als default page.
		//je kan options toevoegen om een andere pagina standaard te laten laden.
		app.UseDefaultFiles();
	}
	
	Vorige 2 samen in 1 middelware{
		//Deze is gewoon een combinatie van staticfiles en defaultfiles.
		app.UseFileServer();
	}
	
	Controllers{
		//Voor deze te gebruiken moet je ook een service toevoegen
		public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
			app.UseRouting();
			app.UseEndpoints(endpoints =>
            {
				//Door onderstaande lijn gaat je pad gezien worden als controller/actie
				//dwz dat localhost/home/index verwijst naar HomeController en de methode genaamd index.
				//Als je een andere Controller anamaakt bv MathController en je gebruikt methode Add
				//zou het dus zijn localhost/math/add, wat in de functie add staat gereturned zal op het scherm komen.
                endpoints.MapDefaultControllerRoute();
            });	
		}
		//Dan maak je een nieuwe folder in je applicatie genaamd "Controllers"
		//hierin nieuwe klasse maken genaamd "HomeController"
	
	
		Route{
			//Met [Route("[actie]")] boven een methode te plaatsen kan je specifiëren hoe de methode aangesproken moet worden.
			//Dit gaat ook boven de klasse zelf voor de controller, om te kiezen hoe je de controller wil aanspreken.
		}
		
	}
	
	
	
	
}