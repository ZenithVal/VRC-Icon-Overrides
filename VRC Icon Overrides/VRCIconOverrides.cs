#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class VRCIconOverrides : MonoBehaviour
{
    static VRCIconOverrides()
    {
        OverrideIcons();
    }

    private static void OverrideIcons()
    {
        // Define the type of the components and the icons
        var icons = new (string typeName, Texture2D icon)[]
        {
            ("VRCAvatarDescriptor", EditorGUIUtility.IconContent("Avatar Icon").image as Texture2D),

            ("VRCContactSender", EditorGUIUtility.IconContent("sv_icon_dot4_pix16_gizmo").image as Texture2D),
            ("VRCContactReceiver", EditorGUIUtility.IconContent("sv_icon_dot1_pix16_gizmo").image as Texture2D),

            ("VRCPhysBone", Resources.Load<Texture2D>("VRC_Physbone Icon")),
            ("VRCPhysBoneCollider", Resources.Load<Texture2D>("VRC_PhysboneCollider Icon")),

            ("VRCAimConstraint", Resources.Load<Texture2D>("VRC_AimConstraint Icon")),
            ("VRCLookAtConstraint", Resources.Load<Texture2D>("VRC_LookAtConstraint Icon")),
            ("VRCParentConstraint", Resources.Load<Texture2D>("VRC_ParentConstraint Icon")),
            ("VRCPositionConstraint", Resources.Load<Texture2D>("VRC_PositionConstraint Icon")),
            ("VRCRotationConstraint", Resources.Load<Texture2D>("VRC_RotationConstraint Icon")),
            ("VRCScaleConstraint", Resources.Load<Texture2D>("VRC_ScaleConstraint Icon"))
        };

        var previewRenderUtility = new PreviewRenderUtility();
        try
        {
            var assetPath = AssetDatabase.GUIDToAssetPath("e0c36ee579935424ea1c7f55c4dfc91e");
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            var go = previewRenderUtility.InstantiatePrefabInScene(prefab);

            foreach (var icon in icons)
            {
                var type = GetTypeByName(icon.typeName);
                if (type != null)
                {
                    EditorGUIUtility.SetIconForObject(go.AddComponent(type), icon.icon);
                    DestroyImmediate(go.GetComponent(type)); 

                    //if someone has an easy way of doing this that doesn't involve creating a gameobject, please let me know
                }
            }
            Debug.Log("VRC Icon Overrides: Icons successfully overridden.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("VRC Icon Overrides: Failed to override icons: " + e.Message);
        }
        finally
        {
            previewRenderUtility.Cleanup();
        }
    }

    private static System.Type GetTypeByName(string typeName)
    {
        foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.Name == typeName)
                {
                    return type;
                }
            }
        }
        return null;
    }


}
#endif