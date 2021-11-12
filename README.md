# StreamDeckLockScreenHack
Turns off the StreamDeck when computer is locked.<br />
At least, most of the times.

Unzip to `%appdata%` folder, and create a scheduled task that triggers `At startup`, check `Hidden` and `Run whether user is logged in or not`.

Configure:
- Edit `StreamDeckLockScreenHack.exe.config`<br />
- When config is saved, nothing will change untill you rerun the application (end process and restart).

There are 3 properties:
```xml
<!-- Used to set how often it should check for the computer beeing locked (the window here is quite small, so keep this number low ex. 1 second) -->
<add key="WaitTime" value="00:00:01" />
<!-- Set the locked computer brightness, 0 is "off" -->
<add key="BrightnessLocked" value="0" />
<!-- Set the locked computer brightness, 30 is my default, you might want another one. -->
<add key="BrightnessUnlocked" value="30" />
```

Dependencies:
 - [StreamDeckSharp](https://github.com/OpenMacroBoard/StreamDeckSharp) (they did 99.99% of the work)
