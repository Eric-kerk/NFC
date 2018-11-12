<?php
require("connect.php");
$username = $_POST["penggunaid"];
$password = $_POST["katalaluan"];
$mobile = $_POST["mobilenum"];

if (  ($username != "") && ($password != "") && ($mobile != "")  )    
{
    $CheckClash = "SELECT * FROM LectureAcc WHERE mobile='$mobile'";
    $Clashresult = mysqli_query($link, $CheckClash);
    $num = mysqli_num_rows($Clashresult);
    
    if ($num >=1)
    {
         echo "Duplicate";
        
    }   
    else
    {
        $sql = "INSERT INTO LectureAcc (username, password, mobile)
        VALUES ('$username' , '$password' , '$mobile')";
        $stmt = $link->prepare($sql);
        if($stmt === false){
            http_response_code(404);
            die(mysqli_error($link));
        }

        if(!$stmt->execute()){
            die(mysqli_error($link));
        }
        $stmt->close();
           echo "Everything OK";
           }
}


else 
{
    echo "Incomplete Data";
}

// Close mySQL Connection
mysqli_close($link);

unset($_POST);

?>