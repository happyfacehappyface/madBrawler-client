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
    [SerializeField] private AudioClip _voiceSungjunSkill2After;

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

    [SerializeField] private AudioClip _voiceSeowooPick;

    [SerializeField] private AudioClip _voiceJaehyeonBasicAttack;
    [SerializeField] private AudioClip _voiceJaehyeonSkill0;
    [SerializeField] private AudioClip _voiceJaehyeonSkill1;
    [SerializeField] private AudioClip _voiceJaehyeonSkill2;

    [SerializeField] private AudioClip _sfxFireball;
    [SerializeField] private AudioClip _sfxFireballHit;
    [SerializeField] private AudioClip _sfxFireballGround;
    [SerializeField] private AudioClip _sfxStupefy;
    [SerializeField] private AudioClip _sfxStupefyHit;

    [SerializeField] private AudioClip _sfxPortalOn;
    [SerializeField] private AudioClip _sfxPortalRide;
    [SerializeField] private AudioClip _sfxStorm;

    [SerializeField] private AudioClip _sfxThunder;


    [SerializeField] private AudioClip _sfxStock;
    [SerializeField] private AudioClip _sfxDinosaurRoar;
    [SerializeField] private AudioClip _sfxDinosaurStomp;
    [SerializeField] private AudioClip _sfxTennis;
    [SerializeField] private AudioClip _sfxRollingChair;

    [SerializeField] private AudioClip _sfxWhip;
    [SerializeField] private AudioClip _sfxPunch;
    [SerializeField] private AudioClip _sfxHook;
    [SerializeField] private AudioClip _sfxBump;
    [SerializeField] private AudioClip _sfxWind;
    [SerializeField] private AudioClip _sfxSword;
    [SerializeField] private AudioClip _sfxFall;
    [SerializeField] private AudioClip _sfxKarate;


    [SerializeField] private AudioClip _sfxClap;
    [SerializeField] private AudioClip _sfxJump;
    [SerializeField] private AudioClip _sfxWaveHit;
    [SerializeField] private AudioClip _sfxDebris;

    [SerializeField] private AudioClip _sfxChain;
    [SerializeField] private AudioClip _sfxLaser;
    [SerializeField] private AudioClip _sfxLaserHit;
    [SerializeField] private AudioClip _sfxComputer;

    
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
    public void PlayVoiceSungjunSkill2After(float delay) => PlayVoiceWithDelay(CharacterType.Sungjun, _voiceSungjunSkill2After, delay);
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

    public void PlayVoiceSeowooPick(float delay) => PlayVoiceWithDelay(CharacterType.Seowoo, _voiceSeowooPick, delay);

    public void PlayVoiceJaehyeonBasicAttack(float delay) => PlayVoiceWithDelay(CharacterType.Jaehyeon, _voiceJaehyeonBasicAttack, delay);
    public void PlayVoiceJaehyeonSkill0(float delay) => PlayVoiceWithDelay(CharacterType.Jaehyeon, _voiceJaehyeonSkill0, delay);
    public void PlayVoiceJaehyeonSkill1(float delay) => PlayVoiceWithDelay(CharacterType.Jaehyeon, _voiceJaehyeonSkill1, delay);
    public void PlayVoiceJaehyeonSkill2(float delay) => PlayVoiceWithDelay(CharacterType.Jaehyeon, _voiceJaehyeonSkill2, delay);


    public void PlaySfxFireball(float delay) => PlaySfxWithDelay(_sfxFireball, delay);
    public void PlaySfxFireballHit(float delay) => PlaySfxWithDelay(_sfxFireballHit, delay);
    public void PlaySfxFireballGround(float delay) => PlaySfxWithDelay(_sfxFireballGround, delay);
    public void PlaySfxStupefy(float delay) => PlaySfxWithDelay(_sfxStupefy, delay);
    public void PlaySfxStupefyHit(float delay) => PlaySfxWithDelay(_sfxStupefyHit, delay);
    public void PlaySfxPortalOn(float delay) => PlaySfxWithDelay(_sfxPortalOn, delay);
    public void PlaySfxPortalRide(float delay) => PlaySfxWithDelay(_sfxPortalRide, delay);
    public void PlaySfxStorm(float delay) => PlaySfxWithDelay(_sfxStorm, delay);
    public void PlaySfxThunder(float delay) => PlaySfxWithDelay(_sfxThunder, delay);

    public void PlaySfxDinosaurRoar(float delay) => PlaySfxWithDelay(_sfxDinosaurRoar, delay);
    public void PlaySfxDinosaurStomp(float delay) => PlaySfxWithDelay(_sfxDinosaurStomp, delay);
    public void PlaySfxTennis(float delay) => PlaySfxWithDelay(_sfxTennis, delay);
    public void PlaySfxStock(float delay) => PlaySfxWithDelay(_sfxStock, delay);
    public void PlaySfxRollingChair(float delay) => PlaySfxWithDelay(_sfxRollingChair, delay);

    public void PlaySfxWhip(float delay) => PlaySfxWithDelay(_sfxWhip, delay);
    public void PlaySfxPunch(float delay) => PlaySfxWithDelay(_sfxPunch, delay);
    public void PlaySfxHook(float delay) => PlaySfxWithDelay(_sfxHook, delay);
    public void PlaySfxBump(float delay) => PlaySfxWithDelay(_sfxBump, delay);
    public void PlaySfxWind(float delay) => PlaySfxWithDelay(_sfxWind, delay);
    public void PlaySfxSword(float delay) => PlaySfxWithDelay(_sfxSword, delay);
    public void PlaySfxFall(float delay) => PlaySfxWithDelay(_sfxFall, delay);
    public void PlaySfxKarate(float delay) => PlaySfxWithDelay(_sfxKarate, delay);

    public void PlaySfxClap(float delay) => PlaySfxWithDelay(_sfxClap, delay);
    public void PlaySfxJump(float delay) => PlaySfxWithDelay(_sfxJump, delay);
    public void PlaySfxWaveHit(float delay) => PlaySfxWithDelay(_sfxWaveHit, delay);
    public void PlaySfxDebris(float delay) => PlaySfxWithDelay(_sfxDebris, delay);

    public void PlaySfxChain(float delay) => PlaySfxWithDelay(_sfxChain, delay);
    public void PlaySfxLaser(float delay) => PlaySfxWithDelay(_sfxLaser, delay);
    public void PlaySfxLaserHit(float delay) => PlaySfxWithDelay(_sfxLaserHit, delay);
    public void PlaySfxComputer(float delay) => PlaySfxWithDelay(_sfxComputer, delay);


    

    private void PlaySfxWithDelay(AudioClip clip, float delay)
    {
        if (clip != null)
        {
            if (delay <= 0f)
            {
                PlaySfx(clip);
            }
            else
            {
                StartCoroutine(CO_PlaySfxWithDelay(clip, delay));
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
