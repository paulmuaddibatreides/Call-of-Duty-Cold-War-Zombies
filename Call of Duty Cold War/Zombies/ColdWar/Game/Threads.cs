using ColdWar.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static ColdWar.Memory.MemoryManager;
using static ColdWar.Memory.Vectors;

namespace ColdWar.Game
{
    public class Threads
    {

        private MemoryManager memoryManager;

        public static Vector3 ZmLocation = new Vector3();

        public static Vector3 P1Location = new Vector3();

        public Threads(MemoryManager memoryManager)
        {
            this.memoryManager = memoryManager;
        }

        public void Update_Thread()
        {

            //Main Threads
            var Threads = new Threads(GlobalMemoryManager.Instance);

            while (true)
            {
                settings.InGame = PlayerFunctions.GetPlayerName(0) != "UnnamedPlayer" && PlayerFunctions.GetPlayerName(0) != "";

                P1Location = memoryManager.ReadVec3(Offsets.PlayerPedPtr + (Offsets.PP_ArraySize_Offset * (ulong)0) + Offsets.PP_Coords);

                if (settings.InGame)
                {
                    PlayerFunctions.SetZMPos(0);

                    if (settings.MoneyLoop)
                    {
                        PlayerFunctions.SetPoints(0, 133700);
                    }

                    if (settings.TP_ZM)
                    {

                        memoryManager.WriteBytes(memoryManager.GameBase + Offsets.ZTeleport, new byte[]
                {
                    184,
                    0,
                    0,
                    128,
                    63,
                    137,
                    129,
                    212,
                    2,
                    0,
                    0,
                    184,
                    0,
                    0,
                    128,
                    63,
                    137,
                    129,
                    216,
                    2,
                    0,
                    0,
                    184,
                    0,
                    0,
                    128,
                    63,
                    137,
                    129,
                    220,
                    2,
                    0,
                    0,
                    199,
                    129,
                    144,
                    3,
                    0,
                    0,
                    1,
                    0,
                    0,
                    0,
                    144,
                    144,
                    144,
                    144,
                    144,
                    144,
                    144,
                    144,
                    144,
                    144,
                    144,
                    144,
                    144
                }, false);

                        memoryManager.WriteFloat(memoryManager.GameBase + Offsets.ZTeleport + 1UL, Threads.P1Location.X, false);
                        memoryManager.WriteFloat(memoryManager.GameBase + Offsets.ZTeleport + 12UL, Threads.P1Location.Y, false);
                        memoryManager.WriteFloat(memoryManager.GameBase + Offsets.ZTeleport + 23UL, Threads.P1Location.Z, false);
                    }

                    if (settings.cylce)
                    {
                        int playerShots = PlayerFunctions.GetPlayerShots(0);
                        int playerKills = PlayerFunctions.GetPlayerKills(0);
                        int num = playerKills - Form1.P1_CurrentKills;
                        bool flag = playerShots >= 5 || num >= 1;
                        if (flag)
                        {
                            Form1.P1_CurrentKills = PlayerFunctions.GetPlayerKills(0);
                            PlayerFunctions.SetPlayerShots(0, 0);
                            PlayerFunctions.GiveWeapon0(0, Form1.P1_CurrentCycle);
                            PlayerFunctions.GiveWeapon1(0, Form1.P1_CurrentCycle);
                            PlayerFunctions.GiveWeapon2(0, Form1.P1_CurrentCycle);
                            PlayerFunctions.GiveWeapon3(0, Form1.P1_CurrentCycle);
                            PlayerFunctions.GiveWeapon4(0, Form1.P1_CurrentCycle);
                            PlayerFunctions.GiveWeapon5(0, Form1.P1_CurrentCycle);
                            Form1.P1_CurrentCycle++;
                        }
                        
                    }


                    PlayerFunctions.SetGodMode(0, settings.GodMode);

                    if (settings.UnlimitedAmmo)
                    {
                        PlayerFunctions.UnlimitedAmmo(0);
                    }

                    if (settings.Speed)
                    {
                        PlayerFunctions.SetPlayerSpeed(0, 2.5f);
                    }
                }
                Thread.Sleep(50);
            }
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }
    }
}
