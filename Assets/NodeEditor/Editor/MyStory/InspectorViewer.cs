using UnityEngine.UIElements;
using UnityEditor;

public class InspectorViewer : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorViewer, UxmlTraits> { }
    Editor editor;
    public InspectorViewer()
    {

    }
    internal void UpdateSelection(PrgViewer prgViewer)
    {
        Clear();
        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(prgViewer.prg);
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            if (editor.target)
            {
                editor.OnInspectorGUI();
            }
        });
        Add(container);
    }
}
