using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    [Header("Volume")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private const string BGM_KEY = "BGM_VOLUME";
    private const string SFX_KEY = "SFX_VOLUME";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        // Dictionary에 자동 등록
        foreach (var clip in bgmClips)
        {
            bgmDict[clip.name] = clip;
        }

        foreach (var clip in sfxClips)
        {
            sfxDict[clip.name] = clip;
        }

        bgmVolume = PlayerPrefs.GetFloat(BGM_KEY, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
    }

    // BGM
    public void PlayBGM(string name, bool restart = false, bool loop = true)
    {
        if (!bgmDict.ContainsKey(name))
        {
            Debug.LogWarning($"BGM '{name}' 없음!");
            return;
        }

        AudioClip clip = bgmDict[name];

        if (bgmSource.clip == clip && !restart) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat(BGM_KEY, volume);
    }

    // SFX
    public void PlaySFX(string name)
    {
        if (!sfxDict.ContainsKey(name))
        {
            Debug.LogWarning($"SFX '{name}' 없음!");
            return;
        }

        sfxSource.PlayOneShot(sfxDict[name], sfxVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }
}
