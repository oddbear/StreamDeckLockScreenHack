using StreamDeckSharp;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace StreamDeckLockScreenHack
{
    internal class Program
    {
        static void Main(string[] args)
		{
			var state = State.Unknown;

			var waitTimeConfig = ConfigurationManager.AppSettings["WaitTime"];
			if (!TimeSpan.TryParse(waitTimeConfig, out var waitTime))
				waitTime = TimeSpan.FromSeconds(1);

			var brightnessLockedConfig = ConfigurationManager.AppSettings["BrightnessLocked"];
			if (!byte.TryParse(brightnessLockedConfig, out var brightnessLocked))
				brightnessLocked = 0;

			var brightnessUnlockedConfig = ConfigurationManager.AppSettings["BrightnessUnlocked"];
			if (!byte.TryParse(brightnessUnlockedConfig, out var brightnessUnlocked))
				brightnessUnlocked = 100;
			else if (brightnessUnlocked > 100)
				brightnessUnlocked = 100;

			using (var sd = StreamDeck.OpenDevice())
			{
				while (true)
				{
					var isLocked = Process.GetProcessesByName("logonui").Any();
					if (isLocked)
					{
						if (state != State.TurnedOff)
						{
							sd.SetBrightness(brightnessLocked);
							state = State.TurnedOff;
						}
					}
					else
					{
						if (state != State.TurnedOn)
						{
							sd.SetBrightness(brightnessUnlocked);
							state = State.TurnedOn;
						}
					}
					Thread.Sleep(waitTime);
				}
			}
		}
    }

    enum State
    {
        Unknown,
        TurnedOff,
        TurnedOn
    }
}
