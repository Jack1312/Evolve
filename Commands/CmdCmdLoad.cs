/*
	Copyright 2010 MCLawl Team - Written by Valek (Modified for use with Evolve)
 
    Licensed under the
	Educational Community License, Version 2.0 (the "License") and
    the binpress license; you may not use this file except in compliance
    with the License. You may obtain a copy of the Licenses at
	
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
using System.Reflection;

namespace MCLawl
{
    class CmdCmdLoad : Command
    {
        public override string name { get { return "cmdload"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }
        public CmdCmdLoad() { }

        public override void Use(Player p, string message)
        {
            if(message == "") { Help(p); return; }
            if (Command.all.Contains(message.Split(' ')[0]))
            {
                Player.SendMessage(p, "That command is already loaded!");
                return;
            }
            message = "Cmd" + message.Split(' ')[0]; ;
            string error = Scripting.Load(message);
            if (error != null)
            {
                Player.SendMessage(p, error);
                return;
            }
            GrpCommands.fillRanks();
            Player.SendMessage(p, "Command was successfully loaded.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdload <command name> - Loads a command into the server for use.");
        }
    }
}
