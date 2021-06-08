using System;
using System.Collections.Generic;

namespace PGAPI.UI
{
	[Serializable]
	public class ObjectJSON
	{

		public string type { get; set; } = "";
		public List<Object> size { get; set; } = new List<Object>();
		public int[] coordinates { get; set; } = new int[2];
		public List<Object> properties { get; set; } = new List<Object>();
	}
}
