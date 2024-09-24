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
        // 상점 메소드
        static void UseStore(RPGUser user, Item gameItem)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("[상점]");
                Console.WriteLine("아이템을 구매, 판매할 수 있는 상점입니다.");

                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                for (int i = 0; gameItem.item[i] != null; i++)
                {
                    Console.Write($"-  {gameItem.item[i].name}\t| ");
                    if (gameItem.item[i].buy == true)
                        Console.Write("구매완료");
                    else
                        Console.Write($"{gameItem.item[i].price} G  ");
                    Console.Write($"\t| {gameItem.item[i].effect}+{gameItem.item[i].effectIfo}\t| {gameItem.item[i].func}");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("[보유 골드] : " + user.Gold + " G");
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck.Check(0, 2);

                switch (select)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        // 아이템 구매
                        BuyItem(user, gameItem);
                        break;
                    case 2:
                        // 아이템 판매
                        SellItem(user, gameItem);
                        break;
                    default:
                        continue;

                }
            }
        }

        // 아이템 구매 메소드
        static void BuyItem(RPGUser user, Item gameItem)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("[상점 - 아이템 구매]");
                Console.WriteLine("구매할 아이템을 선택하세요,");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                int itemCount = 1;
                for (int i = 0; gameItem.item[i] != null; i++)
                {
                    itemCount++;
                    Console.Write($"- {i + 1} ");
                    Console.Write($"{gameItem.item[i].name}\t| ");
                    if (gameItem.item[i].buy == true)
                        Console.Write("구매완료");
                    else
                        Console.Write($"{gameItem.item[i].price} G  ");
                    Console.Write($"\t| {gameItem.item[i].effect}+{gameItem.item[i].effectIfo}\t| {gameItem.item[i].func}");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("[보유 골드] : " + user.Gold + " G");
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck.Check(0, itemCount - 1);

                if (select == 0)
                    break;
                else if (select == -1)
                    continue;
                else
                {
                    // 수정 필요
                    if (gameItem.item[select - 1].buy == true)
                    {
                        Console.Clear();
                        Console.WriteLine("이미 구매한 아이템 입니다.");
                        Thread.Sleep(1200);
                    }
                    else
                    {
                        if (user.Gold >= gameItem.item[select - 1].price)
                        {
                            Console.Clear();
                            Console.WriteLine("구매를 완료했습니다.");
                            gameItem.item[select - 1].buy = true;
                            user.Gold -= gameItem.item[select - 1].price;
                            Thread.Sleep(1200);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다.");
                            Console.WriteLine($"[부족한 골드] : {gameItem.item[select - 1].price - user.Gold} G");
                            Thread.Sleep(1200);
                        }
                    }
                }

            }
        }

        // 아이템 판매
        static void SellItem(RPGUser user, Item gameItem)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("[상점 - 아이템 판매]");
                Console.WriteLine("판매할 아이템을 선택하세요.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                int itemCount = 1;
                for (int i = 0; gameItem.item[i] != null; i++)
                {
                    if (gameItem.item[i].buy == true)
                    {
                        gameItem.item[i].listNum = itemCount++;
                        Console.Write($"- {gameItem.item[i].listNum} ");
                        Console.Write($"{gameItem.item[i].name}\t| ");
                        Console.Write($"{gameItem.item[i].price} G  ");
                        Console.Write($"\t| {gameItem.item[i].effect}+{gameItem.item[i].effectIfo}\t| {gameItem.item[i].func}");
                        Console.WriteLine();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("[보유 골드] : " + user.Gold + " G");
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                itemCount -= 1;
                int select = InputCheck.Check(0, itemCount);

                if (select == 0)
                    break;
                else if (select == -1)
                    continue;
                else
                {
                    int nCnt = 0;
                    while (true) // 아이템 배열에서 선택된 아이템 찾는 중
                    {
                        if (select == gameItem.item[nCnt].listNum)
                            break;
                        else
                            nCnt++;
                    }

                    gameItem.item[nCnt].buy = false;
                    gameItem.item[nCnt].equip = false;

                    user.Gold += gameItem.item[nCnt].price;

                    Console.Clear();
                    Console.WriteLine("[판매 수익] : " + gameItem.item[nCnt].price + " G");
                    Thread.Sleep(1200);

                    for (int i = 0; gameItem.item[i] != null; i++)
                    {
                        gameItem.item[nCnt].listNum = -1;
                    }
                }
            }
        }

        // 휴식 메소드
        static void UseRest(RPGUser user)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("[휴식하기]");
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드] : " + user.Gold + " G");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck.Check(0, 1);

                switch (select)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        // 아이템 구매
                        if (user.Gold >= 500)
                        {
                            Console.Clear();
                            Console.WriteLine("휴식을 완료했습니다.");
                            user.Gold -= 500;
                            user.Health = 100; // 체력회복
                            Thread.Sleep(1200);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다.");
                            Console.WriteLine($"[부족한 골드] : {500 - user.Gold} G");
                            Thread.Sleep(1200);
                        }
                        break;
                    default:
                        continue;

                }
            }
        }

        // 던전 메소드
        static void Dungeon(RPGUser user)
        {
            bool exit = false;
            while (!exit)
            {
                int reward = 0; // 보상
                int recomDef = 0;  // 권장 방어력

                Console.Clear();

                Console.WriteLine("[던전]");
                Console.WriteLine("입장할 난이도를 선택해주세요.");
                Console.WriteLine();
                Console.WriteLine($"[레벨] : {user.Level} Lv  [방어력] : {user.DefensivePower}  [골드] : {user.Gold} G");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("[난이도]");
                Console.WriteLine();
                Console.WriteLine($"1. 쉬움    | 권장 방어력 : {user.DefensivePower - 5}    | 보상 : 1000 G");
                Console.WriteLine($"2. 적정    | 권장 방어력 : {user.DefensivePower}    | 보상 : 1700 G");
                Console.WriteLine($"3. 어려움  | 권장 방어력 : {user.DefensivePower + 5}    | 보상 : 2500 G");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck.Check(0, 3);

                Random random = new Random();
                // 던전 난이도에 따라 보상, 권장 난이도 설정
                switch (select)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        recomDef = user.DefensivePower - 5;
                        reward = 1000;

                        break;
                    case 2:
                        recomDef = user.DefensivePower;
                        reward = 1700;
                        break;
                    case 3:
                        // 40% 확률로 실패 계산, 성공해도 체력 차감에 따라 승패가 갈린다
                        if (random.Next(1, 100) <= 40)
                            user.IsDead = true;
                        recomDef = user.DefensivePower + 5;
                        reward = 2500;
                        break;
                    default:
                        continue;

                }

                if (exit == true) break;

                int gap = user.DefensivePower - recomDef; // 유저 방어력 - 권장 방어력

                user.Health -= random.Next((20 - gap), (35 - gap)); // 남는 체력 계산

                // 체력이 0 이하면 실패
                if (user.Health <= 0 || user.IsDead == true)
                {
                    // 죽으면 마을에서 부활
                    Console.Clear();
                    Console.WriteLine("던전 공략에 실패했습니다...");
                    Console.WriteLine("조금 뒤 마을에서 부활 합니다.");

                    user.Health = 100;
                    user.IsDead = false;

                    Thread.Sleep(1200);
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("던전을 클리어했습니다!");
                    Console.WriteLine($"[현재 체력] : {user.Health}");
                    Console.WriteLine();
                    Console.WriteLine("마을에서 휴식하기로 체력 회복 할 수 있습니다.");
                    user.ClearCount++;

                    // 보상 지급 - 난이도 별 차등 지급
                    user.Gold += (reward + (reward / 100 * random.Next(user.Attack, user.Attack * 2)));

                    Thread.Sleep(1200);
                    LevelUp(user); // 레벨업 유효 검사
                }
            }
        }


        // 레벨업 유효 검사 메소드
        static void LevelUp(RPGUser user)
        {
            if (user.Level == user.ClearCount)
            {
                user.Level++;
                user.ClearCount = 0;

                user.DefensivePower += 5; // 레벨업 할 때마다 방어력 5씩 증가
                user.Attack += 5; // 레벨업 할 때마다 공격력 5씩 증가

                Console.Clear();
                Console.WriteLine("축하합니다! 레벨업 했습니다.");
                Console.WriteLine($"[현재 레벨] : {user.Level} Lv");

                Thread.Sleep(1200);
            }
        }

        static void Main(string[] args)
        {
            // ------------------- 게임 시작전 데이터 관리  -------------------
            // 객체생성
            GameManager gameManager = new GameManager();
            Inventory inventory = new Inventory();
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
                        user.State(user);
                        break;
                    case 2:
                        // 인벤토리
                        inventory.SeeInventory(user, gameItem);
                        break;
                    case 3:
                        // 상점이용
                        UseStore(user, gameItem);
                        break;
                    case 4:
                        // 던전입장
                        Dungeon(user);
                        break;
                    case 5:
                        // 휴식이용
                        UseRest(user);
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
