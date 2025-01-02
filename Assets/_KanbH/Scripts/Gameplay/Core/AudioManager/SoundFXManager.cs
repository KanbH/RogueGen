using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySoundFXClip(SoundFXSO soundFXSO, Transform spawnTransform)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform);

        audioSource.clip = soundFXSO.AudioClip;
        audioSource.volume = soundFXSO.Volume;
        audioSource.pitch = soundFXSO.pitch;
        audioSource.loop = soundFXSO.loop;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);
    }
}
