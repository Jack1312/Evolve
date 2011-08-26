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
    public class CmdTake : Command
    {
        public override string name { get { return "take"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdTake() { }

        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1) { Help(p); return; }
            if (message.Split(' ').Length != 2) { Help(p); return; }

            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player entered"); return; }
            if (who == p) { Player.SendMessage(p, "Sorry. Can't allow you to take money from yourself"); return; }

            int amountTaken;
            try { amountTaken = int.Parse(message.Split(' ')[1]); }
            catch { Player.SendMessage(p, "Invalid amount"); return; }

            if (who.money - amountTaken < 0) { Player.SendMessage(p, "Players cannot have under 0 " + Server.moneys); return; }
            if (amountTaken < 0) { Player.SendMessage(p, "Cannot take negative " + Server.moneys); return; }

            who.money -= amountTaken;
            Player.GlobalMessage(who.color + who.prefix + who.name + Server.DefaultColor + " was rattled down for " + amountTaken + " " + Server.moneys);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/take [player] <amount> - Takes <amount> of " + Server.moneys + " from [player]");
        }
    }
}