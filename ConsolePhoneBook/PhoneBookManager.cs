using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsolePhoneBook
{
    public class PhoneBookManager
    {
        static PhoneBookManager instance;
        private PhoneBookManager()
        {

        }

        public static PhoneBookManager CreateInstance()
        {
            if (instance == null)
            {
                instance = new PhoneBookManager();
            }

            return instance;
        }

        //const int MAX_CNT = 100;
        //PhoneInfo[] infoStorage = new PhoneInfo[MAX_CNT];
        HashSet<PhoneInfo> infoStorage = new HashSet<PhoneInfo>();
        //int curCnt = 0;
        string errmessage = "정보가 없거나 미흡합니다. 다시 확인해 주세요.";

        public void ShowMenu()
        {
            Console.WriteLine("------------------------ 주소록 -------------------------");
            Console.WriteLine("1. 입력  |  2. 목록  |  3. 검색  |  4. 삭제  |  5. 종료  |");
            Console.WriteLine("---------------------------------------------------------");
            Console.Write("선택: ");
        }

        BinaryFormatter serializer = new BinaryFormatter();
        public void Load()
        {
            //PhoneInfo[] newinfo = new PhoneInfo[1];
            if (File.Exists("list.bin"))
            {
                FileStream fs = new FileStream("list.bin", FileMode.OpenOrCreate);
                infoStorage = (HashSet<PhoneInfo>)serializer.Deserialize(fs);
                //curCnt = newinfo.Length;
                //Array.Copy(newinfo, infoStorage, curCnt);
                Console.WriteLine("로드 성공");
                fs.Close();
            }
        }
        // 본래 있던 파일은  PhoneInfo[] 이기때문에 HashSet<PhoneInfo> 변환 x 본래의 파일을 지우고
        // 삭제된 파일에 값이없으니 오류 null
        // load시 파일의 nu.ll 값이 있는지 없는지확인휴 값이 있으면 실행  fs.Length ==0 

        public void Save()
        {
            //PhoneInfo[] infos = new PhoneInfo[curCnt];
            //Array.Copy(infoStorage, infos, curCnt);
            FileStream fs = new FileStream("list.bin", FileMode.Create);
            serializer.Serialize(fs, infoStorage);
            Console.WriteLine("저장 성공");
            fs.Close();
        }


        public void InputData()
        {
            Console.WriteLine("1.일반  2.대학  3.회사");
            Console.Write("선택 >> ");
            int choice;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice))
                    break;
            }
            if (choice < 1 || choice > 3)
            {
                Console.WriteLine("1.일반  2.대학  3.회사 중에 선택하십시오.");
                return;
            }

            PhoneInfo info = null;
            switch (choice)
            {
                case 1:
                    info = InputFriendInfo();
                    break;
                case 2:
                    info = InputUnivInfo();
                    break;
                case 3:
                    info = InputCompanyInfo();
                    break;
            }
            if (info != null)
            {
                infoStorage.Add(info);
                Console.WriteLine("데이터 입력이 완료되었습니다");
            }
        }

        private string[] InputCommonInfo()
        {
            Console.Write("이름: ");
            string name;//if (name == "") or if (name.Length < 1) or if (name.Equals(""))
            try
            {
                name = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("이름은 필수입력입니다");
                    return null;
                }
                else
                {
                    int dataIdx = SearchName(name);
                    if (dataIdx > -1)
                    {
                        Console.WriteLine("이미 등록된 이름입니다. 다른 이름으로 입력하세요");
                        return null;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            Console.Write("전화번호: ");
            string phone;
            string birth;
            string[] arr = new string[3];
            try
            {
                phone = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(phone))
                {
                    Console.WriteLine("전화번호는 필수입력입니다");
                    return null;
                }
                Console.Write("생일: ");
                birth = Console.ReadLine().Trim();

                arr[0] = name;
                arr[1] = phone;
                arr[2] = birth;

                return arr;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        private PhoneInfo InputFriendInfo()
        {
            string[] comInfo = InputCommonInfo();
            if (comInfo == null || comInfo.Length != 3)
                throw new Exception(errmessage);

            return new PhoneInfo(comInfo[0], comInfo[1], comInfo[2]);
        }

        private PhoneInfo InputUnivInfo()
        {
            string[] comInfo = InputCommonInfo();
            if (comInfo == null || comInfo.Length != 3)
                throw new Exception(errmessage);

            string major;
            int year;
            try
            {
                Console.Write("전공: ");
                major = Console.ReadLine().Trim();

                Console.Write("학년: ");
                year = int.Parse(Console.ReadLine().Trim());

                return new PhoneUnivInfo(comInfo[0], comInfo[1], comInfo[2], major, year);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        private PhoneInfo InputCompanyInfo()
        {
            string[] comInfo = InputCommonInfo();
            if (comInfo == null || comInfo.Length != 3)
                throw new Exception(errmessage);
            string company;
            try
            {
                Console.Write("회사명: ");
                company = Console.ReadLine().Trim();

                return new PhoneCompanyInfo(comInfo[0], comInfo[1], comInfo[2], company);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public void ListData()
        {
            if (infoStorage.Count == 0)
            {
                Console.WriteLine("입력된 데이터가 없습니다.");
                return;
            }

            foreach (var item in infoStorage)
            {
                Console.WriteLine(item);
            }
            //for (int i = 0; i < infoStorage.Count; i++)
            //{
            //    Console.WriteLine(infoStorage.ToString());
            //}

            Console.WriteLine("정렬하시겠습니까? (예: 1 / 아니오: 2)");
            int lineUp;

            try
            {
                lineUp = int.Parse(Console.ReadLine());
                LineUp(lineUp);
            }
            catch
            {
                throw new Exception("1~4까지의 숫자로 입력해 주세요.");
            }
        }

        private void LineUp(int lineUp)
        {
            if (lineUp == 1)
            {
                Console.WriteLine("1.이름(오름)  2.이름(내림)  3.전화번호(오름)  4.전화번호 (내림)");
                Console.Write("선택 : ");

                int pick = 0;
                //PhoneInfo[] new_arr;


                pick = int.Parse(Console.ReadLine());
                //new_arr = new PhoneInfo[curCnt];
                PhoneInfo[] copyInfo = infoStorage.ToArray<PhoneInfo>();
                //Array.Copy(infoStorage, copyinfoSto, infoStorage.Count);
                if (pick == 1)
                {
                    Array.Sort(copyInfo);
                }
                else if (pick == 2)
                {
                    Array.Sort(copyInfo);
                    Array.Reverse(copyInfo);
                }
                else if (pick == 3)
                {
                    Array.Sort(copyInfo, new PhoneComparator());
                }
                else if (pick == 4)
                {
                    Array.Sort(copyInfo, new PhoneComparator());
                    Array.Reverse(copyInfo);
                }
                if (pick > 4 || pick <= 0)
                {
                    throw new Exception();
                }

                for (int i = 0; i < copyInfo.Length; i++)
                {
                    Console.WriteLine(copyInfo[i].ToString());
                }

            }
        }

        public void SearchData()
        {
            Console.WriteLine("주소록 검색을 시작합니다......");
            PhoneInfo[] copyInfo = infoStorage.ToArray<PhoneInfo>();
            int dataIdx = SearchName();
            if (dataIdx < 0)
            {
                Console.WriteLine("검색된 데이터가 없습니다");
            }
            else
            {
                copyInfo[dataIdx].ShowPhoneInfo();
                Console.WriteLine();
            }

            #region 모두 찾기
            //int findCnt = 0;
            //for(int i=0; i<curCnt; i++)
            //{
            //    // ==, Equals(), CompareTo()
            //    if (infoStorage[i].Name.Replace(" ","").CompareTo(name) == 0)
            //    {
            //        infoStorage[i].ShowPhoneInfo();
            //        findCnt++;
            //    }
            //}
            //if (findCnt < 1)
            //{
            //    Console.WriteLine("검색된 데이터가 없습니다");
            //}
            //else
            //{
            //    Console.WriteLine($"총 {findCnt} 명이 검색되었습니다.");
            //}
            #endregion
        }

        private int SearchName()
        {
            string name;
            try
            {
                Console.Write("이름: ");
                name = Console.ReadLine().Trim().Replace(" ", "");
                PhoneInfo[] copyInfo = infoStorage.ToArray<PhoneInfo>();
                //infoStorage.ElementAt(i);

                for (int i = 0; i < copyInfo.Length; i++)
                {
                    if (copyInfo[i].Name.Replace(" ", "").CompareTo(name) == 0)
                    {
                        return i;
                    }
                }

                return -1;
            }
            catch
            {
                throw new Exception(errmessage);
            }
        }

        private int SearchName(string name)
        {
            PhoneInfo[] copyInfo = infoStorage.ToArray<PhoneInfo>();
            for (int i = 0; i < copyInfo.Length; i++)
            {
                if (copyInfo[i].Name.Replace(" ", "").CompareTo(name) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public void DeleteData()
        {
            try
            {
                Console.WriteLine("주소록 삭제를 시작합니다......");

                int dataIdx = SearchName();
                if (dataIdx < 0)
                {
                    Console.WriteLine("삭제할 데이터가 없습니다");
                }
                else
                {
                    infoStorage.Remove(infoStorage.ElementAt(dataIdx));

                    Console.WriteLine("주소록 삭제가 완료되었습니다");
                }
            }
            catch
            {
                throw new Exception(errmessage);
            }
        }
    }
}
