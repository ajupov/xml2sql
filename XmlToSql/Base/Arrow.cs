using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public class Arrow : Base
    {
        public long From { get; set; }

        public long To { get; set; }

        public Arrow(XElement element)
            : base(element)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                switch (attribute.Name.ToString())
                {
                    case "from":
                        From = Convert.ToInt64(attribute.Value);
                        break;
                    case "to":
                        To = Convert.ToInt64(attribute.Value);
                        break;
                    case "label":
                        type = new Type("-");
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
            parameters.Add(From.ToString());
            parameters.Add(To.ToString());
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
