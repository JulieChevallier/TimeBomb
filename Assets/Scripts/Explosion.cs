using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator anim;
    public GameObject obj;

    [SerializeField] private AudioSource boomEffect;

    private bool periodeExplose = false;
    private float timer = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        obj.SetActive(false);
    }

    void Update()
    {
        if(periodeExplose == true)
        {
            if(timer <= 0)
            {
                anim.SetBool("fin", false);
                periodeExplose = false;
                timer = 0.5f;
                obj.SetActive(false);
            }
            timer -= Time.deltaTime;
        }
    }

    public void explose()
    {
        boomEffect.Play();
        obj.SetActive(true);
        anim.SetBool("fin", true);
        periodeExplose = true;
    }
    
}
