using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingMinigameMainScript : MonoBehaviour
{
    [SerializeField] private GameObject imageGoodPrefab;
    [SerializeField] private GameObject imageBadPrefab;
    [SerializeField] private int numberSpawn;
    [SerializeField] private int dropSpeed;

    [System.Serializable]
    private struct SpawnedObject
    {
        public GameObject imageObject;
        public float imageWidth;
        public float imageHeight;
    }

    private List<SpawnedObject> _spawnedObjects = new List<SpawnedObject>();

    private int _numberSpawned = 0;
    private bool _end = false;
    private float _spawnY;
    private float _destroyY;
    private float _standartImageHeight = 300;
    private float _canvasWidth;
    private float _canvasHeight;
    private bool _spawnGood;
    private Vector3 _canvasScale;
    private Vector3[] _canvasCorners = new Vector3[4];
    private List<SpawnedObject> _removeObjects = new List<SpawnedObject>();
    private GameObject _spawnedObject;

    private void Start()
    {
        Canvas.ForceUpdateCanvases();

        _canvasScale = gameObject.GetComponent<RectTransform>().localScale;
        _canvasWidth = gameObject.GetComponent<RectTransform>().rect.width * _canvasScale.x;
        _canvasHeight = gameObject.GetComponent<RectTransform>().rect.height * _canvasScale.y;
        gameObject.GetComponent<RectTransform>().GetWorldCorners(_canvasCorners);

        Debug.Log(_canvasCorners[0].x);
        Debug.Log(_canvasCorners[3].x);
        Debug.Log(_canvasWidth);
        _spawnY = _canvasHeight + _standartImageHeight * _canvasScale.y;
        _destroyY = -_standartImageHeight * _canvasScale.y;

        StartCoroutine(DropImage());
    }

    private IEnumerator DropImage()
    {
        do
        {
            if (_numberSpawned < numberSpawn)
            {
                _spawnGood = Random.Range(0, 2) == 1;
                if(_spawnGood)
                {
                    _spawnedObject = Instantiate(imageGoodPrefab, new Vector3(0, 0, 0), new Quaternion(), gameObject.transform);
                }    
                else
                {
                    _spawnedObject = Instantiate(imageBadPrefab, new Vector3(0, 0, 0), new Quaternion(), gameObject.transform);
                }

                _spawnedObject.GetComponent<ShootImage>().UpdateSize();

                float newImageWidth = _spawnedObject.GetComponent<ShootImage>().Width * _canvasScale.x;
                float newImageHeight = _spawnedObject.GetComponent<ShootImage>().Height * _canvasScale.y;

                //Debug.Log(newImageWidth);
                //Debug.Log(_canvasWidth - newImageWidth);

                Vector3[] newImageCorners = {
                    new Vector3(0, 0),
                    new Vector3(0, newImageHeight),
                    new Vector3(newImageWidth, newImageHeight),
                    new Vector3(newImageWidth, 0)
                };
                float newImageRandomX = 0;
                bool foundPosition = false;
                bool tryPlace = true;
                int numberOfTry = 0;

                while (foundPosition == false && tryPlace == true)
                {
                    newImageRandomX = Random.Range(newImageWidth / 2 + _canvasCorners[0].x, _canvasWidth - newImageWidth / 2 + _canvasCorners[0].x);
                    //newImageRandomY = Random.Range(_spawnY, canvasHeight - newImageHeight);

                    newImageCorners[0] = new Vector3(newImageRandomX - newImageWidth / 2, _spawnY - newImageHeight / 2);
                    newImageCorners[1] = new Vector3(newImageRandomX - newImageWidth / 2, _spawnY + newImageHeight / 2);
                    newImageCorners[2] = new Vector3(newImageRandomX + newImageWidth / 2, _spawnY + newImageHeight / 2);
                    newImageCorners[3] = new Vector3(newImageRandomX + newImageWidth / 2, _spawnY - newImageHeight / 2);

                    foundPosition = true;
                    foreach (var obj in _spawnedObjects)
                    {
                        foreach (var corner in newImageCorners)
                        {
                            if (corner.x >= obj.imageObject.transform.position.x - obj.imageWidth / 2 &&
                                corner.x <= obj.imageObject.transform.position.x + obj.imageWidth / 2 &&
                                corner.y >= obj.imageObject.transform.position.y - obj.imageHeight / 2 &&
                                corner.y <= obj.imageObject.transform.position.y + obj.imageHeight / 2)
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
                    PutImage(_spawnedObject, newImageRandomX, _spawnY, newImageWidth, newImageHeight);
                }
                else
                {
                    Destroy(_spawnedObject);
                }

                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return new WaitForSeconds(5);
            }
        } while (_end == false);
    }

    private void PutImage(GameObject image, float positionX, float positionY, float newImageWidth, float newImageHeight)
    {
        image.transform.position = new Vector3(positionX, positionY);
        Debug.Log(positionX);
        image.GetComponent<Image>().enabled = true;
        SpawnedObject newStrucSpawn = new SpawnedObject();
        newStrucSpawn.imageObject = image;
        newStrucSpawn.imageWidth = newImageWidth;
        newStrucSpawn.imageHeight = newImageHeight;
        _spawnedObjects.Add(newStrucSpawn);
        _numberSpawned++;
    }

    private void Update()
    {
        MoveImages();
    }

    private void MoveImages()
    {
        foreach (var obj in _spawnedObjects)
        {
            Vector2 newPos = obj.imageObject.transform.position;
            newPos.y -= dropSpeed;
            obj.imageObject.transform.position = Vector2.Lerp(obj.imageObject.transform.position, newPos, Time.deltaTime);
            if(obj.imageObject.transform.position.y < _destroyY) 
            {
                _removeObjects.Add(obj);
            }
        }

        foreach (var removeObj in _removeObjects)
        {
            Destroy(removeObj.imageObject);
            _spawnedObjects.Remove(removeObj);
            _numberSpawned--;
        }
        _removeObjects.Clear();
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
