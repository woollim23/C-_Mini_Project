using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    public class Inventory
    {
        // 인벤토리 메소드
        public void SeeInventory(User user, Item gameItem)
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
                for (int i = 0; gameItem.item[i] != null; i++)
                {
                    if (gameItem.item[i].buy == true)
                    {
                        Console.Write($"- {itemCount++} ");
                        Console.Write($"{gameItem.item[i].name}");
                        if (gameItem.item[i].equip == true) // 장착하고 있다면 [E]
                            Console.Write("[E]");
                        else
                            Console.Write("\t");
                        Console.Write($"\t| {gameItem.item[i].effect} +{gameItem.item[i].effectIfo}\t| {gameItem.item[i].func}");
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

                int select = InputCheck.Check(0, 1);

                switch (select)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        // 장착관리
                        ItemEquip(user, gameItem);
                        break;
                    default:
                        break;
                }
            }
        }

        // 장착관리 메소드
        public void ItemEquip(User user, Item gameItem)
        {
            Inventory inventory = new Inventory();
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
                for (int i = 0; gameItem.item[i] != null; i++)
                {
                    if (gameItem.item[i].buy == true)
                    {
                        gameItem.item[i].listNum = itemCount++;
                        Console.Write($"- {gameItem.item[i].listNum} ");
                        Console.Write($"{gameItem.item[i].name}");
                        if (gameItem.item[i].equip == true)
                            Console.Write("[E]");
                        else
                            Console.Write("\t");
                        Console.Write($"\t| {gameItem.item[i].effect} +{gameItem.item[i].effectIfo}\t| {gameItem.item[i].func}");
                        Console.WriteLine();
                    }
                }

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
                    int nCnt = 0;
                    while (true) // 아이템 배열에서 선택된 아이템 찾는 중, nCnt에 해당 인덱스 저장
                    {
                        if (select == gameItem.item[nCnt].listNum)
                            break;
                        else
                            nCnt++;
                    }

                    // 장착 개선 
                    if (gameItem.item[nCnt].equip == true)
                    {
                        // 장착 해제
                        gameItem.item[nCnt].equip = false;
                        if (gameItem.item[nCnt].effect == "방어력")
                        {
                            user.EquipArmorStatusNum = 0;
                        }
                        else if (gameItem.item[nCnt].effect == "공격력")
                        {
                            user.EquipWeaponStatusNum = 0;
                        }
                    }
                    else
                    {
                        // 장착
                        // 이전 장착한 아이템에 관련된 후처리
                        for (int i = 0; gameItem.item[i] != null; i++)
                        {
                            // 장착외 아이템은 장착 해제 처리
                            if (gameItem.item[nCnt].effect == gameItem.item[i].effect && gameItem.item[i].equip == true)
                            {
                                gameItem.item[i].equip = false;
                                if (gameItem.item[nCnt].effect == "방어력")
                                {
                                    user.EquipArmorStatusNum = 0;
                                }
                                else if (gameItem.item[nCnt].effect == "공격력")
                                {
                                    user.EquipWeaponStatusNum = 0;
                                }
                            }
                        }
                        // EquipArmorStatusNum -> 유저가 장착한 방어구 수치 저장 변수
                        gameItem.item[nCnt].equip = true;
                        if (gameItem.item[nCnt].effect == "방어력")
                        {
                            user.EquipArmorStatusNum = gameItem.item[nCnt].effectIfo;
                        }
                        else if (gameItem.item[nCnt].effect == "공격력")
                        {
                            user.EquipWeaponStatusNum = gameItem.item[nCnt].effectIfo;
                        }
                    }

                    // 목록 번호 초기화
                    for (int i = 0; gameItem.item[i] != null; i++)
                    {
                        gameItem.item[nCnt].listNum = -1;
                    }
                }
            }
        }
    }
}
