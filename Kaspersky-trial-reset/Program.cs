using System;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] arg)
        {
            Console.WriteLine("Вас приветствует служба сброса пробного периода Kasperky от HYBIK");
            Console.WriteLine("Для того что бы начать сброс нажмите любую клавишу...");
            Console.ReadKey();

            if (File.Exists(@"C:\Program Files\ESET\ESET Security\ecmds.exe"))
            {
                Console.WriteLine("Простите, но ваш антивирус судя по всему Eset. В этом случае вы можете просто переустановить антивирус, и попросить создателя дать вам ключ");
                Console.ReadKey();
            }
            else
            {
                ClearingRegedit();
            }
        }

        static void ClearingRegedit()
        {
            Console.Clear();
            Console.WriteLine("Начинаю чистку реестра...\n");
            Thread.Sleep(300);

            string LoggerSearch, LibFolder;
            
            if (Environment.Is64BitOperatingSystem)
            {
                LoggerSearch = "SOFTWARE\\Wow6432Node\\Microsoft\\SystemCertificates\\SPC";
                LibFolder = "SOFTWARE\\Wow6432Node\\KasperskyLab";
            }
            else
            {
                LoggerSearch = "SOFTWARE\\Microsoft\\SystemCertificates\\SPC";
                LibFolder = "SOFTWARE\\KasperskyLab";
            }

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(LoggerSearch, true))
                {
                    key.DeleteSubKeyTree("Certificates", true);
                }
            }
            catch (System.Security.SecurityException)
            {
                Console.WriteLine("У вас нету прав доступа. Запустите программу от имени администратора\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Внимание, удаление SPC было пропущено или попытка была неудачной. Если сброс не помог, прошу дать эту ошибку создателю:\n" +
                    $"{e}\n");
            }

            Thread.Sleep(300);

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(LibFolder, true))
                {
                    key.DeleteSubKeyTree("LicStorage", true);
                }
            }
            catch (System.Security.SecurityException)
            {
                Console.WriteLine("У вас нету прав доступа. Запустите программу от имени администратора\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Внимание, удаление Lib было пропущено, скорее всего из за того что она пуста. Если сброс не помог, прошу дать эту ошибку создателю:\n" +
                                  $"{e}");
            }
            
            Thread.Sleep(300);
            
            Console.WriteLine("\nЧистка окончена, для продолжения нажмите любую клавишу...");
            Console.ReadKey();
            Final();
        }

        static void Final()
        {
            Console.Clear();
            Console.WriteLine("Отлично, чистка была закончена. Теперь установите антивирус Kaspersky заного, используя эту ссылку:\n\n" +
                              "https://www.kaspersky.ru/downloads\n\n" +
                              "Промотайте в самый низ и установите программу. Затем активируйте при помощи этого ключа:\n\n" +
                              "EFD3J-68G5M-VM642-1PUPB\n\n" +
                              "Сохраните этот ключ и закрывайте программу");
            Console.ReadKey();
        }
    }
}