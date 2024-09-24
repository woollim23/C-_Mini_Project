using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using Newtonsoft.Json;
using System.Data;
using System.Threading;

namespace TextRPG
{
    // 공격 델리게이트 선언 -> 얘가 여기 있어도 되나?
    public delegate void AttackHandle(int damage);

    internal class TextRPG
    {
        // 로딩창 메소드
        static void LodingScreen()
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
        static void StartScreen(RPGUser user)
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

        static void ChoiceUserClass(RPGUser user)
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

                int select = InputCheck(1, 2);
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
                        continue;
                        break;
                }

                if (select != -1) break;
            }
        }

        // 상태창 메소드
        static void State(RPGUser user)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("[상태보기]");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine("레  벨 : {0} Lv", user.Level);
                Console.Write("직  업 : {0}", user.UserClass);

                if (user.UserClass == "좀도둑")
                    Console.WriteLine("       ...왜 하필 이런 직업을... 특이하시군요. ㅍvㅍ");
                else
                    Console.Write("\n");

                Console.WriteLine("공격력 : {0}", user.Attack);
                Console.WriteLine("방어력 : {0}", user.DefensivePower);
                Console.WriteLine("체  력 : {0}", user.Health);
                Console.WriteLine("골  드 : {0} G", user.Gold);
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck(0, 0);

                switch (select)
                {
                    case 0:
                        exit = true;
                        break;
                    default:
                        continue;

                }
            }
        }

        // 인벤토리 메소드
        static void Inventory(RPGUser user, Store store)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("[인벤토리]");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                int itemCount = 1; // 보유 아이템 갯수
                for (int i = 0; store.item[i] != null; i++)
                {
                    if (store.item[i].buy == true)
                    {
                        Console.Write($"- {itemCount++} ");
                        Console.Write($"{store.item[i].name}");
                        if (store.item[i].equip == true)
                            Console.Write("[E]");
                        else
                            Console.Write("\t");
                        Console.Write($"\t| {store.item[i].effect} +{store.item[i].effectIfo}\t| {store.item[i].func}");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("[보유 골드] : " + user.Gold + " G");
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck(0, 1);

                switch (select)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        // 장착관리
                        ItemEquip(user, store);
                        break;
                    default:
                        continue;
                        continue;

                }
            }
        }

        // 장착관리 메소드
        static void ItemEquip(RPGUser user, Store store)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("[인벤토리 - 장착관리]");
                Console.WriteLine("보유 중인 아이템 장착을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                int itemCount = 1;
                for (int i = 0; store.item[i] != null; i++)
                {
                    if (store.item[i].buy == true)
                    {
                        store.item[i].listNum = itemCount++;
                        Console.Write($"- {store.item[i].listNum} ");
                        Console.Write($"{store.item[i].name}");
                        if (store.item[i].equip == true)
                            Console.Write("[E]");
                        else
                            Console.Write("\t");
                        Console.Write($"\t| {store.item[i].effect} +{store.item[i].effectIfo}\t| {store.item[i].func}");
                        Console.WriteLine();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck(0, itemCount - 1);

                if (select == 0)
                    break;
                else if (select == -1)
                    continue;
                else
                {
                    int nCnt = 0;
                    while (true) // 아이템 배열에서 선택된 아이템 찾는 중
                    {
                        if (select == store.item[nCnt].listNum)
                            break;
                        else
                            nCnt++;
                    }

                    // 장착 개선 
                    if (store.item[nCnt].equip == true)
                    {
                        // 장착 해제
                        store.item[nCnt].equip = false;
                        if (store.item[nCnt].effect == "방어력")
                        {
                            user.EquipArmor = false;
                        }
                        else if (store.item[nCnt].effect == "공격력")
                        {
                            user.EquipWeapon = false;
                        }
                    }
                    else
                    {
                        // 장착
                        // 이전 장착한 아이템에 관련된 후처리
                        for (int i = 0; store.item[i] != null; i++)
                        {
                            if(store.item[nCnt].effect == store.item[i].effect)
                                store.item[i].equip = false;
                        }
                        // 지금 장착한 아이템에 관련된 후처리
                        store.item[nCnt].equip = true;
                        if (store.item[nCnt].effect == "방어력")
                        {
                            user.EquipArmor = true;
                        }
                        else if (store.item[nCnt].effect == "공격력")
                        {
                            user.EquipWeapon = true;
                        }
                    }

                    for (int i = 0; store.item[i] != null; i++)
                    {
                        store.item[nCnt].listNum = -1;
                    }
                }
                
            }
        }

        // 상점 메소드
        static void UseStore(RPGUser user, Store store)
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

                for (int i = 0; store.item[i] != null; i++)
                {
                    Console.Write($"-  {store.item[i].name}\t| ");
                    if (store.item[i].buy == true)
                        Console.Write("구매완료");
                    else
                        Console.Write($"{store.item[i].price} G  ");
                    Console.Write($"\t| {store.item[i].effect}+{store.item[i].effectIfo}\t| {store.item[i].func}");
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

                int select = InputCheck(0, 2);

                switch (select)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        // 아이템 구매
                        BuyItem(user, store);
                        break;
                    case 2:
                        // 아이템 판매
                        SellItem(user, store);
                        break;
                    default:
                        continue;

                }
            }
        }

        // 아이템 구매 메소드
        static void BuyItem(RPGUser user, Store store)
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
                for (int i = 0; store.item[i] != null; i++)
                {
                    itemCount++;
                    Console.Write($"- {i + 1} ");
                    Console.Write($"{store.item[i].name}\t| ");
                    if (store.item[i].buy == true)
                        Console.Write("구매완료");
                    else
                        Console.Write($"{store.item[i].price} G  ");
                    Console.Write($"\t| { store.item[i].effect}+{ store.item[i].effectIfo}\t| { store.item[i].func}");
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

                int select = InputCheck(0, itemCount-1);

                if (select == 0)
                    break;
                else if (select == -1)
                    continue;
                else
                {
                    // 수정 필요
                    if (store.item[select-1].buy == true)
                    {
                        Console.Clear();
                        Console.WriteLine("이미 구매한 아이템 입니다.");
                        Thread.Sleep(1200);
                    }
                    else
                    {
                        if(user.Gold >= store.item[select - 1].price)
                        {
                            Console.Clear();
                            Console.WriteLine("구매를 완료했습니다.");
                            store.item[select - 1].buy = true;
                            user.Gold -= store.item[select - 1].price;
                            Thread.Sleep(1200);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다.");
                            Console.WriteLine($"[부족한 골드] : {store.item[select - 1].price - user.Gold} G");
                            Thread.Sleep(1200);
                        }
                    }
                }

            }
        }

        // 아이템 판매
        static void SellItem(RPGUser user, Store store)
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
                for (int i = 0; store.item[i] != null; i++)
                {
                    if (store.item[i].buy == true)
                    {
                        store.item[i].listNum = itemCount++;
                        Console.Write($"- {store.item[i].listNum} ");
                        Console.Write($"{store.item[i].name}\t| ");
                        Console.Write($"{store.item[i].price} G  ");
                        Console.Write($"\t| {store.item[i].effect}+{store.item[i].effectIfo}\t| {store.item[i].func}");
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
                int select = InputCheck(0, itemCount);

                if (select == 0)
                    break;
                else if (select == -1)
                    continue;
                else
                {
                    int nCnt = 0;
                    while (true) // 아이템 배열에서 선택된 아이템 찾는 중
                    {
                        if (select == store.item[nCnt].listNum)
                            break;
                        else
                            nCnt++;
                    }

                    store.item[nCnt].buy = false;
                    store.item[nCnt].equip = false;

                    user.Gold += store.item[nCnt].price;

                    Console.Clear();
                    Console.WriteLine("[판매 수익] : " + store.item[nCnt].price + " G");
                    Thread.Sleep(1200);

                    for (int i = 0; store.item[i] != null; i++)
                    {
                        store.item[nCnt].listNum = -1;
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

                int select = InputCheck(0, 1);

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

                int select = InputCheck(0, 3);

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
                if(user.Health <= 0 || user.IsDead == true)
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

        // 입력  유효 검사 메소드
        static int InputCheck(int start, int end)
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
            catch (Exception) // C#에 이미 있는 포맷 입력한 데이터 형식이 잘못 되었을 때 실행
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1200);
                return -1;
            }

            return select;
        }

        // 게임 데이터 저장 메소드
        static void GameSave(RPGUser user, Store store)
        {
            Console.Clear();
            string filePath1 = "TextRPG_Player";
            string filePath2 = "TextRPG_Item";

            string jsonData1 = JsonConvert.SerializeObject(user, Formatting.Indented);
            File.WriteAllText(filePath1, jsonData1);

            string jsonData2 = JsonConvert.SerializeObject(store, Formatting.Indented);
            File.WriteAllText(filePath2, jsonData2);

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|                                    |");
            Console.WriteLine("|         게임저장 완료!! ^0^/       |");
            Console.WriteLine("|                                    |");
            Console.WriteLine("--------------------------------------");
            Thread.Sleep(1000);
            Console.Clear();
        }

        static void Main(string[] args)
        {
            RPGUser user;
            Store store;
            // 저장 파일 이름
            string filePath1 = "TextRPG_Player";
            string filePath2 = "TextRPG_Item";

            LodingScreen();

            // 저장 파일이 있으면 불러오기
            if (File.Exists(filePath1))
            {
                string jsonData1 = File.ReadAllText(filePath1);
                string jsonData2 = File.ReadAllText(filePath2);

                // 객체 생성, 데이터 불러오기
                user = JsonConvert.DeserializeObject<RPGUser>(jsonData1);
                store = JsonConvert.DeserializeObject<Store>(jsonData2);
            }
            else // 없으면 이름부터 적기
            {
                // 객체 생성
                user = new RPGUser();
                store = new Store();
                // 최초 시작창
                StartScreen(user);
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

                int select = InputCheck(0, 6);
                switch (select)
                {
                    case 0:
                        // 게임종료
                        GameSave(user, store);
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("|                                    |");
                        Console.WriteLine("|     플레이 해주셔서 감사합니다!    |");
                        Console.WriteLine("|                                    |");
                        Console.WriteLine("--------------------------------------");
                        Environment.Exit(0);
                        break;
                    case 1:
                        // 상태창
                        State(user);
                        break;
                    case 2:
                        // 인벤토리
                        Inventory(user, store);
                        break;
                    case 3:
                        // 상점이용
                        UseStore(user, store);
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
                        GameSave(user, store);
                        break;
                    default:
                        continue;
                }

            }
        }
    }
}
