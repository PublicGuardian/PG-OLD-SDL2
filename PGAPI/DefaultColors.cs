using System;
using static SDL2.SDL;

namespace PGAPI
{
    public static class DefaultColors
    {
        public static SDL_Color White = new SDL_Color()
        {
            r = 255,
            g = 255,
            b = 255,
            a = 255
        };

        public static SDL_Color Black = new SDL_Color()
        {
            r = 0,
            g = 0,
            b = 0,
            a = 255
        };

        public static SDL_Color DefaultBackground = new SDL_Color()
        {
            r = 25,
            g = 25,
            b = 25,
            a = 255
        };
    }
}
