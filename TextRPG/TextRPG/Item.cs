using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    // 아이템 정보 클래스
    public class Item
    {
        public string name {  get; set; }
        public bool equip {  get; set; } // 장착 여부
        public bool buy { get; set; } // 구매 여부
        public string effect { get; set; } // 효과
        public int effectIfo { get; set; }  // 효과 수치
        public string func { get; set; } // 기능
        public int price { get; set; }
        public int listNum {  get; set; } // 목록 번호 

        public Item(string name, bool equip, bool buy, string effect, int effectInfo, string func, int price)
        {
            this.name = name;
            this.equip = equip; 
            this.buy = buy;
            this.effect = effect;
            this.effectIfo = effectInfo;
            this.func = func;
            this.price = price;
            this.listNum = 0;
        }

        public Item()
        {
            this.name = "void";
            this.equip = false;
            this.buy = false;
            this.effect = "void";
            this.effectIfo = 0;
            this.func = "void";
            this.price = 0;
            this.listNum = 0;
        }

    }
}
