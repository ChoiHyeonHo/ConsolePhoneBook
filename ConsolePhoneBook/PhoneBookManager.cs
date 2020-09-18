using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    public class PhoneBookManager
    {
        const int MAX_CNT = 100;
        PhoneInfo[] infoStorage = new PhoneInfo[MAX_CNT];
        int curCnt = 0;
        
        public void ShowMenu()
        {
            Console.WriteLine("------------------------------ 주소록 ----------------------------");
            Console.WriteLine("1. 입력    |   2. 목록   |   3. 검색   |   4.삭제    |   5. 종료");
            Console.WriteLine("------------------------------------------------------------------");
            Console.Write("선택: ");
        }

        public void InputData() // 1. 입력
        {
            Console.WriteLine("1. 일반\t 2. 대학\t 3. 회사");
            Console.Write("선택: "); //추가해야할 것. ☆
            int choice = int.Parse(Console.ReadLine());

            Console.Write("이름 입력: "); //Trimstart()문자열의 시작점, Trimend 문자열의 끝의 공백 제거, Replace(" ","") 스페이스 하나를 빈 문자열로 바꾼다.
            string name = Console.ReadLine().Trim().Replace(" ", "");
            // = if (name.Equals("")) // = if (name.Length < 1) // = if(name == "")
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("이름은 필수 입력입니다.");
                return;
            }
            else
            {
                int dataIndex = SearchName(name);
                if (dataIndex > -1)
                {
                    Console.WriteLine("이미 등록된 이름입니다. 다른 이름으로 입력하세요");
                    return;
                }
            }

            Console.Write("번호 입력: ");
            string phone = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(phone))
            {
                Console.WriteLine("전화번호는 필수 입력입니다.");
                return;
            }

            Console.Write("생일 입력: ");
            string birth = Console.ReadLine().Trim();

            if (choice == 1)
            {

                if (birth.Length < 1)
                    infoStorage[curCnt++] = new PhoneInfo(name, phone);
                else
                    infoStorage[curCnt++] = new PhoneInfo(name, phone, birth);
            }
            else if (choice == 2)
            {
                Console.Write("학과 입력: ");
                string major = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(major))
                {
                    Console.WriteLine("학과는 필수 입력입니다.");
                    return;
                }

                Console.Write("학년 입력: ");
                string year = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(year))
                {
                    Console.WriteLine("학과는 필수 입력입니다.");
                    return;
                }
                infoStorage[curCnt++] = new PhoneUnivInfo(name, phone, birth, major, year);
            }
        }

        public void ListData() // 2. 목록
        {
            int cnt = 1;

            if (curCnt == 0)
            {
                Console.WriteLine("입력된 데이터가 없습니다.");
                return;
            }
            for (int i = 0; i < curCnt; i++)
            {
                Console.Write($"{cnt}. ");
                //infoStorage[i].ShowPhoneInfo();
                infoStorage[i].ToString();
                ++cnt;
            }
            Console.WriteLine();
        }
        // 37.2
        public void SearchData() // 3. 검색
        {
            Console.WriteLine("주소록 검색을 시작합니다.");
            int dataIndex = SearchName();
            if (dataIndex < 0)
            {
                Console.WriteLine("검색된 데이터가 없습니다.");
            }
            else
            {
                infoStorage[dataIndex].ShowPhoneInfo();
            }

        }
        private int SearchName()
        {
            Console.Write("이름: ");
            string name = Console.ReadLine().Trim().Replace(" ", "");

            for (int i = 0; i < curCnt; i++)
            {
                if (infoStorage[i].Name.Replace(" ", "").CompareTo(name) == 0) // == , Equals(), CompareTo()
                {
                    return i;
                }
            }
            return -1;
        }

        private int SearchName(string name)
        {
            for (int i = 0; i < curCnt; i++)
            {
                if (infoStorage[i].Name.Replace(" ", "").CompareTo(name) == 0) // == , Equals(), CompareTo()
                {
                    return i;
                }
            }
            return -1;
        }

        public void DeleteData() // 4. 삭제
        {
            Console.WriteLine("주소록 삭제를 시작합니다.");

            int dataIndex = SearchName();
            if (dataIndex < 0)
            {
                Console.WriteLine("삭제할 데이터가 없습니다.");
            }
            else
            {
                for (int i = dataIndex; i < curCnt; i++)
                {
                    infoStorage[i] = infoStorage[i + 1];
                }
                curCnt--;
                Console.WriteLine("주소록 삭제가 완료되었습니다.");
            }
        }
    }
}
