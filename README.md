# Inti_OSBReader
an expremintal project to read inti creates .osb files. It can extract sprite sheets, reconstruct frames, read color palettes, and read animations.

Instructions:
The .osb files must be decrypted before this program can read them. Follow the steps below to prepare your files.
To decrypte them go to "https://github.com/bnnm/vgm-tools/tree/master/misc/inti" and download:
1- intidec.py
2- inti-renamer.py
3- names/Dragon Marked for Death (PC).txt

rename "Dragon Marked for Death (PC).txt" to "names.txt"
make a copy of "DataHash" folder from the game location into the same directory as the scripts

Open a command prompt in that directory and run:
python inti-renamer.py -a "DataHash\**\*"
python intidec.py "DataHash\**\*.osb" -k obj90210 -z

a new fodler will apear called "_out" and now you can delete the other files

The program still in progress and still have many problems in the animations and reconstructing of frames.
In case anyone interested I left a small documentation in the program that can show you the file data it is reading and how it seperate each part with some notes.
