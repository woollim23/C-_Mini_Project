namespace TextRPG_Reform
{
    public class Store
    {
        // 상점 이용 메소드
        public void UseStore(User user, Item gameItem)
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
        public void BuyItem(User user, Item gameItem)
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

        // 아이템 판매 메소드
        public void SellItem(User user, Item gameItem)
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

        // 상점 이용 메소드 - 도둑 이스터에그
        public void UseStore_Thief(User user, Item gameItem)
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
                Console.WriteLine("-1. 훔치기!!!");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck.Check(-1, 2);

                switch (select)
                {
                    case -1:
                        BuyItem_Thief(user, gameItem);
                        break;
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

        // 아이템 구매 메소드 - 도둑 이스터에그
        public void BuyItem_Thief(User user, Item gameItem)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("[상점 - 훔치는중!!]");
                Console.WriteLine("50퍼 확률로 훔치기를 시도하세요. ㅍvㅍ");
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
                        Console.WriteLine("이미 소유한 아이템 입니다.");
                        Thread.Sleep(1200);
                    }
                    else
                    {
                        Random rand = new Random();
                        if (rand.Next(1, 100) > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("훔치기 성공!!! 상점 주인은 아무 것도 못봤습니다. ㅍvㅍ");
                            gameItem.item[select - 1].buy = true;
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이런! 들켰네요.. 잡히기 전에 도망갑시다!!");
                            Thread.Sleep(1500);
                        }
                    }
                }

            }
        }
    }
}
