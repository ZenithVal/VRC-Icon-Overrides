
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
            ("VRCAimConstraint", Resources.Load<Texture2D>("VRC_AimConstraint Icon")),
            ("VRCLookAtConstraint", Resources.Load<Texture2D>("VRC_LookAtConstraint Icon")),
            ("VRCParentConstraint", Resources.Load<Texture2D>("VRC_ParentConstraint Icon")),
            ("VRCPositionConstraint", Resources.Load<Texture2D>("VRC_PositionConstraint Icon")),
            ("VRCRotationConstraint", Resources.Load<Texture2D>("VRC_RotationConstraint Icon")),
            ("VRCScaleConstraint", Resources.Load<Texture2D>("VRC_ScaleConstraint Icon"))
        };

        foreach (var icon in icons)
        {
            var type = GetTypeByName(icon.typeName);
            if (type != null)
            {
                EditorGUIUtility.SetIconForObject(new GameObject(icon.typeName).AddComponent(type), icon.icon);
            }
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