<?php
$aantal=$_GET['aantal'];
if(ctype_digit($aantal) && (int)$aantal>0){
    ?>
    <form action="statistiek.php">
    <?php
         for($i=0;$i<(int)$aantal;$i++){
             print("<input type='text' name='getallen[]'/><br/>");
         }
    ?>
    <input type="submit"/>
    </form>
    <?php
} else {
    header("Location: ingave.html");
}
