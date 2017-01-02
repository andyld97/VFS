<?php

// To encrypt bytes
$MainCounter = 128;
$PackByte = 45;

// Read bytes
$bytes = file_get_contents("id0");
$splt = explode("|", $bytes);

$byteList = array();

$sy = array();
$s = 0;

for ($i = 0; $i <= count($splt) - 1; $i++)
	$sy[$i] = chr($splt[$i]);

for ($i = 0; $i <= count($sy) - 1; $i++)
{
	if (CheckNextItems($MainCounter, $PackByte, $i, $sy))
	{
	  //  $byteList[$s] = array();
	    $i += $MainCounter - 1;
	    $s++;
	}
	else
		$byteList[$s] .= $sy[$i];

}

$files = explode("<", ByteArrayToString($byteList[0]));

if (count($byteList) == 1 && count($files) != 1)
	return;

$rFiles = array();
$rFolders = array();

$fCount = count($files) - 1;

if ($fCount == 0 || $fCount == 1)
	$rFiles = explode("|", $files[0]);
if ($fCount == 1)
	$rFolders = explode(",", $files[1]);

for ($i = 0; $i <= count($rFolders) - 1; $i++)
{
	try 
	{
			$rFolders[$i] = str_replace(">", "", $rFolders[$i]);
			$rFolders[$i] = str_replace("<", "", $rFolders[$i]);
			$pt = correctionPath($rFolders[$i]);
			if ($pt != "" && !is_dir($pt))
				mkdir(correctionPath($rFolders[$i]), 0777, true);
	} 
	catch (Exception $e) 
	{
			
	}
}

$count = 1;
for ($xy = 0; $xy <= count($byteList) - 1; $xy++)
{
	if ($xy != 0)
	{
		$fileName = correctionPath($rFiles[$count - 1]);
		try 
		{
			$path = "";
			if (0 === strpos($fileName, "\\")) 
				$path = correctionPath($fileName);
			else 
				$path = $fileName;
			file_put_contents($path, $byteList[$xy]);

		} catch (Exception $e) {
			
		}
		$count++;
	}
}

function correctionPath($file)
{
	if ($file == "")
		return "";
	$toRet = "";
	for ($i = 1; $i <= strlen($file) - 1; $i++)
		$toRet .= $file[$i];
	$toRet = str_replace("\\", "/", $toRet);
    return $toRet;
}


function ByteArrayToString($byteArr)
{
	$byte_array = unpack('C*', $byteArr);
 	return call_user_func_array("pack", array_merge(array("C*"),  $byte_array));
}

function CheckNextItems($count, $byte, $index, $btArray)
{
	$check = false;
	$x = 0;

	for ($i = $index; $i <= $index + $count - 1; $i++)
	{
		if ($i >= count($btArray) - 1)
			return false;
	    if ($btArray[$i] == chr($byte))
	    {
	    	if ($x == $count)
	    		return true;
	    	$check = true;
	    	$x++;
	    }
	    else 
	    	return false;
	}
	return $check;
}



?>
