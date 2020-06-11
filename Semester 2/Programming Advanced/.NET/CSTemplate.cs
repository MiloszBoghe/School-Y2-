#region Custom Middleware{
	Startup klasse{
		//app.useMath() is om een custom middleware toe te voegen.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			//Er zijn nog andere dingen, maar voor middleware voeg je deze dus toe, na bovenstaande if:
			app.UseMath();
		}
	}		
	
	#region MathMiddleware
	
		//Deze metohde bepaalt wat er zal gebeuren:
		public async Task Invoke(HttpContext context)
		{
			//deze bool gaat bepalen of de middleware gebruikt wordt of niet.
			//als de url begint met /math, dan wel, anders gaat hij deze middleware overslaan.
			bool isCalculation = context.Request.Path.StartsWithSegments("/math", out var operatorPath);
			if (isCalculation)
			{
				string operatorName = operatorPath.ToString();
				if (!string.IsNullOrEmpty(operatorName)) //remove the forward slash
				{
					operatorName = operatorName.Substring(1);
				}

				var reader = new StreamReader(context.Request.Body);
				string body = await reader.ReadToEndAsync();
				var arguments = body.Split(",;-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

				double? result = _calculator.ExecuteOperation(operatorName, arguments);

				if (result.HasValue)
				{
					await context.Response.WriteAsync($"Result: {result}");
				}
				else
				{
					//Einde: (gaat dus naar volgende middleware of service, ..)
					await _next(context);
				}

			}
			else
			{
				//Einde: (gaat dus naar volgende middleware of service, ..)
				await _next(context);
			}
		}
		
		//Eerst gewoon nieuwe klasse aanmaken
		//Deze methode hoort hierin te komen:
		//Deze zorgt ervoor dat je in startup onder Configure "app.UseMath" kan gebruiken
		//Vervang Math met jou Middleware naam
		public static class MathMiddlewareExtensions
		{
			public static IApplicationBuilder UseMath(this IApplicationBuilder builder)
			{
				return builder.UseMiddleware<MathMiddleware>();
			}
		}
		
	#endregion
	
	
#endregion


#region MVC
	StartUp klasse{
		 public void ConfigureServices(IServiceCollection services)
			{
				//Mvc = views, dus:
				services.AddControllersWithViews();
				//Voor elke gebruikte interface, hoort een klasse:
				services.AddScoped<IRestaurantRepository, RestaurantDbRepository>();
				services.AddScoped<IReviewRepository, ReviewDbRepository>();
				services.AddScoped<IConverter, Converter>();
			}

			public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
			{
				if (env.IsDevelopment())
				{
					app.UseDeveloperExceptionPage();
				}
				else
				{
					app.UseExceptionHandler("/Home/Error");
					// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
					app.UseHsts();
				}
				
				app.UseHttpsRedirection();
				
				//staat je toe om met url gewoon path in te geven, deze zal verwijzen naar de folder
				//wwwroot/...
				app.UseStaticFiles();

				app.UseRouting();

				//Alleen nodig als er een User systeem is
				app.UseAuthorization();

				//hier bepaal je de routes:
				app.UseEndpoints(endpoints =>
				{
					//name maakt niet echt uit
					//pattern is het url patroon. searchmusic/rock zal dus naar HomeController methode Search gaan
					endpoints.MapControllerRoute(
						name: "Search",
						pattern: "SearchMusic/{genre=Rock}",
						defaults: new { controller = "Home", action = "Search" });
					endpoints.MapControllerRoute(
						name: "default",
						pattern: "{controller=Home}/{action=Index}/{id?}");
				});
			}
		}
	
	HomeController example{
		public class HomeController : Controller
		{
			private readonly IRestaurantRepository _restaurantRepository;
			private readonly IReviewRepository _reviewRepository;
			private readonly IConverter _converter;

			public HomeController(IRestaurantRepository restaurantRepository, IReviewRepository reviewRepository, IConverter converter)
			{
				_restaurantRepository = restaurantRepository;
				_reviewRepository = reviewRepository;
				_converter = converter;
			}
			
			//De naam van de methode moet hetzelfde zijn als een View naam in de 
			//Views/ControllerNaam (dus Views/Home) of Views/Shared folder, anders weet hij niet welke View.
			public IActionResult Index()
			{	
				var restaurants = _restaurantRepository.GetAll();
				//De parameter die je hier meegeeft zal ook in je View beschikbaar zijn
				//vanboven in de view zal er iets staan als:
				//@model IEnumerable<OdeToFood.Data.DomainClasses.Restaurant>
				return View(restaurants);
			}
		}
		
	}
	
	Views{
		
		Views/_ViewImports.cshtml{
			//hier zet je @usings die je over al je views wil kunnen gebruiken.
			//bv:
			@using OdeToFood
			@using OdeToFood.Models
			@using OdeToFood.Data.DomainClasses
			
			//Ook tag helpers zet je hier:
			@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
			@addTagHelper *, OdeToFood
		}
		
		//Algemeen views:
		//vanboven declareer je welk model binnenkomt, bv:
		@model IEnumerable<OdeToFood.Data.DomainClasses.Restaurant>
		//Als je in je _viewimports deze lijn zet:
		@using OdeToFood.Data.DomainClasses
		//Dan kan dit verkort worden naar:
		@model IEnumerable<Restaurant>
		
		//je kan via @model aan de Enumerable van Restaurants in je html.
		//hiermee kan je de property namen van Restaurant krijgen:
		@Html.DisplayNameFor(model => model.Name)
	
		//Dit loopt door deze enumerable:
		@foreach (var item in Model)
        {
            <tr>
                <td>                    
                    @Html.DisplayFor(modelItem => item.Name)
					//of gewoon item.Name
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.City)
					//same as hierboven
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Country)
					//same as hierboven x2
                </td>
                <td>
					//asp-action verwijst naar een methode. Hier dus de methode Details.
					//Dit zal dan gaan naar je Details methode van de controller waarbij deze view hoort.
					//asp-route-id = parameter van de methode
                    <a asp-action="Details" asp-route-id="@item.Id">Details</a>
                </td>
            </tr>
        }
		
	}
	
	ApiController example{
		[Route("api/[controller]")]
		[ApiController]
		
		//Deze lijn zorgt ervoor dat authenticatie verplicht is, kan ook individueel boven elke metohde.
		//Boven de klasse wil zeggen dat het voor alle methodes in deze controller moet.
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public class RestaurantsController : ControllerBase
		{
			private readonly IRestaurantRepository _restaurantRepository;

			//In de constructor paremeters komt bijna altijd een repository als interface.
			//in je startup moet je hier dan ook duidelijk maken met 
			//services.AddScoped<IRestaurantRepository, RestaurantRepository> (zie startup sectie hierboven) 
			public RestaurantsController(IRestaurantRepository restaurantRepository)
			{
				_restaurantRepository = restaurantRepository;
			}
			
			//methodes voor copy paste en edit:
			
			Gets{
				// GET: api/Restaurants
				[HttpGet]

				public IActionResult GetAll()
				{
					return Ok(_restaurantRepository.GetAll());
				}

				// GET: api/Restaurants/5
				[HttpGet("{id}")]
				public IActionResult GetRestaurant(int id)
				{
					var restaurant = _restaurantRepository.GetById(id);

					if (restaurant == null) return NotFound();

					return Ok(restaurant);
				}
			}
			
			Post{
				// POST: api/Restaurants
				[HttpPost]
				public IActionResult Post([FromBody] Restaurant newRestaurant)
				{
					if (!ModelState.IsValid)
					{
						return BadRequest();
					}

					var createdRestaurant = _restaurantRepository.Add(newRestaurant);

					return CreatedAtRoute("DefaultApi", new { controller = "Restaurants", id = createdRestaurant.Id }, createdRestaurant);
				}
			}
			
			Put{
				// PUT: api/Restaurants/5
				[HttpPut("{id}")]
				public IActionResult Put(int id, [FromBody] Restaurant restaurant)
				{
					if (!ModelState.IsValid)
					{
						return BadRequest();
					}

					if (id != restaurant.Id)
					{
						return BadRequest();
					}

					if (_restaurantRepository.GetById(id) == null)
					{
						return NotFound();
					}

					_restaurantRepository.Update(id,restaurant);

					return Ok();
				}
			}
			
			Delete{
				// DELETE: api/ApiWithActions/5
				[HttpDelete("{id}")]
				public IActionResult Delete(int id)
				{
					var restaurant = _restaurantRepository.GetById(id);
					if (restaurant== null)
					{
						return NotFound();
					}

					_restaurantRepository.Remove(restaurant);

					return Ok();
				}
			}
		}
	}
	
	
