using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    [SerializeField] Vector2 direction;

    Vector2 offset;
    Material material;
    Vector2 textureOffset;

    void Awake()
    {
        // Create an instance of the material to avoid modifying shared material properties
        material = GetComponent<SpriteRenderer>().material;
        material = new Material(material);
        //material.shader = Shader.Find("Sprites/ScrollingSprite"); // Assign the modified shader
        GetComponent<SpriteRenderer>().material = material;
    }

    void Update()
    {
        offset = direction * scrollSpeed * Time.deltaTime;
        textureOffset += offset / material.mainTextureScale; // Calculate the texture offset
        material.mainTextureOffset = textureOffset;
    }
}
