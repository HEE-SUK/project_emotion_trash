using UnityEngine;
public enum SFX
{
    // UI 상호작용
    PLAYER_ATTACK,
    PLAYER_ATTACKED,
    PLAYER_JUMP,
    PLAYER_DEAD,
    MONSTER_DEAD,
    PRESS_F_KEY,
    TYPING,
    SELECT_ANSWER,
    SELECT_EMOTION,
    WARP,
}
public enum BGM
{
    INTRO,
    MAIN,
    GOOD_ENDING,
    BAD_ENDING,
}

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
	private AudioSource sfxSouce = null;
    [SerializeField]
	private AudioSource bgmSouce = null;

    [SerializeField]
    private AudioClip[] sfxList = {};
    [SerializeField]
    private AudioClip[] bgmList = {};

    private void Awake() 
    {
        // 사운드 초기화
        this.sfxSouce.loop = false;
        this.bgmSouce.loop = true;
        this.sfxSouce.volume = 0.5f;
        this.bgmSouce.volume = 0.5f;
    }

    public static void PlayBgm(BGM _bgmType)
	{
        int bgmType = (int)_bgmType;
        Instance.bgmSouce.clip = Instance.bgmList[bgmType];
        Instance.bgmSouce.Play();
	}
    public static void PlaySfx(SFX _sfxType)
    {
        int sfxNumber = (int)_sfxType;
        Instance.sfxSouce.PlayOneShot(Instance.sfxList[sfxNumber]);
    }

	public static void StopBgm()
	{
        Instance.bgmSouce.Stop();
	}

    public static void OnMuteBgm()
    {
        Instance.bgmSouce.mute = !Instance.bgmSouce.mute;
    }
    public static void OnMuteSfx()
    {
        Instance.sfxSouce.mute = !Instance.sfxSouce.mute;
    }
    public static void ChangeBgmVolume(float _value)
    {
        Instance.bgmSouce.volume = _value;
    }
    public static void ChangeSfxVolume(float _value)
    {
        Instance.sfxSouce.volume = _value;
    }
    
    public static float GetVolumeBgm()
    {
        return Instance.bgmSouce.volume;
    }
    public static float GetVolumeSfx()
    {
        return Instance.sfxSouce.volume;
    }

    public static void SetVolumeBgm(float _value)
    {
        Instance.bgmSouce.volume = _value;
    }
    public static void SetVolumeSfx(float _value)
    {
        Instance.sfxSouce.volume = _value;
    }

    public static bool GetMuteBgm()
    {
        return Instance.bgmSouce.mute;
    }
    public static bool GetMuteSfx()
    {
        return Instance.sfxSouce.mute;
    }
}