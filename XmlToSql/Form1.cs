using System;
using System.Windows.Forms;
using System.Xml.Linq;
using Resouces = WindowsFormsApplication1.Queries.Resources;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    public enum DBE{
        SQLServer = 1,
        MySQL = 2,
        PostgreSQL = 3,
        SQLite = 4,
        Prolog = 5
    }

    public partial class Form1 : Form
    {
        private Graph graph;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстовый файл(*.txt) | *.txt|Файл XML(*.xml) | *.xml";
            
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                XElement rootElement = XElement.Load(new StreamReader(openDialog.FileName, Encoding.GetEncoding("windows-1251")));
                graph = new Graph(rootElement.Element("graph"));
                if (graph == null)
                {
                    MessageBox.Show("Не удалось считать граф");
                    return;
                }

                graph.FillTables(conceptsTable, relationsTable, arrowsTable);
                convertButton.Enabled = true;
            }
        }

        private void SaveScript(string filename, DBE dbe)
        {
            using (FileStream fstream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                string query = string.Empty;
                string delimiter = string.Empty;
                
                switch (dbe)
                {
                    case DBE.SQLServer:
                        delimiter = Environment.NewLine + "GO" + Environment.NewLine + Environment.NewLine;
                        break;
                    case DBE.MySQL:
                        delimiter = Environment.NewLine + Environment.NewLine;
                        break;
                    case DBE.PostgreSQL:
                    case DBE.SQLite:
                        delimiter = ";" + Environment.NewLine + Environment.NewLine;
                        break;
                }

                query += Resouces.CreateConceptTable + delimiter;
                query += Resouces.CreateRelationTable + delimiter;
                query += Resouces.CreateArrowTable + delimiter;

                query += "INSERT INTO [Concept]" + Environment.NewLine + "VALUES ";
                foreach (var item in graph.concepts)
                {
                    query += string.Format("({0}, {1}, N{2}, {3}, {4}, {5}, {6}, {7}, {8}),", item.ParamsAsArray());
                    query += Environment.NewLine;
                }
                query = query.Remove(query.Length - 3);
                query += delimiter;

                query += "INSERT INTO [Relation]" + Environment.NewLine + "VALUES ";
                foreach (var item in graph.relations)
                {
                    query += string.Format("({0}, {1}, N{2}, {3}, {4}, {5}, {6}, {7}, {8}),", item.ParamsAsArray());
                    query += Environment.NewLine;
                }
                query = query.Remove(query.Length - 3);
                query += delimiter;

                query += "INSERT INTO [Arrow]" + Environment.NewLine + "VALUES ";
                foreach (var item in graph.arrows)
                {
                    query += string.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}),", item.ParamsAsArray());
                    query += Environment.NewLine;
                }
                query = query.Remove(query.Length - 3);
                query += delimiter;
                query = query.Remove(query.Length - 2);

                if (dbe != DBE.SQLServer)
                {
                    query = query.Replace("[", "");
                    query = query.Replace("]", "");
                }

                byte[] array = Encoding.UTF8.GetBytes(query);
                fstream.Write(array, 0, array.Length);
            }
        }

        private void SavePrologScript(string filename)
        {
            using (FileStream fstream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                string part = string.Empty;
                foreach (var item in graph.concepts)
                {
                    part += string.Format("Concept({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}).", item.ParamsAsArray());
                    part += Environment.NewLine;
                }
                part += Environment.NewLine;

                foreach (var item in graph.relations)
                {
                    part += string.Format("Relation({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}).", item.ParamsAsArray());
                    part += Environment.NewLine;
                }
                part += Environment.NewLine;

                foreach (var item in graph.arrows)
                {
                    part += string.Format("Arrow({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}).", item.ParamsAsArray());
                    part += Environment.NewLine;
                }
                part += Environment.NewLine;

                byte[] array = Encoding.UTF8.GetBytes(part);
                fstream.Write(array, 0, array.Length);
            }
        }

        private string[] GetFilters()
        {
            List<string> filterList = new List<string>();
            foreach (DBE item in Enum.GetValues(typeof(DBE)))
            {
                filterList.Add("Файл скрипта " + item.ToString() + "(*.sql)|*.sql");
            }
            return filterList.ToArray();
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = "Скрипт";
            saveDialog.Filter = string.Join("", string.Join("|", GetFilters()));

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                int dbe = saveDialog.FilterIndex;
                if (dbe == (int)DBE.Prolog)
                {
                    SavePrologScript(saveDialog.FileName);
                }
                else
                {
                    SaveScript(saveDialog.FileName, (DBE)saveDialog.FilterIndex);
                }
            }
        }

        private void SearchAndScroll(object value, DataGridView table){
            int rowIndex = -1;
            foreach (DataGridViewRow row in table.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(value.ToString()))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            if (rowIndex != -1)
            {
                table.Rows[rowIndex].Selected = true;
                table.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        private void arrowsTable_Click(object sender, EventArgs e)
        {
            var from = Convert.ToInt64(arrowsTable.SelectedRows[0].Cells[3].Value);
            var to = Convert.ToInt64(arrowsTable.SelectedRows[0].Cells[4].Value);

            SearchAndScroll(from, conceptsTable);
            SearchAndScroll(to, relationsTable);
            SearchAndScroll(from, relationsTable);
            SearchAndScroll(to, conceptsTable);
        }
    }
}