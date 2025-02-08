using ColdWar.Game;
using ColdWar.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColdWar.Memory;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using MetroFramework.Controls;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ColdWar
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private bool isDragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public Form1()
        {
            InitializeComponent();

            // Allocate a console
            AllocConsole();

            // Redirect console output to the new console
            Console.SetOut(
                new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

            // Setup event handlers for dragging the form
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        MemoryManager MEM = GlobalMemoryManager.Instance;
        private void ZUpdate()
        {
            ulong gameBase = MEM.GetGameBase();
            int gameSize = MEM.GetGameSize();

            Logger.Log($"Game Base: 0x{gameBase:X}");
            Logger.Log($"Game Size: 0x{gameSize:X}");

            if (gameBase == 0 || gameSize <= 0)
            {
                Logger.Log(
                    "Failed to get valid game base address or size. Game base or size is invalid.");
                return;  // Prevent further execution if the base or size is not valid
            }

            ulong endAddress;
            try
            {
                endAddress = checked(gameBase + (ulong)gameSize);  // Use checked to detect overflow
            }
            catch (OverflowException)
            {
                Logger.Log(
                    $"Address range overflow: Base 0x{gameBase:X} + Size 0x{gameSize:X}");
                return;  // Prevent further execution on overflow
            }


            if (endAddress <= gameBase)
            {
                Logger.Log(
                    $"Invalid address range: Start 0x{gameBase:X}, End 0x{endAddress:X}");
                return;  // Ensure that the end address is greater than the start address
            }

            // Scan for ZPlayerBase
            ulong zPlayerBaseOffset =
                MEM.PatternScanGame(
                    gameBase + 0x1, gameBase + (ulong)gameSize,
                    new byte[]{0x4C, 0x8D, 0x05, 0x00, 0x00, 0x00, 0x00, 0x41,
                       0x00, 0x8C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                       0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                       0x00, 0x00, 0x41, 0x00, 0x8C},
                    new string[]{"4C", "8D", "05", "?", "?", "?", "?",  "41", "?", "8C",
                         "?",  "?",  "?",  "?", "?", "?", "?",  "?",  "?", "?",
                         "?",  "?",  "?",  "?", "?", "?", "41", "?",  "8C"},
                    1) +
                3;

            if (MEM.IsValidAddr(zPlayerBaseOffset))
            {
                var offset = MEM.ReadInt32(zPlayerBaseOffset);
                Offsets.ZPlayerBase = zPlayerBaseOffset + (ulong)offset + 0x4 - gameBase;
                Logger.Log($"ZPlayerBase updated: 0x{Offsets.ZPlayerBase:X}");
            }
            else
            {
                Logger.Log("Failed to update ZPlayerBase.");
            }

            // Example for ZNoClipFunc
            ulong zNoClipFuncOffset = MEM.PatternScanGame(
                gameBase + 0x1, gameBase + (ulong)gameSize,
                new byte[]{0xF3, 0x0F, 0x11, 0x80, 0xE8, 0x0D, 0x00, 0x00, 0xF3, 0x0F,
                   0x10, 0x45, 0xA8, 0xF3, 0x0F, 0x11, 0x80},
                new string[]{"F3", "0F", "11", "80", "E8", "0D", "00", "00", "F3", "0F",
                     "10", "45", "A8", "F3", "0F", "11", "80"},
                1);

            if (MEM.IsValidAddr(zNoClipFuncOffset))
            {
                Offsets.ZNoClipFunc = zNoClipFuncOffset - gameBase;
                Logger.Log($"ZNoClipFunc updated: 0x{Offsets.ZNoClipFunc:X}");
            }
            else
            {
                Logger.Log("Failed to update ZNoClipFunc.");
            }


            ulong zNoClipDirOffset =
                MEM.PatternScanGame(
                    gameBase + 0x1, gameBase + (ulong)gameSize,
                    new byte[]{0x48, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0xE8, 0x00,
                       0x00, 0x00, 0x00, 0x80, 0x3E, 0x00},
                    new string[]{"48", "8B", "0D", "?", "?", "?", "?", "E8", "?", "?",
                         "?", "?", "", "80", "3E", "?"},
                    1) +
                3;

            if (MEM.IsValidAddr(zNoClipDirOffset))
            {
                var offset = MEM.ReadInt32(zNoClipDirOffset);
                Offsets.ZNoClipDir = zNoClipDirOffset + (ulong)offset + 0x4 - gameBase;
                Logger.Log($"ZNoClipDir updated: 0x{Offsets.ZNoClipDir:X}");
            }
            else
            {
                Logger.Log("Failed to update ZNoClipDir.");
            }

            ulong zTeleportOffset = MEM.PatternScanGame(
                gameBase + 0x1, gameBase + (ulong)gameSize,
                new byte[] { 0x8B, 0x83, 0x80, 0x06, 0x00, 0x00, 0x89, 0x81 },
                new string[] { "8B", "83", "80", "06", "00", "00", "89", "81" }, 1);

            if (MEM.IsValidAddr(zTeleportOffset))

            {
                Offsets.ZTeleport = zTeleportOffset - gameBase;
                Logger.Log($"ZTeleport updated: 0x{Offsets.ZTeleport:X}");
            }
            else
            {
                Logger.Log("Failed to update ZTeleport.");
            }

            ulong zShootOffset =
                MEM.PatternScanGame(
                    gameBase + 0x1, gameBase + (ulong)gameSize,
                    new byte[]{0xCC, 0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89,
                       0x74, 0x24, 0x10, 0x48, 0x89, 0x7C, 0x24, 0x18,
                       0x55, 0x41, 0x54, 0x41, 0x55, 0x41, 0x56, 0x41,
                       0x57, 0x48, 0x8D, 0x6C, 0x24, 0xA0},
                    new string[]{"CC", "48", "89", "5C", "24", "08", "48", "89",
                         "74", "24", "10", "48", "89", "7C", "24", "18",
                         "55", "41", "54", "41", "55", "41", "56", "41",
                         "57", "48", "8D", "6C", "24", "A0"},
                    1) +
                1;

            if (MEM.IsValidAddr(zShootOffset))

            {
                Offsets.ZShoot = zShootOffset - gameBase;
                Logger.Log($"ZShoot updated: 0x{Offsets.ZShoot:X}");
            }
            else
            {
                Logger.Log("Failed to update ZShoot.");
            }

            ulong zKillOffset =
                MEM.PatternScanGame(
                    gameBase + 0x1, gameBase + (ulong)gameSize,
                    new byte[]{0xE8, 0x00, 0x00, 0x00, 0x00, 0x41, 0xB9, 0x01,
                       0x00, 0x00, 0x00, 0xC6, 0x44, 0x24, 0x28, 0x00,
                       0x4C, 0x8B, 0xC3, 0xC6, 0x44, 0x24, 0x20, 0x01,
                       0xBA, 0x46, 0x13, 0x07, 0x52, 0x33, 0xC9},
                    new string[]{"E8", "00", "00", "00", "00", "41", "B9", "01",
                         "00", "00", "00", "C6", "44", "24", "28", "00",
                         "4C", "8B", "C3", "C6", "44", "24", "20", "01",
                         "BA", "46", "13", "07", "52", "33", "C9"},
                    1) +
                53;

            if (MEM.IsValidAddr(zKillOffset))

            {
                Offsets.ZKill = zKillOffset - gameBase;
                Logger.Log($"ZKill updated: 0x{Offsets.ZKill:X}");
            }
            else
            {
                Logger.Log("Failed to update ZKill.");
            }

            ulong zSeshStateOffset =
                MEM.PatternScanGame(gameBase + 0x1, gameBase + (ulong)gameSize,
                                    new byte[]{0x8B, 0x05, 0x00, 0x00, 0x00, 0x00, 0xC1,
                                       0xE0, 0x1C, 0xC1, 0xF8, 0x1C, 0xC3},
                                    new string[]{"8B", "05", "?", "?", "?", "?", "C1",
                                         "E0", "1C", "C1", "F8", "1C", "C3"},
                                    1) +
                2;

            if (MEM.IsValidAddr(zSeshStateOffset))

            {
                var offset = MEM.ReadInt32(zSeshStateOffset);
                Offsets.ZSeshState = zSeshStateOffset + (ulong)offset + 0x4 - gameBase;
                Logger.Log($"ZSeshState updated: 0x{Offsets.ZSeshState:X}");
            }
            else
            {
                Logger.Log("Failed to update ZSeshState.");
            }

            ulong zRoundOffset =
                MEM.PatternScanGame(gameBase + 0x1, gameBase + (ulong)gameSize,
                                    new byte[]{0x8B, 0x91, 0x20, 0x02, 0x00, 0x00, 0x8B,
                                       0xCA, 0x83, 0xE1},
                                    new string[]{"8B", "91", "20", "02", "00", "00",
                                         "8B", "CA", "83", "E1"},
                                    1) +
                0;

            if (MEM.IsValidAddr(zRoundOffset))
            {
                Offsets.ZRound = zRoundOffset - gameBase;
                Logger.Log($"ZRound updated: 0x{Offsets.ZRound:X}");
            }
            else
            {
                Logger.Log("Failed to update ZRound.");
            }

            //set the label text to the updated offsets

        }

        internal void UpdatePointers()
        {
            Offsets.PlayerCompPtr = MEM.ReadInt64(MEM.GameBase + Offsets.ZPlayerBase);
            Offsets.PlayerPedPtr = MEM.ReadInt64(MEM.GameBase + Offsets.ZPlayerBase + 0x8);
            Offsets.ZMGlobalBase = MEM.ReadInt64(MEM.GameBase + Offsets.ZPlayerBase) + 0x60;
            Offsets.ZMBotBase = MEM.ReadInt64(MEM.GameBase + Offsets.ZPlayerBase) + 0x68;
            Offsets.ZMBotListBase = MEM.ReadInt64(Offsets.ZMBotBase + Offsets.ZM_Bot_List_Offset);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string text = webClient.DownloadString("https://hyperhaxz.com/Products/PornHub/WZM.json");
                    //the text will have a json object with a "Weapons" array
                    JToken jtoken = JToken.Parse(text);
                    JToken jtoken2 = JToken.Parse(jtoken.SelectToken("Weapons").ToString());
                    JToken[] array = jtoken2.Children<JToken>().ToArray<JToken>();
                    List<string> list = new List<string>();
                    List<int> list2 = new List<int>();
                    foreach (JToken jtoken3 in array)
                    {
                        list.Add(jtoken3.SelectToken("Name").ToString());
                        list2.Add(int.Parse(jtoken3.SelectToken("ID").ToString()));
                    }
                    PlayerFunctions.GunNamesZM = list.ToArray();
                    PlayerFunctions.GunListZM = list2.ToArray();
                    PlayerFunctions.GunNames = PlayerFunctions.GunNamesZM;
                    PlayerFunctions.GunList = PlayerFunctions.GunListZM;
                    setupZMGuns();
                    Logger.Log("Successfully loaded weapons list from server.", ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to connect to the server. Please check your internet connection and try again.");
                    Application.ExitThread();
                    Close();
                }
               }

                ZUpdate();
            UpdatePointers();

            this.metroComboBox1.SelectedIndex = 0;

            var threads = new ColdWar.Game.Threads(GlobalMemoryManager.Instance);


            var updateThread = new Thread(threads.Update_Thread);
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate the current position of the mouse compared to the initial cursor position
                Point delta = new Point(Cursor.Position.X - dragCursorPoint.X, Cursor.Position.Y - dragCursorPoint.Y);

                // Set the new position of the form
                this.Location = new Point(dragFormPoint.X + delta.X, dragFormPoint.Y + delta.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Close();
        }

        private void PointsCheck_CheckedChanged(object sender, EventArgs e)
        {
            settings.MoneyLoop = PointsCheck.Checked;
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            settings.GodMode = metroCheckBox1.Checked;
        }

        private void metroCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            settings.Speed = metroCheckBox2.Checked;
        }


        public static int P1_CurrentCycle = 0;
        public static int P1_CurrentKills = 0;

        private void metroCheckBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            
            if (metroCheckBox3.Checked)
            {
                MEM.WriteBytes(MEM.GameBase + Offsets.ZTeleport, new byte[]
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
                Threads.ZmLocation = PlayerFunctions.GetLocation(0);
                MEM.WriteFloat(MEM.GameBase + Offsets.ZTeleport + 1UL, Threads.ZmLocation.X, false);
                MEM.WriteFloat(MEM.GameBase + Offsets.ZTeleport + 12UL, Threads.ZmLocation.Y, false);
                MEM.WriteFloat(MEM.GameBase + Offsets.ZTeleport + 23UL, Threads.ZmLocation.Z, false);
            }
            else
            {
                MEM.WriteBytes(MEM.GameBase + Offsets.ZTeleport, new byte[]
                {
                    139,
                    131,
                    128,
                    6,
                    0,
                    0,
                    137,
                    129,
                    212,
                    2,
                    0,
                    0,
                    243,
                    15,
                    16,
                    131,
                    132,
                    6,
                    0,
                    0,
                    243,
                    15,
                    17,
                    129,
                    216,
                    2,
                    0,
                    0,
                    243,
                    15,
                    16,
                    139,
                    136,
                    6,
                    0,
                    0,
                    243,
                    15,
                    17,
                    137,
                    220,
                    2,
                    0,
                    0,
                    199,
                    131,
                    188,
                    6,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    51,
                    201
                }, false);
            }
        }

        private void metroCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            settings.UnlimitedAmmo = metroCheckBox4.Checked;
        }

        public static ulong CodeCave = 0UL;

        public static ulong CodeCave2 = 0UL;

        private void metroCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            bool inGame = settings.InGame;
            if (inGame)
            {
                bool flag = Form1.CodeCave > 0UL;
                if (flag)
                {
                    MEM.WriteBytes(MEM.GameBase + Offsets.ZShoot, new byte[]
                    {
                        72,
                        137,
                        92,
                        36,
                        8
                    }, false);
                    MEM.WriteBytes(Form1.CodeCave, new byte[75], false);
                }
                Form1.CodeCave = MEM.FindCodeCave(MEM.GameBase + Offsets.ZShoot, MEM.GameBase + MEM.GameSize, 1000UL) + 100UL;
                Debug.WriteLine(Form1.CodeCave.ToString("X"));
                bool flag2 = Form1.CodeCave > Offsets.ZShoot;
                if (flag2)
                {
                    ulong num = MEM.GameBase + Offsets.ZShoot + 5UL - (Form1.CodeCave + 58UL) - 4UL;
                    MEM.WriteBytes(Form1.CodeCave, new byte[]
                    {
                        76,
                        139,
                        84,
                        36,
                        48,
                        77,
                        133,
                        210,
                        116,
                        28,
                        72,
                        184,
                        18,
                        120,
                        146,
                        247,
                        72,
                        86,
                        165,
                        118,
                        73,
                        57,
                        2,
                        117,
                        13,
                        72,
                        184,
                        158,
                        16,
                        187,
                        183,
                        141,
                        61,
                        byte.MaxValue,
                        123,
                        73,
                        137,
                        2,
                        72,
                        199,
                        68,
                        36,
                        56,
                        196,
                        9,
                        0,
                        0,
                        76,
                        137,
                        84,
                        36,
                        48,
                        72,
                        137,
                        92,
                        36,
                        8,
                        233,
                        35,
                        235,
                        239,
                        5
                    }, false);
                    MEM.WriteInt32(Form1.CodeCave + 58UL, (int)num, false);
                    ulong address = MEM.GameBase + Offsets.ZShoot;
                    byte[] array = new byte[5];
                    array[0] = 233;
                    MEM.WriteBytes(address, array, false);
                    MEM.WriteInt32(MEM.GameBase + Offsets.ZShoot + 1UL, -(int)num - 58 - 4, false);
                }
            }
        }

        void setupZMGuns()
        {
            this.metroComboBox1.Items.Clear();
            foreach (string item in PlayerFunctions.GunNames)
            {
                this.metroComboBox1.Items.Add(item);
            }
        }

        private void metroCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            bool switched = metroCheckBox6.Checked;
            settings.cylce = switched;
            if (switched)
            {
                Form1.P1_CurrentCycle = this.metroComboBox1.SelectedIndex;
                Form1.P1_CurrentKills = PlayerFunctions.GetPlayerKills(0);
                PlayerFunctions.SetPlayerShots(0, 0);
                PlayerFunctions.GiveWeapon0(0, this.metroComboBox1.SelectedIndex);
                PlayerFunctions.GiveWeapon1(0, this.metroComboBox1.SelectedIndex);
                PlayerFunctions.GiveWeapon2(0, this.metroComboBox1.SelectedIndex);
                PlayerFunctions.GiveWeapon3(0, this.metroComboBox1.SelectedIndex);
                PlayerFunctions.GiveWeapon4(0, this.metroComboBox1.SelectedIndex);
                PlayerFunctions.GiveWeapon5(0, this.metroComboBox1.SelectedIndex);
            } 
        }
    }
    public class GlobalMemoryManager
    {
        private static MemoryManager instance;

        // Lock synchronization object
        private static object syncLock = new object();

        // Constructor is 'protected'
        protected GlobalMemoryManager()
        {
        }

        public static MemoryManager Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new MemoryManager("BlackOpsColdWar");
                    }
                    return instance;
                }
            }
        }
    }

}
