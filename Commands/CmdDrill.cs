﻿/*
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

namespace MCLawl
{
    public class CmdDrill : Command
    {
        public override string name { get { return "drill"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdDrill() { }

        public override void Use(Player p, string message)
        {
            CatchPos cpos;
            cpos.distance = 20;

            if (message != "")
                try
                {
                    cpos.distance = int.Parse(message);
                }
                catch { Help(p); return; }

            cpos.x = 0; cpos.y = 0; cpos.z = 0; p.blockchangeObject = cpos;

            Player.SendMessage(p, "Destroy the block you wish to drill."); p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/drill [distance] - Drills a hole, destroying all similar blocks in a 3x3 rectangle ahead of you.");
        }
        
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands) p.ClearBlockchange();
            CatchPos cpos = (CatchPos)p.blockchangeObject;
            byte oldType = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, oldType);

            int diffX = 0, diffZ = 0;

            if (p.rot[0] <= 32 || p.rot[0] >= 224) { diffZ = -1; }
            else if (p.rot[0] <= 96) { diffX = 1; }
            else if (p.rot[0] <= 160) { diffZ = 1; }
            else diffX = -1;

            List<Pos> buffer = new List<Pos>();
            Pos pos;
            int total = 0;

            if (diffX != 0)
            {
                for (ushort xx = x; total < cpos.distance; xx += (ushort)diffX)
                {
                    for (ushort yy = (ushort)(y - 1); yy <= (ushort)(y + 1); yy++)
                        for (ushort zz = (ushort)(z - 1); zz <= (ushort)(z + 1); zz++)
                        {
                            pos.x = xx; pos.y = yy; pos.z = zz;
                            buffer.Add(pos);
                        }
                    total++;
                }
            }
            else
            {
                for (ushort zz = z; total < cpos.distance; zz += (ushort)diffZ)
                {
                    for (ushort yy = (ushort)(y - 1); yy <= (ushort)(y + 1); yy++)
                        for (ushort xx = (ushort)(x - 1); xx <= (ushort)(x + 1); xx++)
                        {
                            pos.x = xx; pos.y = yy; pos.z = zz;
                            buffer.Add(pos);
                        }
                    total++;
                }
            }

            if (buffer.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, "You tried to drill " + buffer.Count + " blocks.");
                Player.SendMessage(p, "You cannot drill more than " + p.group.maxBlocks + ".");
                return;
            }

            foreach (Pos pos1 in buffer)
            {
                if (p.level.GetTile(pos1.x, pos1.y, pos1.z) == oldType)
                    p.level.Blockchange(p, pos1.x, pos1.y, pos1.z, Block.air);
            }
            Player.SendMessage(p, buffer.Count + " blocks.");
        }

        struct CatchPos { public ushort x, y, z; public int distance; }
        struct Pos { public ushort x, y, z; }
    }
}