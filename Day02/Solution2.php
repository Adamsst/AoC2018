<?php 

$input = array();
$lines = file('input.txt');
$first;
$second;

foreach ($lines as $singleLine){
	array_push($input,trim($singleLine));
}

for($i = 0; $i < count($input); $i++){
	$needle = $input[$i];
	for($j = 0; $j < count($input); $j++){
		if($j != $i){
			$needle2 = $input[$j];
			$total = 0;
			for($k = 0; $k < strlen($needle); $k++){
				if($needle[$k] != $needle2[$k]){
					$total++;
				}
				if($total > 1){
					$k = strlen($needle);
				}
				else if($k == strlen($needle) - 1){
					if($total == 1){
						$i = count($input);
						$j = count($input);
						$first = $needle;
						$second = $needle2;
					}
				}
			}
		}
	}
}

for($i = 0; $i < strlen($first); $i++){
	if($first[$i] == $second[$i]){
		echo $first[$i];
	}
}



?>