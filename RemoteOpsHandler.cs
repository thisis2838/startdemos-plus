using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus
{
    class RemoteOpsHandler
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        static extern IntPtr CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll")]
        static extern uint WaitForSingleObject(IntPtr handle, uint time);

        [DllImport("kernel32.dll")]
        static extern bool TerminateThread(IntPtr handle, uint code);
        [DllImport("kernel32.dll")]
        static extern bool VirtualFreeEx(IntPtr handle, IntPtr address, uint size, uint type);

        // privileges
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint MEM_RELEASE = 0x00008000;
        const uint PAGE_READWRITE = 4;

        public Process CurProcess;

        public RemoteOpsHandler(Process process)
        {
            CurProcess = process;
        }

        public void CallFunctionString(string input, IntPtr funcPtr)
        {
            if (funcPtr == IntPtr.Zero || CurProcess.HasExited || CurProcess.Handle == IntPtr.Zero)
                return;

            IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 
                false, 
                CurProcess.Id);

            uint bufSize = (uint)((input.Length + 1) * Marshal.SizeOf(typeof(char)));

            IntPtr stringBuf = VirtualAllocEx(
                procHandle, 
                IntPtr.Zero, 
                bufSize,
                MEM_COMMIT | MEM_RESERVE, 
                PAGE_READWRITE);

            if (stringBuf == IntPtr.Zero)
                return;

            WriteProcessMemory(procHandle, stringBuf, Encoding.Default.GetBytes(input), bufSize, out UIntPtr bytesWritten);
            var s = CreateRemoteThread(procHandle, IntPtr.Zero, 0, funcPtr, stringBuf, 0, IntPtr.Zero);

            if (s != IntPtr.Zero)
            {
                WaitForSingleObject(s, 0xFFFFFFFF);
                TerminateThread(s, 0);
                CloseHandle(s);
            }

            VirtualFreeEx(procHandle, stringBuf, bufSize, MEM_RELEASE);
        }
    }
}
