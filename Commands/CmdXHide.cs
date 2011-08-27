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
    public class CmdXHide : Command
    {
        public override string name { get { return "xhide"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdXHide() { }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            if (p.possess != "")
            {
                Player.SendMessage(p, "Stop your current possession first.");
                return;
            }
            p.hidden = !p.hidden;
            if (p.hidden)
            {
                Player.GlobalDie(p, true);
                Player.GlobalChat(p, "&c- " + p.color + p.prefix + p.name + Server.DefaultColor + " disconnected.", false);
                Player.SendMessage(p, "You're now &finvisible&e.");
            }
            else
            {
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                Player.GlobalChat(p, "&a+ " + p.color + p.prefix + p.name + Server.DefaultColor + " joined the game.", false);
                Player.SendMessage(p, "You're now &8visible&e.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/xhide - Hide without admins being notified.");
        }
    }
}
