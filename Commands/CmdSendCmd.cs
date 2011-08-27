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
    public class CmdSendCmd : Command
    {
        public override string name { get { return "sendcmd"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdSendCmd() { }

        public override void Use(Player p, string message)
        {
            int length = 0;
            string msg = String.Empty;
            string plyr = String.Empty;
            string cmd = String.Empty;
            string work = String.Empty;
            string cmd2 = String.Empty;
            try
            {
                msg = message;
                length = msg.Split(' ').Length;
            }
            catch { }
            try
            {
                plyr = msg.Split(' ')[0];
                cmd = msg.Replace(plyr + " ", "");
            }
            catch { }
            if (length == 4)
            {
                work = cmd.Split(' ')[0];
                cmd2 = cmd.Replace(work + " ", "");
            }
            Player who = Player.Find(plyr);
            if (who == null)
            {
                Player.SendMessage(p, "Could not find player specified!");
                return;
            }
            if (length == 1)
            {
                Player.SendMessage(p, "You did not specify a command for " + who.name + " to execute!");
                return;
            }
            if (who.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "Cannot sendcmd a player of equal or higher rank to use a command!");
                return;
            }
            if (!p.group.CanExecute(Command.all.Find(work)))
            {
                Player.SendMessage(p, "You cannot force a player to complete a command, that you cannot yourself!");
                return;
            }
            Command.all.Find(work).Use(who, cmd2);            
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/sendcmd [Player] [Command] - Force the player to complete the command.");
        }
    }
}
