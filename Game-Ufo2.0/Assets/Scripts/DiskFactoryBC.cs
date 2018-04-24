using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiskFactory : System.Object
{
    private static DiskFactory instance;
    private static List<GameObject> diskList = new List<GameObject>();
    private GameObject disk;


    public void setDisk(GameObject disk)
    {
        this.disk = disk;
    }


    public static DiskFactory getInstance()
    {
        if (instance == null)
        {
            instance = new DiskFactory();
        }
        return instance;
    }

  
    public GameObject getUseableDisk()
    {
        for (int i = 0; i < diskList.Count; i++)
        {
            if (diskList[i].activeInHierarchy == false)
            {
                diskList[i].SetActive(true);
                return diskList[i];
            }
        }

        diskList.Add(GameObject.Instantiate(disk) as GameObject);
        diskList[diskList.Count - 1].SetActive(true);
        return diskList[diskList.Count - 1];
    }


    public void free(GameObject disk)
    {

        disk.GetComponent<Rigidbody>().velocity = Vector3.zero;

        disk.transform.localScale = disk.transform.localScale;

        disk.SetActive(false);
    }
    
    public bool isLaunching()
    {
        for (int i = 0; i < diskList.Count; i++)
        {
            if(diskList[i].activeInHierarchy == true)
            {
                return true;
            }
        }
        return false;
    }


    public void recycleLanded()
    {
        for (int i = 0; i < diskList.Count; i++)
        {
            if (diskList[i].transform.position.y <= -8)
            {
                free(diskList[i]);
            }
        }
    }
}

public class DiskFactoryBC : MonoBehaviour
{
    public GameObject disk;

    void Awake()
    {
        // 初始化预设对象  
        DiskFactory.getInstance().setDisk(disk);
    }
}