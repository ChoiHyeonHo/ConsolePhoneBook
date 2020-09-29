using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneBookManager manager = PhoneBookManager.CreateInstance(); //싱글톤 적용
            manager.Load();

            while (true)
            {
                manager.ShowMenu();
                int choice;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice > 5 || choice <= 0)
                    {
                        throw new Exception("1~5까지의 숫자로 입력해 주세요.");
                    }

                    switch (choice)
                    {
                        case 1: manager.InputData(); break;
                        case 2: manager.ListData(); break;
                        case 3: manager.SearchData(); break;
                        case 4: manager.DeleteData(); break;
                        case 5: Console.WriteLine("프로그램을 종료합니다.");
                            manager.Save();
                            return;

                    }
                }
                catch(Exception err)
                {
                    Console.WriteLine(err);
                }
            }
        }
    }
}
