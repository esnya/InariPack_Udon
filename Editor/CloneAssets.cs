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
    };

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
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

        File.Copy(src, dst, true);
      }
    }
  }
}
