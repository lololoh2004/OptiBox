using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    [Space(10)]
    [Header("= Main cfg =")]
    [SerializeField] GameObject[] buttons;
    [SerializeField] string[] buttonFunctions;

    [Space(10)]
    [Header("= Graphic cfg =")]
    [SerializeField] Vector2 circleScale = new Vector2(2.7f, 2.7f);
    [SerializeField][Range(0, 2)] float animSpeed = 1;

    [Space(10)]
    [Header("= Components cfg =")]
    Animator anim;
    RectTransform rectTransform;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rectTransform = gameObject.GetComponent<RectTransform>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("Appear", true);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("Shown", true);
        }
        else
        {
            anim.SetBool("Shown", false);
            anim.SetBool("Appear", false);
        }



    }
}
