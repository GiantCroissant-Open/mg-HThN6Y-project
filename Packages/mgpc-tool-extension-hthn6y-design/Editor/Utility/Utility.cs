namespace MGPC.Tool.Extension.HThN6Y.Design.Utility.EditorPart
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public static class Utility
    {
        public static void RecreateDirectory(string directoryPath)
        {
            var relativeDirectoryPath = Path.Combine("Assets", directoryPath);
            var absoluteDirectoryPath = Path.Combine(Application.dataPath, directoryPath);
            var absoluteDirectoryPathExisted = Directory.Exists(absoluteDirectoryPath);

            if (absoluteDirectoryPathExisted)
            {
                FileUtil.DeleteFileOrDirectory(relativeDirectoryPath);
            }

            Directory.CreateDirectory(absoluteDirectoryPath);
        }
    }
}
