using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class IOSBuildScript
{
    static void DebugBuildIOS()
    {
        BuildSummary summary = BuildIOS();
        if (summary.result == BuildResult.Failed)
        {
            EditorApplication.Exit(1);
        }
    }

    [MenuItem("File/Build iOS", false, 221)]
    static void DebugBuildIOSMenuItem()
    {
        BuildTargetGroup currentBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        BuildTarget currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;

        EditorUserBuildSettings.SwitchActiveBuildTargetAsync(currentBuildTargetGroup, currentBuildTarget);
    }

    static BuildSummary BuildIOS()
    {
        BuildPlayerOptions options = CreateBuildOption();
        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        ShowBuildLog(summary);

        return summary;
    }

    private static BuildPlayerOptions CreateBuildOption()
    {
        var options = new BuildPlayerOptions()
        {
            target = BuildTarget.iOS
        };

        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; ++i)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        options.scenes = scenes;

        string assetsDir = Application.dataPath;
        options.locationPathName = $"{assetsDir}/../Build/{Application.productName}.apk";

        options.options = BuildOptions.Development;

        return options;
    }

    private static void ShowBuildLog(BuildSummary summary)
    {
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.LogError("Build failed");
        }

    }
}
