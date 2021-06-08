using System;
using static SDL2.SDL;

namespace PGAPI
{
    /// <summary>
    /// This class contains a list of default colors that you can use in place of remembering the RGBA values each time you want to use a common color.
    /// </summary>
    public static class DefaultColors
    {
        /// <summary>
        /// Represents White (255, 255, 255, 255).
        /// </summary>
        /// <returns>SDL_Color</returns>
        public static SDL_Color White = new SDL_Color()
        {
            r = 255,
            g = 255,
            b = 255,
            a = 255
        };

        /// <summary>
        /// Represents Black (0, 0, 0, 255).
        /// </summary>
        /// <returns>SDL_Color</returns>
        public static SDL_Color Black = new SDL_Color()
        {
            r = 0,
            g = 0,
            b = 0,
            a = 255
        };

        /// <summary>
        /// Represents the default background color of a Window. (25, 25, 25, 255).
        /// </summary>
        /// <returns>SDL_Color</returns>
        public static SDL_Color DefaultBackground = new SDL_Color()
        {
            r = 25,
            g = 25,
            b = 25,
            a = 255
        };
    }
}
