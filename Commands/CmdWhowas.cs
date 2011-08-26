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
using System.Data;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCLawl
{
    public class CmdWhowas : Command
    {
        public override string name { get { return "whowas"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdWhowas() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player pl = Player.Find(message); 
            if (pl != null && !pl.hidden)
            { 
                Player.SendMessage(p, pl.color + pl.name + Server.DefaultColor + " is online, using /whois instead."); 
                Command.all.Find("whois").Use(p, message);
                return; 
            }

            if (message.IndexOf("'") != -1) { Player.SendMessage(p, "Cannot parse request."); return; }

            string FoundRank = Group.findPlayer(message.ToLower());

            DataTable playerDb = MySQL.fillData("SELECT * FROM Players WHERE Name='" + message + "'");
            if (playerDb.Rows.Count == 0) { Player.SendMessage(p, Group.Find(FoundRank).color + message + Server.DefaultColor + " has the rank of " + Group.Find(FoundRank).color + FoundRank); return; }

            Player.SendMessage(p, Group.Find(FoundRank).color + playerDb.Rows[0]["Title"] + " " + message + Server.DefaultColor + " has :");
            Player.SendMessage(p, "> > the rank of \"" + Group.Find(FoundRank).color + FoundRank);
            try
            {
                if (!Group.Find("Nobody").commands.Contains("pay") && !Group.Find("Nobody").commands.Contains("give") && !Group.Find("Nobody").commands.Contains("take")) Player.SendMessage(p, "> > &a" + playerDb.Rows[0]["Money"] + Server.DefaultColor + " " + Server.moneys);
            }
            catch { }
            Player.SendMessage(p, "> > &cdied &a" + playerDb.Rows[0]["TotalDeaths"] + Server.DefaultColor + " times");
            Player.SendMessage(p, "> > &bmodified &a" + playerDb.Rows[0]["totalBlocks"] + Server.DefaultColor + " blocks.");
            Player.SendMessage(p, "> > was last seen on &a" + playerDb.Rows[0]["LastLogin"]);
            Player.SendMessage(p, "> > first logged into the server on &a" + playerDb.Rows[0]["FirstLogin"]);
            Player.SendMessage(p, "> > logged in &a" + playerDb.Rows[0]["totalLogin"] + Server.DefaultColor + " times, &c" + playerDb.Rows[0]["totalKicked"] + Server.DefaultColor + " of which ended in a kick.");
            Player.SendMessage(p, "> > " + Awards.awardAmount(message) + " awards");

            bool skip = false;
            if (p != null) if (p.group.Permission <= LevelPermission.AdvBuilder) skip = true;

            if (!skip)
            {
                if (Server.bannedIP.Contains(playerDb.Rows[0]["IP"].ToString()))
                    playerDb.Rows[0]["IP"] = "&8" + playerDb.Rows[0]["IP"] + ", which is banned";
                Player.SendMessage(p, "> > the IP of " + playerDb.Rows[0]["IP"]);
                if (Server.useWhitelist)
                {
                    if (Server.whiteList.Contains(message.ToLower()))
                    {
                        Player.SendMessage(p, "> > Player is &fWhitelisted");
                    }
                }
                if (Server.devs.Contains(message.ToLower()))
                {
                    Player.SendMessage(p, Server.DefaultColor + "> > Player is a &9Developer");
                }
            }
            playerDb.Dispose();
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whowas <name> - Displays information about someone who left.");
        }
    }
}