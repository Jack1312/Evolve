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

namespace MCLawl
{
    public class CmdXBan : Command
    {
        public override string name { get { return "xban"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdXBan() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            string msg = String.Empty;
            try
            {
                msg = message;
            }
            catch { }
            Player who = Player.Find(message);
            if (p != null)
            {
                if (who.group.Permission >= p.group.Permission)
                {
                    Player.SendMessage(p, "You cannot xban a player of equal or higher rank!");
                    return;
                }
            }
            Command.all.Find("undo").Use(p, who.name + " all");
            Command.all.Find("ban").Use(p, who.name);
            Command.all.Find("banip").Use(p, "@ " + who.name);
            if (who != null)
            {
                Command.all.Find("kick").Use(p, who.name);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/xban <player> - Ban, ip-ban, undo and kick a player all-in-one");
        }
    }
}
