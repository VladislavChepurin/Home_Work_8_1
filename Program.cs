/*
Задача стоит дослловно:
"Напишите программу, которая чистит нужную нам папку от файлов  и папок, которые не использовались более 30 минут"
Учитывая неточность построения задачи, я ее интерпритирую так: удалять те файлы время поледнего доступа было не менее 30 минут
*/

namespace HomeWork_8_1;
class Program
{
    static void Main(string[] args)
    {
        // Входящая папка Clear размещена на рабочем столе, которая чистится 
        string directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Clear";
        var timeDelts = DateTime.Now - TimeSpan.FromMinutes(30);

        // Проверяем наличие директории
        if (Directory.Exists(directory))
        {
            DirectoryInfo info = new(directory);
            try
            {
                // Иррретация по файлам
                foreach (var itemFiles in info.GetFiles())
                {
                    Console.WriteLine($"Файл {itemFiles}, время последнего доступа {itemFiles.LastAccessTime}");
                    if (itemFiles.LastAccessTime < timeDelts)
                    {
                        FileInfo file = new FileInfo(itemFiles.ToString());
                        file.Delete();
                    }
                }
                // Иррретация по директориям
                foreach (var itemDir in info.GetDirectories())
                {
                    Console.WriteLine($"Папка {itemDir}, время последнего доступа {itemDir.LastAccessTime}");
                    if (itemDir.LastAccessTime < timeDelts)
                    {
                        DirectoryInfo deleteDir = new DirectoryInfo(itemDir.ToString());
                        deleteDir.Delete(true);
                    }
                }
            }

            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Произошла ошибка, проверьте атрибуты файлов, возможно они помечены как \"только для чтения\"  {Environment.NewLine} {e.Message}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Произошла ошибка, проверьте возможно файлы использовались другой программой. {Environment.NewLine} {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка, {Environment.NewLine} {e.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Указанной папки не существует");
        }

        Console.ReadKey();
    }
}