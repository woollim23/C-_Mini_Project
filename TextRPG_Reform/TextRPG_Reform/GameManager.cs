using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    public class GameManager
    {
        // 파일 경로
        public string filePath1 = "TextRPG_Reform_User";
        public string filePath2 = "TextRPG_Reform_Item";

        // 로딩창 메소드
        public void LodingScreen()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|                                    |");
            Console.WriteLine("|             Wellcome!!             |");
            Console.WriteLine("|          Sparta  TextRPG           |");
            Console.WriteLine("|                                    |");
            Console.WriteLine("--------------------------------------");

            Thread.Sleep(1200);
            Console.Clear();
        }

        // 시작창 메소드
        public void StartScreen(RPGUser user)
        {
            // ------------------- 시작창 -------------------
            Console.WriteLine("[계정 생성]");
            Console.WriteLine("Sparta TextRPG 게임을 처음 시작합니다.");
            Console.WriteLine();
            // 닉네임 설정
            Console.WriteLine("환영합니다. 모험가님!");
            Console.WriteLine("사용하실 닉네임을 입력해주세요.");
            Console.WriteLine();
            Console.Write(">> ");
            user.Name = Console.ReadLine();

            ChoiceUserClass(user);
        }

        // 캐릭터선택창 메소드
        public void ChoiceUserClass(RPGUser user)
        {
            // ---------------- 캐릭터 직업 선택 -------------------
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[직업 선택]");
                // 종족 선택
                Console.WriteLine("직업을 선택해주세요.(해당 번호 입력)");
                Console.WriteLine();
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 좀도둑");
                Console.WriteLine();
                Console.Write(">> ");

                int select = InputCheck.Check(1, 2);
                switch (select)
                {
                    case 1:
                        user.UserClass = "전사";
                        break;
                    case 2:
                        user.UserClass = "좀도둑"; // 좀도둑의 공격력과 방어력은 전사보다 약함
                        user.Attack = 5;
                        user.Gold = 3000; // 대신 초기자금이 더 많음
                        user.DefensivePower = 5;
                        break;
                    default:
                        break;
                }

                if (select != -1) break;
            }
        }

        // 게임 데이터 저장 메소드
        public void GameSave(RPGUser user, Item gameItem)
        {
            Console.Clear();

            string jsonData1 = JsonConvert.SerializeObject(user, Formatting.Indented);
            File.WriteAllText(filePath1, jsonData1);

            string jsonData2 = JsonConvert.SerializeObject(gameItem, Formatting.Indented);
            File.WriteAllText(filePath2, jsonData2);

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|                                    |");
            Console.WriteLine("|         게임저장 완료!! ^0^/       |");
            Console.WriteLine("|                                    |");
            Console.WriteLine("--------------------------------------");
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}
