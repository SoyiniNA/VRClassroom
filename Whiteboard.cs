using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 texturesize = new Vector2(x: 2048, y: 2048);
    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D(width:(int)texturesize.x, height:(int)texturesize.y);  
        r.material.mainTexture = texture;
    }

}
