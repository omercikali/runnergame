using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UIManager : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    AudioSource WinSound;
    AudioSource LostSound;

    //reklamlar
    [SerializeField]
    Text text;

    Text LevelText;
    GameObject levelte;


    public delegate void ShowBanner();
    public event ShowBanner showbanner;

    private void Start()
    {

        WinSound = GameObject.FindWithTag("WinSound").GetComponent<AudioSource>();
        LostSound = GameObject.FindWithTag("LostSound").GetComponent<AudioSource>();
        levelte = GameObject.Find("LevelText");
        LevelText =levelte.gameObject.GetComponent<Text>();
        


        LevelText.text = "LEVEL"+SceneManager.GetActiveScene().buildIndex;

        showbanner?.Invoke();

    }


    void ShowAd()
    {
        UnityInterstitialAd.Instace.ShowAd();

    }






    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        gameUI.SetActive(true);
        startUI.SetActive(true);
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);

        GameEvents.instance.gameStarted.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(gameUI);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                //  
                // WIN UI

                if (value)
                {
                    WinSound.Play();
                    StartCoroutine(WaitWinUI());

                }

                IEnumerator WaitWinUI()
                {
                    yield return new WaitForSeconds(2);
                    ActivateMenu(winUI);
                    winUI.transform.DOScale(new Vector3(.67f, .67f, .67f), 1f).SetEase(Ease.OutElastic);

                    // REKLAM EKLENECEK
                    ShowAd();
                    text.text = "" + PlayerPrefs.GetInt("Coin", 0);



                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    LostSound.Play();
                    StartCoroutine(WaitLouseUI());
                }

                IEnumerator WaitLouseUI()
                {
                    yield return new WaitForSeconds(2);
                    ActivateMenu(loseUI);
                    loseUI.transform.DOScale(new Vector3(.67f, .67f, .67f), 1f).SetEase(Ease.OutElastic);
                }
            })
            .AddTo(subscriptions);
    }

    private void OnDisable()
    {
        subscriptions.Clear();
    }

    private void ActivateMenu(GameObject _menu)
    {
        gameUI.SetActive(false);
        startUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
        _menu.SetActive(true);
    }

    //Level functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {

        int activeScene = SceneManager.GetActiveScene().buildIndex;

        if (activeScene != 80)
        {
            if (activeScene >= PlayerPrefs.GetInt("Levels"))
            {
                PlayerPrefs.SetInt("Levels", activeScene + 1);
                SceneManager.LoadScene(activeScene + 1); ;
            }
            else
            {
                SceneManager.LoadScene(activeScene + 1); ;

            }
        }
        else
        {
            PlayerPrefs.SetInt("Levels",50);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Levels"));

        }

    }
    public void HomeLevel()
    {
        SceneManager.LoadScene("Main");
    }
}