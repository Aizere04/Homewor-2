using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSorter : MonoBehaviour
{
    public GameObject prefab;
    public int numberOfObjects = 10;
    public float sortingSpeed = 1.0f;

    private List<GameObject> objectsList = new List<GameObject>();

    void Start()
    {
        CreateObjects();
        StartCoroutine(SortAndVisualize());
    }

    void CreateObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject obj = Instantiate(prefab, new Vector3(i * 2.0f, Random.Range(1.0f, 5.0f), 0), Quaternion.identity);
            float randomSize = Random.Range(1.0f, 3.0f);
            obj.transform.localScale = new Vector3(randomSize, 1.0f, 1.0f);

            
            obj.AddComponent<Sortable>().size = randomSize;

            objectsList.Add(obj);
        }
    }

    IEnumerator SortAndVisualize()
    {
        
        BubbleSort(objectsList);

        
        for (int i = 0; i < objectsList.Count; i++)
        {
            StartCoroutine(MoveObject(objectsList[i], new Vector3(i * 1.0f, objectsList[i].transform.position.y, 0), sortingSpeed * i));
        }

        yield return null;
    }

    IEnumerator MoveObject(GameObject obj, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = obj.transform.position;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
    }

    void BubbleSort(List<GameObject> arr)
    {
        int n = arr.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                
                if (arr[j].GetComponent<Sortable>().size > arr[j + 1].GetComponent<Sortable>().size)
                { 

                    GameObject temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }
}


public class Sortable : MonoBehaviour
{
    public float size;
}
