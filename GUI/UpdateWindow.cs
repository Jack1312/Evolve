/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCLawl/Evolve) 
    Dual-Licensed under the Educational Community License, Version 2.0 and
    the binpress license you mayy not use this file except in compliance with 
    the License. You may obtain a copy of the Licenses at
	
	http://www.osedu.org/licenses/ECL-2.0
    http://www.binpress.com/license/view/l/6cfa4c36602b0f90ab898dc9fbd77b84
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the License is distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the License for the specific language governing
	permissions and limitations under the License.
 
    The binpress license states that:
     - May only be used for personal use (cannot be resold or distributed)
     - Non-commerical use only
     - Cannot modify source code for any purpose (cannot create derivative works
    The implications of not following the license may lead to legal action, depending
    on the circumstances.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;

namespace MCLawl.Gui
{
    public partial class UpdateWindow : Form
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }
        private void UpdateWindow_Load(object sender, EventArgs e)
        {
            UpdLoadProp("properties/update.properties");
            WebClient client = new WebClient();
            client.DownloadFile("http://www.mclawl.tk/revs.txt", "text/revs.txt");
            listRevisions.Items.Clear();
            FileInfo file = new FileInfo("text/revs.txt");
            StreamReader stRead = file.OpenText();
            if (File.Exists("text/revs.txt"))
            {
                while (!stRead.EndOfStream)
                {
                    listRevisions.Items.Add(stRead.ReadLine());
                }
            }
            stRead.Close();
            stRead.Dispose();
            file.Delete();
            client.Dispose();
        }


        public void UpdSave(string givenPath)
        {
            StreamWriter SW = new StreamWriter(File.Create(givenPath));
            SW.WriteLine("#This file manages the update process");
            SW.WriteLine("#Toggle AutoUpdate to true for the server to automatically update");
            SW.WriteLine("#Notify notifies players in-game of impending restart");
            SW.WriteLine("#Restart Countdown is how long in seconds the server will count before restarting and updating");
            SW.WriteLine();
            SW.WriteLine("autoupdate= " + chkAutoUpdate.Checked.ToString());
            SW.WriteLine("notify = " + chkNotify.Checked.ToString());
            SW.WriteLine("restartcountdown = " + txtCountdown.Text);
            SW.Flush();
            SW.Close();
            SW.Dispose();
            this.Close();
        }


        public void UpdLoadProp(string givenPath)
        {
            if (File.Exists(givenPath))
            {
                string[] lines = File.ReadAllLines(givenPath);

                foreach (string line in lines)
                {
                    if (line != "" && line[0] != '#')
                    {
                        //int index = line.IndexOf('=') + 1; // not needed if we use Split('=')
                        string key = line.Split('=')[0].Trim();
                        string value = line.Split('=')[1].Trim();

                        switch (key.ToLower())
                        {
                            case "autoupdate":
                                chkAutoUpdate.Checked = (value.ToLower() == "true") ? true : false;
                                break;
                            case "notify":
                                chkNotify.Checked = (value.ToLower() == "true") ? true : false;
                                break;
                            case "restartcountdown":
                                txtCountdown.Text = value;
                                break;
                        }
                    }
                }
                //Save(givenPath);
            }
            //else Save(givenPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string chkNum = txtCountdown.Text.Trim();
            double Num;
            bool isNum = double.TryParse(chkNum, out Num);
            if (!isNum || txtCountdown.Text == "")
            {
                MessageBox.Show("You must enter a number for the countdown");
            }
            else
            {
                UpdSave("properties/update.properties");
                Server.autoupdate = chkAutoUpdate.Checked;
            }
        }

        private void cmdDiscard_Click(object sender, EventArgs e)
        {
            UpdLoadProp("properties/update.properties");
            this.Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (Server.selectedrevision != "")
            {
                MCLawl_.Gui.Program.PerformUpdate(true);
                
            }
            else { MCLawl_.Gui.Program.PerformUpdate(false); }
      /*      if (!Program.CurrentUpdate)
                Program.UpdateCheck();
            else
            {
                Thread messageThread = new Thread(new ThreadStart(delegate
                {
                    MessageBox.Show("Already checking for updates.");
                })); messageThread.Start();
            } */
        }


        private void listRevisions_SelectedValueChanged(object sender, EventArgs e)
        {
            Server.selectedrevision = listRevisions.SelectedItem.ToString();
            
        }

    }
}
