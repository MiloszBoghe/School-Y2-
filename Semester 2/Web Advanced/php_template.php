//OPEN THIS FILE IN NOTEPAD++
<?php
	<? composer:
		//terminal --> composer init
		//in composer.json:
		{
			"name": "11800460/oop",
			"scripts": {
				"build": "webpack --config webpack.config.js", //voor webpack, belangrijk!
				"test": "jest",
			},
			"autoload": {
				"psr-4": {
					//"namespace\\": "dir/subdir"
					"Util\\": "src/Util/"
					}
			},
			"authors": [
				{
					"name": "Satan",
					"email": "11800460@student.pxl.be"
				}
			],
			"require": {}
		}
		//daarna:
		//composer dump-autoload
		
		Om te importeren: {
			//Alleen de file die je zal uitvoeren:
			require_once 'vendor/autoload.php';
			use (namespace)\(class) //deze use kan je genereren met alt+enter als je een externe klasse gebruikt.
		}
	?>

	<? classes & interfaces:
		//Interface:		
		//namespace hoeft niet, is voor composer. Mag je zelf kiezen.
		namespace interfaces;

		interface Tool
		{
			public function doSomething(): void;
		}
		
		namespace Util;
		
		class Date //om interface te implementen: implements Tool
		{
			private $day;
			private $month;
			private $year;
			private static $MONTHS = array("", "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sept", "oct", "nov", "dec");

			
			//constructor, meestal public maar kijk naar opgave. Soms willen ze private
			//(default waardes meegeven maakt parameters optioneel)
			private function __construct($day = 1, $month = 1, $year = 2008)
			{
				$this->day = $day;
				$this->month = $month;
				$this->year = $year;
			}
			
			//Als de constructor private is , make methode maken:
			public static function make($day = 1, $month = 1, $year = 2008)
			{
				//self verwijst naar huidige klasse, dus new self() = new Date();
				return new self($day, $month, $year);
			}				

			public function print(): string
			{
				return $this->day . "/" . $this->month . "/" . $this->year;
			}

			//Voor getters/setters: alt+shift+0 op numpad 
			//(officieel alt+insert, voor de mensen met ander toetsenbord)
		}
	?>

	<? Exceptions:
		use Exception;
		//Dit is alles:
		class DateException extends Exception
		{
			
		}
		//om te gebruiken in een klasse:
		try{
			if(is_nan($day)){
				throw new DateException("Day is not a number!.");
			}
		}
		catch(DateException $e){
			echo "Error: ". $e->getMessage();
		}
		
	?>
	
	PDO{		
		<?php //SQL Query
			$user = "root";
			$password = "";
			$database = "database name";
			$pdo = null;

			try {
				$pdo = new PDO("mysql:host=localhost;dbname=$database",$user);
				$pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
				//:title & :id --> zorgt ervoor dat je de parameter kan inserten door ze aan te maken met de bindParam eronder, dit is secure tegen SQL injections
				
				//STATEMENT EXAMPLES:
				//$statement = $pdo->prepare("SELECT * FROM persons WHERE id=:id AND title=:title")
				//$statement = $pdo->prepare("UPDATE table_name SET title=:title WHERE id=:id");
				//$statement = $pdo->prepare("DELETE FROM table_name WHERE id=:id");
				//$statement = $pdo->prepare("INSERT INTO table_name(titel, naam , ...) VALUES(?,?,?)")
				//													  ^alle kolommen
				//------------------------------------------------------------------------------------------
				
				$statement->bindParam(":title", $title, PDO::PARAM_STR);
				$statement->bindParam(":id", $id, PDO::PARAM_INT);
				
				//OF korter:
				$result = $statement->execute(array(':title' => $title, ':id' => $id));
				//------------------------------------------------------------------------------------------
				
				//result is number of rows bij update/delete/insert, array van values bij SELECT.
				$result = $statement->execute();
			
			} catch ( PDOException $e){
				print "Exception!: " . $e->getMessage();
			}
			//close connection
			$pdo = null;
		?>	
		
		Example{
			//-----------------EXAMPLE----------------------
			<?php //SQL Query
				$user = "root";
				$database = "examenwa2019";
				$pdo = null;

				try {
					$pdo = new PDO("mysql:host=localhost;dbname=$database", $user);
					$pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
					$statement = $pdo->prepare("SELECT * FROM author");
					$statement->setFetchMode(PDO::FETCH_ASSOC);
					
					//KIES 1:
				
					$statement->execute();   		  //--> de while gebruiken in de form
					//$result = $statement->fetchAll(); --> de foreach gebruiken in de form
				
				} catch (PDOException $e) {
					print "Exception!: " . $e->getMessage();
				}
				//close connection
				$pdo = null;
			?>

				<form action="showbooks.php">
					<select name='id'>
						<?php
						
							//while ($row = $statement->fetch()) {
							//	print("<option value='$row[id]'>$row[name]</option>");
							//}
							
							//OF
							
							//foreach($result as $author){
							//	print("<option value='$author[id]'>$author[name]</option>");
							//}
						
						?>
					</select>
					<input type="submit" value="Submit">
				</form>
		}
	}
?>