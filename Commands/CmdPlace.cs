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

namespace MCLawl.Commands
{
    class CmdPlace : Command
    {
        public override string name { get { return "place"; } }
        public override string shortcut { get { return "pl"; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdPlace() { }

        public override void Use(Player p, string message)
        {
            byte b = Block.Zero;
            ushort x = 0; ushort y = 0; ushort z = 0;

            x = (ushort)(p.pos[0] / 32);
            y = (ushort)((p.pos[1] / 32) - 1);
            z = (ushort)(p.pos[2] / 32);

            try
            {
                switch (message.Split(' ').Length)
                {
                    case 0: b = Block.rock; break;
                    case 1: b = Block.Byte(message); break;
                    case 3:
                        x = Convert.ToUInt16(message.Split(' ')[0]);
                        y = Convert.ToUInt16(message.Split(' ')[1]);
                        z = Convert.ToUInt16(message.Split(' ')[2]);
                        break;
                    case 4:
                        b = Block.Byte(message.Split(' ')[0]);
                        x = Convert.ToUInt16(message.Split(' ')[1]);
                        y = Convert.ToUInt16(message.Split(' ')[2]);
                        z = Convert.ToUInt16(message.Split(' ')[3]);
                        break;
                    default: Player.SendMessage(p, "Invalid parameters"); return;
                }
            }
            catch { Player.SendMessage(p, "Invalid parameters"); return; }

            if (b == Block.Zero) b = (byte)1;
            if (!Block.canPlace(p, b)) { Player.SendMessage(p, "Cannot place that block type."); return; }

            Level level = p.level;

            if (y >= p.level.depth) y = (ushort)(p.level.depth - 1);

            p.level.Blockchange(p, x, y, z, b);
            Player.SendMessage(p, "A block was placed at (" + x + ", " + y + ", " + z + ").");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/place [block] <x y z> - Places block at your feet or <x y z>");
        }
    }
}
