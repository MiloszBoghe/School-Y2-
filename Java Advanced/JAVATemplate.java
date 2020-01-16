
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

	
	
	
	