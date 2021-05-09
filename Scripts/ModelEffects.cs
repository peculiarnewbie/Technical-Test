using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelEffects : MonoBehaviour
{
    Material m_Material;
    public bool isBlinking = true;
    public bool isAnimating = true;
    [SerializeField] private float blinkSpeed = 5f;
    float colorAlpha;
    float timeSpent = 0;
    public Animator modelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        ModelInteraction.current.OnInteraction += PauseEffects;
        ModelInteraction.current.OnEndInteraction += ResumeEffects;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime * blinkSpeed;
        if (isBlinking)
            colorAlpha = (Mathf.Cos(timeSpent) * 3 / 4 + 0.75f);
        else
            colorAlpha = 1f;

        m_Material.color = new Color(m_Material.color.r, m_Material.color.g, m_Material.color.b, colorAlpha);

        if (isAnimating)
            modelAnimator.speed = 1;
        else
            modelAnimator.speed = 0;
    }

    //pauses animation and blinking
    public void PauseEffects()
    {
        isBlinking = false;
        isAnimating = false;
    }

    public void ResumeEffects()
    {
        isBlinking = true;
        isAnimating = true;
    }
}
