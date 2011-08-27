/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCLawl/Evolve) 
    Dual-Licensed under the Educational Community License, Version 2.0 and
    the binpress license you may not use this file except in compliance with 
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
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using MCLawl;

namespace MCLawl.Gui
{
    public partial class Window : Form
    {
        Regex regex = new Regex(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\." +
                                "([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");
        // for cross thread use
        delegate void StringCallback(string s);
        delegate void PlayerListCallback(List<Player> players);
        delegate void ReportCallback(Report r);
        delegate void VoidDelegate();

        public static event EventHandler Minimize;
        public NotifyIcon notifyIcon1 = new NotifyIcon();
        //  public static bool Minimized = false;
        
        internal static Server s;

        private Level levelGenerater;
        private bool generatorUsed = false;
        private Level levelExisted = null;
        Form mapForm = new Form();
        private bool formShow = false;
        PictureBox map2 = new PictureBox();

        Player playerstatsplayer;
        Player console;

        bool shuttingDown = false;
        public Window() {
            InitializeComponent();
        }

        private void Window_Minimize(object sender, EventArgs e)
        {
      /*     if (!Minimized)
            {
                Minimized = true;
                ntf.Text = "MCZall";
                ntf.Icon = this.Icon;
                ntf.Click += delegate
                {
                    try
                    {
                        Minimized = false;
                        this.ShowInTaskbar = true;
                        this.Show();
                        WindowState = FormWindowState.Normal;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                };
                ntf.Visible = true;
                this.ShowInTaskbar = false;
            } */
        }

        public static Window thisWindow;

        private void Window_Load(object sender, EventArgs e) {
            thisWindow = this;
            MaximizeBox = false;
            this.Text = Server.name;

            mapForm.Name = "mapForm";
            mapForm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(mapForm_Closed);
            mapForm.Controls.Add(map2);

            this.Show();
            this.BringToFront();
            WindowState = FormWindowState.Normal;

            s = new Server();
            s.OnLog += WriteLine;
            s.OnAdminChat += AdminWrite;
            s.OnOpChat += OpWrite;
            s.OnGlobalChat += GlobalWrite;
            s.OnCommand += newCommand;
            s.OnError += newError;
            s.OnSystem += newSystem;

            foreach (TabPage tP in tabControl1.TabPages)
                tabControl1.SelectTab(tP);
            tabControl1.SelectTab(tabControl1.TabPages[0]);

            s.HeartBeatFail += HeartBeatFail;
            s.OnURLChange += UpdateUrl;
            s.OnPlayerListChange += UpdateClientList;
            s.OnSettingsUpdate += SettingsUpdate;
            s.Start();
            notifyIcon1.Text = ("Evolve Server: " + Server.name);

            this.notifyIcon1.ContextMenuStrip = this.iconContext;
            this.notifyIcon1.Icon = this.Icon;
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);

            System.Timers.Timer MapTimer = new System.Timers.Timer(10000);
            MapTimer.Elapsed += delegate {
                UpdateMapList("'");
            }; MapTimer.Start();

            System.Timers.Timer PlayerTimer = new System.Timers.Timer(10000);
            PlayerTimer.Elapsed += delegate
            {
                UpdateClientList(Player.players);
            }; PlayerTimer.Start();

            //if (File.Exists(Logger.ErrorLogPath))
                //txtErrors.Lines = File.ReadAllLines(Logger.ErrorLogPath);
            if (File.Exists("extra/Changelog.txt"))
            {
                txtChangelog.Text = "Changelog for " + Server.Version + ":";
                foreach (string line in File.ReadAllLines(("extra/Changelog.txt")))
                {
                    txtChangelog.AppendText("       " + line + "\r\n");
                }            
            }
        }

        void SettingsUpdate()
        {
            if (shuttingDown) return;
            if (txtLog.InvokeRequired)
            {
                VoidDelegate d = new VoidDelegate(SettingsUpdate);
                this.Invoke(d);
            }  else {
                this.Text = Server.name + " Evolve Version: " + Server.Version;
            }
        }

        void HeartBeatFail() {
            WriteLine("Recent Heartbeat Failed");
        }

        void newError(string message)
        {
            try
            {
                if (txtErrors.InvokeRequired)
                {
                    LogDelegate d = new LogDelegate(newError);
                    this.Invoke(d, new object[] { message });
                }
                else
                {
                    txtErrors.AppendText(Environment.NewLine + message);
                }
            } catch { }
        }
        void newSystem(string message)
        {
            try
            {
                if (txtSystem.InvokeRequired)
                {
                    LogDelegate d = new LogDelegate(newSystem);
                    this.Invoke(d, new object[] { message });
                }
                else
                {
                    txtSystem.AppendText(Environment.NewLine + message);
                }
            } catch { }
        }

        delegate void LogDelegate(string message);
        delegate void ListDelegate(string message);

