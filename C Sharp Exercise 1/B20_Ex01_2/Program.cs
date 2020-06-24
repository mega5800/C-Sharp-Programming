using System;
using System.Text;

namespace B20_Ex01_2
{
    public class Program
    {
        // ENTERY POINT
        public static void Main()
        {
            runExercise2();
        }

        // RUN METHOD
        private static void runExercise2()
        {
            StringBuilder hourglassStringBuilder = new StringBuilder();
            string hourglassString = BuildRecursiveHourglass(hourglassStringBuilder, 0, 5).ToString();

            Console.WriteLine(hourglassString);
        }

        // STRING BUILDER FUNCTION
        public static StringBuilder BuildRecursiveHourglass(StringBuilder i_HourglassStringBuilder, int i_CurrentRowToPrint, int i_AmountOfRowsToPrint)
        {
            if (i_CurrentRowToPrint != i_AmountOfRowsToPrint)
            {
                if (i_CurrentRowToPrint < i_AmountOfRowsToPrint / 2)
                {
                    i_HourglassStringBuilder.Append(new string(' ', i_CurrentRowToPrint));
                    i_HourglassStringBuilder.AppendLine(new string('*', i_AmountOfRowsToPrint - (2 * i_CurrentRowToPrint)));
                }
                else
                {
                    i_HourglassStringBuilder.Append(new string(' ', i_AmountOfRowsToPrint - i_CurrentRowToPrint - 1));
                    i_HourglassStringBuilder.AppendLine(new string('*', (2 * i_CurrentRowToPrint) - i_AmountOfRowsToPrint + 2));
                }

                i_HourglassStringBuilder = BuildRecursiveHourglass(i_HourglassStringBuilder, i_CurrentRowToPrint + 1, i_AmountOfRowsToPrint);
            }

            return i_HourglassStringBuilder;
        }
    }
}