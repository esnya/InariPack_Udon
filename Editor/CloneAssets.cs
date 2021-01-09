using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO;

namespace EsnyaFactory
{
  public class ImportProcessor : AssetPostprocessor
  {
    struct StringPair {
      string src;
      string dst;
    }

    static (string, string)[] paths = {
      ("InariPack_Udon/CyanEmu/CyanEmu/version.txt", "CyanEmu/version.txt"),
      ("InariPack_Udon/UdonSharp/Assets/UdonSharp/version.txt", "UdonSharp/version.txt"),
      ("InariPack_Udon/UdonToolkit/Resources", "UdonToolkit/Resources"),
    };

    [MenuItem("EsnyaTools/Copy InariPack Resources")]
    static void CopyResources()
    {
      foreach (var path in paths)
      {
        Debug.Log($"{path.Item1} -> {path.Item2}");

        var src = $"{Application.dataPath}/{path.Item1}";
        var dst = $"{Application.dataPath}/{path.Item2}";

        var dstDir = Path.GetDirectoryName(dst);

        if (!Directory.Exists(dstDir))
        {
          Directory.CreateDirectory(dstDir);
        }

        if (Directory.Exists(dst)) FileUtil.ReplaceDirectory(src, dst);
        else if (File.Exists(dst)) FileUtil.ReplaceFile(src, dst);
        else FileUtil.CopyFileOrDirectory(src, dst);
      }
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
      if (importedAssets.Concat(deletedAssets).Concat(movedAssets).Concat(movedFromAssetPaths).Where(p => p.Contains("InariPack_Udon/")).FirstOrDefault() == null) return;
      CopyResources();
    }
  }
}
