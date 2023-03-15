using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class Game : MonoBehaviour
{
    [SerializeField] private PlayerMovement player1;
    [SerializeField] private Player2Movement player2;
    [SerializeField] private CountDown countDown;
    [SerializeField] private PlayerLife player1Life;
    [SerializeField] private Player2Life player2Life;
    [SerializeField] private GameObject h1p1;
    [SerializeField] private GameObject h2p1;
    [SerializeField] private GameObject h3p1;
    [SerializeField] private GameObject h1p2;
    [SerializeField] private GameObject h2p2;
    [SerializeField] private GameObject h3p2;

    [SerializeField] private GameObject messageWinP1;
    [SerializeField] private GameObject messageWinP2;

    [SerializeField] private Explosion explosion;

    [SerializeField] private ParticleSystem windParticleL;
    [SerializeField] private ParticleSystem windParticleR;
    [SerializeField] private AreaEffector2D wind;
    [SerializeField] private Tilemap neige;

    


    private int pv1;
    private int pv2;

    private bool isEnd = false;
    private float timeEnd = 5f;

    // Start is called before the first frame update
    void Start()
    {
        messageWinP1.SetActive(false);
        messageWinP2.SetActive(false);
        pv1 = 3;
        pv2 = 3;
        int a = Random.Range(1, 3);
        //player1
        if(a == 1)
        {
            player1Bomb();
        }
        //player2
        else if(a==2)
        {
            player2Bomb();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isEnd == true)
        {

            player1Life.ResetPlayer();
            player2Life.ResetPlayer();
            countDown.ResetCountDown();

            windParticleL.Stop();
            windParticleR.Stop();
            wind.gameObject.SetActive(false);

            neige.color = new Color(neige.color.r, neige.color.g, neige.color.b, 0f);

            afficheGagnant();
            
            if(timeEnd <= 0)
            {

                EndGame();
            }
            timeEnd -= Time.deltaTime;

        }
        else
        {
            if (countDown.timeStart <= 0)
            {
                
              
                if (player1Life.bomb == true)
                {
                    pv1--;
                    if (pv1 == 2)
                    {
                        h3p1.SetActive(false);
                    }
                    if (pv1 == 1)
                    {
                        h2p1.SetActive(false);
                    }
                    if (pv1 == 0)
                    {
                        h1p1.SetActive(false);
                    }
                    player2Bomb();
                }
                else if (player2Life.bomb == true)
                {
                    pv2--;
                    if (pv2 == 2)
                    {
                        h1p2.SetActive(false);
                    }
                    if (pv2 == 1)
                    {
                        h2p2.SetActive(false);
                    }
                    if (pv2 == 0)
                    {
                        h3p2.SetActive(false);
                    }
                    player1Bomb();
                }
                newRound();

                if (pv1 == 0 || pv2 == 0)
                {
                    isEnd = true;
                }
            }
        }
    }

    private void afficheGagnant()
    {
        explosion.explose();
        if (pv1 <= 0)
        {
            messageWinP2.SetActive(true);
        }
        else
        {
            messageWinP1.SetActive(true);
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene("End_Screen");
    }

    private void newRound()
    {
        explosion.explose();
        
        

        player1Life.ResetPlayer();
        player2Life.ResetPlayer();
        countDown.ResetCountDown();

        windParticleL.Stop();
        windParticleR.Stop();
        wind.gameObject.SetActive(false);

        neige.color = new Color(neige.color.r, neige.color.g, neige.color.b, 0f);

    }
    

    private void player1Bomb()
    {
        player1Life.bomb = true;
        player2Life.bomb = false;
        player2Life.bombImageP1.SetActive(true);
        player2Life.bombImageP2.SetActive(false);
    }

    private void player2Bomb()
    {
        player1Life.bomb = false;
        player2Life.bomb = true;
        player2Life.bombImageP1.SetActive(false);
        player2Life.bombImageP2.SetActive(true);
    }
    
    
    
}