        /// <summary>
        /// Does the same as Console.Write() only in the form
        /// </summary>
        /// <param name="s">The string to write</param>
        public void Write(string s) {
            if (shuttingDown) return;
            if (txtLog.InvokeRequired) {
                LogDelegate d = new LogDelegate(Write);
                this.Invoke(d, new object[] { s });
            } else {
                txtLog.AppendText(s);
            }
        }
        /// <summary>
        /// Does the same as Console.WriteLine() only in the form
        /// </summary>
        /// <param name="s">The line to write</param>
        public void WriteLine(string s)
        {
            if (shuttingDown) return;
            if (this.InvokeRequired) {
                LogDelegate d = new LogDelegate(WriteLine);
                this.Invoke(d, new object[] { s });
            } else {
                txtLog.AppendText(s + "\r\n");
            }
        }
        /// <summary>
        /// Updates the list of client names in the window
        /// </summary>
        /// <param name="players">The list of players to add</param>
        public void UpdateClientList(List<Player> players) {
            if (this.InvokeRequired) {
                PlayerListCallback d = new PlayerListCallback(UpdateClientList);
                this.Invoke(d, new object[] { players });
            } else {
                dgvPlayers.Rows.Clear();
                Player.players.ForEach(delegate(Player p) { string[] row = { p.name, p.group.name, p.level.name }; dgvPlayers.Rows.Add(row); });
                //Handles the Players Tab Shit
                liClients.Items.Clear();
                dgvPlayerStats.Rows.Clear();
                Player.players.ForEach(delegate(Player p) { string[] row1 = { p.name, p.title, p.group.name, p.money.ToString(), p.level.name, p.totalLogins.ToString(), p.overallBlocks.ToString() }; dgvPlayerStats.Rows.Add(row1);
                liClients.Items.Add(p.name);
                if (File.Exists("ranks/banned-ip.txt"))
                {
                    cmbIpBans.Items.Clear();
                    foreach (string line in File.ReadAllLines(("ranks/banned-ip.txt")))
                    {
                        cmbIpBans.Items.Add(line);
                    }
                }
                if (txtPName.Text != "[Selected Player]")
                {
                    liClients.SelectedIndex = 0;
                }
                else if (!liClients.Items.Contains(txtPName.Text))
                {
                    liClients.SelectedIndex = 0;
                }
                else
                {
                    for (int i = 0; i < liClients.Items.Count; i++)
                    {
                        if (liClients.Items[i].ToString().Contains(txtPName.Text))
                        {
                            liClients.SelectedIndex = i;
                        }
                    }
                }
                });
            }
        }

        public void UpdateMapList(string blah) {            
            if (this.InvokeRequired) {
                LogDelegate d = new LogDelegate(UpdateMapList);
                this.Invoke(d, new object[] { blah });
            } else {
                dgvMaps.Rows.Clear();
                dataGridView1.Rows.Clear();
                cmbMaps.Items.Clear();
                foreach (Level level in Server.levels) {
                    cmbMaps.Items.Add(level.name);
                    string[] row = { level.name, level.players.Count.ToString(), level.physics.ToString(), level.permissionbuild.ToString() };
                    dgvMaps.Rows.Add(row);
                    string[] row1 = { level.name, level.players.Count.ToString(), level.physics.ToString(), level.permissionbuild.ToString(), level.permissionvisit.ToString(), level.owner, level.width.ToString(), level.depth.ToString(), level.height.ToString() };
                    dataGridView1.Rows.Add(row1);
                    cmbRanks.Items.Clear();
                    foreach (Group grp in Group.GroupList)
                    {
                        if (grp.name != "nobody")
                            cmbRanks.Items.Add(grp.name);
                    }
                    List<string> loadedmapslist = new List<string>(Server.levels.Count);
                    List<string> unloadedmapslist = new List<string>();
                    DirectoryInfo di = new DirectoryInfo("levels/");
                    FileInfo[] fi = di.GetFiles("*.lvl");
                    Thread.Sleep(10);
                    foreach (Level l in Server.levels) { loadedmapslist.Add(l.name.ToLower()); }
                    foreach (FileInfo file in fi)
                    {
                        if (!loadedmapslist.Contains(file.Name.Replace(".lvl", "").ToLower()))
                        {
                            unloadedmapslist.Add(file.Name.Replace(".lvl", ""));
                        }
                    }
                    UnloadedMaps.Items.Clear();
                    foreach (String unloaded in unloadedmapslist)
                    {
                        UnloadedMaps.Items.Add(unloaded);
                    }
                    LoadedMaps.Items.Clear();
                    foreach (String loaded in loadedmapslist)
                    {
                        LoadedMaps.Items.Add(loaded);
                    }
                }
            }
        }

