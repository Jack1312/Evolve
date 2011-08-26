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
    public class CmdPatrol : Command
    {
        public override string name { get { return "patrol"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdPatrol() { }

        public override void Use(Player p, string message)
        {
            bool notonlevel = false;
            List<string> guests = new List<string>();
            foreach (Player pl in Player.players)
            {
                if (pl.group.Permission == LevelPermission.Guest)
                {
                    guests.Add(pl.name);
                }
            }
            if (guests.Count <= 0)
            {
                Player.SendMessage(p, "You cannot patrol if no guests are online!");
                return;
            }
            Random select = new Random();
            int selected = select.Next(guests.Count);
            string player = guests[selected];
            Player who = Player.Find(player);
            p.ignorePermission = true;
            if (who.level != p.level)
            {
                notonlevel = true;
            }
            Command.all.Find("tp").Use(p, player);
            if (notonlevel == true)
            {
                while (p.Loading) { }
            }
            Player.SendMessage(p, "You are now visiting " + who.color + who.name + "&e!");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/patrol - Teleports you to a random guest player.");
        }
    }
}
