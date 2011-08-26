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
using System.IO;

namespace MCLawl
{
    public class CmdText : Command
    {
        public override string name { get { return "text"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdText() { }

        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("extra/text/")) Directory.CreateDirectory("extra/text");
            if (message.IndexOf(' ') == -1) { Help(p); return; }

            try
            {
                if (message.Split(' ')[0].ToLower() == "delete")
                {
                    if (File.Exists("extra/text/" + message.Split(' ')[1] + ".txt"))
                    {
                        File.Delete("extra/text/" + message.Split(' ')[1] + ".txt");
                        Player.SendMessage(p, "Deleted file");
                    }
                    else
                    {
                        Player.SendMessage(p, "Could not find file specified");
                    }
                }
                else
                {
                    bool again = false;
                    string fileName = "extra/text/" + message.Split(' ')[0] + ".txt";
                    string group = Group.findPerm(LevelPermission.Guest).name;
                    if (Group.Find(message.Split(' ')[1]) != null)
                    {
                        group = Group.Find(message.Split(' ')[1]).name;
                        again = true;
                    }
                    message = message.Substring(message.IndexOf(' ') + 1);
                    if (again)
                        message = message.Substring(message.IndexOf(' ') + 1);
                    string contents = message;
                    if (contents == "") { Help(p); return; }
                    if (!File.Exists(fileName))
                        contents = "#" + group + System.Environment.NewLine + contents;
                    else
                        contents = " " + contents;
                    File.AppendAllText(fileName, contents);
                    Player.SendMessage(p, "Added text");
                }
            } catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/text [file] [rank] [message] - Makes a /view-able text");
            Player.SendMessage(p, "The [rank] entered is the minimum rank to view the file");
            Player.SendMessage(p, "The [message] is entered into the text file");
            Player.SendMessage(p, "If the file already exists, text will be added to the end");
        }
    }
}