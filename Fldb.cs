using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;

namespace FLDVB
{
    class Fldb
    {
        private string db_name;
        private string final_db_directory;
        StreamWriter db_insert_info;


        public Fldb(string db_name)
        {
            this.db_name =  Directory.GetCurrentDirectory() + "\\" + db_name;

            if(!Directory.Exists(this.db_name))
            {
                Directory.CreateDirectory(this.db_name);
                Console.WriteLine("database create at " + this.db_name);
            }
            else
            {
                Console.WriteLine("Data base exists..");
            }
        }

        public void Create_table(string table_name, string[] collums, string references = null)
        {
            this.final_db_directory = this.db_name + "\\" + table_name + ".fldb";

            if (Directory.Exists(this.db_name) && !File.Exists(this.final_db_directory))
            {
                using (db_insert_info = File.CreateText(this.final_db_directory))
                {
                    db_insert_info.WriteLine("table_name: " + table_name);

                    if (references == null)
                        db_insert_info.WriteLine("ref: null");
                    else
                        db_insert_info.WriteLine("ref: " + references+".fldb");

                    db_insert_info.Write("[");
                    db_insert_info.Write("PrmID; ");
                    foreach (string collum in collums)
                    {
                        db_insert_info.Write(collum + "; ");
                    }
                    
                    if(references != null)
                    {
                        db_insert_info.Write("refID");
                    }

                    db_insert_info.Write("]");
                    db_insert_info.WriteLine();
                }

                Console.WriteLine("table \"{0}\" create sucess..", table_name);
            }
            else
            {
                Console.WriteLine("table \"{0}\" already exists..", table_name);
            }
        }


    }

}
