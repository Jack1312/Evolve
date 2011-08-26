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
using System.IO;

namespace MCLawl
{
    class CmdRules : Command
    {
        public override string name { get { return "rules"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdRules() { }

        public override void Use(Player p, string message)
        {
            List<string> rules = new List<string>();
            if (!File.Exists("text/rules.txt"))
            {
                File.WriteAllText("text/rules.txt", "No rules entered yet!");
            }
            StreamReader r = File.OpenText("text/rules.txt");
            while (!r.EndOfStream)
                rules.Add(r.ReadLine());

            r.Close();
            r.Dispose();

            Player who = null;
            if (message != "")
            {
                if (p.group.Permission <= LevelPermission.Guest)
                { Player.SendMessage(p, "You cant send /rules to another player!"); return; }
                who = Player.Find(message);
            }
            else
            {
                who = p;
            }

            if (who != null)
            {
                if (who.level == Server.mainLevel && Server.mainLevel.permissionbuild == LevelPermission.Guest) { who.SendMessage("You are currently on the guest map where anyone can build"); }
                who.SendMessage("Server Rules:");
                foreach (string s in rules)
                    who.SendMessage(s);
            }
            else
            {
                Player.SendMessage(p, "There is no player \"" + message + "\"!");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rules [player]- Displays server rules to a player");
        }
    }
}
