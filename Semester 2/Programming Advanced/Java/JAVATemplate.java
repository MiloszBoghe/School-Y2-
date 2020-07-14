//Semester 2
//Commando's niet nodig voor spring, wel voor JEE.
Tomee command:
mvn clean install tomee:run

//Examen gebruikt H2 database dus docker niet nodig.
Docker{
	//new folder --> docker
	//new file --> docker-compose.yaml
	//in this file:
	
	version: "3.3"
	services:
		dbname-db:
			image: mysql:5.6
			ports:
				- "3306:3306"
			environment:
				MYSQL_DATABASE: 'dbname'
				# So you don't have to use root, but you can if you like
				MYSQL_USER: 'user'
				# You can use whatever password you like
				MYSQL_PASSWORD: 'password'
				# Password for root access
				MYSQL_ROOT_PASSWORD: 'admin'
			volumes:
				# volumes: is optional. If you do not have an SQL script, delete it.
				# File containing sql script to create database (it's in the same folder, called "data.sql")
				- ./data.sql:/docker-entrypoint-initdb.d/0_init.sql
	
	//in terminal:
	//cd to docker directory containing this yml file-->
	//docker-compose up
}

//Als je spring gebruikt, is dit JPA deel vrijwel useless.
//Alleen het Database setup gedeelte is voor beide. Dus voor spring --> database setup --> spring.
JPA{
	
	Database setup{
		Tabel{
			//Person tabel:
			@Entity
			@Table(name="PERSONS")
			public class Person{ .. }
			
			//met unieke last_name constraint:
			@Table(name="PERSONS", uniqueConstraints = {@UniqueConstraint(columnNames = "LAST_NAME")})	
			public class Person{ .. }
			
			//mapping enums:
			public enum GenderType{ MALE, FEMALE}
			-->
			@Enumerated(EnumType.String) --> string representatie
			@Enumerated(EnumType.Ordinal) --> standaardwaarde (volgnummer--> 0 of 1 in dit geval)
		}
		
		Relaties{
			
			OneToOne{
				
				//Mogelijke Annotations:
				//@OneToOne(annotation = ...)
				//Zie les 5 - jpa.pdf pagina 28+
				cascade = ... 
				fetch = ...
				mappedBy = ...
				optional = ... 
				orphanRemoval = ... //Geeft aan of objecten die losgekoppeld worden automatisch verwijderd moeten worden. False standaard.
				targetEntity = ... //meestal niet nodig. Enkel bij bv raw collection.
				
				@Entity
				public class Patient{
					@Id
					@GeneratedValue
					private long id;
					private String name;
					
					//Boven de property die bij een ander object hoort:
					@OneToOne
					private MedicalFile medicalFile;

					public MedicalFile getMedicalFile(){
						return medicalFile;
					}
					
					public void setMedicalFile(MedicalFile medicalFile){
						this.medicalFile = medicalFile;
					}
				}
				
				public class MedicalFile{
					//In de child klasse op de parent property zet je mappedBy="naam van de child property in de parent klasse.":
					@OneToOne(mappedBy="medicalFile")
					private Patient patient;

					public Patient getPatient(){
						return patient;
					}
					
					public void setPatient(Patient patient){
						this.patient = patient;
					}
				}
			}
			
			
			OneToMany{
				//zie les 5 - jpa.pdf pagina 31-33
				@Entity
				public class School{
					........
					
					@OneToMany(mappedBy="school") //school = property bij de studenten
					private List<Student> students;
				}
				
				@Entity
				public class Student{
					
					@ManyToOne
					private School school;
				}
			}
			
			
			ManyToMany{
				//zie jpa.pdf pagina 34-35
				
				public class Reader{
					
					....................
					
					@ManyToMany
					private Set<Magazine> magazines = new HashSet<>();
					
				}
				
				public class Magazine{
					
					....................
					
					@ManyToMany
					private Set<Reader> readers = new HashSet<>();
					
				}
				
				
			}
			
			
		}
	}
	
	
	DAO{
		
		
		EntityManager (Context in C#){
			persist: Toevoegen van een object aan de persistence context 
			Wegschrijven naar de database is afhankelijk van de implementatie.
			--> Rekening houden met automatisch gegenereerde primary keys.
			
			find: Opzoeken record en toevoegen aan de persistence context.
			
			merge: Een unmanaged object samenvoegen met een managed object.
			
			remove: Verwijderen van een object uit de persistence context en database.
			
			refresh: Gegevens van een managed object opnieuw uitlezen uit de database.
			
			clear: Leegmaken van de persistence context.
			
			detach: Een managed object unmanaged maken.
			
			contains: Nagaan of een object zich in de persistence context bevindt.	
		}

		
		//Creating and saving new Entity:
		Create{
			//Eerste 4 lijnen worden altijd gedaan, dit is vergelijkbaar met de context aanmaken in EFCore
			EntityManagerFactory emf = Persistence.createEntityManagerFactory("example");
			EntityManager em = emf.createEntityManager(); //Dit is je context in C#, maar DBSet bestaat niet.
			EntityTransaction tx = em.getTransaction();
			tx.begin();
			//Create gedeelte:
			Message message = new Message(1, "Hello Hibernate"); 
			em.persist(message); //= C#: context.Messages.Add(message);
			
			//Save:
			tx.commit(); // = C# context.SaveChanges();
			
			//Sluit connectie:
			em.close();
			emf.close();
		}
	
	
		//Updating entity:
		Update{
		//Eerste 4 lijnen worden altijd gedaan, dit is vergelijkbaar met de context aanmaken in EFCore
		EntityManagerFactory entityManagerFactory = Persistence.createEntityManagerFactory("example");
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		EntityTransaction tx = entityManager.getTransaction();
		tx.begin();
		
		//Get de juiste message: 1ste parameter = soort klasse
		//2de parameter is ID (hier een long value dus L erbij)
		Message message = em.find(Message.class, 1L)
		
		//Dit is nu een tracked entity, zolang tx en em open zijn.
		//Dus je kan aanpassen wat je wil en je moet gewoon tx.commit doen om te saven.
		
		//Update gedeelte:
		message.setText("Updating text"); 
		
		//Save:
		tx.commit(); // = C# context.SaveChanges();
		
		//Sluit connectie:
		em.close();
		emf.close();
		
	}
	
	
		Typedqueries{
			public class MessageDaoImpl implements MessageDao {

				private EntityManager entityManager;

				//Deze klasse is eigenlijk je Repository in C#. 
				//De interface MessageDao is dan de IRepository in C#.
				//entityManager is Context in C#.
				public MessageDaoImpl(EntityManager entityManager) {
					this.entityManager = entityManager;
				}
				
				//@Override hoeft niet maar komt automatisch als je methodes van een interface implement
				@Override
				public List<Message> findAll() {
					TypedQuery<Message> query = entityManager.createQuery("SELECT m FROM Message m", Message.class);
					return query.getResultList();
				}
				
				//@Override hoeft niet maar komt automatisch als je methodes van een interface implement
				@Override
				public long count() {
					TypedQuery<Long> query = entityManager.createQuery("SELECT COUNT(m) FROM Message m", Long.class);
					return query.getSingleResult();
				}
			}
		}
	}
	
}
	

