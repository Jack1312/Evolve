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
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace MCLawl
{
    public class CmdServerReport : Command
    {
        public override string name { get { return "serverreport"; } }
        public override string shortcut { get { return "sr"; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        //public override bool consoleUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdServerReport() { }

        public override void Use(Player p, string message)
        {
            if (Server.PCCounter == null)
            {
                Player.SendMessage(p, "Starting PCCounter...one second");
                Server.PCCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                Server.PCCounter.BeginInit();
                Server.PCCounter.NextValue();
            }
            if (Server.ProcessCounter == null)
            {
                Player.SendMessage(p, "Starting ProcessCounter...one second");
                Server.ProcessCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
                Server.ProcessCounter.BeginInit();
                Server.ProcessCounter.NextValue();
            }

            TimeSpan tp = Process.GetCurrentProcess().TotalProcessorTime;
            TimeSpan up = (DateTime.Now - Process.GetCurrentProcess().StartTime);

            //To get actual CPU% is OS dependant
            string ProcessorUsage = "CPU Usage (Processes : All Processes):" + Server.ProcessCounter.NextValue() + " : " + Server.PCCounter.NextValue();
            //Alternative Average?
            //string ProcessorUsage = "CPU Usage is Not Implemented: So here is ProcessUsageTime/ProcessTotalTime:"+String.Format("00.00",(((tp.Ticks/up.Ticks))*100))+"%";
            //reports Private Bytes because it is what the process has reserved for itself and is unsharable
            string MemoryUsage = "Memory Usage: " + Math.Round((double)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576).ToString() + " Megabytes";
            string Uptime = "Uptime: " + up.Days + " Days " + up.Hours + " Hours " + up.Minutes + " Minutes " + up.Seconds + " Seconds";
            string Threads = "Threads: " + Process.GetCurrentProcess().Threads.Count;
            Player.SendMessage(p, Uptime);
            Player.SendMessage(p, MemoryUsage);
            Player.SendMessage(p, ProcessorUsage);
            Player.SendMessage(p, Threads);

        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/serverreport - Get server CPU%, RAM usage, and uptime.");
        }
    }
}
