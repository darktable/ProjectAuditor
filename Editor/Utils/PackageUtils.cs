using System.Linq;
using UnityEditor.PackageManager;
using Unity.ProjectAuditor.Editor;

namespace Unity.ProjectAuditor.Editor.Utils
{
    public static class PackageUtils
    {
        const string k_UnknownVersion = "Unknown";

        public static string GetPackageVersion(string packageName)
        {
            var request = Client.List();
            while (!request.IsCompleted)
                System.Threading.Thread.Sleep(10);

            var packageInfo = request.Result.FirstOrDefault(p => p.name == packageName);
            if (request.Status != StatusCode.Success || packageInfo == null)
                return k_UnknownVersion;

            return packageInfo.version;
        }

        public static string GetPackageRecommendedVersion(UnityEditor.PackageManager.PackageInfo package)
        {
#if UNITY_2023_1_OR_NEWER
            return package.versions.recommended;
#elif UNITY_2019_1_OR_NEWER
            return package.versions.verified;
#else
            return package.versions.recommended;
#endif
        }

        public static bool IsPackageInstalled(string packageName)
        {
            var request = Client.List();
            while (!request.IsCompleted)
                System.Threading.Thread.Sleep(10);
            if (request.Status == StatusCode.Failure)
            {
                return false;
            }

            return request.Result.Any(p => p.name == packageName);
        }
    }
}
