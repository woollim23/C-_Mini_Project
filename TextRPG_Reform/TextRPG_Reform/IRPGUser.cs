using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    // 유저 인터페이스
    public interface IRPGUser
    {
        public string UserClass { get; set; } // 직업
        public int Level { get; set; } // 레벨
        public int DefensivePower { get; set; } // 방어력
        public int Gold { get; set; } // 골드
        public int ClearCount { get; set; } // 던전 클리어 횟수
        public int EquipArmorStatusNum { get; set; } // 장착 갑옷 상태수치
        public int EquipWeaponStatusNum { get; set; } // 장착 갑옷 상태수치
    }
}