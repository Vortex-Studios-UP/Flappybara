/*

This script helps draw enums in the inspector when they're used as flags ([Flags] attribute)

Given an enum:   [System.Flags] public enum MyMaskedEnum
    Use it like this:   [SerializeField] [EnumFlagsAttribute] MyMaskedEnum m_flags;

*/


using UnityEngine;
using UnityEditor;

public class EnumFlagsAttribute : PropertyAttribute
{
    public EnumFlagsAttribute() { }
}

[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        _property.intValue = EditorGUI.MaskField(_position, _label, _property.intValue, _property.enumNames);
    }
}