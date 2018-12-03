<?php 

$input = array();
$lines = file('input.txt');

foreach ($lines as $singleLine){
	array_push($input,trim($singleLine));
}

$total = 0;
$found = false;
$freqs = array();

while(!$found){
	for($i = 0; $i < count($input); $i++){
		$length=strlen($input[$i]);
		if($input[$i][0] == "+"){
			$total += (int)substr($input[$i],1,$length-1);
		}
		else{
			$total -= (int)substr($input[$i],1,$length-1);
		}
	
		if(in_array($total,$freqs)){
			$found=true;
			break;
		}
		else{
			array_push($freqs,$total);
		}
	}
}

echo $total;

?>