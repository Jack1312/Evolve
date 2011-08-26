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
using System.Data;

namespace MCLawl
{
    public class CmdTimer : Command
    {
        public override string name { get { return "timer"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdTimer() { }

        public override void Use(Player p, string message)
        {
            if (p.cmdTimer == true) { Player.SendMessage(p, "Can only have one timer at a time. Use /abort to cancel your previous timer."); return; }

            System.Timers.Timer messageTimer = new System.Timers.Timer(5000);
            if (message == "") { Help(p); return; }

            int TotalTime = 0;
            try
            {
                TotalTime = int.Parse(message.Split(' ')[0]);
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            catch
            {
                TotalTime = 60;
            }

            if (TotalTime > 300) { Player.SendMessage(p, "Cannot have more than 5 minutes in a timer"); return; }

            Player.GlobalChatLevel(p, Server.DefaultColor + "Timer lasting for " + TotalTime + " seconds has started.", false);
            TotalTime = (int)(TotalTime / 5);

            Player.GlobalChatLevel(p, Server.DefaultColor + message, false);

            p.cmdTimer = true;
            messageTimer.Elapsed += delegate
            {
                TotalTime--;
                if (TotalTime < 1 || p.cmdTimer == false)
                {
                    Player.SendMessage(p, "Timer ended.");
                    messageTimer.Stop();
                }
                else
                {
                    Player.GlobalChatLevel(p, Server.DefaultColor + message, false);
                    Player.GlobalChatLevel(p, "Timer has " + (TotalTime * 5) + " seconds remaining.", false);
                }
            };

            messageTimer.Start();
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/timer [time] [message] - Starts a timer which repeats [message] every 5 seconds.");
            Player.SendMessage(p, "Repeats constantly until [time] has passed");
        }
    }
}