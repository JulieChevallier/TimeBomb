using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource collectSoundEffect;

    [Header("Wind")]
    [SerializeField] private Transform zoneWind;

    [SerializeField] float time;

    private float timeStore;
    [SerializeField] private ParticleSystem windParticlesL;
    [SerializeField] private ParticleSystem windParticlesR;


    [Header("Slow")]
    private bool isSlow = false;
    [SerializeField] private float vitesse;
    [SerializeField] private float ralenti;

    private float currentVitesse;

    [Header("Respawn")]
    [SerializeField] float timeToRespawn;
    //private float timeStoreRespawn;

    private IEnumerator co;

    public bool action = false;

    [Header("Snow")]
    [SerializeField] private ParticleSystem snowParticle;
    [SerializeField] private Tilemap neigeLayer;
    private float currantAlpha;
    [SerializeField] private float vitesseTransition;
    private bool isSnow;


    void Start()
    {
        timeStore = time;
    }

    void Update()
    {
        if (isSlow)
        {
            Time.timeScale = Mathf.SmoothDamp(Time.timeScale, ralenti, ref currentVitesse, vitesse);
        }
        else
        {
            Time.timeScale = Mathf.SmoothDamp(Time.timeScale, 1f, ref currentVitesse, vitesse);
        }

        if (isSnow)
        {
            neigeLayer.color = new Color(neigeLayer.color.r, neigeLayer.color.g, neigeLayer.color.b, Mathf.SmoothDamp(neigeLayer.color.a, 1f, ref currantAlpha, vitesseTransition));
        }
        
        //Arreter

        if (action)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;

            }
            else
            {
                zoneWind.gameObject.SetActive(false);
                windParticlesL.Stop();
                windParticlesR.Stop();

                snowParticle.Stop();
                GameObject.Find("Player1").GetComponent<PlayerMovement>().ice = false;
                GameObject.Find("Player2").GetComponent<Player2Movement>().ice = false;

                Time.timeScale = 1f;
                isSlow = false;
                isSnow = false;
                action = false;
            }
        }
    }



    private void Wind()
    {
        AreaEffector2D eff = zoneWind.gameObject.GetComponent(typeof(AreaEffector2D)) as AreaEffector2D;
        int direction = Random.Range(0, 2);
        eff.forceAngle = direction * 180;
        zoneWind.gameObject.SetActive(true);

        if (direction == 0)
        {
            windParticlesL.Play();
        }
        else
        {
            windParticlesR.Play();
        }

    }

    private void Slow()
    {
        isSlow = true;
    }

    IEnumerator LateCall(float seconds)
    {
        GameObject obj = GameObject.Find("Item");
        obj.SetActive(false);

        yield return new WaitForSeconds(seconds);

        obj.SetActive(true);
    }

    private void Snow()
    {
        snowParticle.Play();
        GameObject.Find("Player1").GetComponent<PlayerMovement>().ice = true;
        GameObject.Find("Player2").GetComponent<Player2Movement>().ice = true;
        isSnow = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            time = timeStore;
            collectSoundEffect.Play();
            action = true;

            co = LateCall(timeToRespawn);
            StartCoroutine(co);

            int choix = Random.Range(0, 3);
            switch (choix)
            {
                case 0:
                    Wind();
                    break;
                case 1:
                    Slow();
                    break;
                case 2:
                    Snow();
                    break;
            }
        }
    }
}
