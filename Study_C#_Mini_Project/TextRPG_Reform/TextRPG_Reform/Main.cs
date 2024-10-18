using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Reform
{
    internal class TextRPG_Reform
    {
        static void Main(string[] args)
        {
            // ------------------- 게임 시작 전 데이터 관리  -------------------
            // 객체생성
            GameManager gameManager = new GameManager();

            User user;
            Item gameItem;

            // 로딩창
            gameManager.LodingScreen();

            // 저장 파일이 있으면 불러오기
            if (File.Exists(gameManager.filePath1))
            {
                string jsonData1 = File.ReadAllText(gameManager.filePath1); // 유저 정보
                string jsonData2 = File.ReadAllText(gameManager.filePath2); // 아이템 정보

                // 객체 생성, 데이터 불러오기
                user = JsonConvert.DeserializeObject<User>(jsonData1);
                gameItem = JsonConvert.DeserializeObject<Item>(jsonData2);
            }
            else // 없으면 이름부터 적기
            {
                // 객체 생성
                user = new User();
                gameItem = new Item();
                gameItem.AddItem();
                // 최초 시작창
                gameManager.StartScreen(user);
            }

            // ------------------- 게임 플레이 -------------------
            gameManager.GamePlay(user, gameItem);
        }
    }
}
