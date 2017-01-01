using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public class Type
    {
        public string Label { get; set; }

        public Type(string label)
        {
            Label = label;
        }

        public Type(XElement element)
        {
            Label = element.Element("label").Value;
        }
    }
}