#endregion


#region Repositories
	//Kies 1: non generic of generic
	//Option 1
	#region Repo (NON GENERIC)
		Repository interface{		
			public interface IRestaurantRepository
			{
				IEnumerable<Restaurant> GetAll();
				Restaurant GetById(int id);
				Restaurant Add(Restaurant restaurant);
				void Update(Restaurant restaurant);
				void Delete(int id);
			}
		}
	
		Repository example{
		public class RestaurantDbRepository : IRestaurantRepository
		{
			private readonly OdeToFoodContext _context;

			public RestaurantDbRepository(OdeToFoodContext context)
			{
				_context = context;
			}

			public Restaurant Add(Restaurant restaurant)
			{
				_context.Restaurants.Add(restaurant);

				_context.SaveChanges();

				return restaurant;
			}

			public IEnumerable<Restaurant> GetAll()
			{
				return _context.Restaurants.ToList();
			}

			public Restaurant GetById(int id)
			{
				return _context.Restaurants.Find(id);
			}

			public void Update(Restaurant restaurant)
			{
				//Restaurant might not be tracked (attached) by the entity framework -> get original from DB and copy values
				var original = _context.Restaurants.Find(restaurant.Id);
				var entry = _context.Entry(original);
				entry.CurrentValues.SetValues(restaurant);

				_context.SaveChanges();
			}

			public void Delete(int id)
			{
				var entityToDelete = _context.Restaurants.Find(id);
				_context.Restaurants.Remove(entityToDelete);

				_context.SaveChanges();
			}
		}
	}
	#endregion
	
	
	//Option 2
	#region Repo (GENERIC)
		//Eerst implementeer je de generic interface en repo:
		Generic IRepo{
			public interface IGenericRepository<TEntity> where TEntity : class
			{
				TEntity GetById(int id);
				IEnumerable<TEntity> GetAll();
				IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> statement);
				void Add(TEntity entity);
				void Remove(TEntity entity);
				void Update(int id, TEntity entity);
				void Save();
				
				#region Async
				Task<IList<TEntity>> GetAllAsync();
				Task<TEntity> GetByIdAsync(int id);
				Task<TEntity> AddAsync(TEntity entity);
				Task UpdateAsync(int id, TEntity entity);
				Task DeleteAsync(TEntity entity);
				#endregion
			}
		}
		
		Generic Repo{
			public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
			{
				private readonly OdeToFoodContext _context;
				private readonly DbSet<TEntity> _dbSet;

				public GenericRepository(OdeToFoodContext context)
				{
					_context = context;
					_dbSet = _context.Set<TEntity>();
				}
				
				public IEnumerable<TEntity> GetAll()
				{
					var entities = _dbSet.ToList();
					return entities;
				}
				
				public TEntity GetById(int id)
				{
					var entity = _dbSet.Find(id);
					return entity;
				}
				
				public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> statement)
				{
					var entities = _dbSet.Where(statement).ToList();
					return entities;
				}

				public void Add(TEntity entity)
				{
					_dbSet.Add(entity);
					Save();
				}

				public void Delete(TEntity entity)
				{
					_dbSet.Remove(entity);
					Save();
				}

				public void Update(int id, TEntity entity)
				{
					var originalEntity = _dbSet.Find(id);
					_context.Entry(originalEntity).CurrentValues.SetValues(entity);
					Save();
				}


				public void Save()
				{
					_context.SaveChanges();
				}
				
				#region Async versions
				public async Task<IList<TEntity>> GetAllAsync()
				{
					return await _dbSet.ToListAsync();
				}
				public async Task<TEntity> GetByIdAsync(int id)
				{
					return await _dbSet.FindAsync(id);
				}

				public async Task<TEntity> AddAsync(TEntity entity)
				{
					var addedEntity = await _dbSet.AddAsync(entity);
					await SaveAsync();
					return addedEntity.Entity;
				}

				public async Task UpdateAsync(int id, TEntity entity)
				{
					var originalEntity = await _dbSet.FindAsync(id);
					_context.Entry(originalEntity).CurrentValues.SetValues(entity);
					await SaveAsync();
				}

				public async Task DeleteAsync(TEntity entity)
				{
					_dbSet.Remove(entity);
					await SaveAsync();
				}
				private async Task SaveAsync()
				{
					await _context.SaveChangesAsync();
				}
				#endregion
			}
		}
			
		
	
		//Dan maak je de andere IRepo's en Repo's:
		IRepo{
			//Hier hoeft niets meer in dan dit, voor de standaard CRUD methodes
			//Die worden gewoon gebruikt van de generic repo.
			//Als je speciale methodes moet doen, komen ze wel hier.
			//(dus andere dingen dan gewone get/getbyId/put/post/delete komen hier)
			public interface IRestaurantRepository : IGenericRepository<Restaurant>
			{
				
			}
		}
		
		region Repo{
			//Hier hoeft niets meer in dan dit, voor de standaard CRUD methodes
			//Die worden gewoon gebruikt van de generic repo.
			//Als je speciale methodes moet doen, komen ze wel hier.
			//(dus andere dingen dan gewone get/getbyId/put/post/delete komen hier)
			public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
			{
				private readonly OdeToFoodContext _context;

				public RestaurantRepository(OdeToFoodContext context) :base(context)
				{
					_context = context;
				}
			}
		}
		
	#endregion
	
	
