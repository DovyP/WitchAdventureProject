using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTop;
    [SerializeField] private Transform plateGFXPrefab;

    public List<GameObject> plateGFXGameObjectList;

    private void Awake()
    {
        plateGFXGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateGFXGameObjectList[plateGFXGameObjectList.Count - 1];
        plateGFXGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateGFXTransform = Instantiate(plateGFXPrefab, counterTop);

        float plateOffsetY = .1f;
        plateGFXTransform.localPosition = new Vector3(0, plateOffsetY * plateGFXGameObjectList.Count,0);

        plateGFXGameObjectList.Add(plateGFXTransform.gameObject);
    }
}