Spring{
	//Spring heeft een logische mappenstructuur.
	//be.pxl.examen
		//dao (bevat de interfaces, bij spring zijn dit al tegelijk de repositories.)
		//model (bevat de klasses)
		//rest
			//resources (bevat de modellen van de klasses)
		//service (bevat de controllers)
		//servlet (bevat de servlets)

	
	
	//Werk in deze volgorde. (behalve tests, doe die whenever you want to.)
	models{
	@Entity
	//Hiermee kies je de tabel naam. Default is de klassenaam.
	@Table(name = "Auctions")
	public class Auction {
		@Id
		@GeneratedValue(strategy = GenerationType.IDENTITY)
		//voor properties met camelCasing wordt default de naam opgesplitst met _ als kolomnaam.
		//dus voor een property genaamd firstName zou de kolomnaam first_name zijn.
		//Je kan kolomnaam zelf kiezen met @Column(name="....")
		private long id;
		private String description;
		private LocalDate endDate;
		//fetchtype EAGER meenemen! mappedBy zet je bij de parent property (dus de property met de collection/list)
		//cascadetype ALL ook belangrijk.
		@OneToMany(fetch = FetchType.EAGER, mappedBy = "auction", cascade = CascadeType.ALL)
		private List<Bid> bids = new ArrayList<>();
		
		//Getters en setters hier nog onder..
		}
		
		@Entity
		@Table(name = "Bids")
		public class Bid {

			@Id
			@GeneratedValue(strategy = GenerationType.IDENTITY)
			private long id;
			private double amount;
			private LocalDate date;
			@ManyToOne
			private Auction auction;
			@ManyToOne
			private User user;

			public Bid() {
			}
			//getters en setters hier nog onder..
		}
	}


	DAO{
		//zorg zeker voor de @Repository.
		@Repository
		public interface MessageDao extends JpaRepository<Message, Long> {
											//belangrijk!  ^klasse,  ^id type
			//Hier kan je allemaal custom methodes maken.
			//Begin gewoon met type (dus als je 1 Message wil, Message, als je meerdere wilt List<Message>)
			
			Message /*ctrl+spatie* geeft je heel veel autofills op basis van de properties van de klasse*/
			List<Message> /*ctrl+spatie* geeft je heel veel autofills op basis van de properties van de klasse*/
			
			//Deze is dus ook autofilled, hij gaf veel opties voor getMessageBy, hij zag de Text property
			//dan had ik nog opties voor starts with, ends with, ... 
			Message getMessageByTextEndsWith(String text);
		}	
	}


	restResources{
		//Resources zijn DTO objecten (data transfer objecten)
		//Dit is hetzelfde als het model maar dan met alleen de nodige properties in.
		//properties die niet voor het publiek bestemd zijn komen hier dus niet in.
		public class MessageResource {
			private Long id;
			private String text;

			public Long getId() {
				return id;
			}

			public void setId(Long id) {
				this.id = id;
			}

			public void setText(String text) {
				this.text = text;
			}

			public String getText() {
				return text;
			}
		}
		
		//CreateResource is dan ook weer hetzelfde maar deze resource is wat je binnenkrijgt als iemand
		//een post doet om een entity aan te maken (in dit geval Brewer)
		//niet alle properties van de klasse Brewer zijn nodig dus hier is een aparte klasse voor
		//met alleen de nodige properties.
		public class BrewerCreateResource {
			private String name;
			private String address;
			private String zipCode;
			private String city;
			private int turnover;	
		}
	}
	
	
	service{
		
		@Service
		public class MessageService {

			@Autowired
			private MessageDao messageDao;
			
			//de stream().map() zorgt er gewoon voor dat elk object in de lijst die ik terugkrijg
			//omgezet wordt in een messageResource via de private methode onderaan.
			//Collect stopt alles dan weer in een List.
			public List<MessageResource> getMessages() {
				return messageDao.findAll().stream().map(this::mapMessageToMessageResource).collect(Collectors.toList());
			}

			public List<MessageResource> findByTextContaining(String text){
				return messageDao.findByTextContains(text).stream().map(this::mapMessageToMessageResource).collect(Collectors.toList());
			}

			public MessageResource getMessage(Long id, String text){
				return mapMessageToMessageResource(messageDao.getMessageByIdEqualsAndTextStartsWith(id, text));
			}

			private MessageResource mapMessageToMessageResource(Message message) {
				MessageResource result = new MessageResource();
				result.setId(message.getId());
				result.setText(message.getText());
				return result;
			}

			public long countMessages() {
				return messageDao.count();
			}
		}
		
	}
		
	
	rest(controller){
		//Hier komen de controllers. Dit zijn de aanspreekpunten van buitenaf.
		@RestController
		//Dit zal het path zijn dat komt na je applicatiepath. bij mn auctions is dit dus 
		//localhost:8080/auctions/messages (auctions heeft geen messages, dit is van een ander project :p )
		
		@RequestMapping(path = "messages")
		public class MessageRest {

			//AutoWired moet in de controller boven de service en in de service boven de DAO
			@Autowired
			private MessageService messageService;

			@GetMapping
			public List<MessageResource> getMessages() {
				return messageService.getMessages();
			}
			
			//
			@GetMapping(value="{text}")
			public MessageResource getMessage(@PathVariable String text){
				return messageService.getMessageByText(text);
			}
			
			@PostMapping()
			public BrewerDTO createBrewer(@RequestBody BrewerCreateResource brewerCreateResource) {
				//BrewerCreateResource is een klasse in de rest.resources package.
			}
			
			//voor meerdere values:
			//Het zal automatisch proberen de eerste variable in de url te converten naar een Long:
			//als het niet gaat , krijg je een error page.
			@GetMapping(value="{id}/{text}")
			//url voor dit is dus bv: localhost:8080/2/Veel
			//id = 2, text = "Veel" (hoofdletter gevoelig)
			public MessageResource getMessage(@PathVariable Long id, @PathVariable String text){
				return messageService.getMessageByTextAndId(id, text);
			}
			
			//Om een andere status dan 200 te krijgen gebruik ik ResponseEntity return type:
			@PostMapping
			public ResponseEntity<AuctionResource> addAuction(@RequestBody AuctionCreateResource auction) {
				try {
					//201
					return new ResponseEntity(auctionService.createAuction(auction), HttpStatus.CREATED);
				} catch (InvalidDateException e) {
					LOGGER.error(e.getMessage(), e);
					//400
					return new ResponseEntity("Invalid date!", HttpStatus.BAD_REQUEST);
				}
			}

		}
	}


	Servlets{
	//vergelijkbaar met cshtml, of laravel, angular, vue, ....
	//Je kan de repo gebruiken om data op te halen en deze data kan je in html verwerken.
	bv:
	//Spring versie:
	//path is nu: localhost:8080/NaamVanApplication/index
	//								   ^dependencies in spring skelet
	@WebServlet("/index")
	public class MessageCountServlet extends HttpServlet {
		@Autowired
		private MessageService messageService;

		@Override
		protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
			resp.setContentType("text/html");
			resp.setCharacterEncoding("UTF-8");
			List<MessageResource> messages = messageService.getMessages();
			try (PrintWriter out = resp.getWriter()) {
				out.println("<html>");
				out.println("<body>");
				out.println("<h1>Er zijn momenteel  " + messages.size() + " boodschappen.</h1>");
				messages.forEach(m -> out.println("<p>" + m.getId()+": "+ m.getText() + "</p>"));
				out.println("</body>");
				out.println("</html>");
			}
		}
	}
}


	Tests{	

		DaoTest{
			//springboottest is nodig om @Autowired te doen op de DAO.
			@SpringBootTest
			//transactional zorgt ervoor dat alles teruggedraaid wordt na elke test (rollback)
			@Transactional
			public class AuctionDaoTest {

				//je kan geen interface object aanmaken, dus Autowired zorgt ervoor dat je deze kan gebruiken, don't ask me how.
				@Autowired
				private AuctionDao auctionDao;

				@BeforeEach
				public void SetUp() {
					Auction auction = new Auction();
					Auction auction2 = new Auction();
					auctionDao.save(auction);
					auctionDao.save(auction2);
				}

				@Test
				public void findAllAuctions_ReturnsAllAuctions() {
					List<Auction> result = auctionDao.findAll();

					assertEquals(2, result.size());
				}

				@Test
				public void findAuctionById_Returns_CorrectAuction() {
					Auction result = auctionDao.findAuctionById(1L);

					assertEquals(1L,result.getId());
				}
			}
		}
		
		
		serviceTest{
			@ExtendWith(MockitoExtension.class)
			public class AuctionServiceTests {


				private Auction auction;
				private User user;
				private Bid bid;
				private BidCreateResource bidCreateResource;
				@Mock
				private AuctionDao auctionDao;
				@Mock
				private UserDao userDao;
				@InjectMocks
				private AuctionService auctionService;

				@BeforeEach
				public void SetUp() {
					auction = new Auction();
					auction.setEndDate(LocalDate.now().plusDays(1));
					auction.setId(1);
					user = new User();
					user.setEmail("Satan@hell.com");
					bidCreateResource = new BidCreateResource();
					bidCreateResource.setPrice(2000);
					bidCreateResource.setEmail(user.getEmail());
					bid = new Bid();
					bid.setAmount(1);
					bid.setUser(user);
				}
				
				 @Test
				public void doBid_ThrowsException_When_NewBid_IsLower_Than_HighestBid() {
					bid.setAmount(3000);
					auction.addBid(bid);
					
					//
					when(auctionDao.findAuctionById(1L)).thenReturn(auction);
					when(userDao.findUserByEmail(user.getEmail())).thenReturn(user);

					//om te kijken of een methode een error throwed:
					//assertThrows(Exception klasse,()->methode om uit te voeren) lege haakjes lambda verplicht!
					Assertions.assertThrows(InvalidBidException.class, () -> auctionService.doBid(1L, bidCreateResource));
				}

				
			}
			
		}


	}

}






