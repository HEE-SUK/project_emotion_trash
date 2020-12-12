using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Renderer backgroundRenderer = null;    
    public Renderer cloudRenderer = null;  
    public Renderer treeRenderer = null; 
    
    
    public PlayerCtrl player = null;
    public float backgroundSpeed = 0f;
    public float cloudSpeed = 0f;
    public float treeSpeed = 0f;


    public float backgroundOffset = 0f;
    public float cloudOffset = 0f;
    public float treeOffset = 0f;

    public void Update()
    {
        this.transform.position = this.player.transform.position;
        this.backgroundOffset += Time.deltaTime * (backgroundSpeed * this.player.rb.velocity.x);
        this.cloudOffset += Time.deltaTime *  (cloudSpeed * this.player.rb.velocity.x);
        this.treeOffset += Time.deltaTime *  (treeSpeed * this.player.rb.velocity.x);

        this.backgroundRenderer.material.SetTextureOffset("_MainTex", new Vector2(this.backgroundOffset, 0));
        this.cloudRenderer.material.SetTextureOffset("_MainTex", new Vector2(this.cloudOffset, 0));
        this.treeRenderer.material.SetTextureOffset("_MainTex", new Vector2(this.treeOffset, 0));

    }
}
