using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
#endif
using System.IO;

public static class XCodePostProcess
{

#if UNITY_EDITOR
    [PostProcessBuild(999)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
#if UNITY_5
        if (target != BuildTarget.iOS)
        {
#else
        if (target != BuildTarget.iOS) {
#endif
            Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
            return;
        }

        // Create a new project object from build target
        XCProject project = new XCProject(pathToBuiltProject);

        // Find and run through all projmods files to patch the project.
        // Please pay attention that ALL projmods files in your project folder will be excuted!
        string[] files = Directory.GetFiles(Application.dataPath, "*.projmods", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            Debug.Log("ProjMod File: " + file);
            project.ApplyMod(file);
        }

        ////TODO disable the bitcode for iOS 9
        //project.overwriteBuildSetting("ENABLE_BITCODE", "NO", "Release");
        //project.overwriteBuildSetting("ENABLE_BITCODE", "NO", "Debug");

        //关闭bitcode
        project.overwriteBuildSetting("ENABLE_BITCODE", "NO");

        Debug.LogError("     ����֤��~~~~~~~~~~~   ");

        //TODO implement generic settings as a module option
        //project.overwriteBuildSetting("CODE_SIGN_IDENTITY[sdk=iphoneos*]", "iPhone Distribution", "Release");
        //project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Developer: wang ke (LYDZ2B92D4)", "Release");
        //project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Developer: wang ke (LYDZ2B92D4)", "Debug");
        //project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Developer: client Tgame (8NK3ABPV64)", "Release");
        //project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Developer: client Tgame (8NK3ABPV64)", "Debug");
#if AUDIT
        #region krly
        project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Distribution: BeiJing TianShenHuDong Science and Technology Co., Ltd. (9ZK4D6KRR3)", "Release");
        project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Developer: wang ke (LYDZ2B92D4)", "Debug");

        project.overwriteBuildSetting("PROVISIONING_PROFILE", "tfkrly-dev");
        project.overwriteBuildSetting("PROVISIONING_PROFILE_SPECIFIER", "tfkrly-dis");
        #endregion
#elif NORMAL
        
        #region tgame
        project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Distribution: BeiJing TianShenHuDong Science and Technology Co., Ltd.", "Release");
        project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Developer: Hengli Liu (98M285BSQB)", "Debug");

        project.overwriteBuildSetting("PROVISIONING_PROFILE", "tgame-dev");
        project.overwriteBuildSetting("PROVISIONING_PROFILE_SPECIFIER", "Tgame-dis");
        #endregion

#endif




        project.overwriteBuildSetting("GCC_ENABLE_OBJC_EXCEPTIONS", "YES", "Release");
        project.overwriteBuildSetting("GCC_ENABLE_OBJC_EXCEPTIONS", "YES", "Debug");

        project.overwriteBuildSetting("GCC_ENABLE_CPP_EXCEPTIONS", "YES", "Release");
        project.overwriteBuildSetting("GCC_ENABLE_CPP_EXCEPTIONS", "YES", "Debug");

        project.overwriteBuildSetting("GCC_ENABLE_CPP_RTTI", "YES", "Release");
        project.overwriteBuildSetting("GCC_ENABLE_CPP_RTTI", "YES", "Debug");

        //TODO implement generic settings as a module option
        //		project.overwriteBuildSetting("CODE_SIGN_IDENTITY[sdk=iphoneos*]", "iPhone Distribution", "Release");



        var pbxproj = project.project;

        var attrs = pbxproj.attributes;
        var targetAttrs = (PBXDictionary)attrs["TargetAttributes"];
        PBXDictionary targetSetting = new PBXDictionary();
        targetSetting["ProvisioningStyle"] = "Manual";
#if AUDIT
        targetSetting["PRODUCT_BUNDLE_IDENTIFIER"] = "com.zeus.awesome.krly";
        targetSetting["PRODUCT_NAME"] = "krly";
#elif NORMAL
        targetSetting["PRODUCT_BUNDLE_IDENTIFIER"] = "com.tianshen.shuguang.tgame";
        targetSetting["PRODUCT_NAME"] = "krly";
#endif


        var targets = pbxproj.targets;
        foreach (var t in targets)
        {
            var targetID = (string)t;
            if (targetAttrs.ContainsKey(targetID))
            {
                var TargetAttr = (PBXDictionary)targetAttrs[targetID];
                TargetAttr.Append(targetSetting);
            }
            else
            {
                targetAttrs[targetID] = targetSetting;
            }

        }


        // Finally save the xcode project
        project.Save();

    }
#endif

    public static void Log(string message)
    {
        Debug.Log("PostProcess: " + message);
    }
}
