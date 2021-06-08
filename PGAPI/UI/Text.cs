using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using Newtonsoft.Json.Linq;

namespace PGAPI.UI
{
    public class Text : UIObject
    {
        /// <summary>
        /// The representation of the Font.
        /// </summary>
        private IntPtr Font;

        /// <summary>
        /// The representation of the Renderer.
        /// </summary>
        private IntPtr Renderer;

        public void SubmitRenderer(IntPtr renderer, bool isLocal) {
            Renderer = renderer;
            if (isLocal) {
                Font = TTF_OpenFont(AppDomain.CurrentDomain.BaseDirectory + (string)properties[1], Convert.ToInt32(size[0]));
            } else {
                Font = TTF_OpenFont((string)properties[1], (int)size[0]);
            }
        }

        /// <summary>
        /// Draws the predefined Text. Call in the game loop.
        /// </summary>
        public override void Draw() {
            SDL_Rect tempRect;
            SDL_Color color;

            JArray array = (JArray)properties[2];

            color.r = Convert.ToByte((int)array[0]);
            color.g = Convert.ToByte((int)array[1]);
            color.b = Convert.ToByte((int)array[2]);
            color.a = Convert.ToByte((int)array[3]);

            int sizeText = TTF_SizeText(Font, (string)properties[0], out tempRect.w, out tempRect.h);
            tempRect.x = coordinates[0];
            tempRect.y = coordinates[1];

            IntPtr surfaceText = TTF_RenderText_Blended(Font, (string)properties[0], color);
            IntPtr textureText = SDL_CreateTextureFromSurface(Renderer, surfaceText);

            SDL_RenderCopy(Renderer, textureText, IntPtr.Zero, ref tempRect);

            SDL_FreeSurface(surfaceText);
            SDL_DestroyTexture(textureText);
        }

        /// <summary>
        /// TODO
        /// </summary>
        public override void OnDestroy() {
			TTF_CloseFont(Font);
        }
    }
}
