﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public class Rectangle
    {
        public Rectangle(IEnumerable<XAttribute> attributes)
        {
            foreach (XAttribute attribute in attributes)
            {
                string val = attribute.Value.Replace(".", ",");

                switch (attribute.Name.ToString())
                {
                    case "x":
                        X = Convert.ToSingle(val);
                        break;
                    case "y":
                        Y = Convert.ToSingle(val);
                        break;
                    case "width":
                        Width = Convert.ToSingle(val);
                        break;
                    case "height":
                        Height = Convert.ToSingle(val);
                        break;
                }
            }
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }
    }
}