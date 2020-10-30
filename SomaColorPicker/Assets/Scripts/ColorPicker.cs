## kyeong tae - dyeing table work 2020.10.30##

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    Camera Camera;
    Texture2D tex;
    // Start is called before the first frame update
    public void Start()
    {
        tex = new Texture2D(1, 1);
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        this.GetComponent<RawImage>().texture = Resources.Load<Texture2D>("milbon");
        StartCoroutine(CaptureTempArea());
    }

    IEnumerator CaptureTempArea(){
        yield return new WaitForEndOfFrame();

        // tex = new Texture2D(Screen.width, Screen.height);
        // tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        // tex.Apply();

        Vector2 pos = Input.mousePosition;
        Debug.Log("pos : " + pos);

        int x = (int)pos.x;
        int y = (int)pos.y;
        Debug.Log("x : " + x);
        Debug.Log("y : " + y);

        tex.ReadPixels(new Rect(pos.x, pos.y, 1, 1), 0, 0);
        tex.Apply();

        Color color = tex.GetPixel(x, y);

        Debug.Log(color);
        Debug.Log(255*color.r);
        Debug.Log(255*color.g);
        Debug.Log(255*color.b);
        
        // Destroy(tex);
        // tex = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestory(){
        Destroy(tex);
        tex = null;
    }
}
