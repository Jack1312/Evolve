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
using System.Linq;
using System.Text;

namespace MCLawl
{
    public class CmdVoice : Command
    {
        public override string name { get { return "voice"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdVoice() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player who = Player.Find(message);
            if (who != null)
            {
                if (who.voice)
                {
                    who.voice = false;
                    Player.SendMessage(p, "Removing voice status from " + who.name);
                    who.SendMessage("Your voice status has been revoked.");
                    who.voicestring = "";
                }
                else
                {
                    who.voice = true;
                    Player.SendMessage(p, "Giving voice status to " + who.name);
                    who.SendMessage("You have received voice status.");
                    who.voicestring = "&f+";
                }
            }
            else
            {
                Player.SendMessage(p, "There is no player online named \"" + message + "\"");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/voice <name> - Toggles voice status on or off for specified player.");
        }
    }
}