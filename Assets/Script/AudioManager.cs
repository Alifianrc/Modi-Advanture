using UnityEngine.Audio;
using System;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public Sound[] sounds;

	public static bool soundIsOn = true;
	private bool soundTurnedOff =  false;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
	}

    private void Start()
    {
		Play("BGM");
    }

    private void Update()
    {
        if(soundIsOn == false && soundTurnedOff == false)
        {
			foreach(Sound s in sounds)
            {
				if (s.source.isPlaying == true)
				{
					s.source.Pause();
				}
			}
			soundTurnedOff = true;
        }
		else if(soundIsOn == true && soundTurnedOff == true)
        {
			Play("BGM");
			soundTurnedOff = false;
		}
    }

    public void Play(string sound)
	{
        if (soundIsOn)
        {
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

            if (s.source.isPlaying == false)
            {
				s.source.Play();
			}						
		}
	}

	public void Stop(string sound)
	{
		if (soundIsOn)
		{
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			s.source.Stop();
		}
	}

	public void SetPitch(string sound, int a)
	{
		if (soundIsOn)
		{
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}

			s.pitch = a;
			s.source.pitch = a;
		}
	}
}
