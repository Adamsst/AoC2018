<?php 

$input = array();
$lines = file('input.txt');
$sum=0;

foreach ($lines as $singleLine){
	$temp = explode(" ",$singleLine);
	foreach($temp as $num){
		array_push($input,(int)$num);
	}
} 

$child = array();
$data = array();
$i=2;
$l = 1;
$child[0] = $input[0];
$data[0] = $input[1];

while($i < count($input)){	
	$child[$l] = $input[$i];
	$i++;
	$data[$l] = $input[$i];
	$i++;
	
	if($child[$l] == 0){
		$j=$i;
		while($i < $data[$l] + $j){
			$sum += $input[$i];
			$i++;
		}
		$child[$l-1]--;
	}
	else{
		$l++;
	}
	$x = 1;
	while($x > 0 && $i < count($input)){
		if($child[$l-$x]==0 && $l > 0){
			$j=$i;
			while($i < $data[$l-1] + $j){
				$sum += $input[$i];
				$i++;
			}
			if($l > 0 && $i <count($input)){
				$l--;
				$child[$l-$x]--;
			}
			else{
				$i = count($input);
			}
		}
		else{
			$x = 0;
		}
	}
}

echo "<br>";
echo $sum;

?>