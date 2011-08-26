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

namespace MCLawl
{
    public class CmdDevs : Command
    {
        public override string name { get { return "devs"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdDevs() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            string devlist = "";
            string temp;
            foreach (string dev in Server.devs)
            {
                temp = dev.Substring(0, 1);
                temp = temp.ToUpper() + dev.Remove(0, 1);
                devlist += temp + ", ";
            }
            devlist = devlist.Remove(devlist.Length - 2);
            Player.SendMessage(p, "&9Evolve Development Team: " + Server.DefaultColor + devlist);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/devs - Displays the list of MCLawl developers.");
        }
    }
}