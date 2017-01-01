using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public abstract class Base
    {
        public long Id { get; set; }

        public long Owner { get; set; }

        public Type type { get; set; }

        public Layout layout { get; set; }

        public Base(XElement element)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                switch (attribute.Name.ToString())
                {
                    case "id":
                        Id = Convert.ToInt64(attribute.Value);
                        break;
                    case "owner":
                        Owner = Convert.ToInt64(attribute.Value);
                        break;
                }
            }

            foreach (XElement item in element.Elements())
            {
                switch (item.Name.ToString())
                {
                    case "type":
                        type = new Type(item);
                        break;
                    case "layout":
                        layout = new Layout(item);
                        break;
                }
            }
        }

        public virtual string[] ParamsAsArray() 
        { 
            List<string> parameters = new List<string>();
            parameters.Add(Id.ToString());
            parameters.Add(Owner.ToString());
            parameters.Add("'" + type.Label + "'");
            parameters.Add(layout.rectangle.Width.ToString());
            parameters.Add(layout.rectangle.Height.ToString());
            parameters.Add(Convert.ToInt32(layout.rectangle.X).ToString());
            parameters.Add(Convert.ToInt32(layout.rectangle.Y).ToString());
            parameters.Add(layout.color.Foreground.ToString());
            parameters.Add(layout.color.Background.ToString());
            return parameters.ToArray();
        }
    }
}
