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
using System.Windows.Forms;

namespace MCLawl.Gui
{
    public partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message msg)
        {
            const int WM_SIZE = 0x0005;
            const int SIZE_MINIMIZED = 1;

            if ((msg.Msg == WM_SIZE) && ((int)msg.WParam == SIZE_MINIMIZED) && (Window.Minimize != null))
            {
                this.Window_Minimize(this, EventArgs.Empty);
            }

            base.WndProc(ref msg);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dgvMaps = new System.Windows.Forms.DataGridView();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Players = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Physics = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Perbuild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mapsStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.physicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finiteModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animalAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeWaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.growingGrassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.survivalDeathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killerBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rPChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvPlayers = new System.Windows.Forms.DataGridView();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Map = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.whoisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clonesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCommandsUsed = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGlobalMessage = new System.Windows.Forms.TextBox();
            this.txtGlobalChat = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOpMessage = new System.Windows.Forms.TextBox();
            this.txtOpChat = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAdminMessage = new System.Windows.Forms.TextBox();
            this.txtAdminChat = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.txtChangelog = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtErrors = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSystem = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txtPName = new System.Windows.Forms.TextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.cmbIpBans = new System.Windows.Forms.ComboBox();
            this.button14 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.cmbColors = new System.Windows.Forms.ComboBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnDemote = new System.Windows.Forms.Button();
            this.btnPromote = new System.Windows.Forms.Button();
            this.txtGive = new System.Windows.Forms.TextBox();
            this.btnGive = new System.Windows.Forms.Button();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.btnNick = new System.Windows.Forms.Button();
            this.cmbRanks = new System.Windows.Forms.ComboBox();
            this.btnRank = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtKickReason = new System.Windows.Forms.TextBox();
            this.btnKick = new System.Windows.Forms.Button();
            this.btnTitle = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.cmbMaps = new System.Windows.Forms.ComboBox();
            this.btnMap = new System.Windows.Forms.Button();
            this.btnBan = new System.Windows.Forms.Button();
            this.btnJail = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.btnMute = new System.Windows.Forms.Button();
            this.liClients = new System.Windows.Forms.ListBox();
            this.dgvPlayerStats = new System.Windows.Forms.DataGridView();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserMap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserTotalLogins = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserBlockChanges = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.map1 = new System.Windows.Forms.PictureBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.LoadedMaps = new System.Windows.Forms.ListBox();
            this.btnUnload = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.UnloadedMaps = new System.Windows.Forms.ListBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.mapStyle = new System.Windows.Forms.ComboBox();
            this.txtLayer = new System.Windows.Forms.TextBox();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMapName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbMapType = new System.Windows.Forms.ComboBox();
            this.z = new System.Windows.Forms.ComboBox();
            this.x = new System.Windows.Forms.ComboBox();
            this.y = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.button11 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MapName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapPlayers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapPhysics = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapPerbuild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapPervisit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dimensionx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dimensiony = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dimensionz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExtra = new System.Windows.Forms.Button();
            this.txtCommands = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrRestart = new System.Windows.Forms.Timer(this.components);
            this.iconContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownServer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnProperties = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaps)).BeginInit();
            this.mapsStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).BeginInit();
            this.playerStrip.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayerStats)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.map1)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.iconContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabControl1.Location = new System.Drawing.Point(2, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(711, 508);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.dgvMaps);
            this.tabPage1.Controls.Add(this.dgvPlayers);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.txtUrl);
            this.tabPage1.Controls.Add(this.txtHost);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(703, 482);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(6, 452);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 22);
            this.btnUpdate.TabIndex = 38;
            this.btnUpdate.Text = "Updater";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dgvMaps
            // 
            this.dgvMaps.AllowUserToAddRows = false;
            this.dgvMaps.AllowUserToDeleteRows = false;
            this.dgvMaps.AllowUserToResizeColumns = false;
            this.dgvMaps.AllowUserToResizeRows = false;
            this.dgvMaps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaps.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMaps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Level,
            this.Players,
            this.Physics,
            this.Perbuild});
            this.dgvMaps.ContextMenuStrip = this.mapsStrip;
            this.dgvMaps.Location = new System.Drawing.Point(6, 231);
            this.dgvMaps.Name = "dgvMaps";
            this.dgvMaps.RowHeadersVisible = false;
            this.dgvMaps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaps.Size = new System.Drawing.Size(244, 216);
            this.dgvMaps.TabIndex = 41;
            // 
            // Level
            // 
            this.Level.HeaderText = "Name";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            // 
            // Players
            // 
            this.Players.HeaderText = "Players";
            this.Players.Name = "Players";
            this.Players.ReadOnly = true;
            // 
            // Physics
            // 
            this.Physics.HeaderText = "Physics";
            this.Physics.Name = "Physics";
            this.Physics.ReadOnly = true;
            // 
            // Perbuild
            // 
            this.Perbuild.HeaderText = "Perbuild";
            this.Perbuild.Name = "Perbuild";
            this.Perbuild.ReadOnly = true;
            // 
            // mapsStrip
            // 
            this.mapsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.physicsToolStripMenuItem,
            this.unloadToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.mapsStrip.Name = "mapsStrip";
            this.mapsStrip.Size = new System.Drawing.Size(114, 92);
            this.mapsStrip.Opening += new System.ComponentModel.CancelEventHandler(this.mapsStrip_Opening);
            // 
            // physicsToolStripMenuItem
            // 
            this.physicsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.physicsToolStripMenuItem.Name = "physicsToolStripMenuItem";
            this.physicsToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.physicsToolStripMenuItem.Text = "Physics";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem2.Text = "0";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem3.Text = "1";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem4.Text = "2";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem5.Text = "3";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem6.Text = "4";
            // 
            // unloadToolStripMenuItem
            // 
            this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
            this.unloadToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.unloadToolStripMenuItem.Text = "Unload";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finiteModeToolStripMenuItem,
            this.animalAIToolStripMenuItem,
            this.edgeWaterToolStripMenuItem,
            this.growingGrassToolStripMenuItem,
            this.survivalDeathToolStripMenuItem,
            this.killerBlocksToolStripMenuItem,
            this.rPChatToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // finiteModeToolStripMenuItem
            // 
            this.finiteModeToolStripMenuItem.Name = "finiteModeToolStripMenuItem";
            this.finiteModeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.finiteModeToolStripMenuItem.Text = "Finite Mode";
            // 
            // animalAIToolStripMenuItem
            // 
            this.animalAIToolStripMenuItem.Name = "animalAIToolStripMenuItem";
            this.animalAIToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.animalAIToolStripMenuItem.Text = "Animal AI";
            // 
            // edgeWaterToolStripMenuItem
            // 
            this.edgeWaterToolStripMenuItem.Name = "edgeWaterToolStripMenuItem";
            this.edgeWaterToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.edgeWaterToolStripMenuItem.Text = "Edge Water";
            // 
            // growingGrassToolStripMenuItem
            // 
            this.growingGrassToolStripMenuItem.Name = "growingGrassToolStripMenuItem";
            this.growingGrassToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.growingGrassToolStripMenuItem.Text = "Grass Growing";
            // 
            // survivalDeathToolStripMenuItem
            // 
            this.survivalDeathToolStripMenuItem.Name = "survivalDeathToolStripMenuItem";
            this.survivalDeathToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.survivalDeathToolStripMenuItem.Text = "Survival Death";
            // 
            // killerBlocksToolStripMenuItem
            // 
            this.killerBlocksToolStripMenuItem.Name = "killerBlocksToolStripMenuItem";
            this.killerBlocksToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.killerBlocksToolStripMenuItem.Text = "Killer Blocks";
            // 
            // rPChatToolStripMenuItem
            // 
            this.rPChatToolStripMenuItem.Name = "rPChatToolStripMenuItem";
            this.rPChatToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.rPChatToolStripMenuItem.Text = "RP Chat";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // dgvPlayers
            // 
            this.dgvPlayers.AllowUserToAddRows = false;
            this.dgvPlayers.AllowUserToDeleteRows = false;
            this.dgvPlayers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlayers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User,
            this.Rank,
            this.Map});
            this.dgvPlayers.ContextMenuStrip = this.playerStrip;
            this.dgvPlayers.Location = new System.Drawing.Point(6, 6);
            this.dgvPlayers.Name = "dgvPlayers";
            this.dgvPlayers.ReadOnly = true;
            this.dgvPlayers.RowHeadersVisible = false;
            this.dgvPlayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlayers.Size = new System.Drawing.Size(244, 216);
            this.dgvPlayers.TabIndex = 39;
            // 
            // User
            // 
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            // 
            // Rank
            // 
            this.Rank.HeaderText = "Rank";
            this.Rank.Name = "Rank";
            this.Rank.ReadOnly = true;
            // 
            // Map
            // 
            this.Map.HeaderText = "Map";
            this.Map.Name = "Map";
            this.Map.ReadOnly = true;
            // 
            // playerStrip
            // 
            this.playerStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whoisToolStripMenuItem,
            this.kickToolStripMenuItem,
            this.banToolStripMenuItem,
            this.voiceToolStripMenuItem,
            this.clonesToolStripMenuItem,
            this.promoteToolStripMenuItem,
            this.demoteToolStripMenuItem});
            this.playerStrip.Name = "playerStrip";
            this.playerStrip.Size = new System.Drawing.Size(115, 158);
            // 
            // whoisToolStripMenuItem
            // 
            this.whoisToolStripMenuItem.Name = "whoisToolStripMenuItem";
            this.whoisToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.whoisToolStripMenuItem.Text = "whois";
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.kickToolStripMenuItem.Text = "kick";
            // 
            // banToolStripMenuItem
            // 
            this.banToolStripMenuItem.Name = "banToolStripMenuItem";
            this.banToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.banToolStripMenuItem.Text = "ban";
            // 
            // voiceToolStripMenuItem
            // 
            this.voiceToolStripMenuItem.Name = "voiceToolStripMenuItem";
            this.voiceToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.voiceToolStripMenuItem.Text = "voice";
            // 
            // clonesToolStripMenuItem
            // 
            this.clonesToolStripMenuItem.Name = "clonesToolStripMenuItem";
            this.clonesToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.clonesToolStripMenuItem.Text = "clones";
            // 
            // promoteToolStripMenuItem
            // 
            this.promoteToolStripMenuItem.Name = "promoteToolStripMenuItem";
            this.promoteToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.promoteToolStripMenuItem.Text = "promote";
            // 
            // demoteToolStripMenuItem
            // 
            this.demoteToolStripMenuItem.Name = "demoteToolStripMenuItem";
            this.demoteToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.demoteToolStripMenuItem.Text = "demote";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCommandsUsed);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(256, 324);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(441, 152);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Commands";
            // 
            // txtCommandsUsed
            // 
            this.txtCommandsUsed.BackColor = System.Drawing.Color.White;
            this.txtCommandsUsed.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCommandsUsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommandsUsed.Location = new System.Drawing.Point(6, 19);
            this.txtCommandsUsed.Multiline = true;
            this.txtCommandsUsed.Name = "txtCommandsUsed";
            this.txtCommandsUsed.ReadOnly = true;
            this.txtCommandsUsed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommandsUsed.Size = new System.Drawing.Size(429, 127);
            this.txtCommandsUsed.TabIndex = 39;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(655, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(42, 22);
            this.button4.TabIndex = 42;
            this.button4.Text = "Join";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(256, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 286);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Log";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(6, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(429, 261);
            this.txtLog.TabIndex = 1;
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtUrl.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(256, 6);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(396, 20);
            this.txtUrl.TabIndex = 25;
            // 
            // txtHost
            // 
            this.txtHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHost.Location = new System.Drawing.Point(130, 453);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(120, 20);
            this.txtHost.TabIndex = 28;
            this.txtHost.Text = "Alive";
            this.txtHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHost.TextChanged += new System.EventHandler(this.txtHost_TextChanged);
            this.txtHost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommands_KeyDown);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.groupBox9);
            this.tabPage7.Controls.Add(this.groupBox7);
            this.tabPage7.Controls.Add(this.groupBox8);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(703, 482);
            this.tabPage7.TabIndex = 7;
            this.tabPage7.Text = "Chat";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label5);
            this.groupBox9.Controls.Add(this.txtGlobalMessage);
            this.groupBox9.Controls.Add(this.txtGlobalChat);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(6, 329);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(689, 147);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "GlobalChat";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Message: ";
            // 
            // txtGlobalMessage
            // 
            this.txtGlobalMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGlobalMessage.Location = new System.Drawing.Point(73, 121);
            this.txtGlobalMessage.Name = "txtGlobalMessage";
            this.txtGlobalMessage.Size = new System.Drawing.Size(610, 20);
            this.txtGlobalMessage.TabIndex = 3;
            this.txtGlobalMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGlobalMessage_KeyDown);
            // 
            // txtGlobalChat
            // 
            this.txtGlobalChat.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtGlobalChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGlobalChat.Location = new System.Drawing.Point(6, 19);
            this.txtGlobalChat.Multiline = true;
            this.txtGlobalChat.Name = "txtGlobalChat";
            this.txtGlobalChat.ReadOnly = true;
            this.txtGlobalChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGlobalChat.Size = new System.Drawing.Size(677, 96);
            this.txtGlobalChat.TabIndex = 2;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.txtOpMessage);
            this.groupBox7.Controls.Add(this.txtOpChat);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(6, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(689, 147);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "OpChat";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Message: ";
            // 
            // txtOpMessage
            // 
            this.txtOpMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpMessage.Location = new System.Drawing.Point(73, 121);
            this.txtOpMessage.Name = "txtOpMessage";
            this.txtOpMessage.Size = new System.Drawing.Size(610, 20);
            this.txtOpMessage.TabIndex = 1;
            this.txtOpMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpMessage_KeyDown);
            // 
            // txtOpChat
            // 
            this.txtOpChat.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtOpChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpChat.Location = new System.Drawing.Point(6, 19);
            this.txtOpChat.Multiline = true;
            this.txtOpChat.Name = "txtOpChat";
            this.txtOpChat.ReadOnly = true;
            this.txtOpChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOpChat.Size = new System.Drawing.Size(677, 96);
            this.txtOpChat.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.txtAdminMessage);
            this.groupBox8.Controls.Add(this.txtAdminChat);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(6, 166);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(689, 147);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "AdminChat";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Message: ";
            // 
            // txtAdminMessage
            // 
            this.txtAdminMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdminMessage.Location = new System.Drawing.Point(73, 121);
            this.txtAdminMessage.Name = "txtAdminMessage";
            this.txtAdminMessage.Size = new System.Drawing.Size(610, 20);
            this.txtAdminMessage.TabIndex = 2;
            this.txtAdminMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAdminMessage_KeyDown);
            // 
            // txtAdminChat
            // 
            this.txtAdminChat.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtAdminChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdminChat.Location = new System.Drawing.Point(6, 19);
            this.txtAdminChat.Multiline = true;
            this.txtAdminChat.Name = "txtAdminChat";
            this.txtAdminChat.ReadOnly = true;
            this.txtAdminChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAdminChat.Size = new System.Drawing.Size(677, 96);
            this.txtAdminChat.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.txtChangelog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(703, 482);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Changelog";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(7, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Update Changelog";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // txtChangelog
            // 
            this.txtChangelog.BackColor = System.Drawing.Color.White;
            this.txtChangelog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtChangelog.Location = new System.Drawing.Point(7, 35);
            this.txtChangelog.Multiline = true;
            this.txtChangelog.Name = "txtChangelog";
            this.txtChangelog.ReadOnly = true;
            this.txtChangelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChangelog.Size = new System.Drawing.Size(690, 441);
            this.txtChangelog.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(703, 482);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "System/Errors";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtErrors);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(372, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(325, 466);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Errors";
            // 
            // txtErrors
            // 
            this.txtErrors.BackColor = System.Drawing.Color.White;
            this.txtErrors.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErrors.Location = new System.Drawing.Point(6, 19);
            this.txtErrors.Multiline = true;
            this.txtErrors.Name = "txtErrors";
            this.txtErrors.ReadOnly = true;
            this.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrors.Size = new System.Drawing.Size(313, 441);
            this.txtErrors.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSystem);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 466);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "System Logs";
            // 
            // txtSystem
            // 
            this.txtSystem.BackColor = System.Drawing.Color.White;
            this.txtSystem.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSystem.Location = new System.Drawing.Point(6, 19);
            this.txtSystem.Multiline = true;
            this.txtSystem.Name = "txtSystem";
            this.txtSystem.ReadOnly = true;
            this.txtSystem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystem.Size = new System.Drawing.Size(313, 441);
            this.txtSystem.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtPName);
            this.tabPage5.Controls.Add(this.groupBox11);
            this.tabPage5.Controls.Add(this.liClients);
            this.tabPage5.Controls.Add(this.dgvPlayerStats);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(703, 482);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Players";
            // 
            // txtPName
            // 
            this.txtPName.Location = new System.Drawing.Point(6, 247);
            this.txtPName.Name = "txtPName";
            this.txtPName.ReadOnly = true;
            this.txtPName.Size = new System.Drawing.Size(173, 20);
            this.txtPName.TabIndex = 26;
            this.txtPName.Text = "[Selected Player]";
            this.txtPName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.cmbIpBans);
            this.groupBox11.Controls.Add(this.button14);
            this.groupBox11.Controls.Add(this.button10);
            this.groupBox11.Controls.Add(this.cmbColors);
            this.groupBox11.Controls.Add(this.btnColor);
            this.groupBox11.Controls.Add(this.button5);
            this.groupBox11.Controls.Add(this.btnDemote);
            this.groupBox11.Controls.Add(this.btnPromote);
            this.groupBox11.Controls.Add(this.txtGive);
            this.groupBox11.Controls.Add(this.btnGive);
            this.groupBox11.Controls.Add(this.txtNickName);
            this.groupBox11.Controls.Add(this.btnNick);
            this.groupBox11.Controls.Add(this.cmbRanks);
            this.groupBox11.Controls.Add(this.btnRank);
            this.groupBox11.Controls.Add(this.txtTitle);
            this.groupBox11.Controls.Add(this.txtKickReason);
            this.groupBox11.Controls.Add(this.btnKick);
            this.groupBox11.Controls.Add(this.btnTitle);
            this.groupBox11.Controls.Add(this.button21);
            this.groupBox11.Controls.Add(this.button20);
            this.groupBox11.Controls.Add(this.button19);
            this.groupBox11.Controls.Add(this.button18);
            this.groupBox11.Controls.Add(this.button17);
            this.groupBox11.Controls.Add(this.button16);
            this.groupBox11.Controls.Add(this.cmbMaps);
            this.groupBox11.Controls.Add(this.btnMap);
            this.groupBox11.Controls.Add(this.btnBan);
            this.groupBox11.Controls.Add(this.btnJail);
            this.groupBox11.Controls.Add(this.btnRules);
            this.groupBox11.Controls.Add(this.btnMute);
            this.groupBox11.Location = new System.Drawing.Point(185, 247);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(510, 227);
            this.groupBox11.TabIndex = 25;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Remote Controls";
            // 
            // cmbIpBans
            // 
            this.cmbIpBans.FormattingEnabled = true;
            this.cmbIpBans.Location = new System.Drawing.Point(343, 78);
            this.cmbIpBans.Name = "cmbIpBans";
            this.cmbIpBans.Size = new System.Drawing.Size(160, 21);
            this.cmbIpBans.TabIndex = 51;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(263, 77);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 50;
            this.button14.Text = "IP Unban";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click_1);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(263, 106);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 49;
            this.button10.Text = "Trust";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // cmbColors
            // 
            this.cmbColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColors.FormattingEnabled = true;
            this.cmbColors.Items.AddRange(new object[] {
            "Black",
            "Navy",
            "Green",
            "Teal",
            "Maroon",
            "Purple",
            "Gold",
            "Silver",
            "Gray",
            "Blue",
            "Lime",
            "Aqua",
            "Red",
            "Pink",
            "Yellow",
            "White"});
            this.cmbColors.Location = new System.Drawing.Point(428, 107);
            this.cmbColors.Name = "cmbColors";
            this.cmbColors.Size = new System.Drawing.Size(75, 21);
            this.cmbColors.TabIndex = 48;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(343, 106);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(75, 23);
            this.btnColor.TabIndex = 47;
            this.btnColor.Text = "Color:";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(343, 135);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 46;
            this.button5.Text = "IP Ban";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnDemote
            // 
            this.btnDemote.BackColor = System.Drawing.Color.Red;
            this.btnDemote.Location = new System.Drawing.Point(263, 164);
            this.btnDemote.Name = "btnDemote";
            this.btnDemote.Size = new System.Drawing.Size(75, 23);
            this.btnDemote.TabIndex = 26;
            this.btnDemote.Text = "Demote";
            this.btnDemote.UseVisualStyleBackColor = false;
            this.btnDemote.Click += new System.EventHandler(this.btnDemote_Click);
            // 
            // btnPromote
            // 
            this.btnPromote.BackColor = System.Drawing.Color.Lime;
            this.btnPromote.Location = new System.Drawing.Point(263, 135);
            this.btnPromote.Name = "btnPromote";
            this.btnPromote.Size = new System.Drawing.Size(75, 23);
            this.btnPromote.TabIndex = 25;
            this.btnPromote.Text = "Promote";
            this.btnPromote.UseVisualStyleBackColor = false;
            this.btnPromote.Click += new System.EventHandler(this.btnPromote_Click);
            // 
            // txtGive
            // 
            this.txtGive.Location = new System.Drawing.Point(343, 50);
            this.txtGive.Name = "txtGive";
            this.txtGive.Size = new System.Drawing.Size(160, 20);
            this.txtGive.TabIndex = 28;
            this.txtGive.Text = "Enter Amount";
            // 
            // btnGive
            // 
            this.btnGive.Location = new System.Drawing.Point(263, 48);
            this.btnGive.Name = "btnGive";
            this.btnGive.Size = new System.Drawing.Size(75, 23);
            this.btnGive.TabIndex = 27;
            this.btnGive.Text = "Give";
            this.btnGive.UseVisualStyleBackColor = true;
            this.btnGive.Click += new System.EventHandler(this.btnGive_Click);
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(343, 21);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(160, 20);
            this.txtNickName.TabIndex = 21;
            this.txtNickName.Text = "Enter Nick";
            // 
            // btnNick
            // 
            this.btnNick.Location = new System.Drawing.Point(263, 19);
            this.btnNick.Name = "btnNick";
            this.btnNick.Size = new System.Drawing.Size(75, 23);
            this.btnNick.TabIndex = 20;
            this.btnNick.Text = "Nick";
            this.btnNick.UseVisualStyleBackColor = true;
            this.btnNick.Click += new System.EventHandler(this.btnNick_Click);
            // 
            // cmbRanks
            // 
            this.cmbRanks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRanks.FormattingEnabled = true;
            this.cmbRanks.Location = new System.Drawing.Point(91, 195);
            this.cmbRanks.Name = "cmbRanks";
            this.cmbRanks.Size = new System.Drawing.Size(160, 21);
            this.cmbRanks.TabIndex = 30;
            // 
            // btnRank
            // 
            this.btnRank.Location = new System.Drawing.Point(6, 193);
            this.btnRank.Name = "btnRank";
            this.btnRank.Size = new System.Drawing.Size(75, 23);
            this.btnRank.TabIndex = 29;
            this.btnRank.Text = "Rank:";
            this.btnRank.UseVisualStyleBackColor = true;
            this.btnRank.Click += new System.EventHandler(this.btnRank_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(91, 166);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(160, 20);
            this.txtTitle.TabIndex = 37;
            this.txtTitle.Text = "Title";
            // 
            // txtKickReason
            // 
            this.txtKickReason.Location = new System.Drawing.Point(91, 137);
            this.txtKickReason.Name = "txtKickReason";
            this.txtKickReason.Size = new System.Drawing.Size(160, 20);
            this.txtKickReason.TabIndex = 17;
            this.txtKickReason.Text = "Console just trolled you";
            // 
            // btnKick
            // 
            this.btnKick.Location = new System.Drawing.Point(6, 135);
            this.btnKick.Name = "btnKick";
            this.btnKick.Size = new System.Drawing.Size(75, 23);
            this.btnKick.TabIndex = 16;
            this.btnKick.Text = "Kick";
            this.btnKick.UseVisualStyleBackColor = true;
            this.btnKick.Click += new System.EventHandler(this.btnKick_Click);
            // 
            // btnTitle
            // 
            this.btnTitle.Location = new System.Drawing.Point(6, 164);
            this.btnTitle.Name = "btnTitle";
            this.btnTitle.Size = new System.Drawing.Size(75, 23);
            this.btnTitle.TabIndex = 31;
            this.btnTitle.Text = "Title";
            this.btnTitle.UseVisualStyleBackColor = true;
            this.btnTitle.Click += new System.EventHandler(this.btnTitle_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(176, 106);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(75, 23);
            this.button21.TabIndex = 44;
            this.button21.Text = "Hide";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(91, 106);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(75, 23);
            this.button20.TabIndex = 43;
            this.button20.Text = "Slap";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(176, 77);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(75, 23);
            this.button19.TabIndex = 42;
            this.button19.Text = "Kill";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(91, 77);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(75, 23);
            this.button18.TabIndex = 41;
            this.button18.Text = "Voice";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(6, 106);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 23);
            this.button17.TabIndex = 40;
            this.button17.Text = "Freeze";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(6, 77);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 39;
            this.button16.Text = "Joker";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // cmbMaps
            // 
            this.cmbMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaps.FormattingEnabled = true;
            this.cmbMaps.Location = new System.Drawing.Point(91, 50);
            this.cmbMaps.Name = "cmbMaps";
            this.cmbMaps.Size = new System.Drawing.Size(160, 21);
            this.cmbMaps.TabIndex = 38;
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(6, 48);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(75, 23);
            this.btnMap.TabIndex = 37;
            this.btnMap.Text = "Map:";
            this.btnMap.UseVisualStyleBackColor = true;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // btnBan
            // 
            this.btnBan.BackColor = System.Drawing.Color.Transparent;
            this.btnBan.Location = new System.Drawing.Point(428, 135);
            this.btnBan.Name = "btnBan";
            this.btnBan.Size = new System.Drawing.Size(75, 23);
            this.btnBan.TabIndex = 18;
            this.btnBan.Text = "Ban";
            this.btnBan.UseVisualStyleBackColor = false;
            this.btnBan.Click += new System.EventHandler(this.btnBan_Click);
            // 
            // btnJail
            // 
            this.btnJail.Location = new System.Drawing.Point(6, 19);
            this.btnJail.Name = "btnJail";
            this.btnJail.Size = new System.Drawing.Size(75, 23);
            this.btnJail.TabIndex = 19;
            this.btnJail.Text = "Jail";
            this.btnJail.UseVisualStyleBackColor = true;
            this.btnJail.Click += new System.EventHandler(this.btnJail_Click);
            // 
            // btnRules
            // 
            this.btnRules.Location = new System.Drawing.Point(176, 19);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(75, 23);
            this.btnRules.TabIndex = 23;
            this.btnRules.Text = "Rules";
            this.btnRules.UseVisualStyleBackColor = true;
            this.btnRules.Click += new System.EventHandler(this.btnRules_Click);
            // 
            // btnMute
            // 
            this.btnMute.Location = new System.Drawing.Point(91, 19);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(75, 23);
            this.btnMute.TabIndex = 22;
            this.btnMute.Text = "Mute";
            this.btnMute.UseVisualStyleBackColor = true;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // liClients
            // 
            this.liClients.FormattingEnabled = true;
            this.liClients.Location = new System.Drawing.Point(6, 273);
            this.liClients.Name = "liClients";
            this.liClients.Size = new System.Drawing.Size(173, 199);
            this.liClients.TabIndex = 15;
            this.liClients.SelectedIndexChanged += new System.EventHandler(this.liClients_SelectedIndexChanged);
            // 
            // dgvPlayerStats
            // 
            this.dgvPlayerStats.AllowUserToAddRows = false;
            this.dgvPlayerStats.AllowUserToDeleteRows = false;
            this.dgvPlayerStats.AllowUserToResizeRows = false;
            this.dgvPlayerStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlayerStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayerStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.UserTitle,
            this.UserRank,
            this.UserMoney,
            this.UserMap,
            this.UserTotalLogins,
            this.UserBlockChanges});
            this.dgvPlayerStats.Location = new System.Drawing.Point(6, 6);
            this.dgvPlayerStats.Name = "dgvPlayerStats";
            this.dgvPlayerStats.RowHeadersVisible = false;
            this.dgvPlayerStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlayerStats.Size = new System.Drawing.Size(689, 235);
            this.dgvPlayerStats.TabIndex = 14;
            // 
            // Username
            // 
            this.Username.HeaderText = "Name";
            this.Username.Name = "Username";
            this.Username.ReadOnly = true;
            // 
            // UserTitle
            // 
            this.UserTitle.HeaderText = "Title";
            this.UserTitle.Name = "UserTitle";
            this.UserTitle.ReadOnly = true;
            // 
            // UserRank
            // 
            this.UserRank.HeaderText = "Rank";
            this.UserRank.Name = "UserRank";
            this.UserRank.ReadOnly = true;
            // 
            // UserMoney
            // 
            this.UserMoney.HeaderText = "Money";
            this.UserMoney.Name = "UserMoney";
            this.UserMoney.ReadOnly = true;
            // 
            // UserMap
            // 
            this.UserMap.HeaderText = "Map";
            this.UserMap.Name = "UserMap";
            this.UserMap.ReadOnly = true;
            // 
            // UserTotalLogins
            // 
            this.UserTotalLogins.HeaderText = "Total Logins";
            this.UserTotalLogins.Name = "UserTotalLogins";
            this.UserTotalLogins.ReadOnly = true;
            // 
            // UserBlockChanges
            // 
            this.UserBlockChanges.HeaderText = "Blocks";
            this.UserBlockChanges.Name = "UserBlockChanges";
            this.UserBlockChanges.ReadOnly = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox15);
            this.tabPage6.Controls.Add(this.groupBox14);
            this.tabPage6.Controls.Add(this.groupBox13);
            this.tabPage6.Controls.Add(this.groupBox12);
            this.tabPage6.Controls.Add(this.label6);
            this.tabPage6.Controls.Add(this.groupBox10);
            this.tabPage6.Controls.Add(this.dataGridView1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(703, 482);
            this.tabPage6.TabIndex = 8;
            this.tabPage6.Text = "Maps";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.map1);
            this.groupBox15.Location = new System.Drawing.Point(181, 6);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(275, 285);
            this.groupBox15.TabIndex = 30;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Map";
            // 
            // map1
            // 
            this.map1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.map1.Location = new System.Drawing.Point(10, 20);
            this.map1.Name = "map1";
            this.map1.Size = new System.Drawing.Size(256, 256);
            this.map1.TabIndex = 0;
            this.map1.TabStop = false;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.btnShow);
            this.groupBox14.Controls.Add(this.LoadedMaps);
            this.groupBox14.Controls.Add(this.btnUnload);
            this.groupBox14.Controls.Add(this.button15);
            this.groupBox14.Location = new System.Drawing.Point(6, 6);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(169, 242);
            this.groupBox14.TabIndex = 29;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Loaded Maps";
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(6, 213);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(156, 23);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "Show map";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // LoadedMaps
            // 
            this.LoadedMaps.FormattingEnabled = true;
            this.LoadedMaps.Location = new System.Drawing.Point(6, 19);
            this.LoadedMaps.Name = "LoadedMaps";
            this.LoadedMaps.Size = new System.Drawing.Size(156, 160);
            this.LoadedMaps.TabIndex = 9;
            // 
            // btnUnload
            // 
            this.btnUnload.Location = new System.Drawing.Point(6, 189);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(75, 23);
            this.btnUnload.TabIndex = 11;
            this.btnUnload.Text = "Unload";
            this.btnUnload.UseVisualStyleBackColor = true;
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(87, 189);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 12;
            this.button15.Text = "Delete";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.UnloadedMaps);
            this.groupBox13.Controls.Add(this.button12);
            this.groupBox13.Controls.Add(this.button13);
            this.groupBox13.Location = new System.Drawing.Point(6, 254);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(169, 222);
            this.groupBox13.TabIndex = 28;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Unloaded Maps";
            // 
            // UnloadedMaps
            // 
            this.UnloadedMaps.FormattingEnabled = true;
            this.UnloadedMaps.Location = new System.Drawing.Point(6, 19);
            this.UnloadedMaps.Name = "UnloadedMaps";
            this.UnloadedMaps.Size = new System.Drawing.Size(156, 160);
            this.UnloadedMaps.TabIndex = 4;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(6, 189);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 6;
            this.button12.Text = "Load";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(87, 189);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 7;
            this.button13.Text = "Delete";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.mapStyle);
            this.groupBox12.Controls.Add(this.txtLayer);
            this.groupBox12.Controls.Add(this.btnMinus);
            this.groupBox12.Controls.Add(this.btnPlus);
            this.groupBox12.Controls.Add(this.btnSave);
            this.groupBox12.Controls.Add(this.label11);
            this.groupBox12.Controls.Add(this.label7);
            this.groupBox12.Controls.Add(this.btnGenerate);
            this.groupBox12.Controls.Add(this.label8);
            this.groupBox12.Controls.Add(this.txtMapName);
            this.groupBox12.Controls.Add(this.label9);
            this.groupBox12.Controls.Add(this.cmbMapType);
            this.groupBox12.Controls.Add(this.z);
            this.groupBox12.Controls.Add(this.x);
            this.groupBox12.Controls.Add(this.y);
            this.groupBox12.Location = new System.Drawing.Point(462, 6);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(233, 165);
            this.groupBox12.TabIndex = 26;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Create Level";
            // 
            // mapStyle
            // 
            this.mapStyle.FormattingEnabled = true;
            this.mapStyle.Items.AddRange(new object[] {
            "Standart",
            "Height mask",
            "Layer"});
            this.mapStyle.Location = new System.Drawing.Point(124, 138);
            this.mapStyle.Name = "mapStyle";
            this.mapStyle.Size = new System.Drawing.Size(100, 21);
            this.mapStyle.TabIndex = 8;
            this.mapStyle.Text = "Standart";
            this.mapStyle.SelectionChangeCommitted += new System.EventHandler(this.mapStyle_SelectionChangeCommitted);
            // 
            // txtLayer
            // 
            this.txtLayer.Location = new System.Drawing.Point(90, 114);
            this.txtLayer.Name = "txtLayer";
            this.txtLayer.Size = new System.Drawing.Size(25, 20);
            this.txtLayer.TabIndex = 30;
            this.txtLayer.Text = "30";
            this.txtLayer.TextChanged += new System.EventHandler(this.txtLayer_ChangedText);
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(90, 138);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(25, 20);
            this.btnMinus.TabIndex = 29;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(90, 88);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(25, 20);
            this.btnPlus.TabIndex = 28;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(9, 126);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Save New Map";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Height-z";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(9, 88);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 32);
            this.btnGenerate.TabIndex = 15;
            this.btnGenerate.Text = "Generate New Map";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(121, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Depth-y";
            // 
            // txtMapName
            // 
            this.txtMapName.Location = new System.Drawing.Point(9, 35);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.Size = new System.Drawing.Size(215, 20);
            this.txtMapName.TabIndex = 16;
            this.txtMapName.Text = "FreebuildLevel";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(119, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Width-x";
            // 
            // cmbMapType
            // 
            this.cmbMapType.FormattingEnabled = true;
            this.cmbMapType.Items.AddRange(new object[] {
            "flat",
            "island",
            "mountains",
            "forest",
            "ocean",
            "pixel",
            "desert"});
            this.cmbMapType.Location = new System.Drawing.Point(9, 61);
            this.cmbMapType.Name = "cmbMapType";
            this.cmbMapType.Size = new System.Drawing.Size(106, 21);
            this.cmbMapType.TabIndex = 18;
            this.cmbMapType.Text = "flat";
            // 
            // z
            // 
            this.z.FormattingEnabled = true;
            this.z.Items.AddRange(new object[] {
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
            this.z.Location = new System.Drawing.Point(168, 115);
            this.z.Name = "z";
            this.z.Size = new System.Drawing.Size(56, 21);
            this.z.TabIndex = 22;
            this.z.Text = "32";
            // 
            // x
            // 
            this.x.FormattingEnabled = true;
            this.x.Items.AddRange(new object[] {
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
            this.x.Location = new System.Drawing.Point(168, 61);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(56, 21);
            this.x.TabIndex = 21;
            this.x.Text = "32";
            // 
            // y
            // 
            this.y.FormattingEnabled = true;
            this.y.Items.AddRange(new object[] {
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
            this.y.Location = new System.Drawing.Point(168, 88);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(56, 21);
            this.y.TabIndex = 20;
            this.y.Text = "32";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Live overview of Maps:";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.button11);
            this.groupBox10.Controls.Add(this.button9);
            this.groupBox10.Controls.Add(this.button8);
            this.groupBox10.Controls.Add(this.button7);
            this.groupBox10.Controls.Add(this.button6);
            this.groupBox10.Location = new System.Drawing.Point(462, 177);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(233, 127);
            this.groupBox10.TabIndex = 2;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Physics";
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Red;
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(6, 15);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(97, 29);
            this.button11.TabIndex = 7;
            this.button11.Text = "OFF";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Transparent;
            this.button9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button9.Location = new System.Drawing.Point(130, 15);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(97, 29);
            this.button9.TabIndex = 6;
            this.button9.Text = "Instant";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.Location = new System.Drawing.Point(130, 54);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(97, 29);
            this.button8.TabIndex = 5;
            this.button8.Text = "Hardcore";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.Location = new System.Drawing.Point(6, 92);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(97, 29);
            this.button7.TabIndex = 4;
            this.button7.Text = "Advanced";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.Location = new System.Drawing.Point(6, 54);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(97, 29);
            this.button6.TabIndex = 3;
            this.button6.Text = "Normal";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MapName,
            this.MapPlayers,
            this.MapPhysics,
            this.MapPerbuild,
            this.MapPervisit,
            this.MapOwner,
            this.Dimensionx,
            this.Dimensiony,
            this.Dimensionz});
            this.dataGridView1.Location = new System.Drawing.Point(183, 310);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(512, 166);
            this.dataGridView1.TabIndex = 1;
            // 
            // MapName
            // 
            this.MapName.HeaderText = "Name";
            this.MapName.Name = "MapName";
            this.MapName.ReadOnly = true;
            // 
            // MapPlayers
            // 
            this.MapPlayers.HeaderText = "Players";
            this.MapPlayers.Name = "MapPlayers";
            this.MapPlayers.ReadOnly = true;
            // 
            // MapPhysics
            // 
            this.MapPhysics.HeaderText = "Physics";
            this.MapPhysics.Name = "MapPhysics";
            this.MapPhysics.ReadOnly = true;
            // 
            // MapPerbuild
            // 
            this.MapPerbuild.HeaderText = "Perbuild";
            this.MapPerbuild.Name = "MapPerbuild";
            this.MapPerbuild.ReadOnly = true;
            // 
            // MapPervisit
            // 
            this.MapPervisit.HeaderText = "Pervisit";
            this.MapPervisit.Name = "MapPervisit";
            this.MapPervisit.ReadOnly = true;
            // 
            // MapOwner
            // 
            this.MapOwner.HeaderText = "Owner";
            this.MapOwner.Name = "MapOwner";
            this.MapOwner.ReadOnly = true;
            // 
            // Dimensionx
            // 
            this.Dimensionx.HeaderText = "X";
            this.Dimensionx.Name = "Dimensionx";
            this.Dimensionx.ReadOnly = true;
            // 
            // Dimensiony
            // 
            this.Dimensiony.HeaderText = "Y";
            this.Dimensiony.Name = "Dimensiony";
            this.Dimensiony.ReadOnly = true;
            // 
            // Dimensionz
            // 
            this.Dimensionz.HeaderText = "Z";
            this.Dimensionz.Name = "Dimensionz";
            this.Dimensionz.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(486, 527);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Command:";
            // 
            // btnExtra
            // 
            this.btnExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnExtra.Location = new System.Drawing.Point(675, 522);
            this.btnExtra.Name = "btnExtra";
            this.btnExtra.Size = new System.Drawing.Size(28, 22);
            this.btnExtra.TabIndex = 35;
            this.btnExtra.Text = ">>";
            this.btnExtra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExtra.UseVisualStyleBackColor = true;
            this.btnExtra.Click += new System.EventHandler(this.btnExtra_Click_1);
            // 
            // txtCommands
            // 
            this.txtCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommands.Location = new System.Drawing.Point(549, 524);
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(120, 20);
            this.txtCommands.TabIndex = 28;
            this.txtCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommands_KeyDown);
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(41, 524);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(439, 20);
            this.txtInput.TabIndex = 27;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 527);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Chat:";
            // 
            // tmrRestart
            // 
            this.tmrRestart.Enabled = true;
            this.tmrRestart.Interval = 1000;
            // 
            // iconContext
            // 
            this.iconContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConsole,
            this.shutdownServer});
            this.iconContext.Name = "iconContext";
            this.iconContext.Size = new System.Drawing.Size(158, 48);
            // 
            // openConsole
            // 
            this.openConsole.Name = "openConsole";
            this.openConsole.Size = new System.Drawing.Size(157, 22);
            this.openConsole.Text = "Open Console";
            this.openConsole.Click += new System.EventHandler(this.openConsole_Click);
            // 
            // shutdownServer
            // 
            this.shutdownServer.Name = "shutdownServer";
            this.shutdownServer.Size = new System.Drawing.Size(157, 22);
            this.shutdownServer.Text = "Shutdown Server";
            this.shutdownServer.Click += new System.EventHandler(this.shutdownServer_Click);
            // 
            // btnProperties
            // 
            this.btnProperties.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProperties.Location = new System.Drawing.Point(457, 4);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(80, 23);
            this.btnProperties.TabIndex = 34;
            this.btnProperties.Text = "Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click_1);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(629, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 23);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(543, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 23);
            this.button2.TabIndex = 37;
            this.button2.Text = "Restart";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 550);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.btnExtra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.txtCommands);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.Load += new System.EventHandler(this.Window_Load);
            this.Resize += new System.EventHandler(this.Window_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaps)).EndInit();
            this.mapsStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).EndInit();
            this.playerStrip.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayerStats)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.map1)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.iconContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btnExtra;
        private Label label2;
        private TextBox txtCommands;
        private TextBox txtInput;
        private Label label1;
        private TextBox txtUrl;
        private TabPage tabPage2;
        private TextBox txtChangelog;
        private Timer tmrRestart;
        private Button btnProperties;
        private Button btnClose;
        private ContextMenuStrip iconContext;
        private ToolStripMenuItem openConsole;
        private ToolStripMenuItem shutdownServer;
        private TabPage tabPage4;
        private TextBox txtSystem;
        private TextBox txtHost;
        private DataGridView dgvPlayers;
        private DataGridViewTextBoxColumn User;
        private DataGridViewTextBoxColumn Rank;
        private DataGridViewTextBoxColumn Map;
        private Button button4;
        private GroupBox groupBox1;
        private TextBox txtLog;
        private Button button2;
        private DataGridView dgvMaps;
        private DataGridViewTextBoxColumn Level;
        private DataGridViewTextBoxColumn Players;
        private DataGridViewTextBoxColumn Physics;
        private DataGridViewTextBoxColumn Perbuild;
        private GroupBox groupBox3;
        private TextBox txtCommandsUsed;
        private GroupBox groupBox4;
        private TextBox txtErrors;
        private GroupBox groupBox2;
        private Button btnUpdate;
        private TabPage tabPage5;
        private TabPage tabPage7;
        private GroupBox groupBox9;
        private GroupBox groupBox7;
        private TextBox txtOpChat;
        private GroupBox groupBox8;
        private TextBox txtAdminChat;
        public TextBox txtGlobalChat;
        private TextBox txtGlobalMessage;
        private TextBox txtOpMessage;
        private TextBox txtAdminMessage;
        private Label label5;
        private Label label3;
        private Label label4;
        private Button button3;
        private TabPage tabPage6;
        private DataGridView dataGridView1;
        private GroupBox groupBox10;
        private Button button13;
        private Button button12;
        private ListBox UnloadedMaps;
        private ListBox LoadedMaps;
        private Button button15;
        private Button btnUnload;
        private DataGridView dgvPlayerStats;
        private DataGridViewTextBoxColumn Username;
        private DataGridViewTextBoxColumn UserTitle;
        private DataGridViewTextBoxColumn UserRank;
        private DataGridViewTextBoxColumn UserMoney;
        private DataGridViewTextBoxColumn UserMap;
        private DataGridViewTextBoxColumn UserTotalLogins;
        private DataGridViewTextBoxColumn UserBlockChanges;
        private DataGridViewTextBoxColumn MapName;
        private DataGridViewTextBoxColumn MapPlayers;
        private DataGridViewTextBoxColumn MapPhysics;
        private DataGridViewTextBoxColumn MapPerbuild;
        private DataGridViewTextBoxColumn MapPervisit;
        private DataGridViewTextBoxColumn MapOwner;
        private DataGridViewTextBoxColumn Dimensionx;
        private DataGridViewTextBoxColumn Dimensiony;
        private DataGridViewTextBoxColumn Dimensionz;
        private Button btnKick;
        private ListBox liClients;
        private TextBox txtKickReason;
        private GroupBox groupBox11;
        private Button btnBan;
        private Button btnJail;
        private Button btnRules;
        private Button btnNick;
        private Button btnMute;
        private TextBox txtNickName;
        private TextBox txtGive;
        private Button btnGive;
        private Button btnDemote;
        private Button btnPromote;
        private ComboBox cmbRanks;
        private Button btnRank;
        private TextBox txtTitle;
        private Button btnTitle;
        private ComboBox cmbMaps;
        private Button btnMap;
        private Button button21;
        private Button button20;
        private Button button19;
        private Button button18;
        private Button button17;
        private Button button16;
        private ContextMenuStrip playerStrip;
        private ToolStripMenuItem whoisToolStripMenuItem;
        private ToolStripMenuItem kickToolStripMenuItem;
        private ToolStripMenuItem banToolStripMenuItem;
        private ToolStripMenuItem voiceToolStripMenuItem;
        private ToolStripMenuItem clonesToolStripMenuItem;
        private ToolStripMenuItem promoteToolStripMenuItem;
        private ToolStripMenuItem demoteToolStripMenuItem;
        private ContextMenuStrip mapsStrip;
        private ToolStripMenuItem physicsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem unloadToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem finiteModeToolStripMenuItem;
        private ToolStripMenuItem animalAIToolStripMenuItem;
        private ToolStripMenuItem edgeWaterToolStripMenuItem;
        private ToolStripMenuItem growingGrassToolStripMenuItem;
        private ToolStripMenuItem survivalDeathToolStripMenuItem;
        private ToolStripMenuItem killerBlocksToolStripMenuItem;
        private ToolStripMenuItem rPChatToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private Label label6;
        private GroupBox groupBox12;
        private Label label11;
        private Label label7;
        private Button btnGenerate;
        private Label label8;
        private TextBox txtMapName;
        private Label label9;
        private ComboBox cmbMapType;
        private ComboBox z;
        private ComboBox x;
        private ComboBox y;
        private GroupBox groupBox14;
        private GroupBox groupBox13;
        private Button button5;
        private Button button14;
        private Button button10;
        private ComboBox cmbColors;
        private Button btnColor;
        private ComboBox cmbIpBans;
        private TextBox txtPName;
        private Button btnShow;
        private Button btnSave;
        private GroupBox groupBox15;
        private PictureBox map1;
        private Button button11;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button button6;
        private ComboBox mapStyle;
        private TextBox txtLayer;
        private Button btnMinus;
        private Button btnPlus;
    }
}