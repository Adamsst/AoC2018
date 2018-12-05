data = ''
done = 0

with open('input.txt','r') as tempFile:
	data = tempFile.read().replace('\n','')

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
print(len(data))
	