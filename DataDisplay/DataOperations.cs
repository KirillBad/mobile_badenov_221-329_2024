namespace DataDisplayLogic
{
    public class DataOperations
    {
        public List<string> GetDrives()
        {
            var drives = new List<string>();
            try
            {
                drives.AddRange(Directory.GetLogicalDrives());
            }
            catch (Exception ex)
            {
                drives.Add($"Ошибка: {ex.Message}");
            }

            return drives;
        }

        public List<Tuple<string, long, DateTime, FileAttributes>> GetFilesInfo(string folderPath, string searchQuery)
        {
            var filesInfo = new List<Tuple<string, long, DateTime, FileAttributes>>();

            try
            {
                if (Directory.Exists(folderPath))
                {
                    var files = Directory.GetFiles(folderPath);

                    var filteredFiles = string.IsNullOrEmpty(searchQuery)
                        ? files
                        : files.Where(file => Path.GetFileName(file).ToLower().Contains(searchQuery.ToLower())).ToArray();

                    foreach (var file in filteredFiles)
                    {
                        var fileInfo = new FileInfo(file);

                        if ((fileInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                        {
                            filesInfo.Add(new Tuple<string, long, DateTime, FileAttributes>(
                                fileInfo.Name,
                                fileInfo.Length, 
                                fileInfo.LastWriteTime, 
                                fileInfo.Attributes 
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                filesInfo.Add(new Tuple<string, long, DateTime, FileAttributes>(
                    $"Ошибка: {ex.Message}", 0, DateTime.MinValue, FileAttributes.Normal
                ));
            }

            return filesInfo;
        }

        public List<string> GetDirectories(string folderPath)
        {
            var directories = new List<string>();

            if (Directory.Exists(folderPath))
            {
                try
                {
                    var dirInfo = new DirectoryInfo(folderPath);

                    foreach (var dir in dirInfo.GetDirectories())
                    {
                        if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ||
                            (dir.Attributes & FileAttributes.System) == FileAttributes.System)
                        {
                            continue; 
                        }

                        directories.Add(dir.Name);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Access to folder {folderPath} is denied.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accessing folder {folderPath}: {ex.Message}");
                }
            }

            return directories;
        }


        public string FormatFileSize(long sizeInBytes)
        {
            const long KB = 1024;
            const long MB = 1024 * 1024;
            const long GB = 1024 * 1024 * 1024;

            if (sizeInBytes >= GB)
            {
                return $"{(double)sizeInBytes / GB:F2} GB"; 
            }
            else if (sizeInBytes >= MB)
            {
                return $"{(double)sizeInBytes / MB:F2} MB";  
            }
            else if (sizeInBytes >= KB)
            {
                return $"{(double)sizeInBytes / KB:F2} KB"; 
            }
            else
            {
                return $"{sizeInBytes} Bytes";  
            }
        }
    }
}
