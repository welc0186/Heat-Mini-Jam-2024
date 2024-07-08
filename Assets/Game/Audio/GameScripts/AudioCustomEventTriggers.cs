using UnityEngine;
using Alf.GameManagement;
using Alf.Utils;

public class NewGameCustomEvent        : ICustomEvent {public CustomEvent Event => GameEvents.onNewGame;}
public class GameOverCustomEvent       : ICustomEvent {public CustomEvent Event => GameEvents.onGameOver;}
public class MainMenuCustomEvent       : ICustomEvent {public CustomEvent Event => GameEvents.onMainMenuLoaded;}
public class BalloonCrashEvent         : ICustomEvent {public CustomEvent Event => PlayerEvents.onPlayerLoseLife;}
public class PowerUpPickupEvent        : ICustomEvent {public CustomEvent Event => PlayerEvents.onPowerUpPickedUp;}
public class HeatWaveSpawnedEvent      : ICustomEvent {public CustomEvent Event => PlayerEvents.onHeatWaveSpawned;}
public class PlayerMoveEvent           : ICustomEvent {public CustomEvent Event => PlayerEvents.onPlayerMove;}
public class PlayerSwitchEvent         : ICustomEvent {public CustomEvent Event => PlayerEvents.onPlayerSwitchedDirection;}
