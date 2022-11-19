using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnableByOtherMemValAttribute))]
public class EnableByOtherMemValDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EnableByOtherMemValAttribute attr =attribute as EnableByOtherMemValAttribute;
        if (getIsEnable(attr,property)) 
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    private SerializedProperty getSwitherMember(SerializedProperty EnableProperty,string switherName) 
    {
        int lastDotindex=EnableProperty.propertyPath.LastIndexOf('.');
        string switherPath= EnableProperty.propertyPath.Substring(0,lastDotindex+1)+switherName;
        //Debug.Log(switherPath);
        return EnableProperty.serializedObject.FindProperty(switherPath);
    }

    private bool getIsEnable (EnableByOtherMemValAttribute attr,SerializedProperty property)
    {
        string switcherMemName = attr.SwitcherMemName;
        int valueWhenEnable = attr.ValueWhenEnable;

        SerializedProperty switcherProperty = getSwitherMember(property, switcherMemName);
        int switcherValue = -1;
        switch (switcherProperty.propertyType)
        {
            case SerializedPropertyType.Boolean:
                switcherValue = switcherProperty.boolValue ? 1 : 0;
                break;
            case SerializedPropertyType.Enum:
                switcherValue = switcherProperty.enumValueIndex;
                break;
            case SerializedPropertyType.Integer:
                switcherValue = switcherProperty.enumValueIndex;
                break;
            default:
                Debug.Log(switcherProperty.propertyPath + "is not int or enum or bool");
                switcherValue = -1;
                break;
        }
        return switcherValue == attr.ValueWhenEnable;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 表示されていない場合スペースを詰めるため-2を返す
        return getIsEnable(attribute as EnableByOtherMemValAttribute, property) ? EditorGUI.GetPropertyHeight(property): -2;
    }
}
#endif
