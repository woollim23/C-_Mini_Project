using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    internal class TextRPG_Reform
    {
        static void Main(string[] args)
        {
            // ------------------- 게임 시작전 데이터 관리  -------------------
            // 객체생성
            GameManager gameManager = new GameManager();
            Inventory inventory = new Inventory();
            Store store = new Store();
            Action action = new Action();

            RPGUser user;
            Item gameItem;

            // 로딩창
            gameManager.LodingScreen();

            // 저장 파일이 있으면 불러오기
            if (File.Exists(gameManager.filePath1))
            {
                string jsonData1 = File.ReadAllText(gameManager.filePath1); // 유저 정보
                string jsonData2 = File.ReadAllText(gameManager.filePath2); // 아이템 정보

                // 객체 생성, 데이터 불러오기
                user = JsonConvert.DeserializeObject<RPGUser>(jsonData1);
                gameItem = JsonConvert.DeserializeObject<Item>(jsonData2);
            }
            else // 없으면 이름부터 적기
            {
                // 객체 생성
                user = new RPGUser();
                gameItem = new Item();
                gameItem.AddItem();
                // 최초 시작창
                gameManager.StartScreen(user);
            }

            // ------------------- 게임 플레이 -------------------
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"스파르타 마을에 오신 {user.Name} 님 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("6. 게임저장");
                Console.WriteLine("0. 종료");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck.Check(0, 6);
                switch (select)
                {
                    case 0:
                        // 게임종료
                        gameManager.GameSave(user, gameItem);
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("|                                    |");
                        Console.WriteLine("|     플레이 해주셔서 감사합니다!    |");
                        Console.WriteLine("|                                    |");
                        Console.WriteLine("--------------------------------------");
                        Environment.Exit(0);
                        break;
                    case 1:
                        // 상태창
                        user.State(user, gameItem);
                        break;
                    case 2:
                        // 인벤토리
                        inventory.SeeInventory(user, gameItem);
                        break;
                    case 3:
                        // 상점이용
                        if(user.UserClass == "좀도둑")
                            store.UseStore_Thief(user, gameItem);
                        else
                            store.UseStore(user, gameItem);
                        break;
                    case 4:
                        // 던전입장
                        action.Dungeon(user);
                        break;
                    case 5:
                        // 휴식이용
                        action.UseRest(user);
                        break;
                    case 6:
                        // 게임저장
                        gameManager.GameSave(user, gameItem);
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}
