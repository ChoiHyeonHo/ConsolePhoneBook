using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    public class PhoneInfo
    {
        private string name; //필수
        private string phoneNumber; //필수
        private string birth; //선택

        public string Name 
        {
            get { return name; } 
            set { name = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string Birth
        {
            get { return birth; }
            set { birth = value; }
        }

        public PhoneInfo(string name, string phoneNumber)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.birth = null;
        }

        public PhoneInfo(string name, string phoneNumber, string birth)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.birth = birth;
        }

        public virtual void ShowPhoneInfo()//정보 출력, 상속시켜서 학교,회사명 등 넣기
        {
            if (birth != null)
                Console.WriteLine($"이름: {this.name} \t 번호: {this.phoneNumber} \t 생일: {this.birth}");
            else
                Console.WriteLine($"이름: {this.name} \t 번호: {this.phoneNumber}");
        }
        //ToString()를 override해서 PhoneManager에서 사용해보기
    }

    public class PhoneUnivInfo : PhoneInfo
    {
        public string major;
        public string Major { get; }

        public int year;
        public int Year { get; }


        public PhoneUnivInfo(string name, string phoneNumber, string birth, string major, int year) : base(name, phoneNumber, birth)
        {
            this.major = major; //필수
            this.year = year; //필수
        }


        //showphoneinfo 오버라이딩
        public override void ShowPhoneInfo()
        {
            Console.WriteLine($"이름: {Name} \t 번호: {PhoneNumber} \t 생일: {Birth} \t 학과: {major} \t 학년: {year}");
        }
    }

    public class PhoneCompanyInfo : PhoneInfo
    {
        string company;
        public PhoneCompanyInfo(string name, string phoneNumber, string birth, string company) : base( name, phoneNumber, birth)
        {
            this.company = company;
        }

        public string Company { get; set; }

        public override void ShowPhoneInfo()
        {
            Console.WriteLine($"이름: {Name} \t 번호: {PhoneNumber} \t 생일: {Birth} \t 회사: {company}");
        }
    }
}
