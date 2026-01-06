using UnityEditor;
using UnityEngine;

public class CustomMaterialMenu
{
    [MenuItem("Assets/Create/Shader Factory/Cozy Surface Material", false, 10)]
    public static void CreateCustomMaterial()
    {
        // Pick the shader you want
        Shader shader = Shader.Find("Shader Factory/CozySurfaceComposite");
        if (shader == null)
        {
            Debug.LogError("Shader 'Shader Factory/CozySurfaceComposite' not found!");
            return;
        }

        Material mat = new Material(shader);

        // Place it in the currently selected folder
        string path = "Assets";
        if (Selection.activeObject != null)
        {
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (System.IO.Directory.Exists(assetPath))
                path = assetPath;
            else
                path = System.IO.Path.GetDirectoryName(assetPath);
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New Cozy Surface Composite Material.mat");

        AssetDatabase.CreateAsset(mat, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // Load the created asset, focus project window, and ping it
        Material createdMat = AssetDatabase.LoadAssetAtPath<Material>(assetPathAndName);
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = createdMat;
        EditorGUIUtility.PingObject(createdMat);
    }
}
