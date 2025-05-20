using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource audioSource;   // Efectos
    [SerializeField] private AudioSource musicSource;   // Música
    [SerializeField] private AudioClip backgroundMusic;  // música

    // Player Clips
    public AudioClip playerCroakClip;
    public AudioClip playerDamageClip;
    public AudioClip playerKickClip;
    public AudioClip playerFireballClip;
    public AudioClip playerHealClip;

    // Enemy and Boss Clips
    public AudioClip enemyDefeatedClip;
    public AudioClip enemyDamagedClip;
    public AudioClip enemySwordAttackClip;
    public AudioClip enemySodaAttackClip;
    public AudioClip bossCherryBombsClip;
    public AudioClip bossChargeClip;
    public AudioClip bossDamagedClip;
    public AudioClip bossDefeatedClip;

    // Hazard Clips
    public AudioClip geyserEruptClip;
    public AudioClip acidRiverSplashClip;
    public AudioClip acidRiverFlowClip;

    // Item/Interaction Clips
    public AudioClip keyCollectedClip;
    public AudioClip keyUsedClip;
    public AudioClip pepperCollectedClip;
    public AudioClip teratomaInteractClip;
    public AudioClip doorCloseClip;
    public AudioClip doorOpenClip;
    public AudioClip levelCompleteClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Aseguramos que solo haya una instancia
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // La instancia no se destruye al cambiar de escena
        }
    }

    void Start()
    {
        // Reproduce la música en loop
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
          if (GameEventsManager.Instance != null)
        {
        var g = GameEventsManager.Instance;

        // Player
        g.OnPlayerCroak += PlayPlayerCroak;
        g.OnPlayerDamaged += PlayPlayerDamaged;
        g.OnPlayerKick += PlayPlayerKick;
        g.OnPlayerFireball += PlayPlayerFireball;
        g.OnPlayerHeal += PlayPlayerHeal;

        // Enemy & Boss
        g.OnEnemyDefeated += PlayEnemyDefeated;
        g.OnEnemyDamaged += PlayEnemyDamaged;
        g.OnEnemySwordAttack += PlayEnemySwordAttack;
        g.OnEnemySodaAttack += PlayEnemySodaAttack;
        g.OnBossCherryBombs += PlayBossCherryBombs;
        g.OnBossCharge += PlayBossCharge;
        g.OnBossDamaged += PlayBossDamaged;
        g.OnBossDefeated += PlayBossDefeated;

        // Hazards
        g.OnGeyserErupt += PlayGeyserErupt;
        g.OnAcidRiverSplash += PlayAcidRiverSplash;
        g.OnAcidRiverFlow += PlayAcidRiverFlow;

        // Items & Interactions
        g.OnKeyCollected += PlayKeyCollected;
        g.OnKeyUsed += PlayKeyUsed;
        g.OnPepperCollected += PlayPepperCollected;
        g.OnTeratomaInteract += PlayTeratomaInteract;
        g.OnDoorClose += PlayDoorClose;
        g.OnDoorOpen += PlayDoorOpen;
        g.OnLevelComplete += PlayLevelComplete;
        }
    }

    void OnDestroy()
    { 
        if (GameEventsManager.Instance != null)
        {
        var g = GameEventsManager.Instance;

        // Player
        g.OnPlayerCroak -= PlayPlayerCroak;
        g.OnPlayerDamaged -= PlayPlayerDamaged;
        g.OnPlayerKick -= PlayPlayerKick;
        g.OnPlayerFireball -= PlayPlayerFireball;
        g.OnPlayerHeal -= PlayPlayerHeal;

        // Enemy & Boss
        g.OnEnemyDefeated -= PlayEnemyDefeated;
        g.OnEnemyDamaged -= PlayEnemyDamaged;
        g.OnEnemySwordAttack -= PlayEnemySwordAttack;
        g.OnEnemySodaAttack -= PlayEnemySodaAttack;
        g.OnBossCherryBombs -= PlayBossCherryBombs;
        g.OnBossCharge -= PlayBossCharge;
        g.OnBossDamaged -= PlayBossDamaged;
        g.OnBossDefeated -= PlayBossDefeated;

        // Hazards
        g.OnGeyserErupt -= PlayGeyserErupt;
        g.OnAcidRiverSplash -= PlayAcidRiverSplash;
        g.OnAcidRiverFlow -= PlayAcidRiverFlow;

        // Items & Interactions
        g.OnKeyCollected -= PlayKeyCollected;
        g.OnKeyUsed -= PlayKeyUsed;
        g.OnPepperCollected -= PlayPepperCollected;
        g.OnTeratomaInteract -= PlayTeratomaInteract;
        g.OnDoorClose -= PlayDoorClose;
        g.OnDoorOpen -= PlayDoorOpen;
        g.OnLevelComplete -= PlayLevelComplete;
        }
    }

    // Métodos por cada clip
    void PlayPlayerCroak() => PlayClip(playerCroakClip);
    void PlayPlayerDamaged() => PlayClip(playerDamageClip);
    void PlayPlayerKick() => PlayClip(playerKickClip);
    void PlayPlayerFireball() => PlayClip(playerFireballClip);
    void PlayPlayerHeal() => PlayClip(playerHealClip);

    void PlayEnemyDefeated() => PlayClip(enemyDefeatedClip);
    void PlayEnemyDamaged() => PlayClip(enemyDamagedClip);
    void PlayEnemySwordAttack() => PlayClip(enemySwordAttackClip);
    void PlayEnemySodaAttack() => PlayClip(enemySodaAttackClip);
    void PlayBossCherryBombs() => PlayClip(bossCherryBombsClip);
    void PlayBossCharge() => PlayClip(bossChargeClip);
    void PlayBossDamaged() => PlayClip(bossDamagedClip);
    void PlayBossDefeated() => PlayClip(bossDefeatedClip);

    void PlayGeyserErupt() => PlayClip(geyserEruptClip);
    void PlayAcidRiverSplash() => PlayClip(acidRiverSplashClip);
    void PlayAcidRiverFlow() => PlayClip(acidRiverFlowClip);

    void PlayKeyCollected() => PlayClip(keyCollectedClip);
    void PlayKeyUsed() => PlayClip(keyUsedClip);
    void PlayPepperCollected() => PlayClip(pepperCollectedClip);
    void PlayTeratomaInteract() => PlayClip(teratomaInteractClip);
    void PlayDoorClose() => PlayClip(doorCloseClip);
    void PlayDoorOpen() => PlayClip(doorOpenClip);
    void PlayLevelComplete() => PlayClip(levelCompleteClip);

    void PlayClip(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("SoundManager: audioSource is not assigned!");
            return;
        }

        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

