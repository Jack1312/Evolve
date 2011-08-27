/*
	Copyright 2011 Evolve Team - Written by Jack1312
    
    This code is licensed under the binpress license; a copy
    of the license can be found below:
	
    http://www.binpress.com/license/view/l/6cfa4c36602b0f90ab898dc9fbd77b84
 
    The binpress license states that:
     - May only be used for personal use (cannot be resold or distributed)
     - Non-commerical use only
     - Cannot modify source code for any purpose (cannot create derivative works
    The implications of not following the license may lead to legal action, depending
    on the circumstances.
*/
using System;
using System.Collections.Generic;

namespace MCLawl
{
    public class CmdImpersonate : Command
    {
        public override string name { get { return "impersonate"; } }
        public override string shortcut { get { return "imp"; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdImpersonate() { }

        public override void Use(Player p, string message)
        {
            int length = 0;
            string msg = String.Empty;
            string plyr = String.Empty;
            string plyrmsg = String.Empty;
            try
            {
                msg = message;
                length = msg.Split(' ').Length;
            }
            catch { }
            if (message == "") { Help(p); return; }
            if (length == 1)
            {
                Player.SendMessage(p, "Message not specified.");
                return;
            }
            try
            {
                plyr = msg.Split(' ')[0];
                plyrmsg = msg.Replace(plyr + " ", "");
            }
            catch { }
            Player who = Player.Find(plyr);
            if (who == p)
            {
                Player.SendMessage(p, "What are you doing impersonating yourself?");
                return;
            }
            if (who.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "You cannot impersonate someone of equal or higher rank!");
                return;
            }
            Player.GlobalMessage(who.color + who.prefix + who.name + ": &f" + plyrmsg);            
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/impersonate [Player] [Message] - Makes the specified player, speak the message. ");
        }
    }
}
