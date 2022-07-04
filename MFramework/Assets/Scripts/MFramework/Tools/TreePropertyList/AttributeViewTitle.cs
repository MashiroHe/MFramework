using UnityEngine;
using UnityEngine.UI;

public class AttributeViewTitle : MonoBehaviour
{
    [SerializeField] private Text _textTitle;

    public void SetData(string title)
    {
        _textTitle.text = title;
    }
}