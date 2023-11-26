using System;
using System.IO;

class Logger
{
    private const string notesFileName = "Notes.txt";
    private const string logFileName = "journal.log";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить новую заметку");
            Console.WriteLine("2. Прочитать все заметки");
            Console.WriteLine("3. Выход");

            int choice = Convert.ToInt32(Console.ReadLine());

            try
            {
                switch (choice)
                {
                    case 1:
                        AddNote();
                        LogAction("Создана новая заметка");
                        break;
                    case 2:
                        ReadNotes();
                        LogAction("Заметки прочитаны");
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    static void AddNote()
    {
        Console.WriteLine("Введите текст заметки:");
        string noteText = Console.ReadLine();

        using (StreamWriter writer = File.AppendText(notesFileName))
        {
            string note = $"Дата: {DateTime.Now}\nТекст Заметки: {noteText}\n";
            writer.WriteLine(note);
        }

        Console.WriteLine("Заметка успешно добавлена!");
    }

    static void ReadNotes()
    {
        if (File.Exists(notesFileName))
        {
            string[] notes = File.ReadAllLines(notesFileName);
            foreach (var note in notes)
            {
                Console.WriteLine(note);
            }
        }
        else
        {
            Console.WriteLine("Заметок пока нет.");
        }
    }

    static void LogAction(string action)
    {
        using (StreamWriter writer = File.AppendText(logFileName))
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {action}\n";
            writer.WriteLine(logEntry);
        }
    }

    static void LogException(Exception ex)
    {
        using (StreamWriter writer = File.AppendText(logFileName))
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {ex.Message}\n";
            writer.WriteLine(logEntry);
        }
    }
}
