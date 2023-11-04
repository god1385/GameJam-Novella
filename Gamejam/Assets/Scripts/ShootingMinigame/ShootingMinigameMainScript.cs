using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class ShootingMinigameMainScript : MonoBehaviour
{
    [SerializeField] private GameObject imageGoodPrefab;
    [SerializeField] private GameObject imageBadPrefab;
    [SerializeField] private Image codeImages;
    [SerializeField] private int numberSpawn;

    private struct _spawnedObject {
        public GameObject imageObject;
        public float[] imageSides;
    }

    private List<_spawnedObject> _spawnedObjects = new List<_spawnedObject>();

    private int _numberSpawned = 0;
    private bool _end = false;
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        StartCoroutine(DropImage());
    }

    void Update()
    {
        
    }

    private IEnumerator DropImage()
    {
        do
        {
            if (_numberSpawned < numberSpawn)
            {
                GameObject newObject = Instantiate(imageGoodPrefab, new Vector3(-500,-500,0), new Quaternion(), gameObject.transform);
                newObject.GetComponent<Image>().enabled = false;

                newObject.GetComponent<ShootImage>().UpdateSize();
                float newImageWidth = newObject.GetComponent<ShootImage>().Width;
                float newImageHeight = newObject.GetComponent<ShootImage>().Height;
                float canvasWidth = gameObject.GetComponent<RectTransform>().rect.width;
                float canvasHeight = gameObject.GetComponent<RectTransform>().rect.height;
                Vector3[] newImageCorners = { 
                    new Vector3(0, 0), 
                    new Vector3(0, newImageHeight), 
                    new Vector3(newImageWidth, newImageHeight), 
                    new Vector3(newImageWidth, 0) 
                };
                float newImageRandomX = 0, newImageRandomY = 0;
                bool foundPosition = false;
                bool tryPlace = true;
                int numberOfTry = 0;

                while (foundPosition == false && tryPlace == true)
                {  
                    newImageRandomX = Random.Range(newImageWidth, canvasWidth - newImageWidth);
                    newImageRandomY = Random.Range(newImageHeight, canvasHeight - newImageHeight);

                    newImageCorners[0] = new Vector3(newImageRandomX - newImageWidth / 2, newImageRandomY - newImageHeight / 2);
                    newImageCorners[1] = new Vector3(newImageRandomX - newImageWidth / 2, newImageRandomY + newImageHeight / 2);
                    newImageCorners[2] = new Vector3(newImageRandomX + newImageWidth / 2, newImageRandomY + newImageHeight / 2);
                    newImageCorners[3] = new Vector3(newImageRandomX + newImageWidth / 2, newImageRandomY - newImageHeight / 2);

                    foundPosition = true;
                    foreach (var obj in _spawnedObjects)
                    {
                        foreach (var corner in newImageCorners)
                        {
                            if (corner.x >= obj.imageSides[0] &&
                                corner.x <= obj.imageSides[1] &&
                                corner.y >= obj.imageSides[2] &&
                                corner.y <= obj.imageSides[3])
                            {
                                foundPosition = false;
                                break;
                            }
                        }

                        if (foundPosition == false)
                            break;
                    }

                    if (numberOfTry > 10)
                        tryPlace = false;
                    else
                        numberOfTry++;
                }

                if(foundPosition)
                {
                    newObject.transform.position = new Vector3(newImageRandomX, newImageRandomY);
                    newObject.GetComponent<Image>().enabled = true;
                    _spawnedObject newStrucSpawn = new _spawnedObject();
                    newStrucSpawn.imageObject = newObject;
                    newStrucSpawn.imageSides = new float[4] { newImageCorners[0].x, newImageCorners[2].x, newImageCorners[0].y, newImageCorners[2].y };
                    _spawnedObjects.Add(newStrucSpawn);
                    _numberSpawned++;
                }
                else
                {
                    Destroy(gameObject);
                }
       
                yield return new WaitForSeconds(1);
            } 
            else
            {
                _end = true;
                yield return new WaitForSeconds(5);
            }
        } while (_end == false);        
    }

    public void clearImage(GameObject image)
    {
        foreach (var obj in _spawnedObjects)
        {
            if(obj.imageObject == image)
            {
                _spawnedObjects.Remove(obj);
                _numberSpawned--;
                _end = false;
                break;
            }
        }
    }
}
