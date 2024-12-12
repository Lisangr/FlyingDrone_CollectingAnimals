using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    public GameObject particleSystem;

    public delegate void DeathAction();
    public static event DeathAction OnCollected;

    public delegate void OnDeath(int t);
    public static event OnDeath OnCollectedEnergy;
    public static event OnDeath OnCollectedGold;
    public static event OnDeath OnCollectedExp;

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    public AnimalData animalData;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        int energy = animalData.energy;
        int gold = animalData.gold;
        int exp = animalData.experience;

        if (other.CompareTag("Food"))
        {
            if (audioClips.Length > 0)
            {
                int randomIndex = Random.Range(0, audioClips.Length);
                AudioClip randomClip = audioClips[randomIndex];

                float currentVolume = PlayerPrefs.GetFloat("VolumeSFX", 1.0f);

                if (currentVolume != null)
                {
                    audioSource.volume = currentVolume;
                    audioSource.clip = randomClip;
                    AudioSource.PlayClipAtPoint(randomClip, transform.position, currentVolume);
                }
                else
                {
                    audioSource.volume = 1.0f;
                    audioSource.clip = randomClip;
                    AudioSource.PlayClipAtPoint(randomClip, transform.position);
                }

            }
            Instantiate(particleSystem, transform.position, Quaternion.identity);

            OnCollected?.Invoke();
            OnCollectedEnergy?.Invoke(energy);
            OnCollectedGold?.Invoke(gold);
            OnCollectedExp?.Invoke(exp);

            Destroy(gameObject);

        }
    }
}