//Semester 1
Path{
		//user.home verwijst naar je home directory.
		//Vb:  C:\Users\11800460
		//.resolve plakt hier automatisch het vervolg van het pad aan. (is dus niet verplicht).
		Path p = Paths.get(System.getProperty("user.home")).resolve("Desktop/School-Y2-/Java Advanced/Data/phonedirectory.txt");
		
		//user.dir verwijst naar je huidige project directory
		//Vb: C:\Users\Satan\Desktop\Java-PE\JavaAdvancedPE
		Path p = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources");
	}


Stream (filter, limit, min, max){
	//filter gaat met een conditie werken, collect om er een lijst van te maken.
	ArrayList<Property> propertiesAbovePrice = 
		list.stream().filter(p -> p.getPrice() > price).collect(Collectors.toCollection(ArrayList::new));
	
	//Om het element met de laagste prijs te krijgen:
	Property cheapest = list.stream().min(Comparator.comparing(Property::getPrice)).get();
}
	

Working with Maps{
	//Een map is zoals een Dictionary in C#.
	//Dus je hebt een Value en een key.
	//vb:
	private Map<String, Customer> customers = new HashMap<>();
	
	//om hier gegevens uit te halen:
	//.get(key) 
	customers.get(id);
	
	//Dit zal dus de customer halen met als key dit id.
	//om alle values op te halen of alle keys op te halen kunnen we streams en collect gebruiken
	//dan krijg je dus een ArrayList<Customer> of ArrayList<??> van de key type:
	customers.values().stream().collect(Collectors.toList());
	customers.keySet().stream().collect(Collectors.toList());
	
	
	
	
	
	
}


