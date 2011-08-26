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
    public class CmdRepeat : Command
    {
        public override string name { get { return "repeat"; } }
        public override string shortcut { get { return "m"; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdRepeat() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (p.lastCMD == "") { Player.SendMessage(p, "No commands used yet."); return; }
                if (p.lastCMD.Length > 5)
                    if (p.lastCMD.Substring(0, 6) == "static") { Player.SendMessage(p, "Can't repeat static"); return; }

                Player.SendMessage(p, "Using &b/" + p.lastCMD);

                if (p.lastCMD.IndexOf(' ') == -1)
                {
                    Command.all.Find(p.lastCMD).Use(p, "");
                }
                else
                {
                    Command.all.Find(p.lastCMD.Substring(0, p.lastCMD.IndexOf(' '))).Use(p, p.lastCMD.Substring(p.lastCMD.IndexOf(' ') + 1));
                }
            }
            catch { Player.SendMessage(p, "An error occured!"); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/repeat - Repeats the last used command");
        }
    }
}