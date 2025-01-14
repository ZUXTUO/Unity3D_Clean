### README: Unity3D Clean 1.6

---

## 项目简介 (Project Introduction)

Unity3D Clean 是一个用于清理 Unity3D 项目缓存文件的实用工具（非清理项目资产内容），通过自动删除不必要的目录、文件和模糊匹配的文件，帮助开发者保持项目的整洁和优化。

Unity3D Clean is a utility tool designed to clean up Unity3D project cache files (not project assets) by automatically removing unnecessary directories, files, and pattern-matched items. It helps developers maintain a clean and optimized project structure.

---

## 功能特点 (Features)

- **批量删除目录**：支持从配置文件中加载目录路径并批量删除。
- **批量删除文件**：支持从配置文件中加载文件路径并批量删除。
- **模糊匹配删除**：根据指定的文件或目录匹配模式，删除符合条件的项目。
- **多线程操作**：使用多线程技术并行清理，提高清理效率。
- **简单易用**：通过配置文件轻松管理需要清理的内容。

- **Batch Delete Directories**: Supports loading directory paths from configuration files and batch deleting them.
- **Batch Delete Files**: Supports loading file paths from configuration files and batch deleting them.
- **Pattern-Based Deletion**: Deletes files and directories that match specified patterns.
- **Multithreaded Operation**: Cleans up files and directories in parallel using multithreading for efficiency.
- **User-Friendly**: Manage cleanup content easily via configuration files.

---

## 使用方法 (Usage)

### 1. 准备工作 (Preparation)
把项目目录中 `Clean` 文件夹和编译后的二进制文件一同放入Unity项目内。

Place the `Clean` folder and the compiled binary file into the Unity project directory.

1. `directories_to_delete.txt`  
   - 包含需要删除的目录路径，每行一个。
   - 包含需要删除的目录路径，每行一个。
   - 示例：
     ```
     Library
     Temp
     Logs
     ```

   - Contains paths of directories to delete, one per line.
   - Example:
     ```
     Library
     Temp
     Logs
     ```

2. `files_to_delete.txt`  
   - 包含需要删除的文件路径，每行一个。
   - 示例：
     ```
     temp.log
     Editor.log
     ```

   - Contains paths of files to delete, one per line.
   - Example:
     ```
     temp.log
     Editor.log
     ```

3. `patterns_to_delete.txt`  
   - 包含模糊匹配模式，每行一个。
   - 示例：
     ```
     *.tmp
     *.bak
     ```

   - Contains patterns for fuzzy matching, one per line.
   - Example:
     ```
     *.tmp
     *.bak
     ```

---

### 2. 运行工具 (Run the Tool)
执行程序，如Windows平台执行UnityClean.exe。

Run the program, for example, execute `UnityClean.exe` on Windows.

---

## 注意事项 (Notes)
- 确保 `Clean` 文件夹和配置文件已经正确创建并放置在程序同级目录下。
- 删除操作不可恢复，请谨慎操作。

- Ensure the `Clean` folder and configuration files are correctly created and placed at the same level as the program.
- Deletion is irreversible, so proceed with caution.

---

## 贡献与支持 (Contributions and Support)
欢迎提交问题或贡献代码以改进此工具。

Contributions and suggestions are welcome to improve this tool. 