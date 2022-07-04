using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageEntranceItem : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private Text _desc;

    public void SetData(string imagePath, string desc)
    {
        var texture = Resources.Load<Texture2D>(imagePath);
        if (!texture)
        {
            Debug.LogError($"加载贴图失败:{imagePath}");
        }
        _image.texture = texture;

        _desc.text = desc;

    }
}
