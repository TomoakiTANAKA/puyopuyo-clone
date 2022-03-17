using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AndroidBuildScript {
    /// <summary>
    /// cli経由での呼び出しI/F
    /// </summary>
    static void DebugBuildAndroid() {
        BuildSummary summary = BuildAndroid();
        if (summary.result == BuildResult.Failed) {
            EditorApplication.Exit(1);
        }
    }

    // Unity　Editorにメニューを追加するスクリプト
    // File => Build Androidを追加する
    // see: https://docs.unity3d.com/ScriptReference/MenuItem.html
    [MenuItem("File/Build Android", false /* isValidateFunction */, 222 /* priority*/)]
    static void DebugBuildAndroidMenuItem() {
        BuildTargetGroup currentBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        BuildTarget currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;

        BuildSummary summary = BuildAndroid();

        EditorUserBuildSettings.SwitchActiveBuildTargetAsync(currentBuildTargetGroup, currentBuildTarget);
    }

    /// <summary>
    /// Andoridのビルド
    /// </summary>
    /// <returns></returns>
    private static BuildSummary BuildAndroid() {
        var options = CreateBuildOption();

        // https://docs.unity3d.com/ja/current/ScriptReference/BuildPipeline.BuildPlayer.html
        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded) {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed) {
            Debug.LogError("Build failed");
        }

        return summary;
    }

    /// build optionの作成
    private static BuildPlayerOptions CreateBuildOption() {
        // dotnet_style_object_initializer = true の場合の初期化方法
        // see: https://docs.microsoft.com/ja-jp/dotnet/fundamentals/code-analysis/style-rules/ide0017
        var options = new BuildPlayerOptions()
        {
            // [Error]
            // Error building player because build target was unsupported
            // => File -> BuildSettings を開き、任意のOSを選択する。（build supoportがない場合はDLしておく）。
            target = BuildTarget.Android
        };

        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; ++i) {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        options.scenes = scenes;

        string assetsDir = Application.dataPath;
        options.locationPathName = $"{assetsDir}/../Build/{Application.productName}.apk";

        options.options = BuildOptions.Development;

        return options;
    }
}
