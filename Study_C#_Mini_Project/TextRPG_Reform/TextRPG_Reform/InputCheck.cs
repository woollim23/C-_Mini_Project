using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    static public class InputCheck
    {
        static public int Check(int start, int end)
        {
            int select = -1;
            try
            {
                select = int.Parse(Console.ReadLine());

                if (select < start || select > end)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1200);
                    return -1;
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1200);
                return -1;
            }

            return select;
        }
    }
}
