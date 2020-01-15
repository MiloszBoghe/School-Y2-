
Path{
		//user.home verwijst naar je home directory.
		//Vb:  C:\Users\11800460
		//.resolve plakt hier automatisch het vervolg van het pad aan. (is dus niet verplicht).
		Path p = Paths.get(System.getProperty("user.home")).resolve("Desktop/School-Y2-/Java Advanced/Data/phonedirectory.txt");
		
		//user.dir verwijst naar je huidige project directory
		//Vb: C:\Users\Satan\Desktop\Java-PE\JavaAdvancedPE
		Path p = Paths.get(System.getProperty("user.dir")).resolve("src/main/...");
	}

Stream{
	
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
		

File Reading{

	BufferedReader{
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

	
	
	
	
	
	