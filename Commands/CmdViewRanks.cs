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
using System.Text;

namespace MCLawl
{
    public class CmdViewRanks : Command
    {
        public override string name { get { return "viewranks"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdViewRanks() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            Group foundGroup = Group.Find(message);
            if (foundGroup == null)
            {
                Player.SendMessage(p, "Could not find group");
                return;
            }


            string totalList = "";
            foreach (string s in foundGroup.playerList.All())
            {
                totalList += ", " + s;
            }

            if (totalList == "")
            {
                Player.SendMessage(p, "No one has the rank of " + foundGroup.color + foundGroup.name);
                return;
            }
            
            Player.SendMessage(p, "People with the rank of " + foundGroup.color + foundGroup.name + ":");
            Player.SendMessage(p, totalList.Remove(0, 2));
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/viewranks [rank] - Shows all users who have [rank]");
            Player.SendMessage(p, "Available ranks: " + Group.concatList());
        }
    }
}
