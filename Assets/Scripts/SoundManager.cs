using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BGMType
{
    None,
    Title,
    InGame,
    InBoss,
}

public enum SEType
{
    GameClear,
    GameOver,
    Shoot,
}
public class SoundManager : MonoBehaviour
{
    public AudioClip bgmInTitle;
    public AudioClip bgmInGame;
    public AudioClip bgmInBoss;

    public AudioClip seGameClear;
    public AudioClip seGameOver;
    public AudioClip seShoot;
    public BGMType playingBGM = BGMType.None;


    //싱글톤 예시
    public static SoundManager soundManager = null;
    private void Awake()  //만약 원래 있던 오디오가있다면  파괴하고 없다면 새로 자기의 오브젝트 넣음
    {
        if(soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //싱글톤예시

    public void PlayBGM(BGMType type)
    {
        if(type != playingBGM)
        {
            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.InGame)
            {
                audio.clip = bgmInGame;
            }

            audio.Play();
        }
    }

    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }


    public void SEPlay(SEType type)
    {
        if (type == SEType.Shoot)
        {
            GetComponent<AudioSource>().PlayOneShot(seShoot); //playoneshot 메소스 1번만 
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
