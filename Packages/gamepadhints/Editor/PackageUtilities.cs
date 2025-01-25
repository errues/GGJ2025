using UnityEditor;
using System.IO;

namespace BZ.GamepadHints.Editor
{
    public class PackageUtilities : UnityEditor.Editor
    {
        [MenuItem("Window/BZ/GamepadHints/Import Gamepad Hints Essential", false)]
        public static void ImportProjectResourcesMenu()
        {
            ImportEssentialResources();
        }

        private static void ImportEssentialResources()
        {
            string packageFullPath = GetPackageFullPath();

            AssetDatabase.ImportPackage(packageFullPath + "/Package Resources/GamepadHintsEssentials.unitypackage", true);
        }

        private static string GetPackageFullPath()
        {
            // Check for potential UPM package
            string packagePath = Path.GetFullPath("Packages/BZ Gamepad Hints");
            if (Directory.Exists(packagePath))
            {
                return packagePath;
            }

            packagePath = Path.GetFullPath("Assets/..");
            if (Directory.Exists(packagePath))
            {
                // Search default location for development package
                if (Directory.Exists(packagePath + "/Assets/BZ_GamepadHints/Editor"))
                {
                    return packagePath + "/Assets/BZ_GamepadHints";
                }

                // Search for potential alternative locations in the user project
                string[] matchingPaths = Directory.GetDirectories(packagePath, "Gamepad Hints", SearchOption.AllDirectories);
                string path = ValidateLocation(matchingPaths, packagePath);
                if (path != null) return packagePath + path;
            }

            return null;
        }

        private static string ValidateLocation(string[] paths, string projectPath)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                // Check if any of the matching directories contain a Package Resources directory.
                if (Directory.Exists(paths[i] + "/Package Resources"))
                {
                    string folderPath = paths[i].Replace(projectPath, "");
                    folderPath = folderPath.TrimStart('\\', '/');
                    return folderPath;
                }
            }

            return null;
        }
    }
}
