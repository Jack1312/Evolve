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
using System.Text;

namespace MCLawl
{
    class CmdSay : Command
    {
        public override string name { get { return "say"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdSay() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            message = message.Replace("%", "&"); // Alow colors in global messages
            Player.GlobalChat(p, message, false);
            message = message.Replace("&", ""); // converts the MC color codes to IRC. Doesn't seem to work with multiple colors
            IRCBot.Say(message);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/say - broadcasts a global message to everyone in the server.");
        }
    }
}
