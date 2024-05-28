using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PrgViewer : UnityEditor.Experimental.GraphView.Node
{
    public Action<PrgViewer> OnPrgViewerSelected;
    public Paragraph prg;
    public Port inPort;
    public Port outPort;
    public VisualElement rootVE;
    public VisualElement leftVE;
    public VisualElement rightVE;
    public VisualElement middleVE;
    public VisualElement toDeleteVE;
    public Action<TheNode> OnRemoveNode;
    public Button CreateNodeBtn;

    //public ObjectField objectField;
    public PrgViewer(Paragraph prg)
    {
        CreateNodeBtn = new();
        //手动调整父子关系
        rootVE = this.Q<VisualElement>("node-border");
        leftVE = this.Q<VisualElement>("input");
        rootVE.Add(leftVE);
        middleVE = this.Q<VisualElement>("title");
        rootVE.Add(middleVE);
        rightVE = this.Q<VisualElement>("output");
        rootVE.Add(rightVE);
        rootVE.Add(CreateNodeBtn);
        //删除多余的
        toDeleteVE = this.Q<VisualElement>("contents");
        toDeleteVE.RemoveFromHierarchy();
        toDeleteVE = this.Q<VisualElement>("title-button-container");
        toDeleteVE.RemoveFromHierarchy();
        //使用预设的布局
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/NodeEditor/Editor/MyStory/MyStory.uss");
        this.styleSheets.Add(styleSheet);
        //赋值
        this.prg = prg;
        this.title = prg.prg_Name;
        style.left = prg.position.x;
        style.top = prg.position.y;
        CreateInputPorts();
        CreateOutputPorts();
    }
    private void CreateInputPorts()
    {
        /*将节点入口设置为 
            接口链接方向 横向Orientation.Vertical  竖向Orientation.Horizontal
            接口可链接数量 Port.Capacity.Single
            接口类型 typeof(bool)
        */
        inPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        if (inPort != null)
        {
            inPort.portName = "I";
            inputContainer.Add(inPort);
        }
    }
    private void CreateOutputPorts()
    {
        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        if (outPort != null)
        {
            outPort.portName = "O";
            outputContainer.Add(outPort);
        }
    }
    //记录段落视图位置
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        //记录位置
        prg.position.x = newPos.xMin;
        prg.position.y = newPos.yMin;
        EditorUtility.SetDirty(prg);
    }
    public override void OnSelected()
    {
        base.OnSelected();
        Selection.activeObject = this.prg;
        OnPrgViewerSelected(this);
    }
}
