using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ColdWar.Memory.Vectors;

namespace ColdWar.Memory
{
    public class MemoryManager
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, ulong lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, ulong lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CloseHandle(IntPtr hObject);


        private const uint PROCESS_VM_READ = 0x0010;
        private const uint PROCESS_VM_WRITE = 0x0020;
        private const uint PROCESS_VM_OPERATION = 0x0008;
        private IntPtr processHandle = IntPtr.Zero;
        private Process process;
        public byte[] GameBuffer;

        public MemoryManager(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                process = processes[0];
                processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);
                GameBase = GetGameBase();
                GameSize = (ulong)GetGameSize();
            }
            else
            {
                throw new Exception($"No processes with the name {processName} were found.");
            }
        }

        public ulong GameBase { get; private set; }

        public ulong GameSize { get; private set; }

        public ulong LastProcessId { get; private set; }

        public string LastProcessName { get; private set; }

        public static MemoryManager MEM;


        private bool SetGame(string GameName)
        {
            bool flag = GameName == "";
            if (flag)
            {
                throw new ArgumentNullException("", "Game is null");
            }
            Process[] processesByName = Process.GetProcessesByName(GameName);
            bool flag2 = processesByName.Length != 0;
            bool result;
            if (flag2)
            {
                this.LastProcessId = (ulong)((long)processesByName[0].Id);
                this.LastProcessName = GameName;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool IsValidAddr(ulong Address)
        {
            return Address != 0UL && Address > 1048576UL && Address < 36028797018963967UL;
        }

        public byte[] ReadMemory(ulong address, int size)
        {
            byte[] buffer = new byte[size];
            ReadProcessMemory(processHandle, address, buffer, buffer.Length, out IntPtr bytesRead);
            return buffer;
        }

        public void WriteMemory(ulong address, byte[] data)
        {
            WriteProcessMemory(processHandle, address, data, data.Length, out IntPtr bytesWritten);
        }

        public void Close()
        {
            if (processHandle != IntPtr.Zero)
            {
                CloseHandle(processHandle);
                processHandle = IntPtr.Zero;
            }
        }

        public byte[] ReadBytes(ulong Address, int Length = 4)
        {
            return this.ReadMemory(Address, Length);
        }

        public byte ReadByte(ulong Address)
        {
            byte[] array = this.ReadMemory(Address, 1);
            return array[0];
        }

        public bool ReadBool(ulong Address)
        {
            byte[] array = this.ReadMemory(Address, 1);
            return array[0] > 0;
        }

        public short ReadInt16(ulong Address)
        {
            byte[] value = this.ReadBytes(Address, 2);
            return BitConverter.ToInt16(value, 0);
        }

        public int ReadInt32(ulong Address)
        {
            byte[] value = this.ReadBytes(Address, 4);
            return BitConverter.ToInt32(value, 0);
        }

        public ulong ReadInt64(ulong Address)
        {
            byte[] value = this.ReadBytes(Address, 8);
            return (ulong)BitConverter.ToInt64(value, 0);
        }

        public float ReadFloat(ulong Address)
        {
            byte[] value = this.ReadBytes(Address, 4);
            return BitConverter.ToSingle(value, 0);
        }

        public double ReadDouble(ulong Address)
        {
            byte[] value = this.ReadBytes(Address, 8);
            return BitConverter.ToDouble(value, 0);
        }

        public ulong GetPointer(params ulong[] args)
        {
            ulong num = 0UL;
            for (int i = 0; i <= args.Length - 1; i++)
            {
                bool flag = i != args.Length - 1;
                if (flag)
                {
                    num = this.ReadInt64(num + args[i]);
                }
                else
                {
                    num += args[i];
                }
            }
            return num;
        }

        public string ReadAsciiString(ulong Address, int length)
        {
            ASCIIEncoding asciiencoding = new ASCIIEncoding();
            byte[] array = this.ReadBytes(Address, length);
            for (int i = 0; i < length; i++)
            {
                bool flag = array[i] == 0;
                if (flag)
                {
                    byte[] array2 = new byte[i];
                    Array.Copy(array, array2, array2.Length);
                    return asciiencoding.GetString(array2);
                }
            }
            return asciiencoding.GetString(array);
        }

        public string ReadAsciiString2(ulong Address, int length)
        {
            ASCIIEncoding asciiencoding = new ASCIIEncoding();
            byte[] array = this.ReadBytes(Address, length);
            for (int i = 0; i < length; i++)
            {
                bool flag = array[i] == 0 || array[i] == 10;
                if (flag)
                {
                    byte[] array2 = new byte[i];
                    Array.Copy(array, array2, array2.Length);
                    return asciiencoding.GetString(array2);
                }
            }
            return asciiencoding.GetString(array);
        }

        public string ReadUniCodeString(ulong Address, int length)
        {
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] bytes = this.ReadBytes(Address, length * 2);
            return unicodeEncoding.GetString(bytes);
        }


        public Vectors.Vector2 ReadVec2(ulong Address)
        {
            byte[] array = this.ReadBytes(Address, 8);
            float[] array2 = new float[array.Length / 4];
            Buffer.BlockCopy(array, 0, array2, 0, array.Length);
            return new Vectors.Vector2(array2[0], array2[1]);
        }
        public Vectors.Vector3 ReadVec3 (ulong Address)
        {
            byte[] array = this.ReadBytes(Address, 12);
            float[] array2 = new float[array.Length / 4];
            Buffer.BlockCopy(array, 0, array2, 0, array.Length);
            return new Vectors.Vector3(array2[0], array2[1], array2[2]);
        }
        public Vectors.Vector4 ReadVec4(ulong Address)
        {
            byte[] array = this.ReadBytes(Address, 16);
            float[] array2 = new float[array.Length / 4];
            Buffer.BlockCopy(array, 0, array2, 0, array.Length);
            return new Vectors.Vector4(array2[0], array2[1], array2[2], array2[3]);
        }

        public void WriteBytes(ulong Address, byte[] buffer, bool ProtectionBypass = false)
        {
            this.WriteMemory(Address, buffer);
        }

        public void WriteByte(ulong Address, byte Value, bool ProtectionBypass = false)
        {
            byte[] bytes = BitConverter.GetBytes((short)Value);
            this.WriteBytes(Address, bytes, ProtectionBypass);
        }

        public void WriteBool(ulong Address, bool Value, bool ProtectionBypass = false)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            this.WriteBytes(Address, bytes, ProtectionBypass);
        }

        public void WriteInt16(ulong Address, short Value, bool ProtectionBypass = false)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            this.WriteBytes(Address, bytes, ProtectionBypass);
        }

        public void WriteInt32(ulong Address, int Value, bool ProtectionBypass = false)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            this.WriteBytes(Address, bytes, ProtectionBypass);
        }

        public void WriteInt64(ulong Address, long Value, bool ProtectionBypass = false)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            this.WriteBytes(Address, bytes, ProtectionBypass);
        }

        public void WriteFloat(ulong Address, float Value, bool ProtectionBypass = false)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            this.WriteBytes(Address, bytes, ProtectionBypass);
        }

        public void WriteDouble(ulong Address, double Value, bool ProtectionBypass = false)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            this.WriteBytes(Address, bytes, ProtectionBypass);
        }

        public void WriteAsciiString(ulong Address, string value, bool ProtectionBypass = false)
        {
            ASCIIEncoding asciiencoding = new ASCIIEncoding();
            this.WriteByte(Address + (ulong)((long)asciiencoding.GetBytes(value).Length), 0, ProtectionBypass);
            this.WriteBytes(Address, asciiencoding.GetBytes(value), ProtectionBypass);
        }

        public void WriteUniCodeString(ulong Address, string value, bool ProtectionBypass = false)
        {
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            this.WriteBytes(Address, unicodeEncoding.GetBytes(value), ProtectionBypass);
            this.WriteByte(Address + (ulong)((long)(value.Length * 2)), 0, ProtectionBypass);
        }

        public void ASM_Return_Bool(ulong Address, bool Value, bool ProtectionBypass = false)
        {
            byte[] value;
            if (Value)
            {
                value = new byte[]
                {
                    85,
                    72,
                    139,
                    236,
                    184,
                    1,
                    0,
                    0,
                    0,
                    93,
                    195
                };
            }
            else
            {
                value = new byte[]
                {
                    85,
                    72,
                    139,
                    236,
                    184,
                    0,
                    0,
                    0,
                    0,
                    93,
                    195
                };
            }
            this.WriteMemory(Address, value);
        }

        public void WriteVec2(ulong Address, Vectors.Vector2 Value, bool ProtectionBypass = false)
        {
            byte[] array = new byte[Vectors.Vector2.SizeInBytes];
            Buffer.BlockCopy(BitConverter.GetBytes(Value.X), 0, array, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Value.Y), 0, array, 4, 4);
            this.WriteBytes(Address, array, ProtectionBypass);
        }

        // Token: 0x0600007A RID: 122 RVA: 0x00016CD8 File Offset: 0x00014ED8
        public void WriteVec3(ulong Address, Vector3 Value)
        {
            // Create a byte array to hold the float values
            byte[] array = new byte[Vector3.SizeInBytes];
            // Copy X, Y, Z into the array
            Buffer.BlockCopy(BitConverter.GetBytes(Value.X), 0, array, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Value.Y), 0, array, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Value.Z), 0, array, 8, 4);
            // Write the byte array to memory
            this.WriteBytes(Address, array);
        }
        public ulong GetGameBase()
        {
            try
            {
                if (process.MainModule == null)
                    throw new Exception("MainModule is null");
                return (ulong)process.MainModule.BaseAddress.ToInt64();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get game base address: {ex.Message}");
            }
        }

        public int GetGameSize()
        {
            try
            {
                if (process.MainModule == null)
                    throw new Exception("MainModule is null");
                return process.MainModule.ModuleMemorySize;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get game size: {ex.Message}");
            }
        }

        public ulong FindCodeCave(ulong Start_Range, ulong End_Range, ulong Cave_Size)
        {
            for (ulong num = Start_Range; num < End_Range; num += 2000UL)
            {
                byte[] array = this.ReadMemory(num, 2000);
                for (int i = 0; i < array.Length; i++)
                {
                    ulong num2 = 0UL;
                    bool flag = array[i] == 0;
                    if (flag)
                    {
                        while (i <= array.Length - 1 && array[i] == 0)
                        {
                            num2 += 1UL;
                            i++;
                            bool flag2 = num2 >= Cave_Size;
                            if (flag2)
                            {
                                return num + (ulong)((long)i - (long)num2);
                            }
                        }
                    }
                }
            }
            return 0UL;
        }

        public ulong PatternScanGame(ulong StartAddress, ulong EndAddress, byte[] Bytes, string[] Pattern, int scan_alignment)
        {
            if (EndAddress <= StartAddress)
            {
                throw new ArgumentException($"EndAddress (0x{EndAddress:X}) must be greater than StartAddress (0x{StartAddress:X})");
            }

            ulong bufferSize = EndAddress - StartAddress;

            const ulong maxBufferSize = 0x7FFFFFFF; // Max value for a positive 32-bit integer (2GB)

            if (bufferSize > maxBufferSize)
            {
                Console.WriteLine($"Warning: Large scan range detected. Scanning first {maxBufferSize / (1024 * 1024)}MB.");
                bufferSize = maxBufferSize;
            }

            if (GameBuffer == null || (ulong)GameBuffer.Length != bufferSize)
            {
                GameBuffer = new byte[bufferSize];
                const int maxChunkSize = 1024 * 1024; // 1 MB chunks

                for (ulong offset = 0; offset < bufferSize; offset += (ulong)maxChunkSize)
                {
                    int chunkSize = (int)Math.Min((ulong)maxChunkSize, bufferSize - offset);
                    byte[] chunk = ReadMemory(StartAddress + offset, chunkSize);
                    Array.Copy(chunk, 0, GameBuffer, (long)offset, chunkSize);
                }
            }

            for (long i = 0; i < GameBuffer.Length - Bytes.Length; i += scan_alignment)
            {
                bool found = true;
                for (int j = 0; j < Bytes.Length; j++)
                {
                    if (Bytes[j] != GameBuffer[i + j] && Pattern[j] != "?")
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return StartAddress + (ulong)i;
                }
            }

            return 0; // Pattern not found
        }
    }


    //simple vec3,vec3 
}
