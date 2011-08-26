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
using System.Threading;

namespace MCLawl
{
    public class CmdLine : Command
    {
        public override string name { get { return "line"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdLine() { }

        public override void Use(Player p, string message)
        {
            CatchPos cpos;

            message = message.ToLower();

            if (message == "")
            {
                cpos.maxNum = 0;
                cpos.extraType = 0;
                cpos.type = Block.Zero;
            }
            else if (message.IndexOf(' ') == -1)
            {
                try
                {
                    cpos.maxNum = int.Parse(message);
                    cpos.extraType = 0;
                    cpos.type = Block.Zero;
                }
                catch
                {
                    cpos.maxNum = 0;
                    if (message == "wall")
                    {
                        cpos.extraType = 1;
                        cpos.type = Block.Zero;
                    }
                    else if (message == "straight")
                    {
                        cpos.extraType = 2;
                        cpos.type = Block.Zero;
                    }
                    else
                    {
                        cpos.extraType = 0;
                        cpos.type = Block.Byte(message);
                        if (cpos.type == Block.Zero)
                        {
                            Help(p); return;
                        }
                    }
                }
            }
            else
            {
                if (message.Split(' ').Length == 2)
                {
                    try
                    {
                        cpos.maxNum = int.Parse(message.Split(' ')[0]);
                        cpos.type = Block.Byte(message.Split(' ')[1]);
                        if (cpos.type == Block.Zero)
                            if (message.Split(' ')[1] == "wall") cpos.extraType = 1;
                            else if (message.Split(' ')[1] == "straight") cpos.extraType = 2;
                            else cpos.extraType = 0;
                        else cpos.extraType = 0;
                    }
                    catch
                    {
                        cpos.maxNum = 0;
                        cpos.type = Block.Byte(message.Split(' ')[0]); if (cpos.type == Block.Zero) { Help(p); return; }
                        if (message.Split(' ')[1] == "wall") cpos.extraType = 1;
                        else if (message.Split(' ')[1] == "straight") cpos.extraType = 2;
                        else cpos.extraType = 0;
                    }
                }
                else
                {
                    try { cpos.maxNum = int.Parse(message.Split(' ')[0]); }
                    catch { Help(p); return; }
                    cpos.type = Block.Byte(message.Split(' ')[1]); if (cpos.type == Block.Zero) { Help(p); return; }
                    if (message.Split(' ')[2] == "wall") cpos.extraType = 1;
                    else if (message.Split(' ')[2] == "straight") cpos.extraType = 2;
                    else cpos.extraType = 0;
                }
            }

            if (!Block.canPlace(p, cpos.type) && cpos.type != Block.Zero) { Player.SendMessage(p, "Cannot place this block type!"); return; }

            cpos.x = 0; cpos.y = 0; cpos.z = 0; p.blockchangeObject = cpos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/line [num] <block> [extra] - Creates a line between two blocks [num] long.");
            Player.SendMessage(p, "Possible [extras] - wall");
        }
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            CatchPos bp = (CatchPos)p.blockchangeObject;
            bp.x = x; bp.y = y; bp.z = z; p.blockchangeObject = bp;
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange2);
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            CatchPos cpos = (CatchPos)p.blockchangeObject;
            if (cpos.type == Block.Zero) type = p.bindings[type]; else type = cpos.type;
            List<CatchPos> buffer = new List<CatchPos>();
            CatchPos pos = new CatchPos();

            if (cpos.extraType == 2)
            {  //Fun part of making a straight line
                int xdif = Math.Abs(cpos.x - x);
                int ydif = Math.Abs(cpos.y - y);
                int zdif = Math.Abs(cpos.z - z);

                if (xdif > ydif && xdif > zdif)
                {
                    y = cpos.y; z = cpos.z;
                }
                else if (ydif > xdif && ydif > zdif)
                {
                    x = cpos.x; z = cpos.z;
                }
                else if (zdif > ydif && zdif > xdif)
                {
                    y = cpos.y; x = cpos.x;
                }
            }

            if (cpos.maxNum == 0) cpos.maxNum = 100000;

            int i, dx, dy, dz, l, m, n, x_inc, y_inc, z_inc, err_1, err_2, dx2, dy2, dz2;
            int[] pixel = new int[3];

            pixel[0] = cpos.x; pixel[1] = cpos.y; pixel[2] = cpos.z;
            dx = x - cpos.x; dy = y - cpos.y; dz = z - cpos.z;

            x_inc = (dx < 0) ? -1 : 1; l = Math.Abs(dx);
            y_inc = (dy < 0) ? -1 : 1; m = Math.Abs(dy);
            z_inc = (dz < 0) ? -1 : 1; n = Math.Abs(dz);

            dx2 = l << 1; dy2 = m << 1; dz2 = n << 1;

            if ((l >= m) && (l >= n))
            {
                err_1 = dy2 - l;
                err_2 = dz2 - l;
                for (i = 0; i < l; i++)
                {
                    pos.x = (ushort)pixel[0];
                    pos.y = (ushort)pixel[1];
                    pos.z = (ushort)pixel[2];
                    buffer.Add(pos);

                    if (err_1 > 0)
                    {
                        pixel[1] += y_inc;
                        err_1 -= dx2;
                    }
                    if (err_2 > 0)
                    {
                        pixel[2] += z_inc;
                        err_2 -= dx2;
                    }
                    err_1 += dy2;
                    err_2 += dz2;
                    pixel[0] += x_inc;
                }
            }
            else if ((m >= l) && (m >= n))
            {
                err_1 = dx2 - m;
                err_2 = dz2 - m;
                for (i = 0; i < m; i++)
                {
                    pos.x = (ushort)pixel[0];
                    pos.y = (ushort)pixel[1];
                    pos.z = (ushort)pixel[2];
                    buffer.Add(pos);

                    if (err_1 > 0)
                    {
                        pixel[0] += x_inc;
                        err_1 -= dy2;
                    }
                    if (err_2 > 0)
                    {
                        pixel[2] += z_inc;
                        err_2 -= dy2;
                    }
                    err_1 += dx2;
                    err_2 += dz2;
                    pixel[1] += y_inc;
                }
            }
            else
            {
                err_1 = dy2 - n;
                err_2 = dx2 - n;
                for (i = 0; i < n; i++)
                {
                    pos.x = (ushort)pixel[0];
                    pos.y = (ushort)pixel[1];
                    pos.z = (ushort)pixel[2];
                    buffer.Add(pos);

                    if (err_1 > 0)
                    {
                        pixel[1] += y_inc;
                        err_1 -= dz2;
                    }
                    if (err_2 > 0)
                    {
                        pixel[0] += x_inc;
                        err_2 -= dz2;
                    }
                    err_1 += dy2;
                    err_2 += dx2;
                    pixel[2] += z_inc;
                }
            }

            pos.x = (ushort)pixel[0];
            pos.y = (ushort)pixel[1];
            pos.z = (ushort)pixel[2];
            buffer.Add(pos);

            int count;
            count = Math.Min(buffer.Count, cpos.maxNum);
            if (cpos.extraType == 1) count = count * Math.Abs(cpos.y - y);

            if (count > p.group.maxBlocks)
            {
                Player.SendMessage(p, "You tried to fill " + count + " blocks at once.");
                Player.SendMessage(p, "You are limited to " + p.group.maxBlocks);
                return;
            }

            for (count = 0; count < cpos.maxNum && count < buffer.Count; count++)
            {
                if (cpos.extraType != 1)
                {
                    p.level.Blockchange(p, buffer[count].x, buffer[count].y, buffer[count].z, type);
                }
                else
                {
                    for (ushort yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); yy++)
                    {
                        p.level.Blockchange(p, buffer[count].x, yy, buffer[count].z, type);
                    }
                }
            }

            Player.SendMessage(p, "Line was " + count.ToString() + " blocks long.");

            if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        struct CatchPos { public ushort x, y, z; public int maxNum; public int extraType; public byte type; }
    }
}