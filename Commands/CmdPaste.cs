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
using System.Collections.Generic;
using System.IO;

namespace MCLawl
{
    public class CmdPaste : Command
    {
        public override string name { get { return "paste"; } }
        public override string shortcut { get { return "v"; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public string loadname;
        public CmdPaste() { }
        
        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }

            CatchPos cpos;
            cpos.x = 0; cpos.y = 0; cpos.z = 0; p.blockchangeObject = cpos;

            Player.SendMessage(p, "Place a block in the corner of where you want to paste."); p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/paste - Pastes the stored copy.");
            Player.SendMessage(p, "&4BEWARE: " + Server.DefaultColor + "The blocks will always be pasted in a set direction");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);

            Player.UndoPos Pos1;
            //p.UndoBuffer.Clear();
            p.CopyBuffer.ForEach(delegate(Player.CopyPos pos)
            {
                Pos1.x = (ushort)(Math.Abs(pos.x) + x);
                Pos1.y = (ushort)(Math.Abs(pos.y) + y);
                Pos1.z = (ushort)(Math.Abs(pos.z) + z);

                if (pos.type != Block.air || p.copyAir)
                    unchecked { if (p.level.GetTile(Pos1.x, Pos1.y, Pos1.z) != Block.Zero) p.level.Blockchange(p, (ushort)(Pos1.x + p.copyoffset[0]), (ushort)(Pos1.y + p.copyoffset[1]), (ushort)(Pos1.z + p.copyoffset[2]), pos.type); }
            });

            Player.SendMessage(p, "Pasted " + p.CopyBuffer.Count + " blocks.");

            if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }

        struct CatchPos { public ushort x, y, z; }
    }
}