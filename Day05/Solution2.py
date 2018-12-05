startData = ""
data = ""
done = 0

with open("input.txt","r") as tempFile:
	startData = tempFile.read().replace("\n","")
answer = len(startData)

while not bool(done):
	i = 0
	done = 1
	while i < (len(startData) - 1):
		if startData[i].islower():
			if startData[i+1].isupper():
				if startData[i] == startData[i+1].lower():
					first = startData[0:i]
					second = startData[i+2:]
					i=len(startData)
					startData = first + second
					done = 0					
		elif startData[i].isupper():
			if startData[i+1].islower():
				if startData[i] == startData[i+1].upper():
					first = startData[0:i]
					second = startData[i+2:]
					i=len(startData)
					startData = first + second
					done = 0	
		i += 1	
		
done = 0
for character in ["a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"]:
	data = startData
	data = data.replace(character,"")
	data = data.replace(character.upper(),"")
	while not bool(done):
		i = 0
		done = 1
		while i < (len(data) - 1):
			if data[i].islower():
				if data[i+1].isupper():
					if data[i] == data[i+1].lower():
						first = data[0:i]
						second = data[i+2:]
						i=len(data)
						data = first + second
						done = 0					
			elif data[i].isupper():
				if data[i+1].islower():
					if data[i] == data[i+1].upper():
						first = data[0:i]
						second = data[i+2:]
						i=len(data)
						data = first + second
						done = 0	
			i += 1				
	if(len(data) < answer):
		answer = len(data)
	done = 0
print(answer)
	 