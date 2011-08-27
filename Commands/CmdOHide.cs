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
    public class CmdOHide : Command
    {
        public override string name { get { return "ohide"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdOHide() { }

        public override void Use(Player p, string message)
        {
            Player who = Player.Find(message);
            if (who == null)
            {
                Player.SendMessage(p, "Could not find player specified!");
                return;
            }
            who.hidden = !who.hidden;
            if (who.hidden)
            {
                Player.GlobalDie(who, true);
                Player.GlobalMessageOps("To Ops -" + who.color + who.name + "-" + Server.DefaultColor + " is now &finvisible" + Server.DefaultColor + ".");
                Player.GlobalChat(who, "&c- " + who.color + who.prefix + who.name + Server.DefaultColor + " disconnected.", false);
                //Player.SendMessage(p, "You're now &finvisible&e.");
            }
            else
            {
                Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
                Player.GlobalMessageOps("To Ops -" + who.color + who.name + "-" + Server.DefaultColor + " is now &8visible" + Server.DefaultColor + ".");
                Player.GlobalChat(who, "&a+ " + who.color + who.prefix + who.name + Server.DefaultColor + " joined the game.", false);
                //Player.SendMessage(who, "You're now &8visible&e.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ohide [Player] - Hide the specified player.");
        }
    }
}
