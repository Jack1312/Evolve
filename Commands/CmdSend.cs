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
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCLawl
{
    public class CmdSend : Command
    {
        public override string name { get { return "send"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdSend() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1) { Help(p); return; }

            Player who = Player.Find(message.Split(' ')[0]);

            string whoTo;
            if (who != null) whoTo = who.name;
            else whoTo = message.Split(' ')[0];

            message = message.Substring(message.IndexOf(' ') + 1);

            //DB
            MySQL.executeQuery("CREATE TABLE if not exists `Inbox" + whoTo + "` (PlayerFrom CHAR(20), TimeSent DATETIME, Contents VARCHAR(255));");
            MySQL.executeQuery("INSERT INTO `Inbox" + whoTo + "` (PlayerFrom, TimeSent, Contents) VALUES ('" + p.name + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + message.Replace("'", "\\'") + "')");
            //DB

            Player.SendMessage(p, "Message sent to &5" + whoTo + ".");
            if (who != null) who.SendMessage("Message recieved from &5" + p.name + Server.DefaultColor + ".");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/send [name] <message> - Sends <message> to [name].");
        }
    }
}