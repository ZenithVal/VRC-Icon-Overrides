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

        foreach (var icon in icons)
        {
            var type = GetTypeByName(icon.typeName);
            if (type != null)
            {
                EditorGUIUtility.SetIconForObject(new GameObject(icon.typeName).AddComponent(type), icon.icon);
                DestroyImmediate(GameObject.Find(icon.typeName));

                //if someone has a way of doing this that doesn't involve creating a gameobject, please let me know
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