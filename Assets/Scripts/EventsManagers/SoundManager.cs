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
    [SerializeField] private AudioClip backgroundMusic;  // música pasillos
    [SerializeField] private AudioClip victoryMusicClip; 
    [SerializeField] private AudioClip battleMusicClip; //música rooms
    [SerializeField] private AudioClip battleBossMusicClip; //música battle boss
    [SerializeField] private AudioClip gameoverMusicClip;



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
    public AudioClip burguerHitClip;

  
    void Start()
    {
        // Play background music on loop at game start
        PlayBackgroundMusic();

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
        env.OnBurguerHit += PlayBurguerHit;

        env.OnLevelComplete += PlayLevelComplete;
        env.OnVictoryMusic += PlayVictoryMusic;
        env.OnGameOverMusic += PlayGameOverMusic;
        env.OnBattleMusic += PlayBattleMusic;
        env.OnBattleBossMusic += PlayBattleBossMusic;
        }
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.OnRoomEntered += HandleRoomEnteredMusic;
            RoomManager.Instance.OnRoomExited += HandleRoomExitedMusic;
            RoomManager.Instance.OnRoomCleared += HandleRoomClearedMusic; 
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
        env.OnBurguerHit -= PlayBurguerHit;

        env.OnLevelComplete -= PlayLevelComplete;
        env.OnVictoryMusic -= PlayVictoryMusic;
        env.OnGameOverMusic -= PlayGameOverMusic;
        env.OnBattleMusic -= PlayBattleMusic;
        env.OnBattleBossMusic -= PlayBattleBossMusic;
        }
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.OnRoomEntered -= HandleRoomEnteredMusic;
            RoomManager.Instance.OnRoomExited -= HandleRoomExitedMusic;
            RoomManager.Instance.OnRoomCleared -= HandleRoomClearedMusic; 

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
    void PlayBurguerHit() => PlayClip(burguerHitClip);
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

    public void PauseMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    public void PlayVictoryMusic()
    {
        if (musicSource != null && victoryMusicClip != null)
        {
            musicSource.Stop();
            musicSource.clip = victoryMusicClip;
            musicSource.loop = true; 
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Victory music clip or musicSource not assigned!");
        }
    }

    public void PlayGameOverMusic()
    {
        if (musicSource != null && gameoverMusicClip != null)
        {
            musicSource.Stop();
            musicSource.clip = gameoverMusicClip;
            musicSource.loop = true; 
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("GameOver music clip or musicSource not assigned!");
        }
    }

    public void PlayBattleMusic()
    {
        if (musicSource != null && battleMusicClip != null)
        {
            musicSource.Stop();
            musicSource.clip = battleMusicClip;
            musicSource.loop = true; 
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Battle music clip or musicSource not assigned!");
        }
    }

    public void PlayBattleBossMusic()
    {
        if (musicSource != null && battleBossMusicClip != null)
        {
            musicSource.Stop();
            musicSource.clip = battleBossMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Battle Boss music clip or musicSource not assigned!");
        }
    }

    private void HandleRoomEnteredMusic(int roomID)
    {
            // Verificamos si hay enemigos vivos en la sala actual
        if (RoomManager.Instance != null && RoomManager.Instance.currentEnemies > 0)
        {
            // Si es la sala del boss
            if (roomID == 13)
            {
                if (EnvironmentEventsManager.Instance != null)
                {
                    EnvironmentEventsManager.Instance.BattleBossMusic();
                }
            }
            else
            {
                if (EnvironmentEventsManager.Instance != null)
                {
                    EnvironmentEventsManager.Instance.BattleMusic();
                }
            }
        }
        else
        {
            Debug.Log($"No hay enemigos vivos en la room {roomID}, no se reproduce música de batalla.");
            // Simplemente seguimos con la música de fondo actual
        }
    }

    private void HandleRoomExitedMusic(int roomID)
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    private void HandleRoomClearedMusic(int roomID)
    {
        Debug.Log("Room cleared, volviendo a música de pasillo");
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip or musicSource not assigned!");
        }
    }


}
