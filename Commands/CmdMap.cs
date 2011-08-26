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
    public class CmdMap : Command
    {
        public override string name { get { return "map"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdMap() { }

        public override void Use(Player p, string message)
        {
            if (message == "") message = p.level.name;

            Level foundLevel;

            if (message.IndexOf(' ') == -1)
            {
                foundLevel = Level.Find(message);
                if (foundLevel == null)
                {
                    if (p != null)
                    {
                        foundLevel = p.level;
                    }
                }
                else
                {
                    Player.SendMessage(p, "MOTD: &b" + foundLevel.motd);
                    Player.SendMessage(p, "Finite mode: " + FoundCheck(foundLevel.finite));
                    Player.SendMessage(p, "Animal AI: " + FoundCheck(foundLevel.ai));
                    Player.SendMessage(p, "Edge water: " + FoundCheck(foundLevel.edgeWater));
                    Player.SendMessage(p, "Grass growing: " + FoundCheck(foundLevel.GrassGrow));
                    Player.SendMessage(p, "Physics speed: &b" + foundLevel.speedPhysics);
                    Player.SendMessage(p, "Physics overload: &b" + foundLevel.overload);
                    Player.SendMessage(p, "Survival death: " + FoundCheck(foundLevel.Death) + "(Fall: " + foundLevel.fall + ", Drown: " + foundLevel.drown + ")");
                    Player.SendMessage(p, "Killer blocks: " + FoundCheck(foundLevel.Killer));
                    Player.SendMessage(p, "Unload: " + FoundCheck(foundLevel.unload));
                    Player.SendMessage(p, "Auto physics: " + FoundCheck(foundLevel.rp));
                    Player.SendMessage(p, "Instant building: " + FoundCheck(foundLevel.Instant));
                    Player.SendMessage(p, "RP chat: " + FoundCheck(!foundLevel.worldChat));
                    return;
                }
            }
            else
            {
                foundLevel = Level.Find(message.Split(' ')[0]);

                if (foundLevel == null || message.Split(' ')[0].ToLower() == "ps" || message.Split(' ')[0].ToLower() == "rp") foundLevel = p.level;
                else message = message.Substring(message.IndexOf(' ') + 1);
            }

            if (p != null)
                if (p.group.Permission < LevelPermission.Operator) { Player.SendMessage(p, "Setting map options is reserved to OP+"); return; }

            string foundStart;
            if (message.IndexOf(' ') == -1) foundStart = message.ToLower();
            else foundStart = message.Split(' ')[0].ToLower();

            try
            {
                switch (foundStart)
                {
                    case "theme": foundLevel.theme = message.Substring(message.IndexOf(' ') + 1); foundLevel.ChatLevel("Map theme: &b" + foundLevel.theme); break;
                    case "finite": foundLevel.finite = !foundLevel.finite; foundLevel.ChatLevel("Finite mode: " + FoundCheck(foundLevel.finite)); break;
                    case "ai": foundLevel.ai = !foundLevel.ai; foundLevel.ChatLevel("Animal AI: " + FoundCheck(foundLevel.ai)); break;
                    case "edge": foundLevel.edgeWater = !foundLevel.edgeWater; foundLevel.ChatLevel("Edge water: " + FoundCheck(foundLevel.edgeWater)); break;
                    case "grass": foundLevel.GrassGrow = !foundLevel.GrassGrow; foundLevel.ChatLevel("Growing grass: " + FoundCheck(foundLevel.GrassGrow)); break;
                    case "ps":
                    case "physicspeed":
                        if (int.Parse(message.Split(' ')[1]) < 10) { Player.SendMessage(p, "Cannot go below 10"); return; }
                        foundLevel.speedPhysics = int.Parse(message.Split(' ')[1]);
                        foundLevel.ChatLevel("Physics speed: &b" + foundLevel.speedPhysics);
                        break;
                    case "overload":
                        if (int.Parse(message.Split(' ')[1]) < 500) { Player.SendMessage(p, "Cannot go below 500 (default is 1500)"); return; }
                        if (p.group.Permission < LevelPermission.Admin && int.Parse(message.Split(' ')[1]) > 2500) { Player.SendMessage(p, "Only SuperOPs may set higher than 2500"); return; }
                        foundLevel.overload = int.Parse(message.Split(' ')[1]);
                        foundLevel.ChatLevel("Physics overload: &b" + foundLevel.overload);
                        break;
                    case "motd":
                        if (message.Split(' ').Length == 1) foundLevel.motd = "ignore";
                        else foundLevel.motd = message.Substring(message.IndexOf(' ') + 1);
                        foundLevel.ChatLevel("Map MOTD: &b" + foundLevel.motd);
                        break;
                    case "death": foundLevel.Death = !foundLevel.Death; foundLevel.ChatLevel("Survival death: " + FoundCheck(foundLevel.Death)); break;
                    case "killer": foundLevel.Killer = !foundLevel.Killer; foundLevel.ChatLevel("Killer blocks: " + FoundCheck(foundLevel.Killer)); break;
                    case "fall": foundLevel.fall = int.Parse(message.Split(' ')[1]); foundLevel.ChatLevel("Fall distance: &b" + foundLevel.fall); break;
                    case "drown": foundLevel.drown = int.Parse(message.Split(' ')[1]) * 10; foundLevel.ChatLevel("Drown time: &b" + (foundLevel.drown / 10)); break;
                    case "unload": foundLevel.unload = !foundLevel.unload; foundLevel.ChatLevel("Auto unload: " + FoundCheck(foundLevel.unload)); break;
                    case "rp":
                    case "restartphysics": foundLevel.rp = !foundLevel.rp; foundLevel.ChatLevel("Auto physics: " + FoundCheck(foundLevel.rp)); break;
                    case "instant":
                        if (p.group.Permission < LevelPermission.Admin) { Player.SendMessage(p, "This is reserved for Super+"); return; }
                        foundLevel.Instant = !foundLevel.Instant; foundLevel.ChatLevel("Instant building: " + FoundCheck(foundLevel.Instant)); break;
                    case "chat":
                        foundLevel.worldChat = !foundLevel.worldChat; foundLevel.ChatLevel("RP chat: " + FoundCheck(!foundLevel.worldChat)); break;
                    default:
                        Player.SendMessage(p, "Could not find option entered.");
                        return;
                }
                foundLevel.changed = true;
                if (p.level != foundLevel) Player.SendMessage(p, "/map finished!");
            }
            catch { Player.SendMessage(p, "INVALID INPUT"); }
        }
        public string FoundCheck(bool check)
        {
            if (check) return "&aON";
            else return "&cOFF";
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/map [level] [toggle] - Sets [toggle] on [map]");
            Player.SendMessage(p, "Possible toggles: theme, finite, ai, edge, ps, overload, motd, death, fall, drown, unload, rp, instant, killer, chat");
            Player.SendMessage(p, "Edge will cause edge water to flow.");
            Player.SendMessage(p, "Grass will make grass not grow without physics");
            Player.SendMessage(p, "Finite will cause all liquids to be finite.");
            Player.SendMessage(p, "AI will make animals hunt or flee.");
            Player.SendMessage(p, "PS will set the map's physics speed.");
            Player.SendMessage(p, "Overload will change how easy/hard it is to kill physics.");
            Player.SendMessage(p, "MOTD will set a custom motd for the map. (leave blank to reset)");
            Player.SendMessage(p, "Death will allow survival-style dying (falling, drowning)");
            Player.SendMessage(p, "Fall/drown set the distance/time before dying from each.");
            Player.SendMessage(p, "Killer turns killer blocks on and off.");
            Player.SendMessage(p, "Unload sets whether the map unloads when no one's there.");
            Player.SendMessage(p, "RP sets whether the physics auto-start for the map");
            Player.SendMessage(p, "Instant mode works by not updating everyone's screens");
            Player.SendMessage(p, "Chat sets the map to recieve no messages from other maps");
        }
    }
}