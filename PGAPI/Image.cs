using System;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace PGAPI
{
    public class Image
    {
        private string Path;
        private IntPtr ImagePtr;

        /// <summary>
        /// Represents an image in a simpler way.
        /// </summary>
        /// <param name="imgPath">The path of the image.</param>
        public Image(string imgPath) {
            Path = imgPath;
        }

        /// <summary>
        /// Renders the image onto the screen.
        /// </summary>
        /// <param name="renderer">The window's renderer.</param>
        /// <param name="x">The x-coordinate of the image.</param>
        /// <param name="y">The y-coordinate of the image.</param>
        /// <param name="scalingW">A multiplier for the width. (e.g. 2 for 2x scale, 0.5 for half. Only supports decimals for 0.1, 0.2, 0.3 and 0.5.)</param>
        /// <param name="scalingH">A multiplier for the height. (e.g. 2 for 2x scale, 0.5 for half. Only supports decimals for 0.1, 0.2, 0.3 and 0.5.)</param>
        /// <param name="windowW">The window width, if applicable. (e.g. for centering)</param>
        /// <param name="scalingH">The window height, if applicable. (e.g. for centering)</param>
        public void RenderTexture(IntPtr renderer, int x, int y, double scalingW = 1, double scalingH = 1, int windowW = 0, int windowH = 0) {
            bool useDivision = false;
            uint bs1;
            int w, h, bs2, scaleW = 0, scaleH = 0;

            if (scalingW % 1 != 0) {
                int scale = ScalingLogic(scalingW);
                if (scale != 0) {
                    useDivision = true;
                    scaleW = scale;
                }
            }
            
            if (scalingH % 1 != 0) {
                int scale = ScalingLogic(scalingH);
                if (scale != 0) {
                    useDivision = true;
                    scaleH = scale;
                }
            }

            if (!useDivision) {
                scaleW = (int)scalingW;
                scaleH = (int)scalingH;
            }

            ImagePtr = IMG_LoadTexture(renderer, Path);
            SDL_QueryTexture(ImagePtr, out bs1, out bs2, out w, out h);
            
            SDL_Rect imageRect;
            int imgWidth, imgHeight, imgX, imgY;

            if (useDivision) {
                imgWidth = w / scaleW;
                imgHeight = h / scaleH;
            } else {
                imgWidth = w * scaleW;
                imgHeight = h * scaleH;
            }

            if (x == -1) {
                imgX = (windowW / 2) - (imgWidth / 2);
            } else {
                imgX = x;
            }

            if (y == -1) {
                imgY = (windowH / 2) - (imgHeight / 2);
            } else {
                imgY = y;
            }

            imageRect = new SDL_Rect() {
                x = imgX,
                y = imgY,
                w = imgWidth,
                h = imgHeight,
            };

            SDL_RenderCopy(renderer, ImagePtr, IntPtr.Zero, ref imageRect);
            SDL_DestroyTexture(ImagePtr);
        }

        /// <summary>
        /// Converts multiplying by specific decimals to division. (only way for ints such as the values of SDL_Rects.)
        /// </summary>
        /// <returns>int</returns>
        private int ScalingLogic(double scaling) {
            switch (scaling) {
                case 0.1:
                    return 10;
                case 0.2:
                    return 5;
                case 0.3:
                    return 3;
                case 0.5:
                    return 2;
                default:
                    return 1;
            }
        }
    }
}