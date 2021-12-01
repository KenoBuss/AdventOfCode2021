using System;
using System.Data;
using System.IO;


namespace CodeofAdvent
{
    public static class utils
    {
        public static DataTable FileRead(string filePath)
        {
            DataTable dt = new DataTable(); //create a DataTable
            dt.Columns.Add(new DataColumn()); //add a DataColumn to dt
            List<string> fileInput = File.ReadAllLines(filePath).ToList(); //Read all rows of the input file and save them in a list<string>
            
            //for each element in the list add the value as int to dt
            for (int i = 0; i < fileInput.Count(); i++)
            {
                dt.Rows.Add(Convert.ToInt32(fileInput[i]));
            }
            return dt; // return dt;
        }
    }
    public class DayOne
    {
        static void Main()
        {
            DataTable dt = utils.FileRead("../../../../../InputFileDay1.txt"); //create a DataTable dt and use the filepath to the input file
            
            Console.WriteLine("The result of puzzle one is: " + count(dt)); //print result one on console

            DataTable dt2 = new DataTable(); //create a secound Datatable
            dt2.Columns.Add(new DataColumn()); //add a column to dt2

            //while dt has more or equals 3 rows
            do
            {
                //save the sum of row[0], row[1],row[2]
                int tempValue = Convert.ToInt32(dt.Rows[0][0])
                              + Convert.ToInt32(dt.Rows[1][0])
                              + Convert.ToInt32(dt.Rows[2][0]);

                dt2.Rows.Add(tempValue); //add tempValue to dt2 as new row
                dt.Rows.RemoveAt(0); //remove row[0] of dt
            } while (dt.Rows.Count >= 3);

            Console.WriteLine("The result of puzzle tow is: " + count(dt2)); //print result tow on console
            Console.ReadLine(); //keep window open
        }

        static int count(DataTable dt)
        {
            int count = 0; //create a counter which contains the result of the puzzles

            //loop throug each element of dt and check if it's larger/smaller as the previous
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                //it row i is larger then row i - 1 add +1 to the counter
                if (Convert.ToInt32(dt.Rows[i][0]) > Convert.ToInt32(dt.Rows[i - 1][0]))
                {
                    count++;
                }
            }
            return count;
        }
    }
}