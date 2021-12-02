using System.Data;



namespace CodeofAdvent
{
    public static class utils
    {
        public static DataTable FileRead(string filePath)
        {
            DataTable dt = new DataTable(); //create a DataTable

            //add tow DataColumns to dt
            dt.Columns.Add(new DataColumn("Direction"));
            dt.Columns.Add(new DataColumn("Value"));
            List<string> fileInput = File.ReadAllLines(filePath).ToList(); //Read all rows of the input file and save them in a list<string>

            //for each element in the list add the value as int to dt
            for (int i = 0; i < fileInput.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Direction"] = fileInput[i].ToString().Split(' ')[0];
                dr["Value"] = Convert.ToInt32(fileInput[i].ToString().Split(' ')[1]);
                dt.Rows.Add(dr);
            }
            return dt; // return dt;
        }
    }
    public class DayOne
    {
        static void Main()
        {
            DataTable dt = utils.FileRead("../../../../InputFileDay2.txt"); //create a DataTable dt and use the filepath to the input file
            puzzleOne(dt);
            puzzleTow(dt);
        }

        public static void puzzleOne(DataTable dt)
        {
            //count all Values
            int forward = count(dt, "forward");
            int down = count(dt, "down");
            int up = count(dt, "up");
            int hight = down - up;

            //calculate result
            int result = forward * hight;
            Console.WriteLine("The result of puzzle one is: " + result);
        }

        static int count(DataTable dt, string key)
        {
            int count = 0; //create a counter which contains the result of the puzzles

            //loop throug each element of dt and check if direction == key
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == key)
                {
                    count += Convert.ToInt32(dt.Rows[i][1].ToString()); //Add Value to count
                }
            }
            return count; //return count
        }

        public static void puzzleTow(DataTable dt)
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            //loop throug each element of dt and switch into the depending case
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //save current value as X
                int X = Convert.ToInt32(dt.Rows[i][1].ToString());

                switch (dt.Rows[i][0].ToString())
                {
                    case "down":
                        aim += X;
                        break;

                    case "up":
                        aim -= X;
                        break;

                    case "forward":
                        horizontal += X;
                        if (aim != 0)
                        {
                            depth += (X * aim);
                        }
                        break;
                }
            }

            int result = horizontal * depth;
            Console.WriteLine("The result of puzzle tow is: " + result);
        }
    }
}