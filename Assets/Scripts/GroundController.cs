using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    Animator anim;

    float changeTime;
    private bool isRed;
    public bool IsRed {
        get
        {
            return isRed;
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(GroundColorChange());
    }
    private void OnEnable()
    {
        StartCoroutine(GroundColorChange());
    }

    IEnumerator GroundColorChange()
    {
        while (true)
        {
            changeTime = Random.Range(1.0f, 4.0f);
            yield return new WaitForSeconds(changeTime);
            anim.SetBool("colorChange", true);
            yield return new WaitForSeconds(0.5f);
            isRed = true;
            yield return new WaitForSeconds(2.5f);
            anim.SetBool("colorChange", false);
            isRed = false;
        }
    }
   
}
