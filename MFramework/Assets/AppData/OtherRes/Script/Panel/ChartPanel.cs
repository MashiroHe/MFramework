using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Script.Panel
{
    public class ChartPanel : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Application.isPlaying)
                {
                    SceneManager.LoadScene("entrance");
                }
            }
        }
    }
}