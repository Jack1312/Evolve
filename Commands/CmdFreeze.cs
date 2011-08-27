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

namespace MCLawl
{
    public class CmdFreeze : Command
    {
        public override string name { get { return "freeze"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdFreeze() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player who = Player.Find(message);
            if (who == null) { Player.SendMessage(p, "Could not find player."); return; }
            if (p != null)
            {
                if (who == p) { Player.SendMessage(p, "Cannot freeze yourself."); return; }
                if (who.group.Permission >= p.group.Permission) { Player.SendMessage(p, "Cannot freeze someone of equal or greater rank."); return; }
            }

            if (!who.frozen)
            {
                who.frozen = true;
                Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " has been &bfrozen.", false);
            }
            else
            {
                who.frozen = false;
                Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " has been &adefrosted.", false);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/freeze <name> - Stops <name> from moving until unfrozen.");
        }
    }
}