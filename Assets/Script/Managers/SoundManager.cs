using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_MANAGER_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }


    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    

    private float volume = 0.5f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_MANAGER_VOLUME, volume);
    }

    private void OnEnable()
    {
        DeliveryManager.Instance.DeliverySucces += DeliveryManager_DeliverySucces;
        DeliveryManager.Instance.DeliveryFailed += DeliveryManager_DeliveryFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void OnDisable()
    {
        DeliveryManager.Instance.DeliverySucces -= DeliveryManager_DeliverySucces;
        DeliveryManager.Instance.DeliveryFailed -= DeliveryManager_DeliveryFailed;
        CuttingCounter.OnAnyCut -= CuttingCounter_OnAnyCut;
        Player.OnPickedSomething -= Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere -= BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed -= TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object obj)
    {
        TrashCounter trashCounter = obj as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object obj)
    {
        BaseCounter baseCounter = obj as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object obj)
    {
        Player player = obj as Player;
        PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object obj)
    {
        CuttingCounter cuttingCounter = obj as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_DeliveryFailed()
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_DeliverySucces()
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootStepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1f) volume = 0f;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_MANAGER_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
