using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoicesManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _clips;
    private List<AudioSource> _sources;
    private bool _isPlaying;

    private void Start()
    {
        _sources = new List<AudioSource>();
        foreach (AudioClip clip in _clips)
        {
            clip.LoadAudioData();
            AudioSource source = transform.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.clip = clip;
            _sources.Add(source);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonBlack)))
        {
            PlayRandomVoice();
        }
    }

    private void PlayRandomVoice()
    {
        if (_isPlaying)
            return;
        _isPlaying = true;
        int rand = Random.Range(0, _sources.Count);
        _sources[rand].Play();
        StartCoroutine(Wait(_sources[rand].clip.length));

    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        _isPlaying = false;
    }
}
