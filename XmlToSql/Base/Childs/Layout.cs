using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public class Layout
    {
        public Layout(XElement element)
        {
            foreach (XElement item in element.Elements())
            {
                switch (item.Name.ToString())
                {
                    case "rectangle":
                        rectangle = new Rectangle(item.Attributes());
                        break;
                    case "color":
                        color = new Color(item.Attributes());
                        break;
                }
            }
        }

        public Rectangle rectangle { get; set; }

        public Color color { get; set; }  
    }
}
