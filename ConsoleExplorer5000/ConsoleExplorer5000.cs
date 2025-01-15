using DataDisplayLogic;

public class ConsoleExplorer5000
{
    private readonly DataOperations _dataDisplay;
    private string _currentPath;
    
    public ConsoleExplorer5000()
    {
        _dataDisplay = new DataOperations();
        _currentPath = Directory.GetCurrentDirectory();
    }
    
    public void Run()
    {
        while (true)
        {
            ShowCurrentDirectory();
            ShowMenu();
            HandleUserInput();
        }
    }
    
    private void ShowCurrentDirectory()
    {
        Console.Clear();
        Console.WriteLine($"Текущая директория: {_currentPath}");
        
        var files = _dataDisplay.GetFilesInfo(_currentPath, "");
        var directories = _dataDisplay.GetDirectories(_currentPath);
        
        Console.WriteLine("\nПапки:");
        foreach (var dir in directories)
        {
            Console.WriteLine($"[DIR] {dir}");
        }
        
        Console.WriteLine("\nФайлы:");
        foreach (var file in files)
        {
            Console.WriteLine($"[FILE] {file.Item1} - {_dataDisplay.FormatFileSize(file.Item2)}");
        }
    }
    
    private void ShowMenu()
    {
        Console.WriteLine("\nКоманды:");
        Console.WriteLine("1. Перейти в папку");
        Console.WriteLine("2. Вернуться назад");
        Console.WriteLine("3. Создать папку");
        Console.WriteLine("4. Создать файл");
        Console.WriteLine("5. Удалить");
        Console.WriteLine("6. Переместить");
        Console.WriteLine("7. Выход");
    }
    
    private void HandleUserInput()
    {
        var key = Console.ReadKey().KeyChar;
        Console.WriteLine();
        
        switch (key)
        {
            case '1':
                NavigateToDirectory();
                break;
            case '2':
                NavigateBack();
                break;
            case '3':
                CreateDirectory();
                break;
            case '4':
                CreateFile();
                break;
            case '5':
                Delete();
                break;
            case '6':
                Move();
                break;
            case '7':
                Environment.Exit(0);
                break;
        }
    }

    private void NavigateToDirectory()
    {
        Console.Write("Введите имя папки: ");
        var dirName = Console.ReadLine();
        if (string.IsNullOrEmpty(dirName)) return;

        var newPath = Path.Combine(_currentPath, dirName);
        if (Directory.Exists(newPath))
        {
            _currentPath = newPath;
        }
        else
        {
            Console.WriteLine("Папка не существует!");
            Console.ReadKey();
        }
    }

    private void NavigateBack()
    {
        var parentDir = Directory.GetParent(_currentPath);
        if (parentDir != null)
        {
            _currentPath = parentDir.FullName;
        }
    }

    private void CreateDirectory()
    {
        Console.Write("Введите имя новой папки: ");
        var dirName = Console.ReadLine();
        if (string.IsNullOrEmpty(dirName)) return;

        try
        {
            Directory.CreateDirectory(Path.Combine(_currentPath, dirName));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании папки: {ex.Message}");
            Console.ReadKey();
        }
    }

    private void CreateFile()
    {
        Console.Write("Введите имя нового файла: ");
        var fileName = Console.ReadLine();
        Console.WriteLine(fileName);
        if (string.IsNullOrEmpty(fileName)) return;

        try
        {
            Console.WriteLine($"Попытка создания файла по пути: {Path.Combine(_currentPath, fileName)}");
            File.Create(Path.Combine(_currentPath, fileName)).Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
            Console.ReadKey();
        }
    }

    private void Delete()
    {
        Console.Write("Введите имя файла или папки для удаления: ");
        var name = Console.ReadLine();
        if (string.IsNullOrEmpty(name)) return;

        var path = Path.Combine(_currentPath, name);
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                Console.WriteLine("Файл или папка не существует!");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении: {ex.Message}");
            Console.ReadKey();
        }
    }

    private void Move()
    {
        Console.Write("Введите имя файла или папки для перемещения: ");
        var sourceName = Console.ReadLine();
        if (string.IsNullOrEmpty(sourceName)) return;

        var sourcePath = Path.Combine(_currentPath, sourceName);
        
        if (!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
        {
            Console.WriteLine("Файл или папка не существует!");
            Console.ReadKey();
            return;
        }

        Console.Write("Введите путь назначения: ");
        var targetPath = Console.ReadLine();

        if (Directory.Exists(targetPath))
        {
            targetPath = Path.Combine(targetPath, sourceName);
        }

        if (string.IsNullOrEmpty(targetPath)) return;

        try
        {
            if (File.Exists(sourcePath))
            {
                Console.WriteLine("Перемещение файла...");
                File.Move(sourcePath, targetPath);
            }
            else
            {
                Console.WriteLine("Перемещение папки...");
                Directory.Move(sourcePath, targetPath);
            }
            Console.WriteLine("Операция завершена успешно");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при перемещении: {ex.Message}");
            Console.WriteLine($"Тип исключения: {ex.GetType().Name}");
            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
            Console.ReadKey();
        }
    }
} 