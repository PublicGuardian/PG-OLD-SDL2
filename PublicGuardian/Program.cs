using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using PGAPI;
using System.Drawing;

namespace PublicGuardian
{
	class Program
	{
		internal static bool isDev = false;

		static string[] titleStrings = {"We Live In A Society",
								"Coming Eventually",
								"C# In VSCode Is Weird",
								"SDL2 Is My True Pain"};
		static Random rand = new Random();

		static void Main(string[] args)
		{
			App.Init();
			/*
			if (args != new string[0]) {
				if (args[0] == "-debug") {
					isDev = true;
				}
			}*/

			Window win = new Window(new Size(1280, 720),
									SDL_WindowFlags.SDL_WINDOW_RESIZABLE,
									isDev ? $"Public Guardian Preliminary - {titleStrings[rand.Next(0, titleStrings.Length)]}" : "Public Guardian");

			SDL_SetWindowMinimumSize(win.window, 1280, 720); // All windows in Public Guardian must be at least 1280 by 720 pixels.
			
			while (win.IsOpen) {
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

			App.Quit();
		}
	}
}
