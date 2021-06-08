using System;
using static SDL2.SDL;
using Newtonsoft.Json.Linq;

namespace PGAPI.UI
{
	public class Rect : UIObject
	{
		/// <summary>
		/// The internal SDL_Rect.
		/// </summary>
		private SDL_Rect Rectangle;

		/// <summary>
		/// The internal Renderer.
		/// </summary>
		private IntPtr Renderer;

		/// <summary>
		/// The Window's current width.
		/// </summary>
		private int WindowW;

		/// <summary>
		/// The Window's current height.
		/// </summary>
		private int WindowH;

		/// <summary>
		/// Submit rendering info.
		/// </summary>
		/// <param name="renderer">The Renderer to use.</param>
		/// <param name="windowW">The size of the window's width. Optional.</param>
		/// <param name="windowH">The size of the window's height. Optional.</param>
		public void SubmitRenderer(IntPtr renderer, int windowW = 1280, int windowH = 720) {
			Renderer = renderer;
			WindowW = windowW;
			WindowH = windowH;
		}

		/// <summary>
		/// Draws the predefined Rect. Call in the game loop.
		/// </summary>
		public override void Draw() {
			Rectangle.x = coordinates[0];
			Rectangle.y = coordinates[1];

			JArray array = (JArray)properties[0];
			byte colorR = Convert.ToByte((int)array[0]);
			byte colorG = Convert.ToByte((int)array[1]);
			byte colorB = Convert.ToByte((int)array[2]);
			byte colorA = Convert.ToByte((int)array[3]);

			SDL_SetRenderDrawColor(Renderer, colorR, colorG, colorB, colorA);

			if (size[0] is string) {
				switch ((string)size[0]) {
					case "WindowX":
						Rectangle.w = WindowW;
						break;
					default:
						break;
				}
			} else {
				Rectangle.w = Convert.ToInt32(size[0]);
			}

			if (size[1] is string) {
				switch ((string)size[1]) {
					case "WindowY":
						Rectangle.h = WindowH;
						break;
					default:
						break;
				}
			} else {
				Rectangle.h = Convert.ToInt32(size[1]);
			}

			SDL_RenderFillRect(Renderer, ref Rectangle);
		}
	}
}
