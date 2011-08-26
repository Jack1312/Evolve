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
using System.Data;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCLawl
{
    public class CmdMessageBlock : Command
    {
        public override string name { get { return "mb"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdMessageBlock() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            CatchPos cpos;
            cpos.message = "";

            try
            {
                switch (message.Split(' ')[0])
                {
                    case "air": cpos.type = Block.MsgAir; break;
                    case "water": cpos.type = Block.MsgWater; break;
                    case "lava": cpos.type = Block.MsgLava; break;
                    case "black": cpos.type = Block.MsgBlack; break;
                    case "white": cpos.type = Block.MsgWhite; break;
                    case "show": showMBs(p); return;
                    default: cpos.type = Block.MsgWhite; cpos.message = message; break;
                }
            }
            catch { cpos.type = Block.MsgWhite; cpos.message = message; }

            if (cpos.message == "") cpos.message = message.Substring(message.IndexOf(' ') + 1);
            p.blockchangeObject = cpos;

            Player.SendMessage(p, "Place where you wish the message block to go."); p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mb [block] [message] - Places a message in your next block.");
            Player.SendMessage(p, "Valid blocks: white, black, air, water, lava");
            Player.SendMessage(p, "/mb show shows or hides MBs");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            CatchPos cpos = (CatchPos)p.blockchangeObject;

            cpos.message = cpos.message.Replace("'", "\\'");

            DataTable Messages = MySQL.fillData("SELECT * FROM `Messages" + p.level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);
            Messages.Dispose();

            if (Messages.Rows.Count == 0)
            {
                MySQL.executeQuery("INSERT INTO `Messages" + p.level.name + "` (X, Y, Z, Message) VALUES (" + (int)x + ", " + (int)y + ", " + (int)z + ", '" + cpos.message + "')");
            }
            else
            {
                MySQL.executeQuery("UPDATE `Messages" + p.level.name + "` SET Message='" + cpos.message + "' WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);
            }

            Player.SendMessage(p, "Message block placed.");
            p.level.Blockchange(p, x, y, z, cpos.type);
            p.SendBlockchange(x, y, z, cpos.type);

            if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }

        struct CatchPos { public string message; public byte type; }

        public void showMBs(Player p)
        {
            p.showMBs = !p.showMBs;

            DataTable Messages = new DataTable("Messages");
            Messages = MySQL.fillData("SELECT * FROM `Messages" + p.level.name + "`");

            int i;

            if (p.showMBs)
            {
                for (i = 0; i < Messages.Rows.Count; i++)
                    p.SendBlockchange((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"], Block.MsgWhite);
                Player.SendMessage(p, "Now showing &a" + i.ToString() + Server.DefaultColor + " MBs.");
            }
            else
            {
                for (i = 0; i < Messages.Rows.Count; i++)
                    p.SendBlockchange((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"], p.level.GetTile((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"]));
                Player.SendMessage(p, "Now hiding MBs.");
            }
            Messages.Dispose();
        }
    }
}