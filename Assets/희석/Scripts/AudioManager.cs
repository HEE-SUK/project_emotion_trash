using UnityEngine;

public enum SFX
{
    // UI 상호작용
    BUTTON,
    COIN,
    EQUIPED,

    // 인게임 상호작용
    WEAPON_HAND,
    WEAPON_SWORD,
    WEAPON_AXE,
    WEAPON_HAMMER,

    //  플레이어를 제외한 상호작용
    ENEMY_DEAD,
}
public enum BGM
{
    OUTGAME,
    INGAME,
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
    private void Start() 
    {
        Instance.bgmSouce.mute = ES3.Load<bool>("MUTE_BGM", false);
        Instance.sfxSouce.mute = ES3.Load<bool>("MUTE_SFX", false);
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
        ES3.Save<bool>("MUTE_BGM", Instance.bgmSouce.mute);
    }
    public static void OnMuteSfx()
    {
        Instance.sfxSouce.mute = !Instance.sfxSouce.mute;
        ES3.Save<bool>("MUTE_SFX", Instance.sfxSouce.mute);
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
        return Instance.bgmSouce.volume;
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