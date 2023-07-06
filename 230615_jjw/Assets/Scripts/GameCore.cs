using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCore : MonoBehaviour{

    public enum GameStatus{
        Ready,
        Init,
        Play,
        GameOver,
    }

    public GameStatus gameStatus;

    private static GameCore instance;

    public static GameCore Instance{
        get{
            return instance;
        }
    }

    [Header("Game UI")]
    [SerializeField] GameObject pannel;
    [SerializeField] Button playBtn;
    [SerializeField] GameObject countdownPannel;
    [SerializeField] TextMeshProUGUI countdownLabel;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] GameObject[] hpObject;
    [SerializeField] TextMeshProUGUI bestScore;

    [Header("Game Core object")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private GameObject bullet;


    int score;
    int bestscore;
    float delay = 0f;
    private bool isFeverTime = false;
    float fireCondition = 1f;

    Player playerInfo;

    float countdown = 3f;

    private int wave = 0;

    private int[] waveEnemey ={10,20,30,50,100,300,400,500,600,800,1000,1200,1500};

    public int Wave
    {
        get
        {
            return this.wave;
        }
    }

    public int GetWaveEnemey
    {
        get
        {
            return this.waveEnemey[wave];
        }
    }

    public void IncreaseWave(){

        Debug.Log($"Increase Wave ==> {wave}");
        this.wave += 1;
    }


    private void Awake(){
        instance = this;
    }

    // UI Method
    public void PlayGame(){
        CloseGamePannel();
        SetGameStatus(GameStatus.Init);
    }


    private void GameOver(){
        wave = 0;
        SaveData();
        score = 0;

        OpenPannel();
        SetGameStatus(GameStatus.GameOver);
        scoreLabel.gameObject.SetActive(false);
    }



    private void OpenPannel(){
        pannel.gameObject.SetActive(true);
    }

    private void CloseGamePannel(){
        pannel.gameObject.SetActive(false);
    }


    public void SetGameStatus(GameStatus status){
        this.gameStatus = status;
    }


    // Core Method
    void Start(){
        playerInfo = new Player(3, 10);
        if (bestscore == 0)
        {
            scoreLabel.gameObject.SetActive(false);
        }
        else
        {
            scoreLabel.gameObject.SetActive(true);
            scoreLabel.text = bestscore.ToString();
        }
        playBtn.onClick.AddListener(PlayGame);
        for (int i = 0; i < hpObject.Length; i++){
            hpObject[i].gameObject.SetActive(false);
        }

    }


    public void EarnItem(){
        if (isFeverTime) return;
        isFeverTime = true;
        fireCondition = 0.09f;
        StartCoroutine(FeverTimeUpdate());
    }



    private void StopFeverTime(){
        if (!isFeverTime) return;
        isFeverTime = false;
        fireCondition = 1f;
        StopCoroutine(FeverTimeUpdate());
    }

    // Update is called once per frame
    void Update(){

        PlayerMove();

        if(gameStatus == GameStatus.Init){
            InitScore();
            InitPlayer();
            ShowCountDownUI();
        }
        else if(gameStatus == GameStatus.Play){

            delay += Time.deltaTime;
            if(delay >= fireCondition){
                delay = 0f;

                GameObject obj = Instantiate(bullet, Vector3.zero, Quaternion.identity, bulletSpawn.transform);
                obj.transform.position = player.transform.position;
                int dmg = playerInfo.Atk;
                obj.GetComponent<Bullet>().InitBullet(dmg);

                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3f);
            }
        }
        else
        {
        }
    }

    private void PlayerMove()
    {
    
    }


    void InitPlayer(){
        playerInfo.Init(3);
    }

    // hp �ʱ�ȭ ���ִ� ��.
    void InitHpObject()
    {
        for (int i = 0; i < 3; i++)
        {
            hpObject[i].gameObject.SetActive(true);
        }
    }


    void ShowCountDownUI(){
        if (!countdownPannel.gameObject.activeSelf) countdownPannel.gameObject.SetActive(true);


        countdown -= Time.deltaTime;
        int number = (int)countdown+1;
        countdownLabel.text = number.ToString();

        if(countdown <= 0f){
            countdown = 3f;
            countdownPannel.gameObject.SetActive(false);
            SetGameStatus(GameStatus.Play);
            scoreLabel.gameObject.SetActive(true);
            InitHpObject();

        }

    }

    public void IncreaseScore(int v = 10){
        score += v;
        scoreLabel.text = $"Score : {score}";
    }

    private void InitScore(){
        score = 0;

        bestScore.text = $"Best : {LoadData()}"; //����Ʈ ���ھ� ���


    }



    IEnumerator FeverTimeUpdate()
    {
        float feverTime = 1.5f;

        while (true){
            if (gameStatus != GameStatus.Play) break;


            feverTime -= Time.deltaTime;
            if (feverTime <= 0){
                StopFeverTime();
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void HitDamage(){
        playerInfo.HitDamage();

        int hpIndex = playerInfo.Hp -1;

        if(hpIndex == -1){
            GameOver();
        }
        else{
            hpObject[hpIndex].gameObject.SetActive(false);
        }

    }



    private void SaveData(){
        if(score > bestscore)
        { 
            PlayerPrefs.SetInt("_Score", score);
            PlayerPrefs.Save();
        }
    }


    private int LoadData(){
         return PlayerPrefs.GetInt("_Score", 0);
    }




}
