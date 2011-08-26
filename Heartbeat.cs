/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCLawl/Evolve) 
    Dual-Licensed under the Educational Community License, Version 2.0 and
    the binpress license you mayy not use this file except in compliance with 
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
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Collections;

namespace MCLawl
{

    public static class Heartbeat
    {

        static string hash;
        public static string serverURL;
        static string staticVars;

        static HttpWebRequest request;
        static StreamWriter beatlogger;

        static System.Timers.Timer heartbeatTimer = new System.Timers.Timer(500);

        public static void Init()
        {
            if (Server.logbeat)
            {
                if (!File.Exists("heartbeat.log"))
                {
                    File.Create("heartbeat.log").Close();
                }
            }
            staticVars = "port=" + Server.port +
                            "&max=" + Server.players +
                            "&name=" + UrlEncode(Server.name) +
                            "&public=" + Server.pub +
                            "&version=" + Server.version;

            Thread backupThread = new Thread(new ThreadStart(delegate
            {
                heartbeatTimer.Elapsed += delegate
                {
                    heartbeatTimer.Interval = 60000;
                    try
                    {
                        if (Server.pub == true)
                        {
                            Pump(Beat.Minecraft);
                            Pump(Beat.Evolve);
                        }
                        else
                            Pump(Beat.Minecraft);

                    }
                    catch (Exception e) { Server.ErrorLog(e); }
                };
                heartbeatTimer.Start();
            }));
            backupThread.Start();
        }

        public static bool Pump(Beat type)
        {
            string postVars = staticVars;

            string url = "http://www.minecraft.net/heartbeat.jsp";
            int totalTries = 0;
        retry: try
            {
                int hidden = 0;
                foreach (Player p in Player.players)
                {
                    if (p.hidden)
                    {
                        hidden++;
                    }
                }
                totalTries++;
                switch (type)
                {
                    case Beat.Minecraft:
                        if (Server.logbeat)
                        {
                            beatlogger = new StreamWriter("heartbeat.log", true);
                        }
                        postVars += "&salt=" + Server.salt;
                        goto default;
                    case Beat.Evolve:
                        postVars = "sname=" + Server.name
                            + "&maxp=" + Server.players
                            + "&nowp=" + (Player.number - hidden)
                            + "&URL=" + Server.URL.Trim()
                            + "&version=" + Server.Version;
                        url = "http://Evolve.x10.mx/hbannounce.php";
                        break;
                    default:
                        postVars += "&users=" + (Player.number - hidden);
                        break;

                }

                request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                byte[] formData = Encoding.ASCII.GetBytes(postVars);
                request.ContentLength = formData.Length;
                request.Timeout = 15000;

            retryStream: try
                {
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(formData, 0, formData.Length);
                        if (type == Beat.Minecraft && Server.logbeat)
                        {
                            beatlogger.WriteLine("Request sent at " + DateTime.Now.ToString());
                        }
                        requestStream.Close();
                    }
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.Timeout)
                    {
                        if (type == Beat.Minecraft && Server.logbeat)
                        {
                            beatlogger.WriteLine("Timeout detected at " + DateTime.Now.ToString());
                        }
                        goto retryStream;
                    }
                    else if (type == Beat.Minecraft && Server.logbeat)
                    {
                        beatlogger.WriteLine("Non-timeout exception detected: " + e.Message);
                        beatlogger.Write("Stack Trace: " + e.StackTrace);
                    }
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(response.GetResponseStream()))
                    {
                        if (hash == null)
                        {
                            string line = responseReader.ReadToEnd();
                            if (type == Beat.Minecraft && Server.logbeat)
                            {
                                beatlogger.WriteLine("Response received at " + DateTime.Now.ToString());
                                beatlogger.WriteLine("Received: " + line);
                            }
                            hash = line.Substring(line.LastIndexOf('=') + 1);
                            string newhash = hash.Trim();
                            String newUrl = "http://www.minecraft.net/play.jsp?server=" + newhash;
                            Server.URL = newhash;
                            Server.s.UpdateUrl(newUrl);
                            File.WriteAllText("text/externalurl.txt", newUrl);
                            Server.s.Log("URL found: " + newUrl);
                        }
                        else if (type == Beat.Minecraft && Server.logbeat)
                        {
                            beatlogger.WriteLine("Response received at " + DateTime.Now.ToString());
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    if (type == Beat.Minecraft && Server.logbeat)
                    {
                        beatlogger.WriteLine("Timeout detected at " + DateTime.Now.ToString());
                    }
                    Pump(type);
                }
            }
            catch
            {
                if (type == Beat.Minecraft && Server.logbeat)
                {
                    beatlogger.WriteLine("Heartbeat failure #" + totalTries + " at " + DateTime.Now.ToString());
                }
                if (totalTries < 3) goto retry;
                if (type == Beat.Minecraft && Server.logbeat)
                {
                    beatlogger.WriteLine("Failed three times.  Stopping.");
                    beatlogger.Close();
                }
                return false;
            }
            finally
            {
                request.Abort();
            }
            if (beatlogger != null)
            {
                beatlogger.Close();
            }
            return true;
        }

        public static string UrlEncode(string input)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] >= '0' && input[i] <= '9') ||
                    (input[i] >= 'a' && input[i] <= 'z') ||
                    (input[i] >= 'A' && input[i] <= 'Z') ||
                    input[i] == '-' || input[i] == '_' || input[i] == '.' || input[i] == '~')
                {
                    output.Append(input[i]);
                }
                else if (Array.IndexOf<char>(reservedChars, input[i]) != -1)
                {
                    output.Append('%').Append(((int)input[i]).ToString("X"));
                }
            }
            return output.ToString();
        }
        public static char[] reservedChars = { ' ', '!', '*', '\'', '(', ')', ';', ':', '@', '&',
                                                 '=', '+', '$', ',', '/', '?', '%', '#', '[', ']' };
    }

    public enum Beat { Minecraft, Evolve }
}