File Reading/Writing{

	BufferedReader + BufferedWriter{
	//BufferedReader gebruik: 
	   try (BufferedReader reader = Files.newBufferedReader(p)) {
			String line = null;
			while ((line = reader.readLine()) != null) {
				System.out.println(line);
			}

		} catch (IOException ex) {
			System.out.println("Oops, something went wrong!");
			System.out.println(ex.getMessage());

		}
	}
	
	//BufferedWriter gebruik:
		public static void main(String[] args) {
			Path path = Paths.get(System.getProperty("user.dir)).resolve("src/main/resources/myfile.txt"); 
			try(BufferedWriter writer = Files.newBufferedWriter(path)) {
				writer.write("test");
			} 
			catch (IOException ex) {
				System.out.println(ex.getMessage());
			} 
		}
	
	//BufferedWriter with Append or create, depending if file exists.
		Path path = Paths.get(System.getProperty("user.dir)).resolve("src/main/resources/myfile.txt"); 
	    try (BufferedWriter writer = Files.newBufferedWriter(path, StandardOpenOption.CREATE, StandardOpenOption.APPEND)) {
            writer.append(message);
            writer.newLine();
        } catch (IOException ex) {
            ex.getMessage();
        }

	}
	
	
	Stream{
		//Stream gebruik:
		Path p = Paths.get(System.getProperty("user.home")).resolve("Opdrachten/Opdracht1/code.code");
		try {
			Stream<String> lines = Files.lines(p);
			//Je kan heel de stream in een list steken:
			//List<String> result = lines.sorted().distinct().collect(Collectors.toList());
			lines.filter(l -> l.toUpperCase() == l).forEach(System.out::print);

		} 
		catch (IOException ex) {
			System.out.println("Oops, something went wrong!");
			System.out.println(ex.getMessage());
		}
	
	
		//Stream gebruik maar in een list opslaan:
		try {
			Stream<String> lines = Files.lines(p);
			List<String> result =
					lines.sorted().distinct().collect(Collectors.toList());

		}
		catch (IOException ex) {
			System.out.println("Oops, something went wrong!");
			System.out.println(ex.getMessage());
		}
	}


	
	Serializable{
		File Writing{
			public static void main(String[] args) {
				Spaarrekening rekening = new Spaarrekening(666, "BE48 321 666 999", "Milosz Boghe");
				try (FileOutputStream file = new FileOutputStream("Rekening.ser");
					 ObjectOutputStream out = new ObjectOutputStream(file)) {
					out.writeObject(rekening);
				} catch (IOException ex) {
					System.out.println(ex.getMessage());
				}
			}
		}
		
		File Reading{
			try (FileInputStream file = new FileInputStream("Rekening.ser");
				 ObjectInputStream in = new ObjectInputStream(file)) {
				Spaarrekening gelezen = (Spaarrekening) in.readObject();
				System.out.println(gelezen.toString());
			} catch (IOException | ClassNotFoundException e) {
				e.printStackTrace();
			}
		}	
	}
	
}


Generics {
	
	Letter{
		//Welke letter maakt geen kloot uit. 
		public class DistanceUtil {
			public static <T extends DistanceFunction<T>> T findClosest(Set<T> elements, T otherElement) {
				ArrayList<T> listOfElements = new ArrayList<T>(elements);
				T smallest = null;
				double smallestDistance = 1000000;
				for (T element : listOfElements) {
					if (element != otherElement) {
						double distance = element.Distance(otherElement);
						if (distance < smallestDistance) {
							smallestDistance = distance;
							smallest = element;
						}
					}
				}
				return smallest;
			}
		}
	}	
	
	Sort/Comparable{
		//Als je met Collections.sort gaat werken moet je comparable implementen in de klasse die gebruikt wordt:
		public class Attraction implements Item, Comparable<Attraction>{
		// in de compareTo methode:
			public int compareTo(Attraction attraction) {
				return Integer.compare(attraction.rating, this.rating);
			}	
		}
	}
}


Formatter{
	//formatter maken
	DateTimeFormatter formatter = DateTimeFormatter.ofPattern("EEE MMM dd HH:mm:ss z yyyy", Locale.US);
    LocalDateTime saleDate = LocalDateTime.parse(split[8], formatter);
}

	
MultiThreading{
	//Implement Runnable OF Thread.
	//Thread is gemakkelijker. 
	//Bij beide moet je instantievariabelen hebben die je kan aanroepen om te gebruiken in de run() methode.
	public class Writer implements Runnable{
		private Path filePath;
		private List<Property> properties;
		
		//constructor
		public Writer(String filename, List<Property> properties) {
			this.filePath = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/" + filename);
			this.properties = properties;
		}
		
		
		//Bij runnable moet je in je main klasse een Thread maken met als constructor
		//het Object dat je wil multithreaden.
		 public static void main(String[] args) {
			Thread Writer = new Thread(new Writer(".........."));
			Writer.start();
		 }
		 
		 
		 //writer.start() zal dan de run() methode oproepen. GEBRUIK GEEN .RUN().
		@Override
		public void run() {
			try (BufferedWriter writer = Files.newBufferedWriter(filePath)) {
				properties.forEach(property -> {
					try{
						writer.write(property.toFormattedOutput());
						writer.newLine();
					}catch(IOException ex){
						ex.printStackTrace();
					}
				});
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		} 
	}
	
	//Bij Thread:
	//als een klasse Thread extend dan kan je de methode .start() erop uitvoeren.
	//deze methode zal de .run() methode in de klasse runnen , met multithreading.
	//dus om dit te gebruiken moet je in main klasse ook nog een object van deze klasse maken en dan .start():
	public static void main(String[] args) {
			Writer writer = new Writer("output.txt", [een of andere list] );
			Writer.start();
	}
	
	public class Writer extends Thread {
		private Path filePath;
		private List<Property> properties;

		public Writer(String filename, List<Property> properties) {
			this.filePath = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/" + filename);
			this.properties = properties;
		}

		@Override
		public void run() {
			try (BufferedWriter writer = Files.newBufferedWriter(filePath)) {
				properties.forEach(property -> {
					try{
						writer.write(property.toFormattedOutput());
						writer.newLine();
					}catch(IOException ex){
						ex.printStackTrace();
					}
				});
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		}
	}

}

	
	
	
	