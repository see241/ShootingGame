using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    private RectTransform rt;

    [Range(1, 5)]
    public int layerSortSpeed;

    public bool isDouble;

    private float apllySpeed;
    public float defaultSpeed;

    // Use this for initialization
    private void Start()
    {
        rt = GetComponent<RectTransform>();
        apllySpeed = defaultSpeed * layerSortSpeed * 4f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (rt.localPosition.x < -3840)
        {
            if (isDouble)
                rt.localPosition = new Vector3(7680, 0, 0);
            if (!isDouble)
                rt.localPosition = new Vector3(3840, 0, 0);
        }

        rt.localPosition += new Vector3(-apllySpeed * Time.deltaTime, 0, 0);
    }
}