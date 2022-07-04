using Script;
using UnityEngine;
using UnityEngine.UI;

public class TreeViewItem : MonoBehaviour
{
    [SerializeField] private Image _imageSelected;//选中图标
    [SerializeField] private Image _imageFolding; //折叠图标
    [SerializeField] private Text _text; //显示内容文本
    [SerializeField] private Text _textIndex;//层次索引文本
    
    private TreeView _parentTreeView;

    private void Awake()
    {
        IsSelected = false;
        IsFolding = false;
    }

    private bool _isFolding = false;
    public bool IsFolding
    {
        get
        {
            return _isFolding;
        }
        set
        {
            _isFolding = value;
            
            var rectTransform = _imageFolding.transform as RectTransform;
            //绕Z轴旋转90度 打开 否则 0 关闭折叠  
            rectTransform.localRotation = Quaternion.AngleAxis(_isFolding ? 0 : 90, Vector3.back);
        }
    }
    
    public bool IsSelected
    {
        get
        {
            return _imageSelected.gameObject.activeSelf;
        }
        set
        {
            _imageSelected.gameObject.SetActive(value);
        }
    }
    /// <summary>
    ///   注入父级管理类
    /// </summary>
    public void SetParentTreeView(TreeView parentTreeView)
    {
        _parentTreeView = parentTreeView;
    }
    /// <summary>
    /// 设置列表内容，并将其Item脚本返回
    /// </summary>
    /// <param name="text"></param>
    /// <param name="userData"></param>
    /// <returns></returns>
    public TreeViewItem SetData(string text, object userData = null)
    {
        _text.text = text;
        UserData = userData;
        
        return this;
    }
    /// <summary>
    /// 设置Item参数
    /// </summary>
    /// <param name="index"></param>
    public void SetIndex(int index)
    {
        _textIndex.text = index.ToString();
    }
    /// <summary>
    /// 设置折叠图标激活关闭
    /// </summary>
    /// <param name="enabled"></param>
    public void SetFoldingEnabled(bool enabled)
    {
        _imageFolding.gameObject.SetActive(enabled);
    }
    public object UserData { get; set;}
    /// <summary>
    /// 按钮注册点击触发事件，将自身Item脚本传递出去，该函数触发由外部面板点击EventTrigger注册触发
    /// </summary>
    public void OnFoldingClicked()
    {
        _parentTreeView.OnTreeViewItemFoldingClicked(this);
    }
    /// <summary>
    ///  文本注册点击触发事件，将自身Item脚本传递出去，该函数触发由外部面板点击EventTrigger注册触发
    /// </summary>
    public void OnTextClicked()
    {
        _parentTreeView.OnTreeViewItemContentClicked(this);
    }  
}
