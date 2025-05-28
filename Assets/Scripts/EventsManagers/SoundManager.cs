using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SoundManager handles playback of all game sound effects and background music.
/// It subscribes to various global game events to trigger the appropriate audio.
/// </summary>

public class SoundManager : MonoBehaviour
{
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

    // Environment Clips
    public AudioClip geyserEruptClip;
    public AudioClip acidRiverSplashClip;
    public AudioClip acidRiverFlowClip;
    public AudioClip keyCollectedClip;
    public AudioClip keyUsedClip;
    public AudioClip pepperCollectedClip;
    public AudioClip teratomaInteractClip;
    public AudioClip doorCloseClip;
    public AudioClip doorOpenClip;
    public AudioClip levelCompleteClip;

  
    void Start()
    {
        // Play background music on loop at game start
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        // Subscribe to player-related events (Observer Pattern)
        if (PlayerEventsManager.Instance != null)
        {
        var p = PlayerEventsManager.Instance;
        p.OnPlayerCroak += PlayPlayerCroak;
        p.OnPlayerDamaged += PlayPlayerDamaged;
        p.OnPlayerKick += PlayPlayerKick;
        p.OnPlayerFireball += PlayPlayerFireball;
        p.OnPlayerHeal += PlayPlayerHeal;
        }
        // Subscribe to enemy and boss-related events
         if (EnemiesEventsManager.Instance != null)
        {
        var enem = EnemiesEventsManager.Instance;
        enem.OnEnemyDefeated += PlayEnemyDefeated;
        enem.OnEnemyDamaged += PlayEnemyDamaged;
        enem.OnEnemySwordAttack += PlayEnemySwordAttack;
        enem.OnEnemySodaAttack += PlayEnemySodaAttack;
        enem.OnBossCherryBombs += PlayBossCherryBombs;
        enem.OnBossCharge += PlayBossCharge;
        enem.OnBossDamaged += PlayBossDamaged;
        enem.OnBossDefeated += PlayBossDefeated;
        }
        // Subscribe to environment-related events
         if (EnvironmentEventsManager.Instance != null)
        {
        var env = EnvironmentEventsManager.Instance;
        env.OnGeyserErupt += PlayGeyserErupt;
        env.OnAcidRiverSplash += PlayAcidRiverSplash;
        env.OnAcidRiverFlow += PlayAcidRiverFlow;
        env.OnKeyCollected += PlayKeyCollected;
        env.OnKeyUsed += PlayKeyUsed;
        env.OnPepperCollected += PlayPepperCollected;
        env.OnTeratomaInteract += PlayTeratomaInteract;
        env.OnDoorClose += PlayDoorClose;
        env.OnDoorOpen += PlayDoorOpen;
        env.OnLevelComplete += PlayLevelComplete;
        }
    }

    void OnDestroy()
    { 
        // Unsubscribe from all events to prevent memory leaks or null references
        if (PlayerEventsManager.Instance != null)
        {
        var p = PlayerEventsManager.Instance;
        // Player
        p.OnPlayerCroak -= PlayPlayerCroak;
        p.OnPlayerDamaged -= PlayPlayerDamaged;
        p.OnPlayerKick -= PlayPlayerKick;
        p.OnPlayerFireball -= PlayPlayerFireball;
        p.OnPlayerHeal -= PlayPlayerHeal;
        }

        if (EnemiesEventsManager.Instance != null)
        {
        var enem = EnemiesEventsManager.Instance;
        // Enemy & Boss
        enem.OnEnemyDefeated -= PlayEnemyDefeated;
        enem.OnEnemyDamaged -= PlayEnemyDamaged;
        enem.OnEnemySwordAttack -= PlayEnemySwordAttack;
        enem.OnEnemySodaAttack -= PlayEnemySodaAttack;
        enem.OnBossCherryBombs -= PlayBossCherryBombs;
        enem.OnBossCharge -= PlayBossCharge;
        enem.OnBossDamaged -= PlayBossDamaged;
        enem.OnBossDefeated -= PlayBossDefeated;
        }

        if (EnvironmentEventsManager.Instance != null)
        {
        var env = EnvironmentEventsManager.Instance;
        // Environment
        env.OnGeyserErupt -= PlayGeyserErupt;
        env.OnAcidRiverSplash -= PlayAcidRiverSplash;
        env.OnAcidRiverFlow -= PlayAcidRiverFlow;
        env.OnKeyCollected -= PlayKeyCollected;
        env.OnKeyUsed -= PlayKeyUsed;
        env.OnPepperCollected -= PlayPepperCollected;
        env.OnTeratomaInteract -= PlayTeratomaInteract;
        env.OnDoorClose -= PlayDoorClose;
        env.OnDoorOpen -= PlayDoorOpen;
        env.OnLevelComplete -= PlayLevelComplete;
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

    /// Plays the given AudioClip once through the main audio source.
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
        }else
        {
            Debug.LogWarning("SoundManager: AudioClip is missing for one of the sounds!");
        }
    }
}
