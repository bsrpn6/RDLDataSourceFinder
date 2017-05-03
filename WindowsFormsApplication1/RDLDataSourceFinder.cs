﻿using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Configuration;
using System.Data;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
//using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class RDLDataSourceFinder : Form
    {

        public RDLDataSourceFinder()
        {
            InitializeComponent();
        }

        delegate void ShowProgressDelegate(string output);

        void ShowProgress(string output)
        {
            //delegated task to populate progress screen
            //used by secondary thread to allow main task to continue loading
            if(OutputTxtBox.InvokeRequired == false)
            {
                OutputTxtBox.AppendText(output);
            }
            else
            {
                ShowProgressDelegate showProgress = new ShowProgressDelegate(ShowProgress);
                BeginInvoke(showProgress, new object[] { output });
            }
        }

        void SearchFiles()
        {
            //lockControls(true);

            //determine if output path is valid, if not set to desktop\SearchResults.csv
            string OutputPath = setOutputPath();
            
            ShowProgress("Loading Search Parameters" + " \r\n");
            //parse search criteria
            List<string[]> distinctDatabaseTables = setSearchCriteria();
            //parse file paths
            List<string> FilePaths = setPaths();

            ShowProgress("Starting Search" + " \r\n");

            try
            {

                using (StreamWriter writer = new StreamWriter(OutputPath))
                {
                    writer.WriteLine("Path,Folder,File Name,File Extension,Database,Table,Column,Percent Match");

                    foreach (string path in FilePaths)
                    {
                        ShowProgress("Loading Files from " + path + " \r\n");

                        IEnumerable<string> Files;

                        try
                        {
                            Files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                                .Where(s => s.EndsWith(".rdl", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".rsd", StringComparison.OrdinalIgnoreCase));
                        }
                        catch
                        {
                            ShowProgress("CANNOT ACCESS PATH: " + path + " Please ensure you have access to the listed path.\r\nIf the path is a listed UNC path, be sure you have opened the\r\nSharePoint admin page prior to running.");
                            continue;
                        }

                        foreach (string f in Files)
                        {
                            //read file text into string
                            string text = cleanString(System.IO.File.ReadAllText(f));

                            //foreach database/table/column if indexof text exists, then add to output
                            foreach (string[] db in distinctDatabaseTables)
                            {
                                if (text.IndexOf(db[0] + ".", StringComparison.CurrentCultureIgnoreCase) >= 0)
                                {
                                    if (db.Length == 1)
                                    {
                                        ShowProgress(String.Format("{0},{1},{2}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper()) + " \r\n");
                                        writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", path, Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), Path.GetExtension(f), db[0].ToUpper(), "", "", "100");
                                    }
                                    //looking for a table
                                    if (db.Length > 1)
                                    {
                                        string databaseTable;

                                        if((text.IndexOf(db[0] + ".dbo." + db[1], StringComparison.CurrentCultureIgnoreCase) >= 0))
                                        {
                                            databaseTable = db[0] + ".dbo." + db[1];
                                        }
                                        else
                                        {
                                            databaseTable = db[0] + "." + db[1];
                                        }

                                        if (text.IndexOf(databaseTable, StringComparison.CurrentCultureIgnoreCase) >= 0)
                                        {
                                            if (db.Length == 2)
                                            {
                                                ShowProgress(String.Format("{0},{1},{2},{3}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), db[1].ToUpper()) + " \r\n");
                                                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", path, Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), Path.GetExtension(f), db[0].ToUpper(), db[1].ToUpper(), "", "100");
                                            }
                                            //looking for a column
                                            if (db.Length > 2)
                                            {

                                                //if looking for explicit definition of database.table.column find a match...
                                                if (text.IndexOf(databaseTable + "." + db[2], StringComparison.CurrentCultureIgnoreCase) >= 0) 
                                                {
                                                    ShowProgress(String.Format("{0},{1},{2},{3},{4}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), db[1].ToUpper(), db[2].ToUpper()) + " \r\n");
                                                    writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", path, Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), Path.GetExtension(f), db[0].ToUpper(), db[1].ToUpper(), db[2].ToUpper(), "100");
                                                }
                                                //otherwise use regular expressions to find database alias and match against 
                                                else
                                                {
                                                    //looking for an alias by finding the database.table combination and looking at the next word
                                                    Regex reg = new Regex(databaseTable + "\\s\\w+");
                                                    MatchCollection matches = reg.Matches(text); ;

                                                    bool matchFound = false;

                                                    //could be multiple matches/aliases, loop through each
                                                    //once found, break the loop on first found occurrence
                                                    foreach (Match match in matches)
                                                    {
                                                        //sub will hold the "database.table alias" data
                                                        //use space as index and return rest of string
                                                        string sub = match.Value.Substring(match.Value.IndexOf(' ') + 1);

                                                        if (text.IndexOf(sub + "." + db[2], StringComparison.CurrentCultureIgnoreCase) >= 0)
                                                        {
                                                            matchFound = true;
                                                            ShowProgress(String.Format("{0},{1},{2},{3},{4}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), db[1].ToUpper(), db[2].ToUpper()) + " \r\n");
                                                            writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", path, Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), Path.GetExtension(f), db[0].ToUpper(), db[1].ToUpper(), db[2].ToUpper(), "99");
                                                            break;
                                                        }
                                                    }

                                                    if (!matchFound)
                                                    {
                                                        if ((text.IndexOf(databaseTable, StringComparison.CurrentCultureIgnoreCase) >= 0) & (text.IndexOf(db[2], StringComparison.CurrentCultureIgnoreCase) >= 0)) 
                                                        {
                                                            ShowProgress(String.Format("{0},{1},{2},{3},{4}", Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), db[0].ToUpper(), db[1].ToUpper(), db[2].ToUpper()) + " \r\n");
                                                            writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", path, Path.GetFileName(Path.GetDirectoryName(f)), Path.GetFileName(f), Path.GetExtension(f), db[0].ToUpper(), db[1].ToUpper(), db[2].ToUpper(), "70");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                ShowProgress("Output file saved to " + OutputPath + " \r\n");
                ShowProgress("COMPLETE" + " \r\n");
            }
            catch
            {
                ShowProgress("CANNOT WRITE TO FILE!!!\r\nBe sure to close any open instances of SearchResults.csv and ensure the path provided is valid.\r\n");
            }
            
            //lockControls(false);
        }

        public delegate void searchFilesDelegate();

        private void button1_Click(object sender, EventArgs e)
        {
            //start search using delegate thread, allowing UI to continue
            searchFilesDelegate searchFiles = new searchFilesDelegate(SearchFiles);
            searchFiles.BeginInvoke(null, null);
        }

        private void lockControls(bool begin)
        {
            //not used yet, need to add additional delegate for this task
            if (begin)
            {
                FilesPathTxtBox.Enabled = false;
                OutputPathTxtBox.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                FilesPathTxtBox.Enabled = true;
                OutputPathTxtBox.Enabled = true;
                button1.Enabled = true;
            }
        }

        private string cleanString(string original)
        {
            return Regex.Replace(original, @"[\[\]']+", "");
        }

        private string setOutputPath()
        {
            //determine if valid output path, if not, use user's desktop
            string determinedPath;

            if (OutputPathTxtBox.Text != null)
            {
                if (!Directory.Exists(OutputPathTxtBox.Text))
                {
                    Directory.CreateDirectory(OutputPathTxtBox.Text);
                }

                determinedPath =  OutputPathTxtBox.Text + "\\SearchResults.csv";
            }
            else
            {
                determinedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SearchResults.csv";
                ShowProgress("INVALID PATH: Output path set to " + determinedPath);
            }

            //OutputPathTxtBox.Text = determinedPath;
            return determinedPath;
        }

        private List<string[]> setSearchCriteria()
        {
            //load search criteria
            //parse by line and then by period
            string[] CriteriaTextBoxSplit = CriteriaTextBox.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            List<string[]> databaseTables = new List<string[]>();

            foreach (string line in CriteriaTextBoxSplit)
            {
                if (line != "")
                {
                    if (line.Substring(line.Length - 1) == ".")
                    {
                        databaseTables.Add(line.Substring(0, line.Length - 1).Split('.'));
                    }
                    else
                    {
                        databaseTables.Add(line.Split('.'));
                    }
                }
            }
            //final list that is returned to main task
            //ordered and duplicates removed
            List<string[]> distinctDatabaseTables = databaseTables.Distinct().OrderBy(arr => arr[0]).ToList();

            return distinctDatabaseTables;
        }

        private List<string> setPaths()
        {
            //parsing multiple line input into list
            string[] FilePathTextBoxSplit = FilesPathTxtBox.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            List<string> FilePaths = new List<string>();

            foreach (string line in FilePathTextBoxSplit)
            {
                if (line != "")
                {
                    FilePaths.Add(line);
                }
            }

            return FilePaths;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //load preconfigured file search paths from app.config file
            string[] paths = ConfigurationManager.AppSettings.AllKeys
                             .Where(key => key.StartsWith("SearchPath"))
                             .Select(key => ConfigurationManager.AppSettings[key])
                             .ToArray();

            foreach (string path in paths)
            {
                FilesPathTxtBox.AppendText(path + "\r\n");
            }

            //pre-populate output textbox with Desktop path
            OutputPathTxtBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
    }
}
