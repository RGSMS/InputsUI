using UnityEditor;
using UnityEngine;

namespace RGSMS
{
    public enum EInputOption : int
    {
        Button = 0,
        Vector,
        Vector2
    }

    [CustomPropertyDrawer(typeof(InputsContainer))]
    public class InputsContainerDrawer : PropertyDrawer
    {
        private EInputOption _inputOption = EInputOption.Button;

        private const string _inputs = "_inputs";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect rect = position;
            rect.height = 18.0f;

            label.text = "Inputs List";
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
            if (property.isExpanded)
            {
                SerializedProperty inputs = property.FindPropertyRelative(_inputs);

                rect.x += 10.0f;
                rect.width -= 10.0f;

                rect.y += 18.0f;
                label.text = "Input Options";

                _inputOption = (EInputOption)EditorGUI.EnumPopup(rect, label, _inputOption);

                rect.y += 18.0f;
                if(GUI.Button(rect, "Add New Input"))
                {
                    InputConfig inputConfig = null;
                    switch(_inputOption)
                    {
                        case EInputOption.Button:
                            inputConfig = new ButtonInputConfig();
                            break;

                        case EInputOption.Vector:
                            inputConfig = new VectorInputConfig();
                            break;

                        case EInputOption.Vector2:
                            inputConfig = new Vector2InputConfig();
                            break;
                    }

                    int index = inputs.arraySize;

                    inputs.InsertArrayElementAtIndex(index);
                    SerializedProperty element = inputs.GetArrayElementAtIndex(index);
                    element.managedReferenceValue = inputConfig;
                }

                rect.y += 18.0f;
                label.text = "Inputs";

                EditorGUI.PropertyField(rect, inputs, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);

            if (!property.isExpanded)
            {
                return height;
            }

            height += 41.0f;

            SerializedProperty inputs = property.FindPropertyRelative(_inputs);
            height += EditorGUI.GetPropertyHeight(inputs, label);

            return height;
        }
    }
}
