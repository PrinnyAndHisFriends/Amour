using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager {
    
    public static Object Load(string path)
    {
        return Resources.Load(path);
    }

    public static GameObject InstantiateGO(string path, Transform parent = null)
    {
        GameObject go = GameObject.Instantiate(Resources.Load(path), parent, true) as GameObject;
        if (go)
        {
            int pos = go.name.IndexOf("(Clone)");
            go.name = go.name.Remove(pos, go.name.Length - pos);
        }
        return go;
    }

    public static Sprite LoadTexture(string path)
    {
        Texture2D texture = (Texture2D)Load(path);
        if (!texture)
        {
            Debug.LogError(path);
            return null;
        }
        Rect rect = new Rect(0f, 0f, texture.width, texture.height);
        return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }
}
