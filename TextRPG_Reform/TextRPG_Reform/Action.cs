using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    public class Action
    {
        // 휴식 메소드
        public void UseRest(RPGUser user)
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
        public void Dungeon(RPGUser user)
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
        public void LevelUp(RPGUser user)
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
    }
}
