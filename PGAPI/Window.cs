using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PGAPI.UI;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace PGAPI
{
	/// <summary>
	/// This class is a wrapper on top of SDL_Window in order to make certain functions far easier.
	/// </summary>
	public class Window : IDisposable
	{
		/// <summary>
		/// This is the raw SDL_Window. Please only use this in API requests when necessary.
		/// </summary>
		public IntPtr window;

		/// <summary>
		/// The raw SDL_Renderer. Only use this when necessary.
		/// </summary>
		public IntPtr renderer; 

		[Obsolete("Will be removed soon, do not use")]
		public Font font = new Font(AppDomain.CurrentDomain.BaseDirectory + "Louis George Cafe.ttf", 24);

		/// <summary>
		/// The boolean that determines if the window is "open" or not. Only modifiable by other functions inside this API.
		/// </summary>
		public bool IsOpen { get; internal set; } = true;

		/// <summary>
		/// The current scene object. Blank before runtime.
		/// </summary>
		public List<UIObject> CurrentScene = new List<UIObject>();

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
			SDL_SetWindowMinimumSize(window, 1280, 720);

			App.HandleWindowCreated(this);
		}

		/// <summary>
		/// Dispose of this window. Run this for every window opened before exiting.
		/// </summary>
		public void Dispose()
		{
			foreach (UIObject obj in CurrentScene) {
				obj.OnDestroy();
			}

			SDL_DestroyRenderer(renderer);
			SDL_DestroyWindow(window);
			App.HandleWindowDestroyed(this);
		}

		/// <summary>
		/// Load a new scene into this Window's memory. To render your newly-loaded scene, call Window.RenderCurrentScene().
		/// </summary>
		/// <param name="sceneFile">The file path to the location where the scene's file is stored.</param>
		public void LoadNewScene(string sceneFile) {
			// If a scene is already loaded, call the appropriate Destroy calls first, and then clear the List
			if (CurrentScene.Count != 0) {
				foreach (UIObject obj in CurrentScene) {
					obj.OnDestroy();
				}

				CurrentScene.Clear();
			}

			using (StreamReader file = File.OpenText(sceneFile)) {
				var rawJson = JsonConvert.DeserializeObject<List<ObjectJSON>>(file.ReadToEnd());

				if (rawJson == null) {
					throw new Exception("Failed to serialize the JSON file");
				}

				foreach (ObjectJSON json in rawJson) {
					UIObject actualObj = json.type switch {
						"rect" => new Rect(),
						"image" => new Image(),
						"text" => new Text(),
						_ => throw new Exception("Object JSON has invalid type property.")
					};

					actualObj.type = json.type;
					actualObj.size = json.size;
					actualObj.coordinates = json.coordinates;
					actualObj.properties = json.properties;

					CurrentScene.Add(actualObj);
				}
			}
		}

		/// <summary>
		/// Render the currently-loaded scene into this Window's renderer. Call inside the game loop.
		/// </summary>
		public void RenderCurrentScene() {
			foreach (UIObject obj in CurrentScene) {
				Type objType = obj.GetType();
				int winW, winH = 0;

				if (objType == typeof(Image)) {
					Image tempImg = (Image)obj;
					SDL_GetWindowSize(window, out winW, out winH);

					tempImg.SubmitRenderer(renderer, winW, winH, true);
					tempImg.Draw();
				} else if (objType == typeof(Rect)) {
					Rect tempRect = (Rect)obj;
					SDL_GetWindowSize(window, out winW, out winH);

					tempRect.SubmitRenderer(renderer, winW, winH);
					tempRect.Draw();
				} else if (objType == typeof(Text)) { 
					Text tempText = (Text)obj;
					
					tempText.SubmitRenderer(renderer, true);
					tempText.Draw();
				} else {
					obj.Draw();
				}
			}
		}

		/// <summary>
		/// Manages whether the window is fullscreen or not.
		/// </summary>
		public void Fullscreen() {
			// TODO
		}
	}
}
