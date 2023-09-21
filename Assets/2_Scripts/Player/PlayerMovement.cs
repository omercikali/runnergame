using UniRx;
using UnityEngine;
using UniRx.Triggers;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] private float limitX;
     private float sidewaySpeed;
    [SerializeField] private Transform playerModel;

    // PLAYER ROTATE
    public float rotationSpeed = 2f; // Dönüþ hýzý
    private Quaternion targetRotation; // Hedef dönüþ rotasyonu

   
    // Global Deðiþken
    private float eksiBir;
    private float artiBir;

    private bool lockControls;
    private float _finalPos;
    private float _currentPos;

    AudioSource Runningsource;
    private float lastZPosition;



    private void Start()
    {
        sidewaySpeed = PlayerPrefs.GetFloat("SliderValue", 2);
        Runningsource = GameObject.FindWithTag("RunningAudio").GetComponent<AudioSource>();

        eksiBir = -5f;
        artiBir = 5f;
        lastZPosition = transform.position.z;
    }
    private void Update()
    {
        // Z konumu deðiþtiðinde AudioSource'u çalýþtýr veya durdur.
        if (transform.position.z != lastZPosition)
        {
            if (!Runningsource.isPlaying)
            {
                Runningsource.Play();
            }
        }
        else
        {
            if (Runningsource.isPlaying)
            {
                Runningsource.Stop();
            }
        }

        // Güncel Z konumunu kaydedin.
        lastZPosition = transform.position.z;
    }

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(x =>
            {
                if (GameEvents.instance.gameStarted.Value && !GameEvents.instance.gameLost.Value
                && !GameEvents.instance.gameWon.Value)
                {
                    MovePlayer();
                  
                }
               
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    lockControls = true;
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    lockControls = true;
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
         
           
            float percentageX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width * 0.5f) * 2;
            percentageX = Mathf.Clamp(percentageX, eksiBir, artiBir);
            _finalPos = percentageX * limitX;
           

        } 
       
       

        float delta = _finalPos - _currentPos;
        _currentPos += (delta * Time.deltaTime * sidewaySpeed);
        _currentPos = Mathf.Clamp(_currentPos, -limitX, limitX);
        playerModel.localPosition = new Vector3(0, _currentPos, 0);

        // Hedef rotasyonu hesapla
        targetRotation = Quaternion.Euler(0, Mathf.Sign(delta) * 20, 0);

        // Karakterin rotasyonunu yavaþça hedef rotasyona doðru interpolate et
      //  playerModel.rotation = Quaternion.Lerp(playerModel.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

}