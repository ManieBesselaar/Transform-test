using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _statsText;
    int _numberOfCubes = 1;
    TMP_InputField _inputField;
    [SerializeField] Transform _cubeParent;
    [SerializeField] GameObject _cubePrefab;
    int _framerateSampleSize = 30;
    int _countedCycles = 0;
   float _averagedFrameRate;
    float _currentTotalFrames = 0;
    // Start is called before the first frame update
    void Start()
    {
        _inputField = FindObjectOfType<TMP_InputField>();
        _inputField.text = "1";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        //   Debug.Log(Time.deltaTime);
        if (Time.deltaTime > 2000) DestroyAllCubes(); //Destroy all the cubes if I really oveload my thread
        if (Input.GetKeyDown(KeyCode.S)) {
            DestroyAllCubes();
            SpawnNewCubes(int.Parse(_inputField.text));
        }
        if (Input.GetKeyDown(KeyCode.D)) { DestroyAllCubes(); }
        // _statsText.text = (1 / Time.deltaTime).ToString();
        // _statsText.text = Profiler.GetTotalReservedMemoryLong().ToString(); 
        _statsText.text = "Memory " + (Profiler.GetTotalAllocatedMemoryLong() / 1000000).ToString() + " FrameRate " + CalculateAverageFramerate().ToString();
    }

    private void SpawnNewCubes(int v)
    {
        for (int i = 0; i < v; i++)
        {


            Instantiate(_cubePrefab, UnityEngine.Random.insideUnitSphere * UnityEngine.Random.Range(-1000, 1000), Quaternion.identity, _cubeParent);

        }
    }

    private void DestroyAllCubes()
    {
        for (int i = 1; i < _cubeParent.childCount; i++)
        {
            Destroy(_cubeParent.GetChild(i).gameObject);
        }
        _statsText.text = "Too many cubes.";
//Reset framerate calculator

        _averagedFrameRate = -1;
        _countedCycles = 0;
    }
    int CalculateAverageFramerate()
    {
        if(_countedCycles < _framerateSampleSize)
        {
            _countedCycles++; 
            _currentTotalFrames += (int)(1 / Time.deltaTime); ;
            return (int)_averagedFrameRate;
        }
        else
        {
            _averagedFrameRate = _currentTotalFrames / _framerateSampleSize;
            _countedCycles = 0;
            _currentTotalFrames = 0;
        }
        return (int)_averagedFrameRate;
    }
}
