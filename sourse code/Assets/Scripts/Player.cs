using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    GameObject currentFloor;
    float timeToDestroy = 1f;
    [SerializeField] float coolDown =0f;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    // Start is called before the first frame update
    int score;
    float scoreTime;
    Animator anim;
    SpriteRenderer rend;
    AudioSource deathSound;
    AudioSource shieldSound;
    [SerializeField] Text scoreText;
    [SerializeField] Text coolDownText;
    [SerializeField] AudioSource gotHit;
    Vector3 tmp;
    [SerializeField] GameObject shield;
    SpriteRenderer shieldRend;
    [SerializeField] GameObject replayButton;
    [SerializeField] GameObject returnButton;
    // SceneManagement scene;
    
    
    void Start()
    {
        Hp = 10;
        score = 0;
        scoreTime = 0;
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        deathSound = GetComponent<AudioSource>();
        shieldSound = shield.GetComponent<AudioSource>();
        shieldRend=shield.GetComponent<SpriteRenderer>(); 
        shieldRend.enabled =false;   
        coolDown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        tmp = transform.position; // get player position
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            rend.flipX = false;
            anim.SetBool("run", true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            rend.flipX = true;
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        if(coolDown<=0){ // generate shield
            if(Input.GetKey(KeyCode.Space)){
                shieldSound.Play();
                ModifyHp(1);
                shieldRend.enabled =true;
                   
                coolDown = 10f;
               
            }

        }
        if(coolDown> 0f){
            coolDown-=Time.deltaTime;
            
        }
        
        if(timeToDestroy<= 0f){
            timeToDestroy = 5f;
            shieldRend.enabled =false;
            
        }else{
            timeToDestroy-=Time.deltaTime;
        }
        // ?????????
        if (tmp.x <= -4.9f) // ????????????????????????
        {
            transform.position = new Vector3 (4.8f,tmp.y,tmp.z);
        }
        else if (transform.position.x >= 4.9)???// ????????????????????????
        {
            transform.position = new Vector3 (-4.8f,tmp.y,tmp.z);
        }

        UpdateScore();
        UpdateCoolTime();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Normal")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            { // ???????????????????????????????????????????????????
                
                currentFloor = other.gameObject;
                ModifyHp(1);
                other.gameObject.GetComponent<AudioSource>().Play();
            }


        }
        if (other.gameObject.tag == "Nail")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            { // ???????????????????????????????????????????????????
                
                currentFloor = other.gameObject;
                ModifyHp(-2);
                anim.SetTrigger("hurt");
                other.gameObject.GetComponent<AudioSource>().Play();
            }

        }
        if (other.gameObject.tag == "Ceiling")
        { //???????????????????????????????????????????????????????????????
            
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-2);
            anim.SetTrigger("hurt");
            other.gameObject.GetComponent<AudioSource>().Play();
        }


        if(shieldRend.enabled==false){
            if (other.gameObject.tag == "RedBullet")
            { //???????????????????????????
            
                ModifyHp(-2);
                gotHit.Play();
                anim.SetTrigger("hurt");
               
            
            }
            if (other.gameObject.tag == "BlueBullet")
            { //??????????????????????????????
            
                ModifyHp(-1);
                gotHit.Play();
                anim.SetTrigger("hurt");
               
            
            }

        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathLine")
        {
            ModifyHp(-10);
            Die();

        }
    }


    void ModifyHp(int num)
    { //Hp?????????
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10;
        }
        else if (Hp < 0)
        {
            Hp = 0;
            Die();
        }
        UpdateHpBar();
    }



    void UpdateHpBar()
    {
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (Hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }


    void UpdateScore() //?????????????????????
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text = "???????????????" + score.ToString() + "???";
        }
    }

    void UpdateCoolTime() //coolDown?????????
    {
    
        if (coolDown <= 0f)
        {
            coolDownText.text = "ok" ;
        }else{
            int coolText =(int)coolDown;
            coolDownText.text = coolText.ToString() ;
        }
    }

    void Die() {
        deathSound.Play();
        Time.timeScale = 0f;
        replayButton.SetActive(true);
        returnButton.SetActive(true);
    }

    public void Replay(){
        Time.timeScale = 1f;
        SceneManager.LoadScene ("SampleScene");
    }
    public void Return(){
        Time.timeScale = 1f;
        SceneManager.LoadScene ("PlayButton");
    }
}
