using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common.Replication;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
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
}