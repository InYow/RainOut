using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class PrgEdge : Edge
{
    public TextField textField;//分支选项信息
    public PrgEdge()
    {
        textField = new();
        textField.name = "PrgEdge-TextInput";
        EdgeControl edgeControl = this.Q<EdgeControl>();
        edgeControl.style.alignItems = Align.Center;
        edgeControl.style.justifyContent = Justify.Center;
        edgeControl.Add(textField);
        textField.bindingPath = "Text";
    }
    public override void OnSelected()
    {
        base.OnSelected();
        PrgViewer forwardPrgViewer = this.output.node as PrgViewer;
        PrgViewer nextPrgViewer = this.input.node as PrgViewer;
        Selection.activeObject = forwardPrgViewer.prg.FindChoice(nextPrgViewer.prg);
    }
}