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
    public event Action OnEndInteraction;
    bool isInteracting = false;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        modelTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    public void CheckInput()
    {
        int touches = Input.touchCount;
        if(touches > 0)
        {
            if(touches == 1)
                RotateModel();
            else
                ScaleModel(touches);

            if (!isInteracting)
            {
                isInteracting = true;
                OnInteraction?.Invoke();
            }
        }
        else
        {
            isInteracting = false;
            OnEndInteraction?.Invoke();
        }
        
    }

    public void RotateModel()
    {

    }

    public void ScaleModel(int fingers)
    {
        Vector2 total = Vector2.zero;
        Vector2 prevTotal = total;
        //Vector2 center = total;

        for(int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            total += touch.position;
            prevTotal += touch.position - touch.deltaPosition;
        }

        modelScale = total.magnitude / prevTotal.magnitude;
        modelTransform.transform.localScale = Vector3.one * modelScale;
    }
}
