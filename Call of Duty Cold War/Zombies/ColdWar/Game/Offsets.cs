using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColdWar.Game
{
    class Offsets
    {
        public static ulong PlayerCompPtr, PlayerPedPtr, ZMGlobalBase, ZMBotBase, ZMBotListBase;

        //Auto Updating Offsets -zqin
        public static ulong ZPlayerBase = 0x0;
        public static ulong ZTeleport = 0x0;
        public static ulong ZShoot = 0x0;
        public static ulong ZRoundSkip = 0x0;
        public static ulong ZRoundEntitiy = 0x0;
        public static ulong ZKill = 0x0;
        public static ulong ZXP = 0x0;
        public static ulong ZSeshState = 0x0;
        public static ulong ZClip_Func = 0x0;
        public static ulong ZClip_Dir = 0x0;
        public static ulong ZRound = 0x0;
        //public static ulong ZKillTrack = 0x0;//fuck this bitch
        //public static ulong KTArrayS = 0x148;//ZKillTrack Arraysize
        public static ulong EndMatchText = 0x1F0;
        public static ulong ZNoClipFunc = 0x0;
        public static ulong ZNoClipDir = 0x0;


        // PlayerComponent Offsets - PlayerCompPtr
        //Weapons stuff
        public static ulong PC_CurrentUsedWeaponID = 0x28; // current guns ID
        public static ulong PC_SetWeaponID0 = 0x70; //first gun, 40 bytes to the next gun
        public static ulong PC_GunStruct = 0x40;

        public static ulong PC_Ammo = 0x13DC; // struct size of 4
                                              //public static ulong PC_MaxAmmo = 0x1360; // +(1-5 * 0x8 for WP1 to WP6) (WP0 Mostly used in MP, ZM first WP is WP1 | WP3-6 Mostly used for Granades and Special) The Game assign the next Free WP Slot so WP1 is MainWeapon, you get a granade, then WP2 is the Granade, you buy a Weapon from wall then this is WP3 and so on..

        //other
        public static ulong PC_ArraySize_Offset = 0xB970;
        public static ulong PC_InfraredVision = 0xE66; // (byte) On=0x10|Off=0x0
        public static ulong PC_GodMode = 0xE67; // (byte) On=0xA0|Off=0x20
        public static ulong PC_RapidFire1 = 0xE6C; // Freeze to 0 how long you press Left Mouse-Key or Reloading and other stuff is not working.
        public static ulong PC_RapidFire2 = 0xE80; // Freeze to 0 how long you press Left Mouse-Key or Reloading and other stuff is not working.
        public static ulong PC_ReadyState1 = 0xE8; // extreme rapid fire / weapon ready state
        public static ulong PC_Points = 0x5D24; // ZM Points / Money
        public static ulong PC_Name = 0x5C1A; // Playername
        public static ulong PC_RunSpeed = 0x5C70; // (float)
        public static ulong PC_ClanTags = 0x605C; // Player Clan/Crew-Tag
        public static ulong PC_Vec3 = 0xDE8; // Real Player POS
        public static ulong PC_NumShots = 0xFE4; // number of shots from all weapons
        public static ulong PC_NumKills = 0x5d28; // number of shots from all weapons
        public static ulong PC_Camo0 = 0x80;
        public static ulong PC_Camo1 = 0xC0;
        public static ulong PC_Camo2 = 0x100;

        //comps i got from zombies
        public static ulong PC_TeamID = 0x220;
        public static ulong PC_EntType = 0x378;



        // PlayerPed Offsets - PlayerPedPtr
        public static ulong PP_ArraySize_Offset = 0x5E8; // ArraySize to next Player.
        public static ulong PP_Health = 0x390;
        public static ulong PP_Model = 0x68;
        public static ulong PP_MaxHealth = 0x39C; // Max Health dont increase by using Perk Juggernog
        public static ulong PP_Coords = 0x2D4; // Vector3
        public static ulong PP_Heading_Z = 0x34; // float
        public static ulong PP_Heading_XY = 0x34; // float | can be used to TP Zombies in front of you by your Heading Position and Forward Distance.

        // ZM Global Offsets - ZMGlobalBase
        // The Move Offset got removed with Patch 1.6.0 so Move Offset is no longer needed! Use 0x0 if you have add this Offset to your code... OLD: public int ZM_Global_MovedOffset = 0x2F20; // Since 1.5.0 The data got moved by this Offset so ZM_Global_MovedOffset + ZM_Global_ZombiesIgnoreAll is the corretly Offset to ZombiesIgnoreAll
        public static ulong ZM_Global_ZombiesIgnoreAll = 0x14; // Zombies Ignore any Player in the Lobby.
        public static ulong ZM_Global_ZMLeftCount = 0x3C; // Zombies Left

        // ZMBotBase
        public static ulong ZM_Bot_List_Offset = 0x8; // Offset to Pointer at ZMBotBase + 0x8 -> ZMBotListBase

        // ZMBotListBase
        public static ulong ZM_Bot_ArraySize_Offset = 0x5F8; // ArraySize to next Zombie.
        public static ulong ZM_Bot_Health = 0x398;
        public static ulong ZM_Bot_Model = 0x68;
        public static ulong ZM_Bot_MaxHealth = 0x39C;
        public static ulong ZM_Bot_Coords = 0x2D4; // Cam be used to Teleport all Zombies in front of any Player with a Heading Variable from the Players.
    }
}
