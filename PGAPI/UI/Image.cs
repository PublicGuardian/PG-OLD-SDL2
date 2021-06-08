using System;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace PGAPI.UI
{
    /// <summary>
    /// A UIObject that relates to images.
    /// </summary>
    [Serializable]
    public class Image : UIObject
    {
        /// <summary>
        /// The representation of the IMG_Image.
        /// </summary>
        private IntPtr ImagePtr;

        /// <summary>
        /// The representation of the Window's Renderer.
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
        /// The processed path.
        /// </summary>
        private string ProcessedPath = "";

        /// <summary>
        /// Send the proper Renderer over to the Image code for rendering. Only needs to be run for each new Renderer used.
        /// </summary>
        /// <param name="renderer">The Renderer to use.</param>
        /// <param name="windowW">Optional. The width of the window, in pixels, for calculating centering.</param>
        /// <param name="windowH">Optional. The height of the window, in pixels, for calculating centering.</param>
        public void SubmitRenderer(IntPtr renderer, int windowW = 1280, int windowH = 720, bool isLocal = false) {
            Renderer = renderer;
            WindowW = windowW;
            WindowH = windowH;

            if (isLocal) {
                ProcessedPath = AppDomain.CurrentDomain.BaseDirectory + (string)properties[0];
            } else {
                ProcessedPath = (string)properties[0];
            }
        }

        /// <summary>
        /// Draws the predefined Image. Call in the game loop.
        /// </summary>
        public override void Draw() {
            // Initialization
            bool useDivision = false;
            uint bs1;
            int w, h, bs2, scaleW = 0, scaleH = 0;
            var scalingW = (double)size[0];
            var scalingH = (double)size[1];

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

            ImagePtr = IMG_LoadTexture(Renderer, ProcessedPath);
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

            if (coordinates[0] == -1) {
                imgX = (WindowW / 2) - (imgWidth / 2);
            } else {
                imgX = coordinates[0];
            }

            if (coordinates[1] == -1) {
                imgY = (WindowH / 2) - (imgHeight / 2);
            } else {
                imgY = coordinates[1];
            }

            imageRect = new SDL_Rect() {
                x = imgX,
                y = imgY,
                w = imgWidth,
                h = imgHeight,
            };

            SDL_RenderCopy(Renderer, ImagePtr, IntPtr.Zero, ref imageRect);
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