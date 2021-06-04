using System;
using System.Linq;
using System.Collections.Generic;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_WindowEventID;

namespace PGAPI
{
	public static class App
	{
		private static List<Window> windows = new();

		public static void Init() {
			Console.WriteLine("Starting the game");
			SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "1");

			if (SDL_Init(SDL_INIT_VIDEO) != 0) Console.WriteLine("Failed to initialize SDL2's video module"); 
			if (SDL_Init(SDL_INIT_AUDIO) != 0) Console.WriteLine("Failed to initialize SDL2's audio module");
			if (TTF_Init() != 0) Console.WriteLine("Failed to initialize SDL2's TTF module");
		}

		public static void Quit() {
			foreach (Window win in windows) {
				win.Dispose();
			}

			SDL_Quit();
		}

		public static void DoEvents() {
			while (SDL_PollEvent(out var ev) != 0) {
				switch (ev.type) {
					case SDL_WINDOWEVENT:
					{
						var win = windows.FirstOrDefault(win => SDL_GetWindowID(win.window) == ev.window.windowID);

						if (win == null) return;

						switch (ev.window.windowEvent) {
							case SDL_WINDOWEVENT_CLOSE:
								win.IsOpen = false;
								win.Dispose();
								break;
						}
					}
					break;
				}
			}
		}

		internal static void HandleWindowCreated(Window window) {
			windows.Add(window);
		}

		internal static void HandleWindowDestroyed(Window window) {
			windows.Remove(window);
		}
	}
}
