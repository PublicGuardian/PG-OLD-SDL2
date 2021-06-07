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
			
			// Game loop. Only runs while the Window hasn't been destroyed.
			while (win.IsOpen) {
				// App events loop.
				App.DoEvents();
				SDL_SetRenderDrawColor(win.renderer, 25, 25, 25, 255);

				SDL_Color textColor = new SDL_Color();
				textColor.r = textColor.g = textColor.b = textColor.a = 255;

				SDL_Rect rect;
				rect.x = rect.y = 0;
				SDL_GetWindowSize(win.window, out rect.w, out rect.h);
				SDL_RenderFillRect(win.renderer, ref rect);
				
				win.RenderFont(DefaultColors.White, "Hello World!", new SDL_Rect() {
					x = 15,
					y = 15
				});

				Image logo = new Image(AppDomain.CurrentDomain.BaseDirectory + "PG Wordmark White.png");
				int imgX, imgY;
				SDL_GetWindowSize(win.window, out imgX, out imgY);

				logo.RenderTexture(win.renderer, -1, 100, 0.5, 0.5, imgX, imgY);
				
				SDL_RenderPresent(win.renderer);
				SDL_Delay(16);
			}

			// Game loop broken by someone exiting out of the game, run the final code to quit SDL
			App.Quit();
		}
	}
}
