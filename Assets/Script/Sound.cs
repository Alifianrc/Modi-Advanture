using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;

	[Range(.1f, 10f)]
	public float pitch = 1f;

	public bool loop = false;

	[HideInInspector]
	public AudioSource source;

}