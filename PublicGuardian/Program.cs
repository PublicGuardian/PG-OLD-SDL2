using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using PGAPI;
using System.Drawing;

namespace PublicGuardian
{
	class Program
	{
		// Determines whether or not to allow certain development functions. Set to false unless specific conditions are true.
		internal static bool isDev = false;

		// Only used when development mode is enabled. The game will display a random "haha funny" on the title bar.
		static string[] titleStrings = {"We Live In A Society",
								"Coming Eventually",
								"C# In VSCode Is Weird",
								"SDL2 Is My True Pain"};

		// A rand. Used to calculate which random string to use. 
		static Random rand = new Random();

		static void Main(string[] args)
		{
			// Run initial SDL initialization. Should ALWAYS come first.
			App.Init();

			/*
			Code crashes when the args are blank. TODO: Fix.

			if (args != new string[0]) {
				if (args[0] == "-debug") {
					isDev = true;
				}
			}*/

			// Create a new Window.
			Window win = new Window(new Size(1280, 720),
									SDL_WindowFlags.SDL_WINDOW_RESIZABLE,
									isDev ? $"Public Guardian Preliminary - {titleStrings[rand.Next(0, titleStrings.Length)]}" : "Public Guardian");
			
			// Load the default scene.
			win.LoadNewScene(AppDomain.CurrentDomain.BaseDirectory + "assets/MainMenu.json");

			// Game loop. Only runs while the Window hasn't been destroyed.
			while (win.IsOpen) {
				// App events loop.
				App.DoEvents();

				// Render the currently-loaded scene.
				win.RenderCurrentScene();

				// Present the rendered items to the Window.
				SDL_RenderPresent(win.renderer);

				// Frame limiter to about 60 FPS.
				SDL_Delay(16);
			}

			// Game loop broken by someone exiting out of the game, run the final code to quit SDL
			App.Quit();
		}
	}
}
