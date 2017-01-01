using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    class Graph : Base
    {
        public List<Relation> relations { get; set; }

        public List<Concept> concepts { get; set; }

        public List<Arrow> arrows { get; set; }

        public Graph(XElement element)
            : base(element)
        {
            relations = new List<Relation>();
            concepts = new List<Concept>();
            arrows = new List<Arrow>();

            foreach (XElement item in element.Elements())
            {
                switch (item.Name.ToString())
                {
                    case "relation":
                        relations.Add(new Relation(item));
                        break;
                    case "concept":
                        concepts.Add(new Concept(item));
                        break;
                    case "arrow":
                        arrows.Add(new Arrow(item));
                        break;
                }
            }

        }

        public void FillTables(DataGridView conceptsTable, DataGridView relationsTable, DataGridView arrowsTable)
        {
            foreach (var item in concepts)
            {
                conceptsTable.Rows.Add(item.ParamsAsArray());
            }

            foreach (var item in relations)
            {
                relationsTable.Rows.Add(item.ParamsAsArray());
            }

            foreach (var item in arrows)
            {
                arrowsTable.Rows.Add(item.ParamsAsArray());
            }
        }
    }
}
