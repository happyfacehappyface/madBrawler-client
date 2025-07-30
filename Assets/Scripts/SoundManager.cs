using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _bgmSource;

    [SerializeField] private AudioSource _voiceSungjunSource;
    [SerializeField] private AudioSource _voiceSinniSource;
    [SerializeField] private AudioSource _voiceGwangho;
    [SerializeField] private AudioSource _voiceSeowooSource;
    [SerializeField] private AudioSource _voiceJaehyeonSource;

    
    [SerializeField] private AudioClip _voiceSungjunBasicAttack;
    [SerializeField] private AudioClip _voiceSungjunSkill0;
    [SerializeField] private AudioClip _voiceSungjunSkill1;
    [SerializeField] private AudioClip _voiceSungjunSkill2;

    [SerializeField] private AudioClip _voiceSinniBasicAttack;
    [SerializeField] private AudioClip _voiceSinniSkill0;
    [SerializeField] private AudioClip _voiceSinniSkill1;
    [SerializeField] private AudioClip _voiceSinniSkill2;

    [SerializeField] private AudioClip _voiceGwanghoBasicAttack;
    [SerializeField] private AudioClip _voiceGwanghoSkill0;
    [SerializeField] private AudioClip _voiceGwanghoSkill1;
    [SerializeField] private AudioClip _voiceGwanghoSkill2;

    [SerializeField] private AudioClip _voiceSeowooBasicAttack;
    [SerializeField] private AudioClip _voiceSeowooSkill0;
    [SerializeField] private AudioClip _voiceSeowooSkill1;
    [SerializeField] private AudioClip _voiceSeowooSkill2;

    [SerializeField] private AudioClip _voiceJaehyeonBasicAttack;
    [SerializeField] private AudioClip _voiceJaehyeonSkill0;
    [SerializeField] private AudioClip _voiceJaehyeonSkill1;
    [SerializeField] private AudioClip _voiceJaehyeonSkill2;

    
    private bool _isReady = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _isReady = true;
    }

    public bool IsReady()
    {
        return _isReady;
    }


    //public void PlaySfxBell(float delay) => PlayRandomSfx(_sfxBellRing, delay);

    public void PlayVoiceSungjunBasicAttack(float delay) => PlayVoiceWithDelay(CharacterType.Sungjun, _voiceSungjunBasicAttack, delay);
    public void PlayVoiceSungjunSkill0(float delay) => PlayVoiceWithDelay(CharacterType.Sungjun, _voiceSungjunSkill0, delay);
    public void PlayVoiceSungjunSkill1(float delay) => PlayVoiceWithDelay(CharacterType.Sungjun, _voiceSungjunSkill1, delay);
    public void PlayVoiceSungjunSkill2(float delay) => PlayVoiceWithDelay(CharacterType.Sungjun, _voiceSungjunSkill2, delay);

    public void PlayVoiceSinniBasicAttack(float delay) => PlayVoiceWithDelay(CharacterType.Sinni, _voiceSinniBasicAttack, delay);
    public void PlayVoiceSinniSkill0(float delay) => PlayVoiceWithDelay(CharacterType.Sinni, _voiceSinniSkill0, delay);
    public void PlayVoiceSinniSkill1(float delay) => PlayVoiceWithDelay(CharacterType.Sinni, _voiceSinniSkill1, delay);
    public void PlayVoiceSinniSkill2(float delay) => PlayVoiceWithDelay(CharacterType.Sinni, _voiceSinniSkill2, delay);

    public void PlayVoiceGwanghoBasicAttack(float delay) => PlayVoiceWithDelay(CharacterType.Gwangho, _voiceGwanghoBasicAttack, delay);
    public void PlayVoiceGwanghoSkill0(float delay) => PlayVoiceWithDelay(CharacterType.Gwangho, _voiceGwanghoSkill0, delay);
    public void PlayVoiceGwanghoSkill1(float delay) => PlayVoiceWithDelay(CharacterType.Gwangho, _voiceGwanghoSkill1, delay);
    public void PlayVoiceGwanghoSkill2(float delay) => PlayVoiceWithDelay(CharacterType.Gwangho, _voiceGwanghoSkill2, delay);

    public void PlayVoiceSeowooBasicAttack(float delay) => PlayVoiceWithDelay(CharacterType.Seowoo, _voiceSeowooBasicAttack, delay);
    public void PlayVoiceSeowooSkill0(float delay) => PlayVoiceWithDelay(CharacterType.Seowoo, _voiceSeowooSkill0, delay);
    public void PlayVoiceSeowooSkill1(float delay) => PlayVoiceWithDelay(CharacterType.Seowoo, _voiceSeowooSkill1, delay);
    public void PlayVoiceSeowooSkill2(float delay) => PlayVoiceWithDelay(CharacterType.Seowoo, _voiceSeowooSkill2, delay);

    public void PlayVoiceJaehyeonBasicAttack(float delay) => PlayVoiceWithDelay(CharacterType.Jaehyeon, _voiceJaehyeonBasicAttack, delay);
    public void PlayVoiceJaehyeonSkill0(float delay) => PlayVoiceWithDelay(CharacterType.Jaehyeon, _voiceJaehyeonSkill0, delay);
    public void PlayVoiceJaehyeonSkill1(float delay) => PlayVoiceWithDelay(CharacterType.Jaehyeon, _voiceJaehyeonSkill1, delay);
    public void PlayVoiceJaehyeonSkill2(float delay) => PlayVoiceWithDelay(CharacterType.Jaehyeon, _voiceJaehyeonSkill2, delay);

    private void PlayRandomSfx(AudioClip[] clips, float delay)
    {
        if (clips != null && clips.Length > 0)
        {
            int randomIndex = Random.Range(0, clips.Length);
            if (delay <= 0f)
            {
                PlaySfx(clips[randomIndex]);
            }
            else
            {
                StartCoroutine(CO_PlaySfxWithDelay(clips[randomIndex], delay));
            }
        }
    }

    private void PlaySfx(AudioClip clip)
    {
        if (clip != null)
        {
            _sfxSource.PlayOneShot(clip);
        }
    }


    private IEnumerator CO_PlaySfxWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySfx(clip);
    }

    private void PlayVoiceWithDelay(CharacterType characterType, AudioClip clip, float delay)
    {
        if (clip != null)
        {
            if (delay <= 0f)
            {
                PlayVoice(characterType, clip);
            }
            else
            {
                StartCoroutine(CO_PlayVoiceWithDelay(characterType, clip, delay));
            }
        }
    }

    private IEnumerator CO_PlayVoiceWithDelay(CharacterType characterType, AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayVoice(characterType, clip);
    }


    private void PlayVoice(CharacterType characterType, AudioClip clip)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
                _voiceSungjunSource.PlayOneShot(clip);
                break;
            case CharacterType.Sinni:
                _voiceSinniSource.PlayOneShot(clip);
                break;
            case CharacterType.Gwangho:
                _voiceGwangho.PlayOneShot(clip);
                break;
            case CharacterType.Seowoo:
                _voiceSeowooSource.PlayOneShot(clip);
                break;
            case CharacterType.Jaehyeon:
                _voiceJaehyeonSource.PlayOneShot(clip);
                break;
        }
    }
}
