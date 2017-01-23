using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class RDLDataSourceFinder : Form
    {
        public RDLDataSourceFinder()
        {
            InitializeComponent();
        }


        public void searchRDLS()
        {
            string RDLPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            RDLPath = RDLPathTxtBox.Text;

            string OutputPath;
            if (OutputPathTxtBox.Text != null)
            {
                if (!Directory.Exists(OutputPathTxtBox.Text))
                {
                    Directory.CreateDirectory(OutputPathTxtBox.Text);
                }

                OutputPath = OutputPathTxtBox.Text + "\\SearchResults.csv";
            }
            else
            {
                OutputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SearchResults.csv";
            }

            OutputTxtBox.AppendText("Loading Files" + " \r\n");
            Refresh();
            string[] Files = Directory.GetFiles(RDLPath, "*.rdl", SearchOption.AllDirectories);

            OutputTxtBox.AppendText("Loading Search Parameters" + " \r\n");
            Refresh();

            string[] TxtBoxSplit = CriteriaTextBox.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            List<string[]> databaseTables = new List<string[]>();

            foreach (string line in TxtBoxSplit)
            {
                if (line != "")
                {
                    databaseTables.Add(line.Split('.'));
                }
            }

            List<string[]> distinctDatabaseTables = databaseTables.Distinct().OrderBy(arr => arr[0]).ToList();

            OutputTxtBox.AppendText("Starting Search" + " \r\n");
            Refresh();
            using (StreamWriter writer = new StreamWriter(OutputPath))
            {
                writer.WriteLine("Folder,Report Name,Database,Table,Column");

                foreach (string f in Files)
                {

                    string text = System.IO.File.ReadAllText(f);

                    foreach (string[] db in distinctDatabaseTables)
                    {
                        if (text.IndexOf(db[0] + ".", StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            if (db.Length == 1)
                            {
                                OutputTxtBox.AppendText(String.Format("{0},{1},{2}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper()) + " \r\n");
                                Refresh();
                                writer.WriteLine("{0},{1},{2},{3},{4}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), "", "");
                            }
                            if (db.Length > 1)
                            {
                                if (text.IndexOf("." + db[1], StringComparison.CurrentCultureIgnoreCase) >= 0)
                                {
                                    if (db.Length == 2)
                                    { 
                                        OutputTxtBox.AppendText(String.Format("{0},{1},{2},{3}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), db[1].ToUpper()) + " \r\n");
                                        Refresh();
                                        writer.WriteLine("{0},{1},{2},{3},{4}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), db[1].ToUpper(), "");
                                    }
                                    if (db.Length > 2)
                                    {
                                        if (text.IndexOf("." + db[2], StringComparison.CurrentCultureIgnoreCase) >= 0)
                                        {
                                            OutputTxtBox.AppendText(String.Format("{0},{1},{2},{3},{4}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), db[1].ToUpper(), db[2].ToUpper()) + " \r\n");
                                            Refresh();
                                            writer.WriteLine("{0},{1},{2},{3},{4}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), db[1].ToUpper(), db[2].ToUpper());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            OutputTxtBox.AppendText("Output file saved to " + OutputPath + " \r\n");
            OutputTxtBox.AppendText("COMPLETE" + " \r\n");
            Refresh();
            Console.ReadLine();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchRDLS();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RDLPathTxtBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            OutputPathTxtBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
    }
}
