using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG.TextRPG;

namespace TextRPG
{
    // 플레이어 클래스
    public class RPGUser : ICharacter, IRPGUser
    {
        // 캐릭터 인터페이스 공통
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

        // 유저 인터페이스 공통
        public string UserClass { get; set; } // 직업
        public int Level { get; set; } // 레벨
        public int DefensivePower { get; set; } // 방어력
        public int Gold {  get; set; } // 골드
        public int ClearCount { get; set; } // 던전 클리어 횟수

        public bool EquipArmor { get; set; } // 장착 갑옷 이름
        public bool EquipWeapon { get; set; } // 장착 무기 이름


        // 전사 고유
        public event AttackHandle OnAttack; // 공격 델리게이트 트리거
        public void WarriorAttack(int Attack) // 유저가 공격할때
        {
            OnAttack?.Invoke(Attack); // 공격 델리게이트 트리거 - 몬스터의 TakeDamage와 묶임
        }


        public RPGUser()
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

            this.EquipArmor = true;
            this.EquipWeapon = true;
        }
    }
}
