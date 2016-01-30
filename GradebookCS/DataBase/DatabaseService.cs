using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GradebookCS.DataBase
{
    /// <summary>
    /// Service class that provides access to the database
    /// </summary>
    public class DatabaseService
    {
        /// <summary>
        /// Connection instance to access database
        /// </summary>
        public static SQLiteConnection _Connection;

        /// <summary>
        /// Creates a connection to the database and creates the necessary tables if they don't exist
        /// </summary>
        public static void LoadDatabase()
        {
            //todo put the database in the right folder

            //Create a new connection to the database file
            _Connection = new SQLiteConnection("GradebookSQLite.db");

            //String to create the course table if it doesn't exist yet
            string sql = @"CREATE TABLE IF NOT EXISTS
                                Course (Id          VARCHAR( 36 ) PRIMARY KEY NOT NULL,
                                        Name        VARCHAR( 10 ), 
                                        UsePercent  BOOLEAN,
                                        ARangeLow   DOUBLE,
                                        ARangeHigh   DOUBLE,
                                        BRangeLow   DOUBLE,
                                        BRangeHigh   DOUBLE,
                                        CRangeLow   DOUBLE,
                                        CRangeHigh   DOUBLE,
                                        NRRangeLow   DOUBLE,
                                        NRRangeHigh   DOUBLE
                            );";

            //create a statement object using the sql string
            using (var statement = _Connection.Prepare(sql))
            {
                statement.Step();   //then  step/execute the statement
            }

            //String to create the component Table if it doesn't exist
            sql = @"CREATE TABLE IF NOT EXISTS
                                Component (Id         VARCHAR( 36 ) PRIMARY KEY NOT NULL,
                                           Name       VARCHAR( 10 ),
                                           Weight     DOUBLE,
                                           CourseId   CHAR( 36 ),
                                           FOREIGN KEY(CourseId) REFERENCES Course(Id) ON DELETE CASCADE 
                            )";

            //create a statement object using the sql string
            using (var statement = _Connection.Prepare(sql))
            {
                statement.Step();   //then step/execute the statement
            }

            //String to create the Assignment Table if it doesn't exist
            sql = @"CREATE TABLE IF NOT EXISTS
                                Assignment (Id          VARCHAR( 36 ) PRIMARY KEY NOT NULL,
                                            Name        VARCHAR( 15 ),
                                            Score       DOUBLE,
                                            MaxScore    DOUBLE,
                                            ComponentId VARCHAR( 36 ),
                                            FOREIGN KEY(ComponentId) REFERENCES Component(Id) ON DELETE CASCADE
                            )";
            
            // String to turn on foreign keys
            sql = @"PRAGMA foreign_keys = ON";

            //Create a statement oject usign the sql string
            using (var statement = _Connection.Prepare(sql))
            {
                statement.Step();   //Step/execute the statement
            }
        }
    }
}
