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
    public class CmdPay : Command
    {
        public override string name { get { return "pay"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdPay() { }

        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1) { Help(p); return; }
            if (message.Split(' ').Length != 2) { Help(p); return; }

            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player entered"); return; }
            if (who == p) { Player.SendMessage(p, "Sorry. Can't allow you to pay yourself"); return; }

            int amountPaid;
            try { amountPaid = int.Parse(message.Split(' ')[1]); }
            catch { Player.SendMessage(p, "Invalid amount"); return; }

            if (who.money + amountPaid > 16777215) { Player.SendMessage(p, "Players cannot have over 16777215 " + Server.moneys); return; }
            if (p.money - amountPaid < 0) { Player.SendMessage(p, "You don't have that much " + Server.moneys); return; }
            if (amountPaid < 0) { Player.SendMessage(p, "Cannot pay negative " + Server.moneys); return; }

            who.money += amountPaid;
            p.money -= amountPaid;
            Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " paid " + who.color + who.name + Server.DefaultColor + " " + amountPaid + " " + Server.moneys);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pay [player] <amount> - Pays <amount> of " + Server.moneys + " to [player]");
        }
    }
}