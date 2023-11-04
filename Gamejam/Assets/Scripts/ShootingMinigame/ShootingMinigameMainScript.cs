using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingMinigameMainScript : MonoBehaviour
{
    [SerializeField] private GameObject imageGoodPrefab;
    [SerializeField] private GameObject imageBadPrefab;
    [SerializeField] private Image codeImages;
    [SerializeField] private int numberSpawn;

    private struct SpawnedObject
    {
        public GameObject imageObject;
        public float[] imageSides;
    }

    private List<SpawnedObject> _spawnedObjects = new List<SpawnedObject>();

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
                GameObject newObject = Instantiate(imageGoodPrefab, new Vector3(-500, -500, 0), new Quaternion(), gameObject.transform);
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

                if (foundPosition)
                {
                    PutImage(newObject, newImageRandomX, newImageRandomY, newImageCorners);
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

    private void PutImage(GameObject image, float positionX, float positionY, Vector3[] corners)
    {
        image.transform.position = new Vector3(positionX, positionY);
        image.GetComponent<Image>().enabled = true;
        SpawnedObject newStrucSpawn = new SpawnedObject();
        newStrucSpawn.imageObject = image;
        newStrucSpawn.imageSides = new float[4] { corners[0].x, corners[2].x, corners[0].y, corners[2].y };
        _spawnedObjects.Add(newStrucSpawn);
        _numberSpawned++;
    }

    public void ClearImage(GameObject image)
    {
        foreach (var obj in _spawnedObjects)
        {
            if (obj.imageObject == image)
            {
                _spawnedObjects.Remove(obj);
                _numberSpawned--;
                _end = false;
                break;
            }
        }
    }
}
