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
    public class CmdDeleteLvl : Command
    {
        public override string name { get { return "deletelvl"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdDeleteLvl() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Level foundLevel = Level.Find(message);
            if (foundLevel != null) foundLevel.Unload();

            if (foundLevel == Server.mainLevel) { Player.SendMessage(p, "Cannot delete the main level."); return; }

            try
            {
                if (!Directory.Exists("levels/deleted")) Directory.CreateDirectory("levels/deleted");

                if (File.Exists("levels/" + message + ".lvl"))
                {
                    if (File.Exists("levels/deleted/" + message + ".lvl"))
                    {
                        int currentNum = 0;
                        while (File.Exists("levels/deleted/" + message + currentNum + ".lvl")) currentNum++;

                        File.Move("levels/" + message + ".lvl", "levels/deleted/" + message + currentNum + ".lvl");
                    }
                    else
                    {
                        File.Move("levels/" + message + ".lvl", "levels/deleted/" + message + ".lvl");
                    }
                    Player.SendMessage(p, "Created backup.");

                    try { File.Delete("levels/level properties/" + message + ".properties"); }
                    catch { }
                    try { File.Delete("levels/level properties/" + message); }
                    catch { }

                    MySQL.executeQuery("DROP TABLE `Block" + message + "`, `Portals" + message + "`, `Messages" + message + "`, `Zone" + message + "`");

                    Player.GlobalMessage("Level " + message + " was deleted.");
                }
                else
                {
                    Player.SendMessage(p, "Could not find specified level.");
                }
            }
            catch (Exception e) { Player.SendMessage(p, "Error when deleting."); Server.ErrorLog(e); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/deletelvl [map] - Completely deletes [map] (portals, MBs, everything");
            Player.SendMessage(p, "A backup of the map will be placed in the levels/deleted folder");
        }
    }
}