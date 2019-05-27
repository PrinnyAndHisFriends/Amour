using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Utility;
using System;

public class UIObstacleSystem : MonoBehaviour
{
    static List<string[]> obstacleRow;

    public static UIObstacleSystem instance;

    private Queue<GameObject[]> nowboard = new Queue<GameObject[]>();
    private Dictionary<string, List<int>> idDict = new Dictionary<string, List<int>>();//obstacle id ;obstacleRow id
    private Vector3[] colPoints = new Vector3[Setting.MAX_COL + 1];
    public Vector3 rowOffset;

    private int index = 0;

    private void Awake()
    {
        instance = this;

        obstacleRow = CSVReader.ReadCSVToList<string[]>("Obstacles", (string[] key) => {
            return new string[Setting.MAX_COL / 2] { key[1], key[2], key[3]};
        });

        for (int i = 0; i < obstacleRow.Count; i++)
        {
            for (int j = 0; j < obstacleRow[i].Length; j++)
            {
                if (!idDict.ContainsKey(obstacleRow[i][j]))
                {
                    idDict[obstacleRow[i][j]] = new List<int>();
                }
                idDict[obstacleRow[i][j]].Add(i);
            }
        }

        for (int i = 0; i < colPoints.Length; i++)
        {
            colPoints[i] = transform.GetChild(i).transform.localPosition;
        }
        colPoints[Setting.MAX_COL] = transform.Find("TriggerPoint").transform.localPosition;

        InitArea();
    }

    int[,] story = new int[42,3] { { 0, 3, 0 }, { 0,3,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 1, 0 }, { 0,2,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 2, 0, 0 }, { 0,0,1 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 7, 0 }, { 0,6,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 0, 0 }, { 9,0,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 4, 0 }, { 0,4,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 5, 0 }, { 3,8,3 },
         { 0, 0, 0 }, { 0,0,0 },
         { 0, 0, 0 }, { 0,0,0 },
    };

