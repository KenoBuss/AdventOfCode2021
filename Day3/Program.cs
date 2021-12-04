using System.Data;

namespace CodeofAdvent
{
    public static class utils
    {
        public static DataTable FileRead(string filePath)
        {
            DataTable dt = new DataTable(); //create a DataTable
            dt.Columns.Add(new DataColumn("Value")); //add a DataColumn to dt
            List<string> fileInput = File.ReadAllLines(filePath).ToList(); //Read all rows of the input file and save them in a list<string>

            //for each element in the list add the value as int to dt
            for (int i = 0; i < fileInput.Count(); i++)
            {
                dt.Rows.Add(fileInput[i].ToString());
            }
            return dt; // return dt;
        }
    }
    public class DayThree
    {
        static void Main()
        {
            DataTable dt = utils.FileRead("../../../../InputFileDay3.txt"); //create a DataTable dt and use the filepath to the input file
            partOne(dt);
            partTow(dt);
        }
        public static void partOne(DataTable dt)
        {
            int bitAmount = dt.Rows[0][0].ToString().Length;
            int[] zeroCounts = new int[bitAmount];
            int[] oneCounts = new int[bitAmount];
            string gamma = "";
            string epsilon = "";
            int result = 0;

            //count the amount of the specified bit at position i and save them in an array
            for (int i = 0; i < bitAmount; i++)
            {
                zeroCounts[i] = getValueOfBit(dt, 0, i);
                oneCounts[i] = getValueOfBit(dt, 1, i);
            }

            //search for the most common bit at positon i and add it to gamma
            for (int i = 0; i < zeroCounts.Length; i++)
            {
                if (zeroCounts[i] < oneCounts[i]) { gamma += "1"; }
                else { gamma += "0"; }
            }

            //search for the least common bit at positon i and add it to epsilon
            for (int i = 0; i < zeroCounts.Length; i++)
            {
                if (zeroCounts[i] < oneCounts[i]) { epsilon += "0"; }
                else { epsilon += "1"; }
            }

            //convert gamma and epsilon into decimal and multiply them
            result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
            Console.WriteLine("The result of puzzle one: " + result);
        }

        public static void partTow(DataTable dt)
        {
            DataTable dtO2 = dt.Copy();
            DataTable dtCo2 = dt.Copy();


            int currentPosition = 0;
            do
            {
                
                int ones = getValueOfBit(dtO2, 1, currentPosition);
                int zeros = getValueOfBit(dtO2, 0, currentPosition);

                if(ones != zeros)
                {
                    //delete the most common bit at currentPosition
                    if (ones > zeros)
                    {
                        dtO2 = deleteRowsOfDt(dtO2, 0, currentPosition);
                    }
                    else
                    {
                        dtO2 = deleteRowsOfDt(dtO2, 1, currentPosition);
                    }
                }
                //delete value 0 at currentPosition
                else
                {
                    dtO2 = deleteRowsOfDt(dtO2, 0, currentPosition);
                }

                currentPosition++;
            } while (dtO2.Rows.Count > 1);

            currentPosition = 0;
            do
            {
                int ones = getValueOfBit(dtCo2, 1, currentPosition);
                int zeros = getValueOfBit(dtCo2, 0, currentPosition);

                if (ones != zeros)
                {
                    //delete the least common bit at currentPosition
                    if (ones < zeros)
                    {
                        dtCo2 = deleteRowsOfDt(dtCo2, 0, currentPosition);
                    }
                    else
                    {
                        dtCo2 = deleteRowsOfDt(dtCo2, 1, currentPosition);
                    }
                }
                //delete value 1 at currentPosition
                else
                {
                    dtCo2 = deleteRowsOfDt(dtCo2, 1, currentPosition);
                }

                currentPosition++;
            } while (dtCo2.Rows.Count > 1);


            //convert O2 binary to dec and Co2 binary to dec and multiply them
            int result = Convert.ToInt32(dtO2.Rows[0][0].ToString(), 2) * Convert.ToInt32(dtCo2.Rows[0][0].ToString(), 2);
            Console.WriteLine("The result of puzzle tow: " + result);
        }

        public static int getValueOfBit(DataTable dt, int value, int position)
        {
            int count = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string temp = dt.Rows[i][0].ToString();
                if (temp[position] == value.ToString()[0])
                {
                    count++;
                }
            }



            return count;
        }

        public static DataTable deleteRowsOfDt(DataTable dt, int value, int currentPosition)
        {
            foreach (DataRow dr in dt.Select())
            {
                if (dr["Value"].ToString()[currentPosition] == value.ToString()[0])
                {
                    dr.Delete();
                }
            }
            return dt;
        }
    }
}