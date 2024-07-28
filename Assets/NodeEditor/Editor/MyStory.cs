using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class MyStory : EditorWindow
{
    private PrgTreeViewer prgTreeViewer;
    public Paragraph currentPrg;
    private ListView listViewer;
    private Button addNodeBtn;
    private Button removeNodeBtn;
    private Button newNodeBtn;
    [MenuItem("Window/UI Toolkit/MyStory")]
    public static void ShowExample()
    {
        MyStory wnd = GetWindow<MyStory>();
        wnd.titleContent = new GUIContent("MyStory");
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/NodeEditor/Editor/MyStory/MyStory.uxml");
        visualTree.CloneTree(root);
        //绑定关键元素
        //FIXME 绑定 没指定名称
        prgTreeViewer = root.Q<PrgTreeViewer>();
        prgTreeViewer.OnPrgViewerSelected += SelectPrgViwer;
        prgTreeViewer.OnRemoveNode += OnRemoveNode;
        //左侧的东西
        listViewer = root.Q<ListView>();
        listViewer.makeItem = MakeListItem;
        listViewer.bindItem = BindListItem;
        listViewer.selectionChanged += ListSelectChange;
        //按钮
        addNodeBtn = root.Q<Button>("addNodeBtn");
        addNodeBtn.clicked += OnAddNodeBtnClick;
        removeNodeBtn = root.Q<Button>("removeNodeBtn");
        removeNodeBtn.clicked += OnRemoveNodeBtnClick;
        newNodeBtn = root.Q<Button>("newNodeBtn");
        newNodeBtn.clicked += OnNewNodeBtnClick;
    }

    private void ListSelectChange(IEnumerable<object> enumerable)
    {
        foreach (var item in enumerable)
        {
            Selection.activeObject = (UnityEngine.Object)item;
        }
    }

    private void OnNewNodeBtnClick()
    {
        TheNode theNode = CreateInstance<TheNode>();
        theNode.name = $"N {currentPrg.name}--{currentPrg.nodes.Count}";
        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(theNode, currentPrg);
        }
        AssetDatabase.SaveAssets();
        currentPrg.nodes.Add(theNode);
        listViewer.RefreshItems();
    }

    private void OnRemoveNodeBtnClick()
    {
        TheNode theNode = listViewer.selectedItem as TheNode;
        if (theNode != null)
        {
            AssetDatabase.RemoveObjectFromAsset(theNode);
            Undo.DestroyObjectImmediate(theNode);
            AssetDatabase.SaveAssets();
            currentPrg.nodes.Remove(theNode);
            listViewer.RefreshItems();
        }
        else
        {
            Debug.Log("未选中删除节点");
        }
    }
    private void OnRemoveNode(TheNode theNode)
    {
        if (theNode != null)
        {
            AssetDatabase.RemoveObjectFromAsset(theNode);
            Undo.DestroyObjectImmediate(theNode);
            AssetDatabase.SaveAssets();
            currentPrg.nodes.Remove(theNode);
            listViewer.RefreshItems();
        }
        else
        {
            Debug.Log("未选中删除节点");
        }
    }

    private void OnAddNodeBtnClick()
    {
        TheNode theNode = CreateInstance<TheNode>();
        if (theNode != null)
        {
            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(theNode, currentPrg);
            }
            AssetDatabase.SaveAssets();
            currentPrg.nodes.Insert(listViewer.selectedIndex + 1, theNode);
            listViewer.RefreshItems();
        }
        else
        {
            Debug.Log("未选中插入节点");
        }
    }

    private void BindListItem(VisualElement element, int index)
    {
        NodeViewer nodeViewer = element as NodeViewer;
        nodeViewer.NodeViewerBind((TheNode)listViewer.itemsSource[index]);
    }

    private void SelectPrgViwer(PrgViewer viewer)
    {
        currentPrg = viewer.prg;
        listViewer.itemsSource = viewer.prg.nodes;
    }

    private VisualElement MakeListItem()
    {
        return new NodeViewer();
    }

    // Unity编辑器中，选择新的一个东西时调用
    private void OnSelectionChange()
    {
        if (Selection.activeObject is PrgTree prgTree)
        {
            prgTreeViewer.PopulateViewer(prgTree);
        }
    }
}