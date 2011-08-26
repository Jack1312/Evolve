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

namespace MCLawl
{
    public class CmdSpin : Command
    {
        public override string name { get { return "spin"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        //public override bool consoleUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdSpin() { }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 1) { Help(p); return; }
            if (message == "") message = "90";

            List<Player.CopyPos> newBuffer = new List<Player.CopyPos>();
            int TotalLoop = 0; ushort temp;
            newBuffer.Clear();

            switch (message)
            {
                case "90":
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        temp = Pos.z; Pos.z = Pos.x; Pos.x = temp;
                        p.CopyBuffer[TotalLoop] = Pos;
                        TotalLoop += 1;
                    });
                    goto case "m";
                case "180":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop -= 1;
                        Pos.x = p.CopyBuffer[TotalLoop].x;
                        Pos.z = p.CopyBuffer[TotalLoop].z;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                case "upsidedown":
                case "u":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop -= 1;
                        Pos.y = p.CopyBuffer[TotalLoop].y;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                case "mirror":
                case "m":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop -= 1;
                        Pos.x = p.CopyBuffer[TotalLoop].x;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                case "z":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop -= 1;
                        Pos.x = (ushort)(p.CopyBuffer[TotalLoop].y - (2 * p.CopyBuffer[TotalLoop].y));
                        Pos.y = p.CopyBuffer[TotalLoop].x;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                case "x":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop -= 1;
                        Pos.z = (ushort)(p.CopyBuffer[TotalLoop].y - (2 * p.CopyBuffer[TotalLoop].y));
                        Pos.y = p.CopyBuffer[TotalLoop].z;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;

                default:
                    Player.SendMessage(p, "Incorrect syntax"); Help(p);
                    return;
            }

            Player.SendMessage(p, "Spun: &b" + message);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spin <90/180/mirror/upsidedown> - Spins the copied object.");
            Player.SendMessage(p, "Shotcuts: m for mirror, u for upside down, x for spin 90 on x, z for spin 90 on z.");
        }
    }
}