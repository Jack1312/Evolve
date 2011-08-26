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

namespace MCLawl
{
    public static class c
    {
        public const string black = "&0";
        public const string navy = "&1";
        public const string green = "&2";
        public const string teal = "&3";
        public const string maroon = "&4";
        public const string purple = "&5";
        public const string gold = "&6";
        public const string silver = "&7";
        public const string gray = "&8";
        public const string blue = "&9";
        public const string lime = "&a";
        public const string aqua = "&b";
        public const string red = "&c";
        public const string pink = "&d";
        public const string yellow = "&e";
        public const string white = "&f";

        public static string Parse(string str)
        {
            switch (str.ToLower())
            {
                case "black": return black;
                case "navy": return navy;
                case "green": return green;
                case "teal": return teal;
                case "maroon": return maroon;
                case "purple": return purple;
                case "gold": return gold;
                case "silver": return silver;
                case "gray": return gray;
                case "blue": return blue;
                case "lime": return lime;
                case "aqua": return aqua;
                case "red": return red;
                case "pink": return pink;
                case "yellow": return yellow;
                case "white": return white;
                default: return "";
            }
        }
        public static string Name(string str)
        {
            switch (str)
            {
                case black: return "black";
                case navy: return "navy";
                case green: return "green";
                case teal: return "teal";
                case maroon: return "maroon";
                case purple: return "purple";
                case gold: return "gold";
                case silver: return "silver";
                case gray: return "gray";
                case blue: return "blue";
                case lime: return "lime";
                case aqua: return "aqua";
                case red: return "red";
                case pink: return "pink";
                case yellow: return "yellow";
                case white: return "white";
                default: return "";
            }
        }
    }
}