using UnityEngine;

namespace Script
{
    public class AttributeViewTest : MonoBehaviour
    {
        [SerializeField] private AttributeView _attributeView;

        private void Start()
        {
            for (int i = 0; i < 2; i++)
            {
                _attributeView.AddTitle().SetData($"标题{i+1}");
                for (int j = 0; j < 5; i++)
                {
                    _attributeView.AddProperty().SetData($"属性{j + 1}", $"值{j + 1}");
                }    
            }
        }
    }
}