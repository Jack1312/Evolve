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
    public class CmdTitle : Command
    {
        public override string name { get { return "title"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdTitle() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            int pos = message.IndexOf(' ');
            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player."); return; }

            string query;
            string newTitle = "";
            if (message.Split(' ').Length > 1) newTitle = message.Substring(pos + 1);
            else
            {
                who.title = "";
                who.SetPrefix();
                Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " had their title removed.", false);
                query = "UPDATE Players SET Title = '' WHERE Name = '" + who.name + "'";
                MySQL.executeQuery(query);
                return;
            }

            if (newTitle != "")
            {
                newTitle = newTitle.ToString().Trim().Replace("[", "");
                newTitle = newTitle.Replace("]", "");
                /* if (newTitle[0].ToString() != "[") newTitle = "[" + newTitle;
                if (newTitle.Trim()[newTitle.Trim().Length - 1].ToString() != "]") newTitle = newTitle.Trim() + "]";
                if (newTitle[newTitle.Length - 1].ToString() != " ") newTitle = newTitle + " "; */
            }

            if (newTitle.Length > 17) { Player.SendMessage(p, "Title must be under 17 letters."); return; }
            if (!Server.devs.Contains(p.name))
            {
                if (Server.devs.Contains(who.name) || newTitle.ToLower() == "dev") { Player.SendMessage(p, "Can't let you do that, starfox."); return; }
            }

            if (newTitle != "")
                Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " was given the title of &b[" + newTitle + "]", false);
            else Player.GlobalChat(null, who.color + who.prefix + who.name + Server.DefaultColor + " had their title removed.", false);

            if (newTitle == "")
            {
                query = "UPDATE Players SET Title = '' WHERE Name = '" + who.name + "'";
            }
            else
            {
                query = "UPDATE Players SET Title = '" + newTitle.Replace("'", "\'") + "' WHERE Name = '" + who.name + "'";
            }
            MySQL.executeQuery(query);
            who.title = newTitle;
            who.SetPrefix();
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/title <player> [title] - Gives <player> the [title].");
            Player.SendMessage(p, "If no [title] is given, the player's title is removed.");
        }
    }
}
