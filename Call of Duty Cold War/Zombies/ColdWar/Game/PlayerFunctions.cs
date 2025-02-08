using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColdWar.Memory;
using static ColdWar.Memory.MemoryManager;
using static ColdWar.Memory.Vectors;


namespace ColdWar.Game
{
    internal class PlayerFunctions
    {
        private static readonly MemoryManager memoryManager = GlobalMemoryManager.Instance;

        private static void CheckInitialization()
        {
            if (memoryManager == null)
                throw new InvalidOperationException("Memory manager is not initialized.");

            if (Offsets.PlayerCompPtr == 0)
                throw new InvalidOperationException("Player component pointer is not set.");
        }

        public static string GetPlayerName(int ID)
        {
            CheckInitialization();

            try
            {
                ulong baseAddress = (ulong)Offsets.PlayerCompPtr;
                ulong offset = Offsets.PC_ArraySize_Offset * (ulong)ID + Offsets.PC_Name;
                return memoryManager.ReadAsciiString(baseAddress + offset, 15);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read player name: {ex.Message}");
                return "Error: Failed to read player name";
            }
        }

        public static void SetZMPos(int ID)
        {
            var PlayerLcoation = memoryManager.ReadVec3(Offsets.PlayerPedPtr + (Offsets.PP_ArraySize_Offset * (ulong)ID) + Offsets.PP_Coords);

            var HeadingXY = memoryManager.ReadVec2(Offsets.PlayerPedPtr + (Offsets.PP_ArraySize_Offset * (ulong)ID) + Offsets.PP_Heading_XY);

            var Radians = (Math.PI / 180) * (HeadingXY.X);

            float MX = 150 * (float)Math.Cos(Radians);
            float MY = 150 * (float)Math.Sin(Radians);

            Threads.ZmLocation = new Vector3(PlayerLcoation.X += MX, PlayerLcoation.Y += MY, PlayerLcoation.Z);
        }


        public static string GetPlayerGunID(int ID)
        {
            //Debug.WriteLine((Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_CurrentUsedWeaponID)).ToString("X"));
            return memoryManager.ReadInt16(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_CurrentUsedWeaponID)).ToString();
        }

        public static int GetPlayerShots(int ID)
        {
            return memoryManager.ReadInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_NumShots));
        }

        public static void SetPlayerShots(int ID, int Shots)
        {
            memoryManager.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_NumShots), Shots);
        }

        public static int GetPlayerKills(int ID)
        {
            return memoryManager.ReadInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_NumKills));
        }

        public static void SetPlayerKills(int ID, Int32 kills)
        {
            memoryManager.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_NumKills), kills);
        }

        public static void SetGodMode(int ID, bool GOD)
        {
            if (GOD)
            {
                memoryManager.WriteByte(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_GodMode), 0xA0);
                //Debug.WriteLine((Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_GodMode)).ToString("X"));
            }
            else
            {
                memoryManager.WriteByte(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_GodMode), 0x20);
            }
        }

        public static void SetPoints(int ID, int Points)
        {
            
            //if memoryManageroryManager is null, return
            if (memoryManager == null)
            {
                memoryManager.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Points), Points);
                return;
            }

            memoryManager.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Points), Points);
        }

        public static Vector3 GetLocation(int ID)
        {
            return memoryManager.ReadVec3(Offsets.PlayerCompPtr + Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + Offsets.PC_Vec3);
        }

        public static void SetRapidFire(int ID)
        {
            memoryManager.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_RapidFire1), 0);
            memoryManager.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_RapidFire2), 0);
        }

        public static void UnlimitedAmmo(int ID)
        {
            for (ulong i = 0; i < 5; i++)
            {
                memoryManager.WriteInt32((Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID)) + Offsets.PC_Ammo + (0x4 * i), 256);
            }
        }

        public static void SetAmmo(int ID, int ammount)
        {
            for (ulong i = 0; i < 2; i++)
            {
                memoryManager.WriteInt32((Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID)) + Offsets.PC_Ammo + (0x4 * i), ammount);
            }
        }

        public static void RemoveAmmo(int ID)
        {
            for (ulong i = 0; i < 5; i++)
            {
                memoryManager.WriteInt32((Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID)) + Offsets.PC_Ammo + (0x4 * i), 0);
            }
        }

        public static void SetPlayerSpeed(int ID, float Speed)
        {
            memoryManager.WriteFloat(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_RunSpeed), Speed);
        }

        public static void GiveWeapon0(int ID, int GunIndex)
        {
            ulong playerCompPtr = Offsets.PlayerCompPtr;
            ulong num = Offsets.PC_ArraySize_Offset * (ulong)((long)ID);
            ulong pc_SetWeaponID = Offsets.PC_SetWeaponID0;
            ulong pc_GunStruct = Offsets.PC_GunStruct;
            memoryManager.WriteInt64(playerCompPtr + (num + (pc_SetWeaponID + 0UL)), (long)PlayerFunctions.GunListZM[GunIndex], false);
            memoryManager.WriteByte(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + Offsets.PC_Camo0), 69, false);
        }

        public static void GiveWeapon1(int ID, int GunIndex)
        {
           memoryManager.WriteInt64(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + (Offsets.PC_SetWeaponID0 + Offsets.PC_GunStruct)), (long)PlayerFunctions.GunList[GunIndex], false);
           memoryManager.WriteByte(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + Offsets.PC_Camo1), 118, false);
        }

        public static void GiveWeapon2(int ID, int GunIndex)
        {
           memoryManager.WriteInt64(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + (Offsets.PC_SetWeaponID0 + Offsets.PC_GunStruct * 2UL)), (long)PlayerFunctions.GunList[GunIndex], false);
           memoryManager.WriteByte(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + Offsets.PC_Camo2), 118, false);
        }

        public static void GiveWeapon3(int ID, int GunIndex)
        {
           memoryManager.WriteInt64(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + (Offsets.PC_SetWeaponID0 + Offsets.PC_GunStruct * 3UL)), (long)PlayerFunctions.GunList[GunIndex], false);
        }

        public static void GiveWeapon4(int ID, int GunIndex)
        {
           memoryManager.WriteInt64(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + (Offsets.PC_SetWeaponID0 + Offsets.PC_GunStruct * 4UL)), (long)PlayerFunctions.GunList[GunIndex], false);
        }

        public static void GiveWeapon5(int ID, int GunIndex)
        {
           memoryManager.WriteInt64(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)((long)ID) + (Offsets.PC_SetWeaponID0 + Offsets.PC_GunStruct * 5UL)), (long)PlayerFunctions.GunList[GunIndex], false);
        }

        public static string[] GunNames = new string[]
{
            ""
};

        // Token: 0x040004CC RID: 1228
        public static int[] GunList = new int[1];

        // Token: 0x040004CD RID: 1229
        public static string[] GunNamesZM = new string[0];

        // Token: 0x040004CE RID: 1230
        public static int[] GunListZM = new int[0];

        // Token: 0x040004CF RID: 1231
        public static string[] GunNamesMP = new string[0];

        // Token: 0x040004D0 RID: 1232
        public static int[] GunListMP = new int[0];

    }
}
