using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FLDVB
{
    class Fldb
    {
        private string db_name;
        private string final_db_directory;
        StreamWriter db_insert_info;
        StreamReader db_read_info;
        
        List<String> database_in_primary_memory = new List<string>();
        List<String> relational_table = new List<string>();

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
                    //--------db insert---------
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

        public void Insert_values_in_table(string table_name, string[] values)
        {
            
        }

        public void View_table(string table_name)
        {
            if(!File.Exists(this.db_name + "\\" + table_name + ".fldb"))
            {
                Console.WriteLine("table is not exists");
            }
            else
            {
                string[] Table = File.ReadAllLines(this.db_name + "\\" + table_name + ".fldb");

                
                Console.WriteLine("=================show table======================");
                Console.WriteLine();
                foreach(string table in Table)
                {
                    Console.WriteLine(table);
                }

                Console.WriteLine("=================================================");
                Console.WriteLine();
            }


        }

    }

}
