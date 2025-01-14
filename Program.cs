using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // 设置控制台窗口标题
        Console.Title = "Unity3D Clean 1.6.1";

        try
        {
            // 检测并准备 Clean 文件夹
            PrepareCleanFolder();

            // 从 GitHub 更新最新配置文件
            await UpdateConfigFiles();

            // 从 Clean 文件夹读取要删除的目录、文件路径和模糊匹配模式
            var directoriesToDelete = LoadPathsFromFile(Path.Combine("Clean", "directories_to_delete.txt"));
            var filesToDelete = LoadPathsFromFile(Path.Combine("Clean", "files_to_delete.txt"));
            var patternsToDelete = LoadPathsFromFile(Path.Combine("Clean", "patterns_to_delete.txt"));

            // 使用多线程删除目录、文件和模糊匹配内容
            var deleteTasks = new List<Task>
            {
                Task.Run(() => DeleteDirectories(directoriesToDelete)),
                Task.Run(() => DeleteFiles(filesToDelete)),
                Task.Run(() => DeleteByPatterns(patternsToDelete))
            };

            // 等待所有任务完成
            await Task.WhenAll(deleteTasks);

            Console.WriteLine("清理完成。");
        }
        catch (Exception ex)
        {
            // 捕获并显示异常信息
            Console.WriteLine($"发生错误: {ex.Message}");
            Console.WriteLine("按任意键继续...");
            Console.ReadKey();  // 类似于 CMD 的 pause，等待用户按下键继续
        }
    }

    /// <summary>
    /// 准备 Clean 文件夹
    /// </summary>
    private static void PrepareCleanFolder()
    {
        if (!Directory.Exists("Clean"))
        {
            Directory.CreateDirectory("Clean");
            Console.WriteLine("已创建 Clean 文件夹。");
        }
    }

    /// <summary>
    /// 更新配置文件
    /// </summary>
    private static async Task UpdateConfigFiles()
    {
        var configUrls = new Dictionary<string, string>
        {
            { "directories_to_delete.txt", "https://raw.githubusercontent.com/ZUXTUO/Unity3D_Clean/refs/heads/main/Clean/directories_to_delete.txt" },
            { "files_to_delete.txt", "https://raw.githubusercontent.com/ZUXTUO/Unity3D_Clean/refs/heads/main/Clean/files_to_delete.txt" },
            { "patterns_to_delete.txt", "https://raw.githubusercontent.com/ZUXTUO/Unity3D_Clean/refs/heads/main/Clean/patterns_to_delete.txt" }
        };

        foreach (var config in configUrls)
        {
            var localPath = Path.Combine("Clean", config.Key);
            try
            {
                using var client = new HttpClient();
                var content = await client.GetStringAsync(config.Value);

                if (File.Exists(localPath))
                {
                    var existingContent = File.ReadAllText(localPath);
                    if (existingContent == content)
                    {
                        Console.WriteLine($"配置文件 {config.Key} 无需更新。");
                        continue;
                    }
                }

                File.WriteAllText(localPath, content);
                Console.WriteLine($"已更新配置文件: {config.Key}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新配置文件 {config.Key} 失败: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 删除指定的目录
    /// </summary>
    static void DeleteDirectories(IEnumerable<string> directories)
    {
        foreach (var dir in directories)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, true); // 递归删除目录及其内容
                    Console.WriteLine($"已删除目录: {dir}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除目录 {dir} 时发生错误: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 删除指定的文件
    /// </summary>
    static void DeleteFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                    Console.WriteLine($"已删除文件: {file}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除文件 {file} 时发生错误: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 根据模糊匹配模式删除文件和目录
    /// </summary>
    static void DeleteByPatterns(IEnumerable<string> patterns)
    {
        foreach (var pattern in patterns)
        {
            try
            {
                // 删除匹配的文件
                var matchedFiles = Directory.GetFiles(".", pattern, SearchOption.TopDirectoryOnly);
                foreach (var file in matchedFiles)
                {
                    File.Delete(file);
                    Console.WriteLine($"已删除文件（模糊匹配）: {file}");
                }

                // 删除匹配的目录
                var matchedDirs = Directory.GetDirectories(".", pattern, SearchOption.TopDirectoryOnly);
                foreach (var dir in matchedDirs)
                {
                    Directory.Delete(dir, true); // 递归删除目录及其内容
                    Console.WriteLine($"已删除目录（模糊匹配）: {dir}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"按模式 {pattern} 删除时发生错误: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 从文件中加载路径列表
    /// </summary>
    /// <param name="filePath">包含路径或模式的文件路径</param>
    /// <returns>路径或模式列表</returns>
    static List<string> LoadPathsFromFile(string filePath)
    {
        var paths = new List<string>();
        if (File.Exists(filePath))
        {
            foreach (var line in File.ReadLines(filePath))
            {
                var trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine) && !trimmedLine.StartsWith("#")) // 忽略空行和注释行
                {
                    paths.Add(trimmedLine);
                }
            }
        }
        else
        {
            Console.WriteLine($"警告: 文件 {filePath} 不存在！");
        }
        return paths;
    }
}