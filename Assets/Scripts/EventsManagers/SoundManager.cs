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

    // Player Sounds
    public SoundData playerCroakSound;
    public SoundData playerDamageSound;
    public SoundData playerKickSound;
    public SoundData playerFireballSound;
    public SoundData playerHealSound;

    // Enemy and Boss Sounds
    public SoundData enemyDefeatedSound;
    public SoundData enemyDamagedSound;
    public SoundData enemySwordAttackSound;
    public SoundData enemySodaAttackSound;
    public SoundData bossCherryBombsSound;
    public SoundData bossChargeSound;
    public SoundData bossDamagedSound;
    public SoundData bossDefeatedSound;

    // Environment Sounds
    public SoundData geyserEruptSound;
    public SoundData acidRiverSplashSound;
    public SoundData acidRiverFlowSound;
    public SoundData keyCollectedSound;
    public SoundData keyUsedSound;
    public SoundData pepperCollectedSound;
    public SoundData teratomaInteractSound;
    public SoundData doorCloseSound;
    public SoundData doorOpenSound;
    public SoundData levelCompleteSound;
    public SoundData burguerHitSound;

  
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
    void PlayPlayerCroak() => PlayClip(playerCroakSound);
    void PlayPlayerDamaged() => PlayClip(playerDamageSound);
    void PlayPlayerKick() => PlayClip(playerKickSound);
    void PlayPlayerFireball() => PlayClip(playerFireballSound);
    void PlayPlayerHeal() => PlayClip(playerHealSound);

    void PlayEnemyDefeated() => PlayClip(enemyDefeatedSound);
    void PlayEnemyDamaged() => PlayClip(enemyDamagedSound);
    void PlayEnemySwordAttack() => PlayClip(enemySwordAttackSound);
    void PlayEnemySodaAttack() => PlayClip(enemySodaAttackSound);
    void PlayBossCherryBombs() => PlayClip(bossCherryBombsSound);
    void PlayBossCharge() => PlayClip(bossChargeSound);
    void PlayBossDamaged() => PlayClip(bossDamagedSound);
    void PlayBossDefeated() => PlayClip(bossDefeatedSound);

    void PlayGeyserErupt() => PlayClip(geyserEruptSound);
    void PlayAcidRiverSplash() => PlayClip(acidRiverSplashSound);
    void PlayAcidRiverFlow() => PlayClip(acidRiverFlowSound);
    void PlayKeyCollected() => PlayClip(keyCollectedSound);
    void PlayKeyUsed() => PlayClip(keyUsedSound);
    void PlayPepperCollected() => PlayClip(pepperCollectedSound);
    void PlayTeratomaInteract() => PlayClip(teratomaInteractSound);
    void PlayDoorClose() => PlayClip(doorCloseSound);
    void PlayDoorOpen() => PlayClip(doorOpenSound);
    void PlayBurguerHit() => PlayClip(burguerHitSound);
    void PlayLevelComplete() => PlayClip(levelCompleteSound);

    void PlayClip(SoundData soundData)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("SoundManager: audioSource is not assigned!");
            return;
        }

        if (soundData != null && soundData.clip != null)
        {
            audioSource.PlayOneShot(soundData.clip, soundData.volume);
        }
        else
        {
            Debug.LogWarning("SoundManager: SoundData or AudioClip is missing!");
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
            musicSource.volume = 0.3f; 
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
            musicSource.volume = 0.6f; 
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
            musicSource.volume = 0.5f; 
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
            musicSource.volume = 0.5f; 
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
