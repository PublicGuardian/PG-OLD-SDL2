using System;
using System.Drawing;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace PGAPI
{
	public class Window : IDisposable
	{
		public IntPtr window;
		public IntPtr renderer;
		public Font font = new Font(AppDomain.CurrentDomain.BaseDirectory + "Louis George Cafe.ttf", 24);
		public bool IsOpen = true;

		/// <summary>
		/// Creates a new window.
		/// </summary>
		/// <param name="title">The title to be displayed on the title bar of the window.</param>
		/// <param name="x">The x coordinate of the window.</param>
		/// <param name="y">The y coordinate of the window.</param>
		/// <param name="size">The size of the window.</param>
		/// <param name="flags">Special flags for the window. (e.g. SDL_WINDOW_RESIZABLE)</param>
		public Window(Size size, SDL_WindowFlags flags = SDL_WindowFlags.SDL_WINDOW_RESIZABLE, string title = "Default Title", int x = SDL_WINDOWPOS_CENTERED, int y = SDL_WINDOWPOS_CENTERED)
		{
			window = SDL_CreateWindow(title, x, y, size.Width, size.Height, flags);
			renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

			App.HandleWindowCreated(this);
		}

		/// <summary>
		/// Dispose of this window. Run this for every window opened before exiting.
		/// </summary>
		public void Dispose()
		{
			TTF_CloseFont(font.font);
			SDL_DestroyRenderer(renderer);
			SDL_DestroyWindow(window);
			App.HandleWindowDestroyed(this);
		}

		/// <summary>
		/// Render text onto this window's renderer.
		/// </summary>
		/// <param name="color">The color to use for the text.</param>
		/// <param name="text">The string to use.</param>
		/// <param name="rect">The coordinates of the textbox. Leave the width and height parameters blank, this is generated automatically.</param>
		public void RenderFont(SDL_Color color, string text, SDL_Rect rect) {
			int w, h;

			try {
				int sizeText = TTF_SizeText(font.font, text, out w, out h);

				rect.w = w;
				rect.h = h;
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}

			font.RenderTexture(renderer, color, text, rect);
		}

		/// <summary>
		/// Manages whether the window is fullscreen or not.
		/// </summary>
		public void Fullscreen() {
			// TODO
		}
	}
}
