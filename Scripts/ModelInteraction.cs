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
    float currentScale = 1f;
    float modelScale;
    public event Action OnInteraction;
    public event Action OnEndInteraction;
    bool isInteracting = false;
    int fingers = 0;
    float totalPosition = 0f;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        modelTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        fingers = CheckInput(fingers);

    }

    public int CheckInput(int fingers)
    {
        int touches = Input.touchCount;
        int lastTouches = fingers;
        bool newTouch = false;

        if (lastTouches != touches)
        {
            newTouch = true;
            if (touches == 1)
                currentScale *= modelScale;
        }

        if(touches > 0)
        {
            if(touches == 1)
                RotateModel();
            else
                totalPosition = ScaleModel(touches,newTouch, totalPosition);

            if (!isInteracting)
            {
                isInteracting = true;
                OnInteraction?.Invoke();
            }
        }
        else
        {
            isInteracting = false;
            totalPosition = 0f;
            OnEndInteraction?.Invoke();
        }

        return touches;
        
    }

    public void RotateModel()
    {

    }

    public float ScaleModel(int touches, bool newTouch, float prevTotal)
    {
        Vector2[] currentPositions = new Vector2[9];
        Vector2[] lastPositions = new Vector2[9];
        float currentDistance = 0f;
        float lastDistance = prevTotal;

        for(int i = 0; i < touches; i++)
        {
            Touch touch = Input.GetTouch(i);
            currentPositions[i] = touch.position;
            lastPositions[i] = touch.position - touch.deltaPosition;
        }
        
        //calculate perimeter of fingers
        for(int i = 0; i < touches; i++)
        {
            int next = (i + 1) % touches;
            currentDistance += Vector2.Distance(currentPositions[i], currentPositions[next]);
            if (newTouch)
                lastDistance += Vector2.Distance(lastPositions[i], lastPositions[next]);
        }

        modelScale = currentDistance / lastDistance;
        modelTransform.transform.localScale = Vector3.one * currentScale * modelScale;

        return lastDistance;
    }
}