    private bool HasId(int[] data, string id)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == Convert.ToInt16(id))
                return true;
        }
        return false;
    }

    public void InitArea()
    {
        //init area
        //for (int i = 0; i < Setting.MAX_COL; i++)
        //{
        //    var gos = GetObstacleRow();
        //    SetItemsPosition(gos);
        //    nowboard.Enqueue(gos);
        //    index++;
        //}

        GameObject[] gos = new GameObject[Setting.MAX_COL + 1];
        for (int i = 0; i < 42; i+=2)
        {
            for (int j = 0; j < 3; j++)
            {
                gos[j] = ResourceManager.InstantiateGO("Prefab/" + story[i,j], transform);
            }

            for (int j = 0; j < 3; j++)
            {
                gos[j + 3] = ResourceManager.InstantiateGO("Prefab/" + story[i+1, j], transform);
            }

            for (int j = 0; j < 3; j++)
            {
                if (story[i, j] == 5)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (story[i+1, k] == 8)
                        {
                            gos[j].GetComponent<ButtonControlableObstacle>().obj =
                                gos[k + 3].GetComponent<ControlableObstacle>();
                        }
                    }
                }
            }

            if (i>=28)
            {
                gos[Setting.MAX_COL] = ResourceManager.InstantiateGO("Prefab/GenNextLineTrigger", transform);
                nowboard.Enqueue(gos);
            }
            else
                gos[Setting.MAX_COL] = ResourceManager.InstantiateGO("Prefab/0", transform);

            SetItemsPosition(gos);
            index++;
        }
    }

    public void GenNextLine()
    {
        var gos = nowboard.Dequeue();
        gos = GetObstacleRow();
        SetItemsPosition(gos);
        nowboard.Enqueue(gos);
        index++;
    }
    
    public bool AddPresentToQueue(Present obs)
    {
        int pos = UnityEngine.Random.Range(2, 5);
        int count = nowboard.Count;
        bool add = false;
        int min, max;
        if (obs.isInLeft)
        {
            min = 3;
            max = 6;
        }
        else
        {
            min = 0;
            max = 3;
        }
        for (int i = 0; i < count; i++)
        {
            var q = nowboard.Dequeue();
            if (!add && i > pos)
            {
                for (int j = min; j < max; j++)
                {
                    //被打掉为null
                    if (q[j] == null || q[j].GetComponent<EmptyObstacle>() != null)
                    {
                        obs.transform.localPosition = q[j].transform.localPosition;
                        add = true;
                    }
                }
            }
            nowboard.Enqueue(q);
        }
        return add;
    }


    private GameObject[] GetObstacleRowHalf(int id)
    {
        GameObject[] objs = new GameObject[3];
        for (int i = 0; i < objs.Length; i++)
        {
            //if (obstacleRow[id][i] != "9")
            //    obstacleRow[id][i] = "0";
            if (!initFirst && UseButton && obstacleRow[id][i] == "5")
            {
                objs[i] = ResourceManager.InstantiateGO("Prefab/" + obstacleRow[id][i], transform);
                first = objs[i];
                initFirst = true;
            }
            else if(!initFirst && UseControl && obstacleRow[id][i] == "8")
            {
                objs[i] = ResourceManager.InstantiateGO("Prefab/" + obstacleRow[id][i], transform);
                first = objs[i];
                initFirst = true;
            }
            else if (UseButton && obstacleRow[id][i] == "8")
            {
                objs[i] = ResourceManager.InstantiateGO("Prefab/" + obstacleRow[id][i], transform);
                first.GetComponent<ButtonControlableObstacle>().obj = objs[i].GetComponent<ControlableObstacle>();
                UseButton = false;
                initFirst = false;
                first = null;
            }
            else if (UseControl && obstacleRow[id][i] == "5")
            {
                objs[i] = ResourceManager.InstantiateGO("Prefab/" + obstacleRow[id][i], transform);
                objs[i].GetComponent<ButtonControlableObstacle>().obj = first.GetComponent<ControlableObstacle>();
                UseControl = false;
                initFirst = false;
                first = null;
            }
            else
            {
                objs[i] = ResourceManager.InstantiateGO("Prefab/" + obstacleRow[id][i], transform);

            }
        }
        return objs;
    }

    bool UseButton = false;
    bool UseControl = false;
    bool initFirst = false;
    GameObject first;
    //const int nineInterval = 10;
    //int nineNow = 0;

    private GameObject[] GetObstacleRow()
    {
        //nineNow++;

        //first
        GameObject[] gos = new GameObject[Setting.MAX_COL+1];
        var id = UnityEngine.Random.Range(0, obstacleRow.Count);
        UseButton = RowHasId(id, "5");
        UseControl = RowHasId(id, "8");
        var gosTemp = GetObstacleRowHalf(id);
        for (int j = 0; j < 3; j++)
        {
            if (obstacleRow[id][j] == "9")
            {
                Present a = gosTemp[j].GetComponent<Present>();
                a.isInLeft = true;
            }
        }
        if (!RowHasId(id, "4"))
            Swap(gosTemp);
        for (int j = 0; j < 3; j++)
        {
            gos[j] = gosTemp[j];
        }
        //if (RowHasId(id, "9"))
        //    nineNow = 0;

        //second
        if (UseButton)
        {
            var index = UnityEngine.Random.Range(0, idDict["8"].Count);
            id = idDict["8"][index];
        }
        else if (UseControl)
        {
            var index = UnityEngine.Random.Range(0, idDict["5"].Count);
            id = idDict["5"][index];
        }
        else
        {
            id = UnityEngine.Random.Range(0, obstacleRow.Count - 1);
            while (RowHasId(id,"5") || RowHasId(id, "8") /*|| nineNow < nineInterval && RowHasId(id, "9")*/)
            {
                id = UnityEngine.Random.Range(0, obstacleRow.Count - 1);
            }
        }
        gosTemp = GetObstacleRowHalf(id);
        if (!RowHasId(id,"4"))
            Swap(gosTemp);
        for (int j = 0; j < 3; j++)
        {
            gos[j + 3] = gosTemp[j];
        }
        gos[Setting.MAX_COL] = ResourceManager.InstantiateGO("Prefab/" + "GenNextLineTrigger", transform);
        return gos;
    }
    
    private void Swap(GameObject[] gos)
    {
        for (int i = 0; i < gos.Length - 1; i++)
        {
            int pos = UnityEngine.Random.Range(i, gos.Length - 1);
            GameObject temp = gos[i];
            gos[i] = gos[pos];
            gos[pos] = temp;
        }
    }

    private void SetItemsPosition(GameObject[] gos)
    {
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i] != null)
            {
                gos[i].transform.localPosition = colPoints[i] + rowOffset * index;
                gos[i].transform.localScale *= 0.5f;
            }
        }
    }

    bool RowHasId(int rowid, string searchid)
    {
        for (int i = 0; i < obstacleRow[rowid].Length; i++)
        {
            if (obstacleRow[rowid][i] == searchid)
                return true;
        }
        return false;
    }
}