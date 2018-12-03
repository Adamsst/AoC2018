<?php 

$input = array();
$lines = file('input.txt');
$two = 0;
$three = 0;

$twoFlag = true;
$threeFlag = true;

foreach ($lines as $singleLine){
	array_push($input,trim($singleLine));
}

foreach($input as $str){
	$twoFlag = true;
	$threeFlag = true;
	for($i = 0; $i < strlen($str); $i++){
		if(substr_count($str,$str[$i]) == 2 && $twoFlag){
			$twoFlag = false;
			$two++;
		}
		else if(substr_count($str,$str[$i]) == 3 && $threeFlag){
			$threeFlag = false;
			$three++;
		}
	}
}

echo $two * $three;

?>