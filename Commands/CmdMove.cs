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
    public class CmdMove : Command
    {
        public override string name { get { return "move"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdMove() { }

        public override void Use(Player p, string message)
        {
            // /move name map
            // /move x y z
            // /move name x y z

            if (message.Split(' ').Length < 2 || message.Split(' ').Length > 4) { Help(p); return; }

            if (message.Split(' ').Length == 2)     // /move name map
            {
                Player who = Player.Find(message.Split(' ')[0]);
                Level where = Level.Find(message.Split(' ')[1]);
                if (who == null) { Player.SendMessage(p, "Could not find player specified"); return; }
                if (where == null) { Player.SendMessage(p, "Could not find level specified"); return; }
                if (p != null && who.group.Permission > p.group.Permission) { Player.SendMessage(p, "Cannot move someone of greater rank"); return; }

                Command.all.Find("goto").Use(who, where.name);
                if (who.level == where)
                    Player.SendMessage(p, "Sent " + who.color + who.name + Server.DefaultColor + " to " + where.name);
                else
                    Player.SendMessage(p, where.name + " is not loaded");
            }
            else
            {
                // /move name x y z
                // /move x y z

                Player who;

                if (message.Split(' ').Length == 4)
                {
                    who = Player.Find(message.Split(' ')[0]);
                    if (who == null) { Player.SendMessage(p, "Could not find player specified"); return; }
                    if (p != null && who.group.Permission > p.group.Permission) { Player.SendMessage(p, "Cannot move someone of greater rank"); return; }
                    message = message.Substring(message.IndexOf(' ') + 1);
                }
                else
                {
                    who = p;
                }

                try
                {
                    ushort x = System.Convert.ToUInt16(message.Split(' ')[0]);
                    ushort y = System.Convert.ToUInt16(message.Split(' ')[1]);
                    ushort z = System.Convert.ToUInt16(message.Split(' ')[2]);
                    x *= 32; x += 16;
                    y *= 32; y += 32;
                    z *= 32; z += 16;
                    unchecked { who.SendPos((byte)-1, x, y, z, p.rot[0], p.rot[1]); }
                    if (p != who) Player.SendMessage(p, "Moved " + who.color + who.name);
                }
                catch { Player.SendMessage(p, "Invalid co-ordinates"); }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/move <player> <map> <x> <y> <z> - Move <player>");
            Player.SendMessage(p, "<map> must be blank if x, y or z is used and vice versa");
        }
    }
}