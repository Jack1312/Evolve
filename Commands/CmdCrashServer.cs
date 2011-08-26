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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCLawl
{
    public class CmdCrashServer : Command
    {
        public override string name { get { return "crashserver"; } }
        public override string shortcut { get { return "crash"; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdCrashServer() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            Player.GlobalMessageOps(p.color + Server.DefaultColor + " used &b/crashserver");
            p.Kick("Server crash! Error code 0x0005A4");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/crashserver - Crash the server with a generic error");
        }
    }

}
