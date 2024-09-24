using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    // 몬스터 클래스
    public class Monster : ICharacter
    {
        public String Name { get; set; } // 이름
        public String Tribe { get; set; } // 종족 : RPGUser or Monster
        public int Health { get; set; } // HP
        public bool IsDead { get; set; } // 생존여부
        public int Attack { get; set; } // 공격력

        public void TakeDamage(int damage) // 몬스터가 피해 입었을때
        {
            Health -= damage;
            Console.WriteLine("몬스터에게 {0}의 데미지를 입혔습니다.", damage);
        }

        public event AttackHandle OnAttack; // 공격 델리게이트 트리거
        public void MonsterAttack(int Attack) // 몬스터가 공격할때
        {
            OnAttack?.Invoke(Attack); // 공격 델리게이트 트리거 - 워리어의 TakeDamage와 묶임

        }
    }
}
