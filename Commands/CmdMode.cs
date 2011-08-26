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
using System.IO;

namespace MCLawl
{
    public class CmdMode : Command
    {
        public override string name { get { return "mode"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdMode() { }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p.modeType != 0)
                {
                    Player.SendMessage(p, "&b" + Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor + " mode: &cOFF");
                    p.modeType = 0;
                    p.BlockAction = 0;
                }
                else
                {
                    Help(p); return;
                }
            }
            else
            {
                byte b = Block.Byte(message);
                if (b == Block.Zero) { Player.SendMessage(p, "Could not find block given."); return; }
                if (b == Block.air) { Player.SendMessage(p, "Cannot use Air Mode."); return; }
                if (!Block.canPlace(p, b)) { Player.SendMessage(p, "Cannot place this block at your rank."); return; }

                if (p.modeType == b)
                {
                    Player.SendMessage(p, "&b" + Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor + " mode: &cOFF");
                    p.modeType = 0;
                    p.BlockAction = 0;
                }
                else
                {
                    p.BlockAction = 6;
                    p.modeType = b;
                    Player.SendMessage(p, "&b" + Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor + " mode: &aON");
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mode [block] - Makes every block placed into [block].");
            Player.SendMessage(p, "/[block] also works");
        }
    }
}