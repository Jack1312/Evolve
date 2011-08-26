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
    public class CmdPromote : Command
    {
        public override string name { get { return "promote"; } }
        public override string shortcut { get { return "pr"; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdPromote() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1) { Help(p); return; }
            Player who = Player.Find(message);
            string foundName;
            Group foundGroup;
            if (who == null)
            {
                foundName = message;
                foundGroup = Group.findPlayerGroup(message);
            }
            else
            {
                foundName = who.name;
                foundGroup = who.group;
            }

            Group nextGroup = null; bool nextOne = false;
            for (int i = 0; i < Group.GroupList.Count; i++)
            {
                Group grp = Group.GroupList[i];
                if (nextOne)
                {
                    if (grp.Permission >= LevelPermission.Nobody) break;
                    nextGroup = grp;
                    break;
                }
                if (grp == foundGroup)
                    nextOne = true;
            }

            if (nextGroup != null)
                Command.all.Find("setrank").Use(p, foundName + " " + nextGroup.name);
            else
                Player.SendMessage(p, "No higher ranks exist");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/promote <name> - Promotes <name> up a rank");
        }
    }
}