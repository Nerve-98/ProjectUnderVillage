using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager s_instance;
    public static SoundManager Instance { get { return s_instance; } }
    AudioSource[] _audioSources = new AudioSource[((int)Define.Sound.TypeCnt)];

    void Awake()
    {
        s_instance = this;
        Init();
    }

    private void Init()
    {
        string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            _audioSources[i] = go.AddComponent<AudioSource>();

            go.transform.parent = s_instance.transform;
        }
        _audioSources[(int)Define.Sound.BGM].loop = true;
    }
    void Start()
    {
        Play(Define.Sound.BGM, "Sound/Level1DungeonBGM", volume:0.5f);
    }

    void Update()
    {

    }
    public void Play(Define.Sound type, string path, float volume = 1.0f)
    {
        //Todo : cache music sound
        if (path.Contains("Sound/") == false)
            path = $"Sounds/{path}";
        if(type == Define.Sound.BGM)
        {
            AudioClip audioClip = Resources.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log("AudioClip is Missing, paht : " + path);
                return;
            }
            AudioSource audioSource = _audioSources[(int)Define.Sound.BGM];
            // audioSource.clip.name
            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            AudioClip audioClip = Resources.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log("AudioClip is Missing, paht : " + path);
                return;
            }
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }

    }
}