        /// <summary>
        /// Places the server's URL at the top of the window
        /// </summary>
        /// <param name="s">The URL to display</param>
        public void UpdateUrl(string s)
        {
            if (this.InvokeRequired)
            {
                StringCallback d = new StringCallback(UpdateUrl);
                this.Invoke(d, new object[] { s });
            }
            else
                txtUrl.Text = s;
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e) {
            if (notifyIcon1 != null) {
                notifyIcon1.Visible = false;
            }
            MCLawl_.Gui.Program.ExitProgram(false);
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtInput.Text == null || txtInput.Text.Trim()=="") { return; }
                string text = txtInput.Text.Trim();
                string newtext = text;
                if (txtInput.Text[0] == '#')
                {
                    newtext = text.Remove(0, 1).Trim();
                    Player.GlobalMessageOps("To Ops &f-"+Server.DefaultColor +"Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                    Server.s.OpChat("<CONSOLE> " + newtext);
                    Server.s.Log("(OPs): Console: " + newtext);
                    IRCBot.Say("Console: " + newtext, true);
                 //   WriteLine("(OPs):<CONSOLE> " + txtInput.Text);
                    txtInput.Clear();
                }
                else if (txtInput.Text[0] == '*')
                {
                    newtext = text.Remove(0, 1).Trim();
                    Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                    Server.s.AdminChat("<CONSOLE> " + newtext);
                    Server.s.Log("(Admins): Console: " + newtext);
                    IRCBot.Say("Console: " + newtext, true);
                    //   WriteLine("(Admins):<CONSOLE> " + txtInput.Text);
                    txtInput.Clear();
                }
                else
                {
                    Player.GlobalMessage("Console [&a" + Server.ZallState + Server.DefaultColor + "]: &f" + txtInput.Text);
                    Server.s.GlobalChat("<CONSOLE> " + newtext);
                    IRCBot.Say("Console [" + Server.ZallState + "]: " + txtInput.Text);
                    WriteLine("<CONSOLE> " + txtInput.Text);
                    txtInput.Clear();
                }
            }
        }

        private void txtCommands_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                string sentCmd = "", sentMsg = "";

                if (txtCommands.Text == null || txtCommands.Text.Trim() == "")
                {
                    newCommand("CONSOLE: Whitespace commands are not allowed.");
                    txtCommands.Clear();
                    return;
                }

                if (txtCommands.Text[0] == '/')
                    if (txtCommands.Text.Length > 1)
                        txtCommands.Text = txtCommands.Text.Substring(1);

                if (txtCommands.Text.IndexOf(' ') != -1) {
                    sentCmd = txtCommands.Text.Split(' ')[0];
                    sentMsg = txtCommands.Text.Substring(txtCommands.Text.IndexOf(' ') + 1);
                } else if (txtCommands.Text != "") {
                    sentCmd = txtCommands.Text;
                } else {
                    return;
                }

                try { 
                    Command.all.Find(sentCmd).Use(null, sentMsg);
                    newCommand("CONSOLE: USED /" + sentCmd + " " + sentMsg);
                } catch (Exception ex) {
                    Server.ErrorLog(ex);
                    newCommand("CONSOLE: Failed command."); 
                }

                txtCommands.Clear();
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e) { 
            if (notifyIcon1 != null) {
                notifyIcon1.Visible = false;
            }
            MCLawl_.Gui.Program.ExitProgram(false); 
        }

        public void newCommand(string p) { 
            if (txtCommandsUsed.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(newCommand);
                this.Invoke(d, new object[] { p });
            }
            else
            {
                txtCommandsUsed.AppendText(p + "\r\n\r\n"); 
            }
        }

        void ChangeCheck(string newCheck) { Server.ZallState = newCheck; }

        private void txtHost_TextChanged(object sender, EventArgs e)
        {
            if (txtHost.Text != "") ChangeCheck(txtHost.Text);
        }

        private void btnProperties_Click_1(object sender, EventArgs e) {
            if (!prevLoaded) { PropertyForm = new PropertyWindow(); prevLoaded = true; }
            PropertyForm.Show();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e) {
            if (!MCLawl_.Gui.Program.CurrentUpdate)
                MCLawl_.Gui.Program.UpdateCheck();
            else {
                Thread messageThread = new Thread(new ThreadStart(delegate {
                    MessageBox.Show("Already checking for updates.");
                })); messageThread.Start();
            }
        }

        public static bool prevLoaded = false;
        Form PropertyForm;
        Form UpdateForm;

        private void gBChat_Enter(object sender, EventArgs e)
        {

        }

        private void btnExtra_Click_1(object sender, EventArgs e) {
            if (!prevLoaded) { PropertyForm = new PropertyWindow(); prevLoaded = true; }
            PropertyForm.Show();
            PropertyForm.Top = this.Top + this.Height - txtCommandsUsed.Height;
            PropertyForm.Left = this.Left;
        }

        private void Window_Resize(object sender, EventArgs e) {
            this.Hide();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e) {
            this.Show();
            this.BringToFront();
            WindowState = FormWindowState.Normal;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UpdateForm = new UpdateWindow();
            UpdateForm.Show();
        }

        private void tmrRestart_Tick(object sender, EventArgs e)
        {
            if (Server.autorestart)
            {
                if (DateTime.Now.TimeOfDay.CompareTo(Server.restarttime.TimeOfDay) > 0 && (DateTime.Now.TimeOfDay.CompareTo(Server.restarttime.AddSeconds(1).TimeOfDay)) < 0) {
                    Player.GlobalMessage("The time is now " + DateTime.Now.TimeOfDay);
                    Player.GlobalMessage("The server will now begin auto restart procedures.");
                    Server.s.Log("The time is now " + DateTime.Now.TimeOfDay);
                    Server.s.Log("The server will now begin auto restart procedures.");

                    if (notifyIcon1 != null) {
                        notifyIcon1.Icon = null;
                        notifyIcon1.Visible = false;
                    }
                    MCLawl_.Gui.Program.ExitProgram(true);
                }
            }
        }

        private void openConsole_Click(object sender, EventArgs e)
        {
            // Yes, it's a hacky fix.  Don't ask :v
            this.Show();
            this.BringToFront();
            WindowState = FormWindowState.Normal;
            this.Show();
            this.BringToFront();
            WindowState = FormWindowState.Normal;
        }

        private void shutdownServer_Click(object sender, EventArgs e)
        {
            if (notifyIcon1 != null)
            {
                notifyIcon1.Visible = false;
            }
            MCLawl_.Gui.Program.ExitProgram(false); 
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            foreach (TabPage tP in tabControl1.TabPages)
            {
                foreach (Control ctrl in tP.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        TextBox txtBox = (TextBox)ctrl;
                        txtBox.Update();
                    }
                }
            }
        }

        private void txtCommandsUsed_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                txtChangelog.Text = "";
                WebClient Client = new WebClient();
                Client.DownloadFile("http://www.evolvemc.net/changelog.txt", "extra/Changelog.txt");
                if (File.Exists("extra/Changelog.txt"))
                {
                    txtChangelog.Text = "Changelog for r" + Server.Version + ":";
                    foreach (string line in File.ReadAllLines(("extra/Changelog.txt")))
                    {
                        txtChangelog.AppendText("\r\n" + line);
                    }
                }
                Client.Dispose();
            }
            catch
            {
                MessageBox.Show("Something went wrong =S.... unable to get or load new Changelog!");
                return;
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtUrl.Text == ""){ } else { System.Diagnostics.Process.Start(txtUrl.Text); }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateWindow Window = new UpdateWindow();
            Window.Show();
        }

        /*public void loadCmdlist()
        {
            txtAvailable.Items.Clear();
            txtDownloaded.Items.Clear();
            WebClient Client = new WebClient();
            Client.DownloadFile("http://www.MCLawl.x10.mx/custom_commands/cmdlist.txt", "extra/cmdlist.txt");
            if (File.Exists("extra/cmdlist.txt"))
            {
                foreach (string line in File.ReadAllLines(("extra/cmdlist.txt")))
                {
                    txtAvailable.Items.Add(line);
                }
            }

            DirectoryInfo di = new DirectoryInfo("extra/commands/dll");
            FileInfo[] rgFiles = di.GetFiles("*.dll");
            foreach (FileInfo fi in rgFiles)
            {
                txtDownloaded.Items.Add(fi);
            }
            Client.Dispose();
        }*/

        /*private void button1_Click(object sender, EventArgs e)
        {
            loadCmdlist();
        }*/

        public void GlobalWrite(string s)
        {
            if (Server.shuttingDown) return;
            if (this.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(GlobalWrite);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                txtGlobalChat.AppendText(s + "\r\n");
            }
        }

        public void OpWrite(string s)
        {
            if (Server.shuttingDown) return;
            if (this.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(OpWrite);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                txtOpChat.AppendText(s + "\r\n");
            }
        }

        public void AdminWrite(string s)
        {
            if (Server.shuttingDown) return;
            if (this.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(AdminWrite);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                txtAdminChat.AppendText(s + "\r\n");
            }
        }

        /*private void txtAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSelectedDownload.Text = txtAvailable.SelectedItem.ToString();
        }

        private void txtDownloadFile_Click(object sender, EventArgs e)
        {
            if (txtSelectedDownload.Text == "")
            {
                MessageBox.Show("No File Was Selected");
            }
            else
            {
                WebClient Client = new WebClient();
                Client.DownloadFile("http://www.MCLawl.x10.mx/custom_commands/" + txtAvailable.SelectedItem.ToString() + ".dll", "extra/commands/dll/Cmd" + txtAvailable.SelectedItem.ToString() + ".dll");
                Command.all.Find("cmdload").Use(null, txtSelectedDownload.Text);
                Client.Dispose();
            }
            loadCmdlist();
        }

        private void txtDownloaded_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSelectedFile.Text = txtDownloaded.SelectedItem.ToString();
        }

        private void txtDeleteFile_Click(object sender, EventArgs e)
        {
            if (txtDownloaded.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose a file to delete first");
            }
            else if (File.Exists("extra/commands/dll/" + txtDownloaded.SelectedItem.ToString()))
            {
                File.Delete("extra/commands/dll/" + txtDownloaded.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("File \"" + txtDownloaded.SelectedItem.ToString() + "\" was not found.");
            }
            loadCmdlist();
        }*/

        private void txtOpMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newtext = txtOpMessage.Text;
                Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                Server.s.OpChat("<CONSOLE> " + newtext);
                Server.s.Log("<CONSOLE> " + newtext);
                IRCBot.Say("Console: " + newtext, true);
                txtOpMessage.Clear();
            }
        }

        private void txtAdminMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newtext = txtAdminMessage.Text;
                Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                Server.s.AdminChat("<CONSOLE> " + newtext);
                Server.s.Log("<CONSOLE> " + newtext);
                IRCBot.Say("Console: " + newtext, true);
                txtAdminMessage.Clear();
            }
        }

        private void txtGlobalMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newtext = txtGlobalMessage.Text;
                Player.GlobalMessage("Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                Server.s.GlobalChat("<CONSOLE> " + newtext);
                Server.s.Log("<CONSOLE> " + newtext);
                IRCBot.Say("Console: " + newtext, true);
                txtGlobalMessage.Clear();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                Command.all.Find("load").Use(null, UnloadedMaps.SelectedItem.ToString());
                UpdateMapList("'");
            }
            catch { Server.s.Log("ERROR: Failed Map load!"); }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            UpdateMapList("'");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string lvl = LoadedMaps.SelectedItem.ToString();
                foreach (Level level in Server.levels)
                {
                    if (level.name == lvl)
                    {
                        Command.all.Find("physics").Use(null, "0");
                    }
                    UpdateMapList("'");
                }
            }
            catch
            {
                MessageBox.Show("Please choose a map first");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string lvl = LoadedMaps.SelectedItem.ToString();
                foreach (Level level in Server.levels)
                {
                    if (level.name == lvl)
                    {
                        Command.all.Find("physics").Use(null, "1");
                    }
                    UpdateMapList("'");
                }
            }
            catch
            {
                MessageBox.Show("Please choose a map first");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                string lvl = LoadedMaps.SelectedItem.ToString();
                foreach (Level level in Server.levels)
                {
                    if (level.name == lvl)
                    {
                        Command.all.Find("physics").Use(null, "2");
                    }
                    UpdateMapList("'");
                }
            }
            catch
            {
                MessageBox.Show("Please choose a map first");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string lvl = LoadedMaps.SelectedItem.ToString();
                foreach (Level level in Server.levels)
                {
                    if (level.name == lvl)
                    {
                        Command.all.Find("physics").Use(null, "3");
                    }
                    UpdateMapList("'");
                }
            }
            catch
            {
                MessageBox.Show("Please choose a map first");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string lvl = LoadedMaps.SelectedItem.ToString();
                foreach (Level level in Server.levels)
                {
                    if (level.name == lvl)
                    {
                        Command.all.Find("physics").Use(null, "4");
                    }
                    UpdateMapList("'");
                }
            }
            catch
            {
                MessageBox.Show("Please choose a map first");
            }
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            try
            {
                string lvl = LoadedMaps.SelectedItem.ToString();
                Command.all.Find("unload").Use(null, lvl);
                UpdateMapList("'");
            }
            catch
            {
                MessageBox.Show("Please choose a level first");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                string lvl = LoadedMaps.SelectedItem.ToString();
                Command.all.Find("deletelvl").Use(null, lvl);
                UpdateMapList("'");
            }
            catch
            {
                MessageBox.Show("Please choose a level first");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string lvl = UnloadedMaps.SelectedItem.ToString();
                Command.all.Find("deletelvl").Use(null, lvl);
                UpdateMapList("'");
            }
            catch
            {
                MessageBox.Show("Please choose a level first");
            }
        }

        private void mapsStrip_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented Yet!");
        }

        #region PlayersTab

        private void LoadPlayersTab(object sender, EventArgs e)
        {
            Player p = Player.Find(liClients.SelectedItem.ToString());
            playerstatsplayer = p;
            playerstatsplayer.level.name = p.level.name;
            playerstatsplayer.group.name = p.group.name;
            playerstatsplayer.money = p.money;
            playerstatsplayer.deathCount = p.deathCount;
            playerstatsplayer.title = p.title;
            playerstatsplayer.ip = p.ip;
            playerstatsplayer.overallBlocks.ToString();
        }

        private void btnJail_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("jail").Use(null, playerstatsplayer.name);
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                try
                {
                    string pl = liClients.SelectedItem.ToString();
                    foreach (Player p in Player.players)
                    {
                        if (p.name == pl)
                        {
                            Command.all.Find("goto").Use(playerstatsplayer, cmbMaps.SelectedItem.ToString());
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("No map selected/Wrong map name");
                }
            }
        }

        private void btnKick_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("kick").Use(null, pl + " " + txtKickReason.Text);
            }
        }

        private void btnTitle_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                try
                {
                    string pl = liClients.SelectedItem.ToString();
                    Command.all.Find("title").Use(null, pl + " " + txtTitle.Text);
                }
                catch
                {
                    MessageBox.Show("Title is over 17 characters/Incorrect title");
                }
            }
        }

        private void btnRank_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                try
                {
                    string pl = liClients.SelectedItem.ToString();
                    Command.all.Find("rank").Use(null, pl + " " + cmbRanks.SelectedItem.ToString());
                }
                catch
                {
                    MessageBox.Show("Incorrect rank");
                }
            }
        }

        private void btnGive_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
                try
                {
                    {
                        string pl = liClients.SelectedItem.ToString();
                        Command.all.Find("give").Use(null, pl + " " + txtGive.Text);
                    }
                }
                catch
                {
                    MessageBox.Show("Incorrect amount");
                }
        }

        private void btnNick_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("nick").Use(null, playerstatsplayer.name + " " + txtNickName.Text);
            }
        }

        private void btnPromote_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                try
                {
                    string pl = liClients.SelectedItem.ToString();
                    Command.all.Find("promote").Use(null, pl);
                }
                catch
                {
                    MessageBox.Show("There is no higher rank");
                }
            }
        }

        private void btnDemote_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                try
                {
                    string pl = liClients.SelectedItem.ToString();
                    Command.all.Find("demote").Use(null, pl);
                }
                catch
                {
                    MessageBox.Show("There is no lower rank");
                }
            }
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("ban").Use(null, pl);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("freeze").Use(console, playerstatsplayer.name);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("slap").Use(console, playerstatsplayer.name);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("hide").Use(playerstatsplayer, null);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("kill").Use(playerstatsplayer, playerstatsplayer.name);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("voice").Use(null, pl);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("joker").Use(null, pl);
            }
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("mute").Use(null, pl);
            }
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("rules").Use(playerstatsplayer, null);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("banip").Use(null, pl);
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                string pl = liClients.SelectedItem.ToString();
                Command.all.Find("trust").Use(null, pl);
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                try
                {
                    string selectedcolor = cmbColors.SelectedItem.ToString();
                    string pl = liClients.SelectedItem.ToString();
                    foreach (Player p in Player.players)
                    {
                        if (p.name == pl)
                        {
                            Command.all.Find("color").Use(playerstatsplayer, cmbColors.SelectedItem.ToString());
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("There is no color with that name");
                }
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            if (liClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Player Selected");
                return;
            }
            else
            {
                try
                {
                    string pl = liClients.SelectedItem.ToString();
                    Command.all.Find("unbanip").Use(null, cmbIpBans.SelectedItem.ToString());
                }
                catch
                {
                    MessageBox.Show("Incorrect IP/IP is not banned");
                }
            }
        }

        #endregion

        private void liClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPlayersTab(sender, e);
            txtPName.Text = playerstatsplayer.name;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (levelGenerater != null)
            {
                levelGenerater.Save();
                levelGenerater = null;
            }
            else
            {
                foreach (Level l in Server.levels)
                {
                    if (l.name == txtMapName.Text)
                    {
                        MessageBox.Show("Already exist!");
                        return;
                    }
                }
                levelGenerater = new Level(txtMapName.Text, ushort.Parse(x.SelectedItem.ToString()), ushort.Parse(y.SelectedItem.ToString()), ushort.Parse(z.SelectedItem.ToString()), cmbMapType.SelectedItem.ToString());
                levelGenerater.Save();
                levelGenerater = null;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string lvlName;
            try
            {
                lvlName = LoadedMaps.SelectedItem.ToString();
            }
            catch
            {
                return;
            }

            generatorUsed = false;
            foreach (Level l in Server.levels)
            {
                if (l.name == lvlName) { levelExisted = l; }
            }
            if (levelExisted != null)
            {
                switcherMap(levelExisted);
            }
            else { return; }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtMapName.Text == "")
            {
                MessageBox.Show("Please enter a map name first", "No map name entered");
            }
            else
            {
                string lvl = txtMapName.Text;
                foreach (Level l in Server.levels)
                {
                    if (l.name == lvl)
                    {
                        MessageBox.Show("Already exist!");
                        return;
                    }
                }
                levelGenerater = new Level(lvl, ushort.Parse(x.SelectedItem.ToString()), ushort.Parse(y.SelectedItem.ToString()), ushort.Parse(z.SelectedItem.ToString()), cmbMapType.SelectedItem.ToString());
                generatorUsed = true;
                switcherMap(levelGenerater);
            }
        }
        //Set's picbox width/height, chooses, form or picbox in console
        private void switcherMap(Level level)
        {
            if (level.width < 256 && level.height < 256)
            {
                map1.Size = new System.Drawing.Size(256, 256);
                if (level.height > level.width && Math.Max(level.height, level.width) < 256)
                {
                    map1.Size = new System.Drawing.Size(256 / (level.height / level.width), 256);
                }
                else if (level.height < level.width && Math.Max(level.height, level.width) < 256)
                {
                    map1.Size = new System.Drawing.Size(256, 256 / (level.width / level.height));
                }
                if (mapStyle.SelectedItem.ToString() == "Standart" || mapStyle.SelectedItem.ToString() == "Height mask")
                {
                    map1.Image = GenerateMap(level);
                    map1.Refresh();
                }
                else
                {
                    map1.Image = GenerateMap(level, ushort.Parse(txtLayer.Text.ToString()));
                    map1.Refresh();
                }
            }
            else
            {
                if (mapStyle.SelectedItem.ToString() == "Standart" || mapStyle.SelectedItem.ToString() == "Height mask")
                {
                    mapFormGen();
                }
                else
                {
                    mapFormGen(ushort.Parse(txtLayer.Text.ToString()));
                }
            }
        }

        //+ button
        private void btnPlus_Click(object sender, EventArgs e)
        {
            int exp = 1;
            if (generatorUsed && levelGenerater != null)
            {
                exp = levelGenerater.depth / 16;
                if (levelGenerater.depth >= 512)
                {
                    exp *= levelGenerater.depth / 256;
                }
            }
            else if (!generatorUsed && levelExisted != null)
            {
                exp = levelExisted.depth / 16;
                if (levelExisted.depth >= 512)
                {
                    exp *= levelExisted.depth / 256;
                }
            }
            if (mapStyle.Text.ToString() == "Height mask")
            {
                txtLayer.Text = (double.Parse(txtLayer.Text.ToString()) + 0.1 * exp).ToString();
            }
            else
            {
                try
                {
                    if (int.Parse(txtLayer.Text.ToString()) + 1 < 1024)
                    {
                        txtLayer.Text = (int.Parse(txtLayer.Text.ToString()) + 1).ToString();
                        txtLayer.Refresh();
                    }
                }
                catch { }
            }
        }

        //- button
        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (mapStyle.Text.ToString() == "Height mask")
            {
                int exp = 1;
                if (generatorUsed && levelGenerater != null)
                {
                    exp = levelGenerater.depth / 16;
                    if (levelGenerater.depth >= 512)
                    {
                        exp *= levelGenerater.depth / 256;
                    }
                }
                else if (!generatorUsed && levelExisted != null)
                {
                    exp = levelExisted.depth / 16;
                    if (levelExisted.depth >= 512)
                    {
                        exp *= levelExisted.depth / 256;
                    }
                }
                txtLayer.Text = (double.Parse(txtLayer.Text.ToString()) - 0.1 * exp).ToString();
            }
            else
            {
                try
                {
                    if (int.Parse(txtLayer.Text.ToString()) - 1 >= 0)
                    {
                        txtLayer.Text = (int.Parse(txtLayer.Text.ToString()) - 1).ToString();
                        txtLayer.Refresh();
                    }
                }
                catch { }
            }
        }

        private void mapForm_Closed(object sender, EventArgs e)
        {
            formShow = false;
        }

        //generates mapform for Standart of Height Mask
        private void mapFormGen()
        {
            map2.Name = "map2";
            map2.Size = new System.Drawing.Size(levelGenerater.width * 2, levelGenerater.height * 2);
            if (levelGenerater.height > levelGenerater.width && Math.Max(levelGenerater.height, levelGenerater.width) < 256)
            {
                map2.Size = new System.Drawing.Size(256 / (levelGenerater.height / levelGenerater.width), 256);
            }
            else if (levelGenerater.height < levelGenerater.width && Math.Max(levelGenerater.height, levelGenerater.width) < 256)
            {
                map2.Size = new System.Drawing.Size(256, 256 / (levelGenerater.width / levelGenerater.height));
            }
            map2.TabIndex = 0;
            map2.TabStop = false;
            map2.Image = GenerateMap(levelGenerater);
            map2.Refresh();
            mapForm.Size = new System.Drawing.Size(map2.Width, map2.Height);
            if (!formShow)
            {
                mapForm.Show();
                formShow = true;
            }
            else { mapForm.Refresh(); }
            mapForm.Focus();
        }

        //generates mapform for Layer
        private void mapFormGen(ushort layer)
        {
            map2.Name = "map2";
            map2.Size = new System.Drawing.Size(levelGenerater.width * 2, levelGenerater.height * 2);
            if (levelGenerater.height > levelGenerater.width && Math.Max(levelGenerater.height, levelGenerater.width) < 256)
            {
                map2.Size = new System.Drawing.Size(256 / (levelGenerater.height / levelGenerater.width), 256);
            }
            else if (levelGenerater.height < levelGenerater.width && Math.Max(levelGenerater.height, levelGenerater.width) < 256)
            {
                map2.Size = new System.Drawing.Size(256, 256 / (levelGenerater.width / levelGenerater.height));
            }
            map2.TabIndex = 0;
            map2.TabStop = false;
            map2.Image = GenerateMap(levelGenerater, layer);
            map2.Refresh();
            mapForm.Size = new System.Drawing.Size(map2.Width, map2.Height);
            if (!formShow)
            {
                mapForm.Show();
                formShow = true;
            }
            else { mapForm.Refresh(); }
            mapForm.Focus();
        }

        //generates map for Standart of Height Mask
        private Bitmap GenerateMap(Level lvl)
        {
            int pixels;
            SolidBrush brush = new SolidBrush(Color.FromArgb(255, Color.Black));
            if (Math.Max(lvl.height, lvl.width) < 256)
            {
                pixels = 256 / lvl.height;
                if (lvl.height > lvl.width)
                {
                    pixels = 256 / lvl.height;
                }
                else if (lvl.height < lvl.width)
                {
                    pixels = 256 / lvl.width;
                }
            }
            else
            {
                pixels = 2;
            }
            if (pixels > 16) { pixels = 16; }
            Bitmap bm;
            try
            {
                bm = new Bitmap("extra/textures/terrain" + pixels + ".png");
            }
            catch
            {
                MessageBox.Show("Missed texture files.");
                return new Bitmap(256, 256);
            }
            Bitmap test = new Bitmap(lvl.width * pixels, lvl.height * pixels);
            Graphics g = Graphics.FromImage(test);
            byte type;
            List<byte> above = new List<byte>();
            Bitmap[] textures = new Bitmap[50];
            for (int i = 0; i < 50; i++)
            {
                textures[i] = bm.Clone(new Rectangle(pixels * i, 0, pixels, pixels), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            int highterBlock = 0;
            int lovestBlock = 0;
            if (mapStyle.SelectedItem.ToString() == "Height mask")
            {
                for (int y = (lvl.depth - 1); y >= 0; y--)
                {
                    for (ushort z = 0; z < lvl.height; z++)
                    {
                        for (ushort x = 0; x < lvl.width; x++)
                        {
                            type = lvl.GetTile(x, (ushort)y, z);
                            type = Block.Convert(type);
                            if (type != 8 || type != 9 || type != 20 || !(type >= 37 && type <= 40) || type != 18 || type != 0)
                            {
                                highterBlock = lvl.depth - y;
                                y = -1;
                                z = lvl.height;
                                x = lvl.width;
                            }
                        }
                    }
                }
                for (int y = 0; y < lvl.depth; y++)
                {
                    for (ushort z = 0; z < lvl.height; z++)
                    {
                        for (ushort x = 0; x < lvl.width; x++)
                        {
                            type = lvl.GetTile(x, (ushort)y, z);
                            type = Block.Convert(type);
                            if (type == 8 || type == 9 || type == 18 || type == 0)
                            {
                                lovestBlock = (int)y;
                                y = lvl.depth;
                                z = lvl.height;
                                x = lvl.width;
                            }
                        }
                    }
                }
            }
            double param = lvl.depth * 2 / double.Parse(txtLayer.Text.ToString());
            int meta, array;
            double modifyer = 1;
            switch (lvl.depth)
            {
                case 32:
                    modifyer = 1;
                    break;
                case 64:
                    modifyer = 1.4;
                    break;
                case 128:
                    modifyer = 1.7;
                    break;
                case 256:
                    modifyer = 1.9;
                    break;
                case 512:
                    modifyer = 2;
                    break;
                case 1024:
                    modifyer = 2;
                    break;
            }
            for (ushort x = 0; x < lvl.width; x++)
            {
                for (ushort z = 0; z < lvl.height; z++)
                {
                    for (int y = (lvl.depth - 1); y >= 0; y--)
                    {
                        type = lvl.GetTile(x, (ushort)y, z);
                        type = Block.Convert(type);
                        if (type == 8 || type == 9 || type == 20 || (type >= 37 && type <= 40) || type == 18)
                        {
                            above.Add(type);
                        }
                        else if (lvl.GetTile(x, (ushort)y, z) != 0)
                        {
                            g.DrawImage(textures[type], x * pixels, z * pixels);
                            if (mapStyle.SelectedItem.ToString() == "Height mask")
                            {
                                meta = (int)(param * (y - modifyer * lovestBlock / 2 - highterBlock));
                                if (meta > 255) { meta = 255; }
                                else if (meta < 0) { meta = 0; }
                                brush = new SolidBrush(Color.FromArgb((255 - meta), Color.Black));
                                g.FillRectangle(brush, x * pixels, z * pixels, pixels, pixels);
                            }
                            if (above.Count != 0)
                            {
                                array = 1;
                                while (above[0] != 0)
                                {
                                    g.DrawImage(textures[above[above.Count - array]], x * pixels, z * pixels);
                                    above[above.Count - array] = 0;
                                    array++;
                                }
                            }

                            y = -1;
                        }
                    }
                }
            }
            g.Dispose();
            return test;
        }

        //generates map for Layer
        private Bitmap GenerateMap(Level lvl, ushort layer)
        {
            int pixels;
            if (Math.Max(lvl.height, lvl.width) < 256)
            {
                pixels = 256 / lvl.height;
                if (lvl.height > lvl.width)
                {
                    pixels = 256 / lvl.height;
                }
                else if (lvl.height < lvl.width)
                {
                    pixels = 256 / lvl.width;
                }
            }
            else
            {
                pixels = 2;
            }
            if (pixels > 16) { pixels = 16; }
            Bitmap bm = new Bitmap("extra/textures/terrain" + pixels + ".png");
            Bitmap test = new Bitmap(lvl.width * pixels, lvl.height * pixels);
            Graphics g = Graphics.FromImage(test);
            byte type;
            Bitmap[] textures = new Bitmap[50];
            for (int i = 0; i < 50; i++)
            {
                textures[i] = bm.Clone(new Rectangle(pixels * i, 0, pixels, pixels), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            for (ushort x = 0; x < lvl.width; x++)
            {
                for (ushort z = 0; z < lvl.height; z++)
                {
                    if (layer > lvl.depth - 1) { layer = (ushort)(lvl.depth - 1); }
                    type = lvl.GetTile(x, (ushort)(layer - 1), z);
                    type = Block.Convert(type);
                    g.DrawImage(textures[type], x * pixels, z * pixels);
                }
            }
            g.Dispose();
            return test;
        }

        private void mapStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (mapStyle.SelectedItem.ToString() == "Height mask")
            {
                txtLayer.Text = "2";
                txtLayer.Refresh();
            }
            else if (mapStyle.SelectedItem.ToString() == "Layer")
            {
                txtLayer.Text = "15";
                txtLayer.Refresh();
            }
            else
            {
                txtLayer.Text = "0";
                txtLayer.Refresh();
            }
            if (generatorUsed && levelGenerater != null) { switcherMap(levelGenerater); }
            else if (!generatorUsed && levelExisted != null) { switcherMap(levelExisted); }
        }

        //Fix needed
        private void txtLayer_ChangedText(object sender, EventArgs e)
        {
            if ((double.Parse(txtLayer.Text.ToString())).ToString() != txtLayer.Text.ToString())
            {
                txtLayer.Text = "1";
                return;
            }
            else if (generatorUsed && levelGenerater != null) { switcherMap(levelGenerater); }
            else if (!generatorUsed && levelExisted != null) { switcherMap(levelExisted); }
        }
    }
}
