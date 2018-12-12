<?php

$grid = array();
$power = array();
$max=0;

for($i=1;$i<=300;$i++){
	$grid[$i]=array();
	for($j=1;$j<=300;$j++){
		$id = $i+10;
		$temp = $id * $j;
		$temp += 7989;//7989 is input
		$temp = $temp * $id;
		$mod = $temp % 1000;
		$mod = floor($mod /100);
		$mod-=5;
		$grid[$i][$j] = $mod;
	}
}

for($i=2;$i<=299;$i++){
	$power[$i]=array();
	for($j=2;$j<=299;$j++){
		$temp = 0;
		for($ii=$i-1;$ii<=$i+1;$ii++){
			for($jj=$j-1;$jj<=$j+1;$jj++){
				$temp += $grid[$ii][$jj];
			}
		}
		$power[$i][$j]=$temp;
		if($temp > $max){
			echo "new max of $temp at $i,$j subtract 1 for upper left \n";
			$max = $temp;
		}
	}
}

?>