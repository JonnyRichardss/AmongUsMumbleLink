# Among Us MumbleLink
A program that reads position data from among us to be fed into mumble's 'link' plugin for positional audio, intended to create a new gamemode.

This program **ONLY** supports windows.


Gameplay rules/notes:
I'm putting this at the top so hopefully everyone actually reads it.
1. You still need to mute when you die
2. 2.5m in mumble settings is the same as 0.5x crewmate vision, you can use this to change the distance you hear each other from
3. I recommend muting when comms sabotage is called to both nerf the enhanced communications and also make that sabotage do something

## WARNING BEFORE USE
THIS PROGRAM DIRECTLY READS THE MEMORY OF AMONG US EXTERNALLY. AT THE MOMENT (25TH OCT 2020) AMONG US DOES *NOT* HAVE ANTI-CHEAT. ANTI-CHEAT PROGRAMS DO *NOT* LIKE THEIR GAME'S MEMORY BEING ACCESSED AND WILL LIKELY EITHER CLOSE THE GAME OR BAN YOU ON THE SPOT FOR USING THIS. I ACCEPT NO RESPONSIBILITY FOR USERS BEING DUMB AND TRIPPING AC WITH THIS PROGRAM.
(Temporarily Disabling Vanguard is also recommended but *unlikely* to be necessary.)
# Short Guide
This is a shortened version of the guide below:
1. Install Mumble from [mumble.info](https://www.mumble.info/downloads/)
2. Make sure you have access to a server (either self-hosted using the bundled server program or by renting one)
3. Set mumble audio output's positional audio settings to those shown in [this image](https://imgur.com/a/Qqu5YMo)
4. Check 'Link to game and transmit position' in plugins
5. Download and extract the correct version of AmongUsMumbleLink from the releases here on GitHub
6. Connect to the same mumble server as your fellow crewmates
7. Start Among Us
8. Run AmongUsMumbleLink and click 'toggle capture' once you are in the game lobby
9. Bind your 'Toggle Meeting' Hotkey so you can lock your position when a meeting starts
9. Play the Game! Be sure to toggle meetings when they start and end otherwise you won't hear everyone properly

# Long Guide
How to get started using AmongUsMumbleLink to play Among Us with positional voice chat.
#### 1. Mumble
##### 1.1 Client
Mumble is the VOIP program that is used to power this 'gamemode'.
Install it from here: [Mumble Downloads](https://www.mumble.info/downloads/)
##### 1.2 Server
You will need a mumble server for your users to connect to, you can install 'murmur', the server software from the client installer linked above (this will require port forwarding/opening to allow users to connect) or you can rent one from the [list of hosters](https://wiki.mumble.info/wiki/Hosters) on the wiki.
##### 1.3 Settings
Mumble's settings need to be configured in order to mute players that are too far away. 
First is the *Audio Output* settings. It is important that 'Positional Audio' is enabled, 'Minimum Volume' is set to 0% and 'Maximum Distance' is set to 2.5m (adjust this at will, 2.5m = Crewmate Vision 0.5x). Check 'Headphones' if you are using headphones. 
The only other setting that needs to be specially changed is in the 'Plugins' tab, check the 'Link to game and transmit position' box.
I will not talk about setting up input/output devices/volumes as this is trivial for most users (if you can't figure that out good luck getting the rest of this to work)
[Picture of 'Audio Output'](https://imgur.com/a/Qqu5YMo)
NOTE:
Although it is adjusted client side, all players MUST set Minimum Volume and Maximum Distance to the same values as each other as using a higher distance will allow you to hear players from further away and in extreme cases let you listen to every conversation on the map. A non-zero minimum volume will mean players are never fully muted when out of range.
##### 1.4 Connecting
Connecting to a mumble server is simple:
1. Go to the 'Mumble Server Connect' window under Server -> Connect at the top
2. Click add server
3. If prompted, type your desired nickname
4. Fill in the 'Address' 'Username' and Optionally 'Label' Fields (The Address is the IP address of the server, given by the server host OR the IP address of the person who is running it)
5. *(Optional)* Fill in the port if your hosting provider gives you one other than *'64738'*
Once connected, you are ready to open the game and program

#### 2. Getting the Program
1. Download the latest release for your current game version (memory locations vary build to build so each version has to be unique)
At last update of this README the current stable (non-beta) release is v2020.9.22s
2. Extract the ZIP contents to a folder on your PC (don't lose this, you will need it later to actually run the program)

It's worth noting that this project requires administrator access due to the way it reads memory. This accessing of other programs' memory can (and probably will) set off antivirus / Windows SmartScreen warnings. You have every right to be suspicious but I have attempted to include all code used (including the offending and previously precompiled 'memory.dll') so that anyone concerned can verify for themself that I have no bad intentions. 
HOT TIP TO INNERSLOTH, MAKE THE GAME OUTPUT LOCAL PLAYER'S NAME, POSITION AND GAME STATE (eg. lobby, tasks, meeting) ETC. SO I DON'T HAVE TO GRAB DATA LIKE THIS :)

#### 3. Game
##### 3.1 Setup
Once you have mumble working and have downloaded the program, all that is left to do is to open Among Us, start the program 'AmongUsMumbleLink.exe' and join a lobby.
AFTER you have been in a lobby at least once (i.e. you have had control of your character), you can click 'Toggle Capture' to start sending game data to mumble.
##### 3.2 Gameplay
The Program will automatically disable positional voice when you are not in a round (lobby or menu) but cannot find whether a meeting is ongoing (mainly because I can't figure out how to import a different memory reader's code that can - any help yoinking GameState from [here](https://github.com/denverquane/amonguscapture) would be appreciated) so meetings have to be enable manually. Simply press the 'Toggle Meeting' button (or bind the hotkey using the instructions in the program) when a meeting starts and ends to lock your 'player' into the meeting table so you can talk to everyone (on polus and mira the table is bigger than vision so some players won't hear each other.)
Other suggested rule:
Everyone mute mics when comms is sabotaged (AGAIN INNERSLOTH I CAN DO THIS IF YOU SEND OUT THE GAME DATA).
#### 4. Done!
Your game should now be set up and working to play among us with positional audio.
Have Fun!

Any issues:
Issues Tab for programming stuff / crashes (bear in mind I am a first year CS student so to say my knowledge is limited is an understatement)
Jonny#0123 on Discord
@jonnyrichardss on twitter



As I think I didn't import them properly, other projects included as source code:
https://github.com/shlifedev/AmongUsMemory (uses https://github.com/erfg12/memory.dll)
https://github.com/StevenLiekens/MumbleLink
