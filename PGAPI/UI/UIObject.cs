using System;
using System.Collections.Generic;

namespace PGAPI.UI
{
    /// <summary>
    /// The default and blank UI Object. Used by other classes that properly render objects to the Renderer.
    /// </summary>
    [Serializable]
    public class UIObject
    {
        public string type = "";
        public List<Object> size = new List<Object>();
        public int[] coordinates = new int[2];
        public List<Object> properties = new List<Object>();

        /// <summary>
        /// The basic Draw() class. Override in your own class.
        /// </summary>
        public virtual void Draw() {
            Console.WriteLine("Add your own drawing code here!");
        }

        /// <summary>
        /// Run exiting destroy-code here.
        /// </summary>
        public virtual void OnDestroy() {
            
        }
    }
}
