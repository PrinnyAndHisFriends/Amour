using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class PresentSystem : MonoBehaviour
{
    public static PresentSystem instance;
    public Transform[] showPos;

    public int gotCount = 0;
    public const int MAX_COUNT = 6;

	public void OnGameFinish(){
		SceneManager.LoadScene ("gameFinished");
	}


    private void Awake()
    {
        instance = this;
    }

    public void PlayerGetPresent(Present obj)
    {
        if (gotCount < MAX_COUNT)
            obj.OnGetPresent(showPos[gotCount++].position);
        if (gotCount >= MAX_COUNT)
        {
            OnGameFinish();
        }
    }

    public Sprite GetSprite()
    {
        return ResourceManager.LoadTexture("Sprites/items/" + gotCount);
    }
    
    /// <summary>
    /// 0-5
    /// </summary>
    /// <returns></returns>
    public int GetNumber()
    {
        return gotCount;
    }
}