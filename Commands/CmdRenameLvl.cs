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
using System.IO;
using System.Data;
using System.Collections.Generic;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCLawl
{
    public class CmdRenameLvl : Command
    {
        public override string name { get { return "renamelvl"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdRenameLvl() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1) { Help(p); return; }
            Level foundLevel = Level.Find(message.Split(' ')[0]);
            string newName = message.Split(' ')[1];

            if (File.Exists("levels/" + newName)) { Player.SendMessage(p, "Level already exists."); return; }
            if (foundLevel == Server.mainLevel) { Player.SendMessage(p, "Cannot rename the main level."); return; }
            if (foundLevel != null) foundLevel.Unload();

            try
            {
                File.Move("levels/" + foundLevel.name + ".lvl", "levels/" + newName + ".lvl");

                try
                {
                    File.Move("levels/level properties/" + foundLevel.name + ".properties", "levels/level properties/" + newName + ".properties");
                }
                catch { }
                try
                {
                    File.Move("levels/level properties/" + foundLevel.name, "levels/level properties/" + newName + ".properties");
                }
                catch { }

                MySQL.executeQuery("RENAME TABLE `Block" + foundLevel.name.ToLower() + "` TO `Block" + newName.ToLower() +
                    "`, `Portals" + foundLevel.name.ToLower() + "` TO `Portals" + newName.ToLower() +
                    "`, `Messages" + foundLevel.name.ToLower() + "` TO Messages" + newName.ToLower() +
                    ", `Zone" + foundLevel.name.ToLower() + "` TO `Zone" + newName.ToLower() + "`");

                Player.GlobalMessage("Renamed " + foundLevel.name + " to " + newName);
            }
            catch (Exception e) { Player.SendMessage(p, "Error when renaming."); Server.ErrorLog(e); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/renamelvl <level> <new name> - Renames <level> to <new name>");
            Player.SendMessage(p, "Portals going to <level> will be lost");
        }
    }
}