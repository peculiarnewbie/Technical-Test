using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    public static ModelInteraction current;
    [SerializeField] private float swipeRotationSpeed = 5f;
    [SerializeField] private float scaleSpeed = 5f;
    Transform modelTransform;
    float modelScale = 1f;
    public event Action OnInteraction;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        modelTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ScaleModel();
    }

    public void ScaleModel()
    {
        modelTransform.transform.localScale = Vector3.one * modelScale;
    }

    public void InteractionFunction()
    {
        OnInteraction?.Invoke();
    }
}
