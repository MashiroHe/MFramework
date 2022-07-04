using Script;
using UnityEngine;
using UnityEngine.UI;

public class TreeViewItem : MonoBehaviour
{
    [SerializeField] private Image _imageSelected;//ѡ��ͼ��
    [SerializeField] private Image _imageFolding; //�۵�ͼ��
    [SerializeField] private Text _text; //��ʾ�����ı�
    [SerializeField] private Text _textIndex;//��������ı�
    
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
            //��Z����ת90�� �� ���� 0 �ر��۵�  
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
    ///   ע�븸��������
    /// </summary>
    public void SetParentTreeView(TreeView parentTreeView)
    {
        _parentTreeView = parentTreeView;
    }
    /// <summary>
    /// �����б����ݣ�������Item�ű�����
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
    /// ����Item����
    /// </summary>
    /// <param name="index"></param>
    public void SetIndex(int index)
    {
        _textIndex.text = index.ToString();
    }
    /// <summary>
    /// �����۵�ͼ�꼤��ر�
    /// </summary>
    /// <param name="enabled"></param>
    public void SetFoldingEnabled(bool enabled)
    {
        _imageFolding.gameObject.SetActive(enabled);
    }
    public object UserData { get; set;}
    /// <summary>
    /// ��ťע���������¼���������Item�ű����ݳ�ȥ���ú����������ⲿ�����EventTriggerע�ᴥ��
    /// </summary>
    public void OnFoldingClicked()
    {
        _parentTreeView.OnTreeViewItemFoldingClicked(this);
    }
    /// <summary>
    ///  �ı�ע���������¼���������Item�ű����ݳ�ȥ���ú����������ⲿ�����EventTriggerע�ᴥ��
    /// </summary>
    public void OnTextClicked()
    {
        _parentTreeView.OnTreeViewItemContentClicked(this);
    }  
}
