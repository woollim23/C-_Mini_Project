﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public interface ICharacter
    {
        String Name { get; set; } // 이름
        String Tribe { get; set; } // 종족 : Warrior or Monster
        int Health { get; set; } // HP
        int Attack { get; set; } // 생존여부
        bool IsDead { get; set; } // 공격력
        void TakeDamage(int damage); // HP 차감 함수
    }
}
