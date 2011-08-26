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
    public class CmdSlap : Command
    {
        public override string name { get { return "slap"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdSlap() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            Player who = Player.Find(message);

            if (who == null)
            {
                Player.SendMessage(p, "Could not find player specified");
                return;
            }

            ushort currentX = (ushort)(who.pos[0] / 32);
            ushort currentY = (ushort)(who.pos[1] / 32);
            ushort currentZ = (ushort)(who.pos[2] / 32);
            ushort foundHeight = 0;

            for (ushort yy = currentY; yy <= 1000; yy++)
            {
                if (!Block.Walkthrough(p.level.GetTile(currentX, yy, currentZ)) && p.level.GetTile(currentX, yy, currentZ) != Block.Zero)
                {
                    foundHeight = (ushort)(yy - 1);
                    who.level.ChatLevel(who.color + who.name + Server.DefaultColor + " was slapped into the roof by " + p.color + p.name);
                    break;
                }
            }

            if (foundHeight == 0)
            {
                who.level.ChatLevel(who.color + who.name + Server.DefaultColor + " was slapped sky high by " + p.color + p.name);
                foundHeight = 1000;
            }
            
            unchecked { who.SendPos((byte)-1, who.pos[0], (ushort)(foundHeight * 32), who.pos[2], who.rot[0], who.rot[1]); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/slap <name> - Slaps <name>, knocking them into the air");
        }
    }
}