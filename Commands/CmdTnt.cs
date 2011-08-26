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
    public class CmdTnt : Command
    {
        public override string name { get { return "tnt"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdTnt() { }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 1) { Help(p); return; }

            if (p.BlockAction == 13 || p.BlockAction == 14)
            {
                p.BlockAction = 0; Player.SendMessage(p, "TNT mode is now &cOFF" + Server.DefaultColor + ".");
            }
            else if (message.ToLower() == "small" || message == "")
            {
                p.BlockAction = 13; Player.SendMessage(p, "TNT mode is now &aON" + Server.DefaultColor + ".");
            }
            else if (message.ToLower() == "big")
            {
                if (p.group.Permission > LevelPermission.AdvBuilder)
                {
                    p.BlockAction = 14; Player.SendMessage(p, "TNT mode is now &aON" + Server.DefaultColor + ".");
                }
                else
                {
                    Player.SendMessage(p, "This mode is reserved for OPs");
                }
            }
            else
            {
                Help(p);
            }

            p.painting = false;
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tnt [small/big] - Creates exploding TNT (with Physics 3).");
            Player.SendMessage(p, "Big TNT is reserved for OP+.");
        }
    }
}