#endregion


#region tests

	Builders{
		//je kan builders schrijven om random objecten voor je aan te maken.
		//map --> Builders
		//Ook kan je hier dan een Helper klasse aanmaken om bv een random generator voor elke builder
		//te gebruiken:
		internal abstract class BuilderBase
		{
			protected static readonly Random RandomGenerator = new Random();
		}
		//Dan een klassebuilder
		//bv: GenreBuilder.cs
		internal class GenreBuilder : BuilderBase
		{
			private readonly Genre _genre;

			public GenreBuilder()
			{
				_genre = new Genre
				{
					Id = RandomGenerator.Next(),
					Name = Guid.NewGuid().ToString()
				};
			}

			public Genre Build()
			{
				return _genre;
			}
		}
		
		//En voor een album met een genre:
		internal class AlbumBuilder : BuilderBase
		{
			private readonly Album _album;

			public AlbumBuilder()
			{
				_album = new Album
				{
					Id = RandomGenerator.Next(),
					Title = Guid.NewGuid().ToString(),
					Artist = Guid.NewGuid().ToString()
				};
			}

			public AlbumBuilder WithGenreId(int genreId)
			{
				_album.GenreId = genreId;
				return this;
			}

			public Album Build()
			{
				return _album;
			}
		}
		
	}

	Tests{
		//Eerst private variabele maken voor nodige Mocks en controller/repo, whatever je test:
		private Mock<RestaurantRepository> _restaurantRepoMock;
		private RestaurantController _restaurantController;
		
		//Dan SetUp()
		[SetUp]
		public void SetUp()
		{
			//Elke interface kan gemocked worden.
			_restaurantsMock = new Mock<RestaurantsController>();
			
			//Voor elke interface parameter in een constructor, maak je een Mock en geef je 
			//bij het maken van dit Object de Mock.Object mee.
			_restaurantController = new RestaurantController(_restaurantRepoMock.Object);
		}	
		
		
		
		ViewTest{
			//Om te testen op een view:
			//De default ViewName is null
			//bij result "as ViewResult" er bij zetten als je weet dat je een View verwacht.
			//Als deze methode geen View returned zal je result null zijn.
			
			[Test]
			public void Index_ReturnsDefaultView()
			{
				ViewResult result = _controller.Index() as ViewResult;
				Assert.That(result, Is.Not.Null);
				Assert.That(result.ViewName, Is.Null);
			}
		}
		
		
		MockTest{
			[Test]
			public void Details_ShowsDetailsOfAlbum()
			{
				//Arrange
				//Deze build methods genereren gewoon een random object van dat type.
				//Zie Builders
				var genre = new GenreBuilder().Build();
				var album = new AlbumBuilder().WithGenreId(genre.Id).Build();
				var albumViewModel = new AlbumViewModel();

				//SetUp zorgt ervoor dat als een methode geroepen wordt op je Mock dat je kan
				//kiezen wat er gereturned wordt.
				//De parameters in je Mock moeten wel exact overeenkomen met hoe de methode opgeroepen wordt.
				//Hiervoor zijn wel andere mogelijkheden, hieronder zie je bv:
				//It.IsAny<int>() --> Dit wil zeggen, als de parameter een int is, return wat er staat:
				
				//Deze Setup returned bovenstaand album als de parameter een int is:
				_albumRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(album);
				
				//Deze Setup returned bovenstaand genre als de parameter een int is:
				_genreRepositoryMock.Setup(repo => repo.GetById((It.IsAny<int>()))).Returns(genre);
				
				//Deze Setup returned bovenstaand albumViewModel als de parameters van het type Album en Genre zijn.
				_albumViewModelFactoryMock.Setup(factory => factory.Create(It.IsAny<Album>(), It.IsAny<Genre>())).Returns(albumViewModel);

				//Act
				var viewResult = _controller.Details(album.Id) as ViewResult;

				//Assert
				Assert.NotNull(viewResult);

				//Deze Asserts checken dat de Mocks allemaal exact 1 keer gebruikt zijn
				//door een methode op de mock opgeroepen te hebben bij het doorlopen van de code die de test doorloopt.
				_albumRepositoryMock.Verify(repo => repo.GetById(album.Id), Times.Once);
				_genreRepositoryMock.Verify(repo => repo.GetById(album.GenreId), Times.Once);
				_albumViewModelFactoryMock.Verify(factory => factory.Create(album, genre), Times.Once);
				
				//controleert of de ViewResult zijn model hetzelfde is als bovenstaand albumViewModel.
				Assert.That(viewResult.Model, Is.SameAs(albumViewModel));
			}
		}
		
		Relevant info{
			//Je kan elke result controleren op type door 
			var result = /*Uit te voeren methode*/ as /*ResultType*/
			//e.g:
			var result = _controller.Search("Rock") as RedirectResult
			//Als deze methode ^^^^^^^^^^^^ geen RedirectResult teruggeeft, zal result null zijn.
			//Dat kan je dus testen met:
			Assert.NotNull(result);
			
			//Relevante types voor nu zijn:
			/*
			
			ViewResult, RedirectResult , ContentResult, OkResult, OkObjectResult, RedirectToActionResult,
			NotFoundResult, UnAuthorizedResult, UnAuthorizedObjectResult,
			NoContentResult
			
			*/
			
			//Random relevante Asserts:
			 Assert.NotNull(redirectToActionResult); //checked of het niet null is
			 Assert.False(redirectToActionResult.Permanent); //checked of het false is
			 Assert.AreEqual(redirectToActionResult.ActionName, "Details"); //checked of 2 variabelen gelijk zijn
			 Assert.GreaterOrEqual(2, 0); // checked of 2 groter dan of gelijk aan 0 is
			 _albumRepositoryMock.Verify(repo => repo.GetById(album.Id), Times.Once); //controleert dat de _albumRepositoryMock exact 1 keer gebruikt is.
			 Assert.That(viewResult.Model, Is.SameAs(albumViewModel));	//controleert of de ViewResult zijn model hetzelfde is als bovenstaand albumViewModel.
		}
	}
	

#endregion

