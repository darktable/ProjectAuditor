using System.Collections.Generic;
using System.Linq;
using Unity.ProjectAuditor.Editor.Core;
using Unity.ProjectAuditor.Editor.Diagnostic;
using Unity.ProjectAuditor.Editor.Modules;
using UnityEditor;

namespace Unity.ProjectAuditor.Editor.SettingsAnalysis
{
    class StrippingAnalyzer : ISettingsModuleAnalyzer
    {
        internal const string PAS0009 = nameof(PAS0009);
        internal const string PAS0025 = nameof(PAS0025);
        internal const string PAS0026 = nameof(PAS0026);

        static readonly Descriptor k_EngineCodeStrippingDescriptor = new Descriptor(
            PAS0009,
            "Player: Engine Code Stripping",
            Area.BuildSize,
            "Engine code stripping is disabled. The generated build will be larger than necessary.",
            "Enable <b>stripEngineCode</b> in <b>Project Settings ➔ Player ➔ Other Settings</b>")
        {
            platforms = new string[] { BuildTarget.Android.ToString(), BuildTarget.iOS.ToString(), BuildTarget.WebGL.ToString() }
        };

        static readonly Descriptor k_AndroidManagedStrippingDescriptor = new Descriptor(
            PAS0025,
            "Player (Android): Managed Code Stripping",
            Area.BuildSize,
            "Managed code stripping on Android is set to ManagedStrippingLevel.Low (or Disabled). The generated build will be larger than necessary.",
            "Set managed stripping level to Medium or High.")
        {
            platforms = new string[] { BuildTarget.Android.ToString() }
        };

        static readonly Descriptor k_iOSManagedStrippingDescriptor = new Descriptor(
            PAS0026,
            "Player (iOS): Managed Code Stripping",
            Area.BuildSize,
            "Managed code stripping on iOS is set to ManagedStrippingLevel.Low (or Disabled). The generated build will be larger than necessary.",
            "Set managed stripping level to Medium or High.")
        {
            platforms = new string[] { BuildTarget.iOS.ToString() }
        };

        public void Initialize(ProjectAuditorModule module)
        {
            module.RegisterDescriptor(k_EngineCodeStrippingDescriptor);
            module.RegisterDescriptor(k_AndroidManagedStrippingDescriptor);
            module.RegisterDescriptor(k_iOSManagedStrippingDescriptor);
        }

        public IEnumerable<ProjectIssue> Analyze(ProjectAuditorParams projectAuditorParams)
        {
            if (k_EngineCodeStrippingDescriptor.platforms.Contains(projectAuditorParams.platform.ToString()) && !PlayerSettings.stripEngineCode)
            {
                yield return ProjectIssue.Create(IssueCategory.ProjectSetting, k_EngineCodeStrippingDescriptor)
                    .WithLocation("Project/Player");
            }

            if (k_AndroidManagedStrippingDescriptor.platforms.Contains(projectAuditorParams.platform.ToString()))
            {
                var value = PlayerSettings.GetManagedStrippingLevel(BuildTargetGroup.Android);
                if (value == ManagedStrippingLevel.Disabled || value == ManagedStrippingLevel.Low)
                    yield return ProjectIssue.Create(IssueCategory.ProjectSetting, k_AndroidManagedStrippingDescriptor)
                        .WithLocation("Project/Player");
            }

            if (k_iOSManagedStrippingDescriptor.platforms.Contains(projectAuditorParams.platform.ToString()))
            {
                var value = PlayerSettings.GetManagedStrippingLevel(BuildTargetGroup.iOS);
                if (value == ManagedStrippingLevel.Disabled || value == ManagedStrippingLevel.Low)
                    yield return ProjectIssue.Create(IssueCategory.ProjectSetting, k_iOSManagedStrippingDescriptor)
                        .WithLocation("Project/Player");
            }
        }
    }
}
