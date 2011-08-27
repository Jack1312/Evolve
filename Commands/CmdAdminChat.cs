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
    public class CmdAdminChat : Command
    {
        public override string name { get { return "adminchat"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdAdminChat() { }

        public override void Use(Player p, string message)
        {
            p.adminchat = !p.adminchat;
            if (p.adminchat) Player.SendMessage(p, "All messages will now be sent to Admins only");
            else Player.SendMessage(p, "Admin chat turned off");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/adminchat - Enable chat to admins.");
        }
    }
}
