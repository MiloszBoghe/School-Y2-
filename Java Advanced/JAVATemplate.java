File Reading{
	
	Path{
//user.home verwijst naar C:\Gebruikers\11800460
//.resolve plakt hier automatisch het volgende pad aan.
//Vb:
	Path p = Paths.get(System.getProperty("user.home")).resolve("Desktop\\School-Y2-\\Java Advanced\\Data\\phonedirectory.txt");
	}
	
	
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
		}finally{
			
		}
	}
	
	
	Stream{
	//Stream gebruik:
		try {
			Stream<String> lines = Files.lines(p);
			//List<String> result =
			  //      lines.sorted().distinct().collect(Collectors.toList());
			lines.filter(l -> l.toUpperCase() == l).forEach(System.out::print);

		} catch (IOException ex) {
			System.out.println("Oops, something went wrong!");
			System.out.println(ex.getMessage());
		}
	
	
		//Stream gebruik maar in een list opslaan:
		try {
			Stream<String> lines = Files.lines(p);
			List<String> result =
					lines.sorted().distinct().collect(Collectors.toList());

		} catch (IOException ex) {
			System.out.println("Oops, something went wrong!");
			System.out.println(ex.getMessage());
		}
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
		


