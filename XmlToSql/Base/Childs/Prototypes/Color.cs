using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public class Color
    {
        public Color(IEnumerable<XAttribute> attributes)
        {
            foreach (XAttribute attribute in attributes)
            {
                string[] color = attribute.Value.Split(',');

                switch (attribute.Name.ToString())
                {
                    case "foreground":
                        Foreground = Convert.ToInt32(color[0]) + Convert.ToInt32(color[1]) * 256 + Convert.ToInt32(color[2]) * 65536;
                        break;
                    case "background":
                        Background = Convert.ToInt32(color[0]) + Convert.ToInt32(color[1]) * 256 + Convert.ToInt32(color[2]) * 65536;
                        break;
                }
            }
        }

        public int Foreground { get; set; }

        public int Background { get; set; }
    }
}