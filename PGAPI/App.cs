using System;
using System.Linq;
using System.Collections.Generic;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_WindowEventID;

namespace PGAPI
{
	/// <summary>
	/// This class contains basic app features, such as handling the opening and closing of windows, basic events, and initializing SDL.
	/// </summary>
	public static class App
	{
		/// <summary>
		/// The list of currently opened Windows. Usually, only one window is open.
		/// </summary>
		/// <returns>Window</returns>
		private static List<Window> windows = new();

		/// <summary>
		/// Initialize required SDL functions. If this fails, the game will not function correctly, if at all.
		/// </summary>
		public static void Init() {
			Console.WriteLine("Starting the game");
			SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "1");

			if (SDL_Init(SDL_INIT_VIDEO) != 0) Console.WriteLine("Failed to initialize SDL2's video module"); 
			if (SDL_Init(SDL_INIT_AUDIO) != 0) Console.WriteLine("Failed to initialize SDL2's audio module");
			if (TTF_Init() != 0) Console.WriteLine("Failed to initialize SDL2's TTF module");
		}

		/// <summary>
		/// Should be ran last before exiting the game.
		/// </summary>
		public static void Quit() {
			foreach (Window win in windows) {
				win.Dispose();
			}

			SDL_Quit();
		}

		/// <summary>
		/// Call this in a while() loop in order to receive important events, such as when the user closes the window.
		/// </summary>
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

		/// <summary>
		/// ONLY used internally in order to handle the creation of a window.
		/// </summary>
		/// <param name="window">The window to add to the App's list.</param>
		internal static void HandleWindowCreated(Window window) {
			windows.Add(window);
		}

		/// <summary>
		/// ONLY used internally in order to handle the deletion of a window.
		/// </summary>
		/// <param name="window">The window to remove from the App's list.</param>
		internal static void HandleWindowDestroyed(Window window) {
			windows.Remove(window);
		}
	}
}
