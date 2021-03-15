using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace VirtualCampaign.net {
    static class SQLManager {
        public const int LOAD_ARTICLE = 0, LOAD_ARTICLE_TAG = 1, LOAD_CHARACTER = 2, LOAD_CHARACTER_TAG = 3, LOAD_TRAIT = 4, LOAD_TALENT = 5, LOAD_SCHOOL = 6, LOAD_ABILITY = 7,
            LOAD_CLASSIC_CHARACTER_TAG = 8, LOAD_CLASSIC_CHARACTER = 9, LOAD_BANK_ITEM = 10, LOAD_ATLAS_TAG = 11, LOAD_MAGIC_EFFECT = 12, DONE = 100;

        static public MySqlConnection getConnection() {
            MySqlConnection conn;
            Console.Out.WriteLine("Connecting... ");
            
            string connectionString = @"Data Source='ayaseye.com';Database='ayaseyec_vcDat';User id='ayaseyec_uaccess';Password='-hC.gv^h_;3}';convert zero datetime=True";
            conn = new MySqlConnection(connectionString);

            conn.Open();
            Console.Out.WriteLine("Connected");

            return conn;
        }

        static public void closeConnection(MySqlConnection conn) {
            if(conn != null) {
                conn.Close();
            }
        }

        static public string buildQueryString(string table, string[] fields, string restrictions) {
            if (fields.Length == 0) return "";
            string query = "SELECT ";
            for(int i = 0; i < fields.Length; i++) {
                if (i > 0) query += ", ";
                query += "`" + fields[i] + "`";
            }
            query += " FROM " + filterForSQL(table);
            if(!String.IsNullOrWhiteSpace(restrictions)) query += " WHERE " + restrictions;
            query += ";";
            
            return query;
        }

        static public string buildDeleteString(string table, string restrictions) {
            if (string.IsNullOrWhiteSpace(restrictions)) return "";
            string query = "DELETE FROM " + filterForSQL(table) + " WHERE " + restrictions + " LIMIT 1;";

            return query;
        }

        static public string buildInsertString(string table, string[] fields, string[] values) {
            if(fields.Length == 0 || values.Length == 0) {
                Console.Out.WriteLine("Cannot perform SQL Insert operation with no fields or values");
                return "";
            }
            if(fields.Length != values.Length) {
                Console.Out.WriteLine("Cannot perform SQL Insert operation with mismatched field and value lengths");
                return "";
            }

            string result = "";

            result += "INSERT INTO `" + filterForSQL(table) + "` (";
            for(int i = 0; i < fields.Length; i++) {
                if(i > 0) {
                    result += ", ";
                }
                result += "`" + filterForSQL(fields[i]) + "`";
            }
            result += ") VALUES (";
            for(int i = 0; i < values.Length; i++) {
                if(i > 0) {
                    result += ", ";
                }
                if(values[i].Equals("NOW()")) {
                    result += "NOW()";
                } else {
                    result += "'" + filterForSQL(values[i]) + "'";
                }
            }

            result += ");";
            return result;
        }

        static public string buildUpdateString(string table, string[] fields, string[] values, string restrictions) {
            if (fields.Length == 0 || values.Length == 0) {
                Console.Out.WriteLine("Cannot perform SQL Update operation with no fields or values");
                return "";
            }
            if (fields.Length != values.Length) {
                Console.Out.WriteLine("Cannot perform SQL Update operation with mismatched field and value lengths");
                return "";
            }

            string result = "UPDATE " + filterForSQL(table) + " SET ";
            
            for(int i = 0; i < fields.Length; i++) {
                if(i > 0) {
                    result += ", ";
                }
                result += "`" + filterForSQL(fields[i]) + "` = ";
                if(values[i].Equals("NOW()")) {
                    result += "NOW()";
                } else {
                    result += "'" + filterForSQL(values[i]) + "'";
                }
            }

            result += " WHERE " + restrictions + ";";
            return result;
        }

        static public void runQuery(string table, string[] fields, string restrictions, SQLListener listener) {
            runQuery(table, fields, restrictions, listener, -1);
        }

        static public void runQuery(string table, string[] fields, string restrictions, SQLListener listener, int action) {
            MySqlConnection conn = getConnection();
            string query = buildQueryString(table, fields, restrictions);
            MySqlCommand command = new MySqlCommand(query, conn);
            
            MySqlDataReader reader = null;
            try {
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    Dictionary<String, Object> dictionary = new Dictionary<string, object>();
                    bool valid = false;
                    for (int i = 0; i < fields.Length; i++) {
                        valid = false;
                        for (int i2 = 0; i2 < reader.FieldCount; i2++) {
                            if (reader.GetName(i2).Equals(fields[i])) valid = true;
                        }
                        if (valid) {
                            dictionary.Add(fields[i], reader[fields[i]]);
                        }
                    }
                    listener.HandleData(action, dictionary);
                }
            } catch (MySql.Data.MySqlClient.MySqlException x) {
                Console.Error.WriteLine(x.Message);
            } finally {
                if (reader != null) {
                    reader.Close();
                    reader.Dispose();
                }
                if (command != null) {
                    command.Dispose();
                }
            }
            closeConnection(conn);
            listener.HandleData(DONE, null);
        }

        static public List<Dictionary<String, Object>> runImmediateQuery(string table, string[] fields, string restrictions) {
            MySqlConnection conn = getConnection();
            string query = buildQueryString(table, fields, restrictions);
            MySqlCommand command = new MySqlCommand(query, conn);
            List<Dictionary<String, Object>> result = new List<Dictionary<String, Object>>();
            MySqlDataReader reader = null;
            
            try {
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    bool valid = false;
                    Dictionary<String, Object> dictionary = new Dictionary<String, Object>();
                    for (int i = 0; i < fields.Length; i++) {
                        valid = false;
                        for (int i2 = 0; i2 < reader.FieldCount; i2++) {
                            if (reader.GetName(i2).Equals(fields[i])) valid = true;
                        }
                        if (valid) dictionary.Add(fields[i], reader[fields[i]]);
                    }
                    result.Add(dictionary);
                }
            } catch (MySql.Data.MySqlClient.MySqlException x) {
                Console.Error.WriteLine(x.Message);
            } finally {
                if (reader != null) { 
                    reader.Close();
                    reader.Dispose();
                }
                command.Dispose();
            }
            closeConnection(conn);
            return result;
        }

        static public bool runInsert(string table, string[] fields, string[] values) {
            long dummyOut;
            return runInsert(table, fields, values, out dummyOut);
        }

        static public bool runInsert(string table, string[] fields, string[] values, out long o) {
            MySqlConnection conn = getConnection();
            string query = buildInsertString(table, fields, values);
            MySqlCommand command = new MySqlCommand(query, conn);
            int rows = command.ExecuteNonQuery();
            o = command.LastInsertedId;
            command.Dispose();
            closeConnection(conn);
            return rows > 0;
        }

        static public void runUpdate(string table, string[] fields, string[] values, string restrictions) {
            MySqlConnection conn = getConnection();
            string query = buildUpdateString(table, fields, values, restrictions);
            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            command.Dispose();
            closeConnection(conn);
        }

        static public void runDelete(string table, string restrictions) {
            MySqlConnection conn = getConnection();
            string query = buildDeleteString(table, restrictions);
            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            command.Dispose();
            closeConnection(conn);
        }

        static public string filterForSQL(string inStr) {
            string outStr = inStr == null ? "" : inStr;
            outStr = outStr.Replace("'", "''").Replace("`", "``");
            return outStr;
        }
    }
}
