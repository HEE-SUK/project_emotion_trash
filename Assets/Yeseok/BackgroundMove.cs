using UnityEngine;
using System.Collections;


    public class BackgroundMove : MonoBehaviour
    {

    private Renderer m_Renderer;
    public GameObject backScroll;
    public float BackWalltime;
    // private float _startOff, _targetOff;
    private float z = 0f;
    private void Start()
    {
        m_Renderer = backScroll.GetComponent<Renderer>();

    }



    private void Update()
    {
        z += Time.deltaTime * BackWalltime;
        if (z >= 1)
        {
            z = 1f;
            return;
        }
        // m_Renderer.material.SetTextureOffset("_MainTex", new Vector2(((1f - z) * _startOff + z * _targetOff), 0));


    }
}

