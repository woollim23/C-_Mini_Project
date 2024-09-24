using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace TextRPG
{
    // 공격 델리게이트 선언 -> 얘가 여기 있어도 되나?
    public delegate void AttackHandle(int damage);

    internal class TextRPG
    {
        // 상태창 메소드
        static void State(Warrior warrior)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("상태보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine("Lv. {0}", warrior.Level);
                Console.WriteLine("Chad ( {0} )", warrior.Tribe);
                Console.WriteLine("공격력 : {0}", warrior.Attack);
                Console.WriteLine("방어력 : {0}", warrior.DefensivePower);
                Console.WriteLine("체  력 : {0}", warrior.Health);
                Console.WriteLine("Gold : {0}", warrior.Gold);
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
        static void Inventory(Warrior warrior, Store store)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("인벤토리");
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
                Console.WriteLine("[보유 골드] : " + warrior.Gold + " G");
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
                        ItemEquip(warrior, store);
                        break;
                    default:
                        continue;
                        continue;

                }
            }
        }

        // 장착관리 메소드
        static void ItemEquip(Warrior warrior, Store store)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("인벤토리 - 장착관리");
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
                            warrior.EquipArmor = false;
                        }
                        else if (store.item[nCnt].effect == "공격력")
                        {
                            warrior.EquipWeapon = false;
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
                            warrior.EquipArmor = true;
                        }
                        else if (store.item[nCnt].effect == "공격력")
                        {
                            warrior.EquipWeapon = true;
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
        static void UseStore(Warrior warrior, Store store)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("상점");
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
                Console.WriteLine("[보유 골드] : " + warrior.Gold + " G");
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
                        BuyItem(warrior, store);
                        break;
                    case 2:
                        // 아이템 판매
                        SellItem(warrior, store);
                        break;
                    default:
                        continue;

                }
            }
        }

        // 아이템 구매 메소드
        static void BuyItem(Warrior warrior, Store store)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("상점 - 아이템 구매");
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
                Console.WriteLine("[보유 골드] : " + warrior.Gold + " G");
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
                        if(warrior.Gold >= store.item[select - 1].price)
                        {
                            Console.Clear();
                            Console.WriteLine("구매를 완료했습니다.");
                            store.item[select - 1].buy = true;
                            warrior.Gold -= store.item[select - 1].price;
                            Thread.Sleep(1200);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다.");
                            Console.WriteLine($"[부족한 골드] : {store.item[select - 1].price - warrior.Gold} G");
                            Thread.Sleep(1200);
                        }
                    }
                }

            }
        }

        // 아이템 판매
        static void SellItem(Warrior warrior, Store store)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("상점 - 아이템 판매");
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
                Console.WriteLine("[보유 골드] : " + warrior.Gold + " G");
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

                    warrior.Gold += store.item[nCnt].price;

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
        static void UseRest(Warrior warrior)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("휴식하기");
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드] : " + warrior.Gold + " G");
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
                        if (warrior.Gold >= 500)
                        {
                            Console.Clear();
                            Console.WriteLine("휴식을 완료했습니다.");
                            warrior.Gold -= 500;
                            Thread.Sleep(1200);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다.");
                            Console.WriteLine($"[부족한 골드] : {500 - warrior.Gold} G");
                            Thread.Sleep(1200);
                        }
                        break;
                    default:
                        continue;

                }
            }
        }

        // 던전 메소드
        static void Dungeon(Warrior warrior)
        {
            Console.Clear();
            Console.WriteLine("던전을 클리어했습니다.");
            warrior.ClearCount++;
            Thread.Sleep(1200);

            LevelUp(warrior); // 레벨업 유효 검사
        }

        // 레벨업 유효 검사 메소드
        static void LevelUp(Warrior warrior)
        {
            if (warrior.Level == warrior.ClearCount)
            {
                warrior.Level++;
                warrior.ClearCount = 0;

                Console.Clear();
                Console.WriteLine("축하합니다! 레벨업 했습니다.");
                Console.WriteLine($"[현재 레벨] : {warrior.Level} Lv");

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

        static void Main(string[] args)
        {
            // 객체 생성
            Warrior warrior = new Warrior();
            Monster monster = new Monster();
            Store store = new Store();

            // 델리게이트 기능 추가(연결)
            warrior.OnAttack += monster.TakeDamage;
            monster.OnAttack += warrior.TakeDamage;

            // ------------------- 시작창 -------------------
            Console.WriteLine("TextRPG 게임을 시작합니다.");
            // 닉네임 설정
            Console.WriteLine("닉네임을 입력해주세요.");
            warrior.Name = Console.ReadLine();

            // ---------------- 캐릭터 직업 선택 -------------------
            while (true)
            {
                Console.Clear();
                // 종족 선택
                Console.WriteLine("종족을 선택해주세요.(해당 번호 입력)");
                Console.WriteLine("1. 워리어");

                int select = InputCheck(1, 1);
                switch (select)
                {
                    case 1:
                        warrior.Tribe = "전사";
                        break;
                    default:
                        continue;
                        break;
                }

                if (select != -1) break; 
            }

            // ------------------- 게임 플레이 -------------------
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"스파르타 마을에 오신 {warrior.Name} 님 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("0. 게임종료");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck(0, 5);
                switch (select)
                {
                    case 0:
                        // 게임종료
                        Console.Clear();
                        Console.WriteLine("플레이해주셔서 감사합니다.");
                        Environment.Exit(0);
                        break;
                    case 1:
                        // 상태창
                        State(warrior);
                        break;
                    case 2:
                        // 인벤토리
                        Inventory(warrior, store);
                        break;
                    case 3:
                        // 상점이용
                        UseStore(warrior, store);
                        break;
                    case 4:
                        // 던전입장
                        Dungeon(warrior);
                        break;
                    case 5:
                        // 휴식이용
                        UseRest(warrior);
                        break;
                    default:
                        continue;
                }

            }
        }
    }
}
