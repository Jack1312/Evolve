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

namespace MCLawl
{
    public class CmdWhisper : Command
    {
        public override string name { get { return "whisper"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdWhisper() { }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                p.whisper = !p.whisper; p.whisperTo = "";
                if (p.whisper) Player.SendMessage(p, "All messages sent will now auto-whisper");
                else Player.SendMessage(p, "Whisper chat turned off");
            }
            else
            {
                Player who = Player.Find(message);
                if (who == null) { p.whisperTo = ""; p.whisper = false; Player.SendMessage(p, "Could not find player."); return; }

                p.whisper = true;
                p.whisperTo = who.name;
                Player.SendMessage(p, "Auto-whisper enabled.  All messages will now be sent to " + who.name + ".");
            }


        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whisper <name> - Makes all messages act like whispers");
        }
    }
}