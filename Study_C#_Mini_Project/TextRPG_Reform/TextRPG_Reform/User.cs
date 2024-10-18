using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    // 플레이어 클래스
    public class User : ICharacter, IUser
    {
        // ------------------ 캐릭터 인터페이스 공통 ------------------
        public String Name { get; set; } // 이름
        public String Tribe { get; set; } // 종족 : 인간, 고블린 등등
        public int Health { get; set; } // HP
        public bool IsDead { get; set; } // 생존여부
        public int Attack { get; set; } // 공격력


        public void TakeDamage(int damage) // 피해 입었을 때
        {
            Health -= damage;
            Console.WriteLine("플레이어가 {0}의 데미지를 입었습니다.", damage);
        }

        // ------------------ 유저 인터페이스 공통 ------------------
        public string UserClass { get; set; } // 직업
        public int Level { get; set; } // 레벨
        public int DefensivePower { get; set; } // 방어력
        public int Gold { get; set; } // 골드
        public int ClearCount { get; set; } // 던전 클리어 횟수

        public int EquipArmorStatusNum { get; set; } // 장착 갑옷 상태수치
        public int EquipWeaponStatusNum { get; set; } // 장착 갑옷 상태수치


        // ------------------ 전사 고유 ------------------
        //public event AttackHandle OnAttack; // 공격 델리게이트 트리거
        public void WarriorAttack(int Attack) // 유저가 공격할때
        {
            //OnAttack?.Invoke(Attack); // 공격 델리게이트 트리거 - 몬스터의 TakeDamage와 묶임
        }

        // 상태창 메소드
        public void State(User user, Item item)
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

                Console.WriteLine("방어력 : {0} (+ {1})", user.DefensivePower, user.EquipArmorStatusNum);
                Console.WriteLine("공격력 : {0} (+ {1})", user.Attack, user.EquipWeaponStatusNum);
                Console.WriteLine("체  력 : {0}", user.Health);
                Console.WriteLine("골  드 : {0} G", user.Gold);
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int select = InputCheck.Check(0, 0);

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

        public User()
        {
            this.Name = "홍길동";
            this.Tribe = "인간";
            this.UserClass = "전사";
            this.Health = 100;
            this.IsDead = false;
            this.Attack = 10;

            this.Level = 1;
            this.DefensivePower = 10;
            this.Gold = 1500;
            this.ClearCount = 0;

            this.EquipArmorStatusNum = 0; // 장착 갑옷 상태수치
            this.EquipWeaponStatusNum = 0; // 장착 갑옷 상태수치
        }
    }
}