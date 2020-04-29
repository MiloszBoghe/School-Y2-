
Unit Testing{
	//in pom.xml
	<dependency>
		<groupId>org.junit.jupiter</groupId>
		<artifactId>junit-jupiter-api</artifactId>
		<version>5.5.2</version>
		<scope>test</scope>
	</dependency>
	
	//groupId = be.pxl....? de packagestructuur waar je project in komt.
	//artifactId = name of the project.
	
}


Path{
		//user.home points to your home directory.
		//Vb:  C:\Users\Satan
		//.resolve adds whatever you put to it.
		Path p = Paths.get(System.getProperty("user.home")).resolve("Desktop/School-Y2-/Java Advanced/Data/phonedirectory.txt");
		
		//user.dir points to your current project, top level.
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
	//A map is like a Dictionary in C#.
	//So you have a value and a key.
	//vb:
	private Map<String, Customer> customers = new HashMap<>();
	//to get data:
	//.get(key) 
	//This will get the customer with the given id:
	customers.get(id);
	
	//to get all values or all keys we can use streams and collect:
	//you'll get an ArrayList<Customer> or ArrayList<keytype>:
	customers.values().stream().collect(Collectors.toList());
	customers.keySet().stream().collect(Collectors.toList());
}


File Reading/Writing{

	BufferedReader + BufferedWriter{
	//BufferedReader: 
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
	
	//BufferedWriter:
		public static void main(String[] args) {
			Path path = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/myfile.txt"); 
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
		//Stream:
		Path p = Paths.get(System.getProperty("user.home")).resolve("Opdrachten/Opdracht1/code.code");
		try {
			Stream<String> lines = Files.lines(p);
			//you can convert the stream to a list:
			//List<String> result = lines.sorted().distinct().collect(Collectors.toList());
			lines.filter(l -> l.toUpperCase() == l).forEach(System.out::print);

		} 
		catch (IOException ex) {
			System.out.println("Oops, something went wrong!");
			System.out.println(ex.getMessage());
		}
	
	
		//using Stream saving to list:
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
		//Buffered is easier, ignore this if you have a choice :P
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
		//Buffered is easier, ignore this if you have a choice :P
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

}

Generics {
	
	Letter{
		//which letter you pick doesn't matter. <T>, <E> , ...? 
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
		//If you're working with Collections.sort, you need to implement a Comparable to your class.
		public class Attraction implements Item, Comparable<Attraction>{
		// compareTo method:
			public int compareTo(Attraction attraction) {
				return Integer.compare(attraction.rating, this.rating);
			}	
		}
	}
}


Formatter{
	//formatter
	DateTimeFormatter formatter = DateTimeFormatter.ofPattern("EEE MMM dd HH:mm:ss z yyyy", Locale.US);
    LocalDateTime saleDate = LocalDateTime.parse(split[8], formatter);
}

	
MultiThreading{
	//Implement Runnable OF Thread.
	//Thread is easier. 
	//For both you'll need instance variabeles which you can call to use in run() method.
	public class Writer implements Runnable{
		private Path filePath;
		private List<Property> properties;
		
		//constructor
		public Writer(String filename, List<Property> properties) {
			this.filePath = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/" + filename);
			this.properties = properties;
		}
		
		
		//For runnable you'll make a Thread in your main method with the object you want as constructor parameter.
		 public static void main(String[] args) {
			Thread Writer = new Thread(new Writer(".........."));
			Writer.start();
		 }
		 
		 
		 //writer.start() will call the run() method. DO NOT USE .run(). or it will call it single threaded.
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
	
	//Using Thread:
	//If a class extends Thread then you can call .start() on it.
	//This method will start the .run() method you override in that class.
	//This will be multithreaded.
	//So to use this , you'll just need to make the Object you want in your main method and call .start().
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




