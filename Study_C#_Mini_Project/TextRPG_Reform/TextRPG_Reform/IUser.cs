using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    // 유저 인터페이스
    public interface IUser
    {
        public string UserClass { get; } // 직업
        public int Level { get; } // 레벨
        public int DefensivePower { get; } // 방어력
        public int Gold { get; } // 골드
        public int ClearCount { get; } // 던전 클리어 횟수
        public int EquipArmorStatusNum { get; } // 장착 갑옷 상태수치
        public int EquipWeaponStatusNum { get; } // 장착 갑옷 상태수치
    }
}