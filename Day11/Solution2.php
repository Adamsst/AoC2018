<?php

$grid = array();
$max=0;//we should max this faster.

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

for($i=90;$i<=300;$i++){
	echo "i is now $i \n";
	for($j=1;$j<=300;$j++){		
		$k = 0;	
		while(($i + $k <= 300) && ($j + $k <= 300)){	
		    $temp = 0;
			for($ii=$i;$ii<=$i+$k;$ii++){
				for($jj=$j;$jj<=$j+$k;$jj++){
					$temp += $grid[$ii][$jj];
				}
			}
			$k++;
		
			if($temp > $max){
				echo "new max of $temp at $i,$j  size $k \n";
				$max = $temp;
			}
		}				
	}
}

?>