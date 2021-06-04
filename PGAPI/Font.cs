using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace PGAPI
{
    public class Font
    {
        public IntPtr font;

        /// <summary>
        /// Initializes a font.
        /// </summary>
        /// <param name="filePath">The location of the TTF file.</param>
        /// <param name="fontSize">The font size you want to render this font at. (The default is 16px.)</param>
        public Font(string filePath, int fontSize = 16) {
            font = TTF_OpenFont(filePath, fontSize);
        }

        /// <summary>
        /// Renders a text texture with the requested parameters.
        /// </summary>
        /// <param name="renderer">The renderer to use.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="text">The text you need.</param>
        /// <param name="rect">The SDL_Rect used to render the font. Should include the coordinates of where the text should go on screen and the size of the textbox.</param>
        /// <returns>IntPtr</returns>
        public void RenderTexture(IntPtr renderer, SDL_Color color, string text, SDL_Rect rect) {
            try {
                IntPtr surfaceText = TTF_RenderText_Blended(font, text, color);
                IntPtr textureText = SDL_CreateTextureFromSurface(renderer, surfaceText);

                int test = SDL_RenderCopy(renderer, textureText, IntPtr.Zero, ref rect);

                SDL_FreeSurface(surfaceText);
                SDL_DestroyTexture(textureText);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
