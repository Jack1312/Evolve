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

namespace MCLawl
{
    public class CmdLimit : Command
    {
        public override string name { get { return "limit"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdLimit() { }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length != 2) { Help(p); return; }
            int newLimit;
            try { newLimit = int.Parse(message.Split(' ')[1]); }
            catch { Player.SendMessage(p, "Invalid limit amount"); return; }
            if (newLimit < 1) { Player.SendMessage(p, "Cannot set below 1."); return; }

            Group foundGroup = Group.Find(message.Split(' ')[0]);
            if (foundGroup != null)
            {
                foundGroup.maxBlocks = newLimit;
                Player.GlobalChat(null, foundGroup.color + foundGroup.name + Server.DefaultColor + "'s building limits were set to &b" + newLimit, false);
                Group.saveGroups(Group.GroupList);
            }
            else
            {
                switch (message.Split(' ')[0].ToLower())
                {
                    case "rp":
                    case "restartphysics":
                        Server.rpLimit = newLimit;
                        Player.GlobalMessage("Custom /rp's limit was changed to &b" + newLimit.ToString());
                        break;
                    case "rpnorm":
                    case "rpnormal":
                        Server.rpNormLimit = newLimit;
                        Player.GlobalMessage("Normal /rp's limit was changed to &b" + newLimit.ToString());
                        break;

                    default:
                        Player.SendMessage(p, "No supported /limit");
                        break;
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/limit <type> <amount> - Sets the limit for <type>");
            Player.SendMessage(p, "<types> - " + Group.concatList(true, true) + ", RP, RPNormal");
        }
    }
}