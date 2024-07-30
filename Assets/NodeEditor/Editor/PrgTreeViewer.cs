using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PrgTreeViewer : GraphView
{
    public new class UxmlFactory : UxmlFactory<PrgTreeViewer, UxmlTraits> { }
    public Action<PrgViewer> OnPrgViewerSelected;
    public Action<TheNode> OnRemoveNode;
    public PrgTree prgTree;
    public GridBackground gridBackground;
    public PrgTreeViewer()
    {
        Insert(0, new GridBackground());
        // 添加视图缩放
        this.AddManipulator(new ContentZoomer());
        // 添加视图拖拽
        this.AddManipulator(new ContentDragger());
        // 添加选中对象拖拽
        this.AddManipulator(new SelectionDragger());
        // 添加框选
        this.AddManipulator(new RectangleSelector());
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/NodeEditor/Editor/PrgTreeViewer.uss");
        styleSheets.Add(styleSheet);
    }
    // 右键菜单栏
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        evt.menu.AppendAction($"创建一个段落", a => CreatePrgInViewer());
    }
    //创建一个段落
    PrgViewer CreatePrgInViewer()
    {
        // 创建运行时段落树上的对应类型段落
        Paragraph prg = prgTree.CreatePrg();
        return CreatePrgViewer(prg);
    }
    //创建段落视图
    PrgViewer CreatePrgViewer(Paragraph prg)
    {
        PrgViewer prgViewer = new PrgViewer(prg);
        prgViewer.CreateNodeBtn.clicked += () => CreateNode(prgViewer);
        prg.guid = prgViewer.viewDataKey;

        prgViewer.OnPrgViewerSelected = OnPrgViewerSelected;
        prgViewer.OnRemoveNode = OnRemoveNode;
        // 将对应段落UI添加到段落树视图上
        AddElement(prgViewer);
        return prgViewer;
    }

    void CreateNode(PrgViewer oldPrgViewer)
    {
        PrgViewer prgViewer = CreatePrgInViewer();
        Rect rect = oldPrgViewer.GetPosition();
        rect.x += 350;
        prgViewer.SetPosition(rect);

        PrgEdge prgEdge = new()
        {
            output = oldPrgViewer.outPort,
            input = prgViewer.inPort
        };
        oldPrgViewer.outPort.Connect(prgEdge);
        prgViewer.inPort.Connect(prgEdge);
        //生成对象
        Choice chc = prgTree.AddChild(oldPrgViewer.prg, prgViewer.prg, prgEdge.textField.text);
        //值绑定
        SerializedObject serializedObject = new(chc);
        prgEdge.textField.Bind(serializedObject);
        AddElement(prgEdge);
    }

    //绘制连接段落的边 
    void CreateChildEdge(Paragraph prg)
    {
        if (prg.inPrgs.Count != 0)
        {
            prg.inPrgs.ForEach(other =>
            {
                var prgViewer = GetPrgViewerByViewDataKey(prg.guid);
                var otherprgViewer = GetPrgViewerByViewDataKey(other.guid);
                PrgEdge edge = new()
                {

                    input = prgViewer.inPort,
                    output = otherprgViewer.outPort
                };
                prgViewer.inPort.Connect(edge);
                otherprgViewer.outPort.Connect(edge);
                //值绑定
                Choice chc = other.FindChoice(prg);
                SerializedObject serializedObject = new(chc);
                edge.textField.Bind(serializedObject);
                AddElement(edge);
            });
        }
    }
    //选中段落树
    internal void PopulateViewer(PrgTree prgTree)
    {
        this.prgTree = prgTree;
        // 在段落树视图重新绘制之前需要取消视图变更方法OnGraphViewChanged的订阅
        // 以防止视图变更记录方法中的信息是上一个段落树的变更信息
        graphViewChanged -= OnGraphViewChanged;
        // 清除之前渲染的graphElements图层元素
        DeleteElements(graphElements);
        // 绘制已有的段落数据
        prgTree.prgList.ForEach(prg =>
        {
            CreatePrgViewer(prg);
        });
        prgTree.prgList.ForEach(prg =>
        {
            CreateChildEdge(prg);
        });
        // 绘制完段落树视图所有的元素之后重新订阅视图变更方法OnGraphViewChanged
        graphViewChanged += OnGraphViewChanged;
    }
    // 处理元素变化引起的数据改动
    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        // 对所有删除进行遍历记录 只要视图内有元素删除进行判断
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                // 找到PrgTreeViewer中删除的PrgView
                if (elem is PrgViewer prgViewer)
                {
                    //将拥有的节点删除
                    for (; prgViewer.prg.nodes.Count > 0;)
                    {
                        OnRemoveNode(prgViewer.prg.nodes[0]);
                    }
                    // 并将该NodeView所关联的段落即段落视图删除
                    prgTree.DeletePrg(prgViewer.prg);
                }
                //找到删除的Edge
                if (elem is PrgEdge prgEdge)
                {
                    //FIXME 先后可能有颠倒
                    PrgViewer outViewer = prgEdge.output.node as PrgViewer;
                    PrgViewer inViewer = prgEdge.input.node as PrgViewer;
                    prgTree.RemovePrgConnection(outViewer.prg, inViewer.prg);
                }
            });
        }
        // 处理边创建的数据变化
        if (graphViewChange.edgesToCreate != null)
        {
            for (int i = 0; i < graphViewChange.edgesToCreate.Count; i++)
            {
                Edge edge = graphViewChange.edgesToCreate[i];
                PrgEdge prgEdge = new PrgEdge()
                {
                    output = edge.output,
                    input = edge.input
                };
                graphViewChange.edgesToCreate[i] = prgEdge;
                //FIXME 先后可能有颠倒
                PrgViewer outViewer = edge.output.node as PrgViewer;
                PrgViewer inViewer = edge.input.node as PrgViewer;
                //生成对象
                Choice chc = prgTree.AddChild(outViewer.prg, inViewer.prg, prgEdge.textField.text);
                //值绑定
                SerializedObject serializedObject = new(chc);
                prgEdge.textField.Bind(serializedObject);
            };
        }
        return graphViewChange;
    }
    //指定可以连接的port
    public override List<Port> GetCompatiblePorts(Port firstPort, NodeAdapter nodeAdapter)
    {
        List<Port> thePorts = new();
        foreach (var secondPort in ports)
        {
            //是同一个
            if (secondPort == firstPort)
            {
                continue;
            }
            //方向相同
            if (secondPort.direction == firstPort.direction)
            {
                continue;
            }
            //已经连接过了
            if (secondPort.connected)
            {
                if (IfConnected(secondPort, firstPort))
                {
                    continue;
                }
            }
            //不能自己连自己
            if (firstPort.node == secondPort.node)
            {
                continue;
            }
            //有一个是单选项节点的有值输出口
            // List<NormalDialogue> normalDialogues = new();
            // if (secondPort.direction == Direction.Output && secondPort.node as NodeView != null)
            // {
            //     if ((secondPort.node as NodeView).node is NormalDialogue normalDialogue)
            //         normalDialogues.Add(normalDialogue);
            // }
            // if (firstPort.direction == Direction.Output && firstPort.node as NodeView != null)
            // {
            //     if ((firstPort.node as NodeView).node is NormalDialogue normalDialogue)
            //         normalDialogues.Add(normalDialogue);
            // }
            // if (normalDialogues.Count != 0)
            // {
            //     bool contin = false;
            //     foreach (var item in normalDialogues)
            //     {
            //         Debug.Log("dw");
            //         if (item.children.Count != 0)
            //         {
            //             contin = true;
            //         }
            //     }
            //     if (contin)
            //     {
            //         continue;
            //     }
            // }

            thePorts.Add(secondPort);
        }
        return thePorts;
        static bool IfConnected(Port port1, Port port2)
        {
            foreach (var edge in port1.connections)
            {
                Port otherPort = edge.input == port1 ? edge.output : edge.input;
                if (otherPort == port2)
                {
                    return true;
                }
            }
            return false;
        }



    }


    public PrgViewer GetPrgViewerByViewDataKey(string viewDataKey)
    {
        // 遍历GraphView中的所有节点
        foreach (var element in this.graphElements)
        {
            if (element is PrgViewer prgViewer && prgViewer.viewDataKey == viewDataKey)
            {
                return prgViewer;
            }
        }

        // 如果未找到匹配的节点，返回null
        return null;
    }
}
