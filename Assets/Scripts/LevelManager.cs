using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //Выбираем у слоя рандомный обжект и вставляем в место попадания
                LayerObj obj = hit.transform.GetComponent<LayerObj>();
                if(obj != null)
                {
                    //GetSpawnableItem(obj);
                    //int index = Random.Range(0,obj.spawnObjects.Count);
                    //Item item = obj.spawnObjects[index];
                    Item item = GetSpawnableItem(obj);
                    if(CheckCount(hit))
                    {
                        SpawnItem(item, hit);
                    } else
                    {
                        DeleteItem(hit);
                        SpawnItem(item,hit);
                    }
                    
                    
                }
            }
            
        }

    }
    //Проверить количество элементов на слое
    private bool CheckCount(RaycastHit2D hit)
    {
        LayerObj obj = hit.transform.GetComponent<LayerObj>();
        int spawnedCount = obj.spawnedObjects.Count;
        int maxCount = obj.maxCount;
        if (spawnedCount<=maxCount)
        {
            return true;
        } else
        {
            return false;
        }
    }
    //Создание элемента
    private void SpawnItem(Item item,RaycastHit2D hit)
    {
        GameObject newItem = Instantiate(item.gameObject, hit.point, Quaternion.identity);
        newItem.transform.parent = hit.transform;
        hit.transform.GetComponent<LayerObj>().spawnedObjects.Enqueue(newItem.GetComponent<Item>());
    }
    //Удаление элемента
    private void DeleteItem(RaycastHit2D hit)
    {
        Item delItem = hit.transform.GetComponent<LayerObj>().spawnedObjects.Dequeue();
        Destroy(delItem.gameObject);
    }
    //Получить индекс спавна
    private Item GetSpawnableItem(LayerObj obj)
    {
        List<Item> spawnObjects = new List<Item>(obj.spawnObjects); //Выборка того что может быть заспавнено
        
        List<Item> spawnedObjects = obj.spawnedObjects.ToList<Item>(); //Уже заспавленные на слое
        Debug.Log("SpawnObjects COunt "+spawnObjects.Count);
        for(int i=0; i<obj.spawnObjects.Count;i++)
        {
            int maxCount = obj.spawnObjects[i].maxStack;
            int count = 0;
            foreach (Item item in spawnedObjects)
            {
                if (item.sysName == obj.spawnObjects[i].sysName)
                {
                    count++;
                }
            }

            if (count >= maxCount)
            {
                Debug.Log("Remove index " + obj.spawnObjects[i].sysName + "-"+ i);
                spawnObjects.RemoveAt(i);
            }
        }
        Debug.Log(spawnObjects.Count);
        int index = Random.Range(0, spawnObjects.Count);
        Debug.Log(index);
        Item itemRes = spawnObjects[index];
        return itemRes;
    }
}
