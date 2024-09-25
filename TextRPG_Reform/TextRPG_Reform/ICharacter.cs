using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{   // 캐릭터 인터페이스
    public interface ICharacter
    {
        String Name { get; } // 이름
        String Tribe { get; } // 종족 
        int Health { get; } // HP
        int Attack { get; } // 생존여부
        bool IsDead { get; } // 공격력
        void TakeDamage(int damage); // HP 차감 함수
    }
}
