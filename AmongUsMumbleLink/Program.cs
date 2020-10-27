
using HamsterCheese.AmongUsMemory;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using mumblelib;
using System.Reflection;
using Microsoft.Win32;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows;

namespace AmongUsMumbleLink
{

    class Program
    {

        public static bool Meeting = false;

        public static bool GameStart = false;

        public static bool CheckingShip = false;

        public static float playerX = 0;

        public static float playerY = 0;

        public static float playerID = 0;

        public static Vector2 nullvector2 = new Vector2(0,0);

        public static Avatar mumbleInfo = new Avatar();

        public static byte[] mumbleBytes;

        public static List<PlayerData> playerDatas = new List<PlayerData>();

        public static PlayerData localplayer = new PlayerData();

        public static ShipStatus ShipStatus = new ShipStatus();

        public static int loopnumber = 0;

        public static Vector2 pos = new Vector2();

        public static Paragraph sendconsole = new Paragraph();

        public static bool Init()
        {
            mumbleInfo.name = "Among Us";
            mumbleInfo.description = "Proximity chat program for Among Us by @jonnyrichardss";
            mumbleInfo.uiVersion = 2;
            
            byte[] mumblecontext = Encoding.ASCII.GetBytes("amongus");
            Array.Resize(ref mumblecontext, 256);
            mumbleInfo.context = mumblecontext;

            return Cheese.Init();
        }
        public static byte[] GetBytes(Avatar str)
        {
            // yoinked from https://stackoverflow.com/questions/3278827/how-to-convert-a-structure-to-a-byte-array-in-c
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
        public static void Update()
        {
            if (ShipStatus.instance.ToString() == "0")
            {
                GameStart = false;
                pos = nullvector2;
            }
            else
            {

                playerID = localplayer.Instance.PlayerId;
                GameStart = true;
                mumbleInfo.identity = playerID.ToString();
                pos = localplayer.GetMyPosition();

                if (Meeting)
                {
                    if (ShipStatus.MeetingSpawnCenter2.x == 0 && ShipStatus.MeetingSpawnCenter2.x == 0)
                    {
                        pos = ShipStatus.MeetingSpawnCenter;
                    }
                    else
                    {
                        pos = AveVec2(ShipStatus.MeetingSpawnCenter, ShipStatus.MeetingSpawnCenter2);
                    }
                }
            }
            playerX = pos.x;
            playerY = pos.y;
            Send(pos);
            loopnumber++;
            
            if (loopnumber >= 100)
            {
                CheckingShip = true;
                ShipStatus = Cheese.GetShipStatus();
                UpdatePlayerData();
                loopnumber = 0;
                CheckingShip = false;
            }

        }
        public static void Send(Vector2 pos)
        {
            mumbleInfo.uiTick++;
            float[] avatarpos = new float[3] { pos.x, pos.y, 0 };
            mumbleInfo.fAvatarPosition = avatarpos;
            mumbleInfo.fCameraPosition = mumbleInfo.fAvatarPosition;



            //convert for sending
            mumbleBytes = GetBytes(mumbleInfo);

            //send to mumble
            using (var file = MumbleLinkFile.CreateOrOpen())
            {
                file.write(mumbleBytes);
            }

        }
        public static float DistanceBetween(Vector2 vec1, Vector2 vec2)
        {
            Vector2 vecRes = new Vector2();
            vecRes.x = vec1.x - vec2.x;
            vecRes.y = vec1.y - vec2.y;
            return (float)(Math.Sqrt(vecRes.x * vecRes.x + vecRes.y * vecRes.y));
        }
        public static void UpdatePlayerData()
        {
            playerDatas = Cheese.GetAllPlayers();
            foreach (var data in playerDatas)
            {
                if (data.IsLocalPlayer)
                {
                    localplayer = data;
                }
            }
        }
        public static Vector2 AveVec2(Vector2 vec1, Vector2 vec2)
        {
            Vector2 VecRes = new Vector2();
            VecRes.x = (vec1.x + vec2.x) / 2;
            VecRes.y = (vec1.y + vec2.y) / 2;
            return VecRes;
        }

    }
}
