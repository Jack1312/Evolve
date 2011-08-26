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
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace MCLawl
{
    static class MySQL
    {
        private static string connString = "Data Source=" + Server.MySQLHost + ";Port=" + Server.MySQLPort + ";User ID=" + Server.MySQLUsername + ";Password=" + Server.MySQLPassword + ";Pooling=" + Server.MySQLPooling;

        public static void executeQuery(string queryString, bool createDB = false)
        {
            if (!Server.useMySQL) return;

            int totalCount = 0;
    retry:  try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    if (!createDB)
                    {
                        conn.ChangeDatabase(Server.MySQLDatabaseName);
                    }
                    MySqlCommand cmd = new MySqlCommand(queryString, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                if (!createDB)
                {
                    totalCount++;
                    if (totalCount > 10)
                    {
                        File.WriteAllText("MySQL_error.log", queryString);
                        Server.ErrorLog(e);
                    }
                    else
                    {
                        goto retry;
                    }
                }
                else
                {
                    throw e;
                }
            }
        }

        public static DataTable fillData(string queryString, bool skipError = false)
        {
            DataTable toReturn = new DataTable("toReturn");
            if (!Server.useMySQL) return toReturn;

            int totalCount = 0;
    retry:  try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    conn.ChangeDatabase(Server.MySQLDatabaseName);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(queryString, conn))
                    {
                        da.Fill(toReturn);
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                totalCount++;
                if (totalCount > 10)
                {
                    if (!skipError)
                    {
                        File.WriteAllText("MySQL_error.log", queryString);
                        Server.ErrorLog(e);
                    }
                }
                else
                    goto retry;
            }

            return toReturn;
        }
    }
}
