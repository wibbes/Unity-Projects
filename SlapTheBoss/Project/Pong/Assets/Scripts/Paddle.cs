using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    [Header("Object[s")]
    public Camera MainCamera;

    [Header("Prefab's")]
    public GameObject PaddlePrefab;

    [Header("Paddle Location")]
    public float PaddleOffsetX;
    public float PaddleOffsetY;

    [HideInInspector]
    public GameObject UserPaddle;

	// Use this for initialization
	void Start () {

        Vector2 PaddleOffestLocation = MainCamera.ScreenToWorldPoint.(PaddleOffset);
        PaddleOffestLocation.x = PaddleOffsetX;
        Instantiate(PaddlePrefab, new Vector3(PaddleOffestLocation.x,PaddleOffestLocation.y,0), Quaternion.identity);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
