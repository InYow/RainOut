using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeViewer : VisualElement
{
    public new class UxmlFactory : UxmlFactory<NodeViewer, UxmlTraits> { }
    public TheNode node;
    public TextField speaker;
    public ObjectField emotion;
    public TextField text;
    public NodeViewer()
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/NodeEditor/Editor/MyStory/NodeViewer.uxml");
        visualTree.CloneTree(this);
        //说话的人
        speaker = this.Q<TextField>("speaker");
        speaker.bindingPath = "Speaker";
        //表情立绘
        emotion = this.Q<ObjectField>();
        emotion.objectType = typeof(Sprite);
        emotion.bindingPath = "Emotion";
        //文本内容
        text = this.Q<TextField>("text");
        text.bindingPath = "Text";

    }
    public void NodeViewerBind(TheNode node)
    {
        this.node = node;
        SerializedObject sO = new(node);
        speaker.Bind(sO);
        emotion.Bind(sO);
        text.Bind(sO);
    }
}