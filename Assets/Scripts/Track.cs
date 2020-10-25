using UnityEngine;

public class Track : MonoBehaviour
{
    private Texture2D texture;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    Color[] colors;


    void Start()
    {
        texture = new Texture2D(400, 300);
        sprite = Sprite.Create(texture, new Rect(0, 0, 400, 300), Vector2.zero);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        colors = new Color[400];
        for (int i = 0; i < 400; i++)
        {
            if (i < 200)
            {
                colors[i] = Color.red;
            }
            else
            {
                colors[i] = Color.blue;
            }
        }
    }

    void Update()
    {
        for (int y = 0; y < texture.height; y++)
        {
            texture.SetPixels(0, y, texture.width, 1, colors);
        }
        texture.Apply();
    }

    //private void Start()
    //{
    //    texture = new Texture2D(400, 300);
    //    sprite = Sprite.Create(texture, new Rect(0, 0, 400, 300), Vector2.zero);
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    spriteRenderer.sprite = sprite;
    //}

    //void Update()
    //{
    //    for (int y = 0; y < texture.height; y++)
    //    {
    //        Color[] colors = new Color[3];
    //        colors[0] = Color.red;
    //        colors[1] = Color.green;
    //        colors[2] = Color.blue;
    //        texture.SetPixels(0, y, texture.width, 1, colors);
    //    }
    //    //for (int y = 0; y < texture.height; y++)
    //    //{
    //    //    for (int x = 0; x < texture.width; x++) //Goes through each pixel
    //    //    {
    //    //        Color pixelColour;
    //    //        if (Random.Range(0, 2) == 1) //50/50 chance it will be black or white
    //    //        {
    //    //            pixelColour = new Color(0, 0, 0, 1);
    //    //        }
    //    //        else
    //    //        {
    //    //            pixelColour = new Color(1, 1, 1, 1);
    //    //        }
    //    //        texture.SetPixel(x, y, pixelColour);
    //    //    }
    //    //}
    //    texture.Apply();
    //}
}