# 🎮 Unity Game Template

> **A base template for rapid Unity game development**

---

## 📋 Overview

This is a **ready-to-use Unity project template** packed with essential core systems, enabling development teams to jump-start new game projects without boilerplate setup.

- **Unity Version:** 6000.3.10f1 (Unity 6)
- **Platform:** Mobile (iOS / Android)

---

## 🏗️ System Architecture

### 🔧 Manager Systems (`Assets/00 Scripts/Manager/`)

| Script | Description |
|---|---|
| `GameManager` | Manages game state, scene navigation, and ads hooks |
| `UIManager` | Manages all UI/popups with pooling & flying objects |
| `AudioManager` | Manages background music and SFX |
| `DataSystem` | Central data hub — sprites, prefabs, sound, fx, IAP |
| `SceneHelper` | Scene transitions with loading screen |
| `ObjectPooler` | Object pool for performance optimization |
| `TimerController` | Global timer — tick, update, new day/week/month |
| `IAPManager` | Unity Purchasing (IAP) integration |
| `IAPController` | Post-purchase logic handler |

### 🧩 Controller Systems (`Assets/00 Scripts/Manager/Controller/`)

Controllers use the `SingletonController<T, D>` pattern — automatically serialize/deserialize JSON to `PlayerPrefs`:

| Controller | Stored Data |
|---|---|
| `PlayerResource` | Coin, Gem, Energy |
| `PlayerInfoController` | Played level history |
| `GameSettingController` | Music, sound, vibration |
| `AchievementController` | Achievement progress |

### 🖥️ UI Systems (`Assets/00 Scripts/UI/`)

- **`UIBase`** — Base class for all panels/popups with animation, back key, and block panel support
- **`UiHome` / `UiGameplay`** — Home screen and gameplay screen
- **Built-in Popups:** WinGame, LoseGame, PauseGame, Setting, Endgame, ConfirmAction, ShowReward, and more
- **Common UI:** CommonButton, CommonFill, CommonResourceBar, SoundButton, ToggleButton, UIFlyingObject
- **`SafeArea`** — Automatically handles safe area on iOS/Android

### 📦 Data Systems (`Assets/00 Scripts/Data/`)

| Script | Description |
|---|---|
| `DataSample` | Demo for loading data from Google Sheets via CSV |
| `DataSprites` | Sprite atlas by resource type |
| `DataGamePrefabs` | Central prefab references |
| `DataSoundEffect` | ESfx → AudioClip mapping |
| `DataFx` | Animation effects |
| `DataIAP` | IAP package configuration |
| `DataSample` | Sample ScriptableObject |

### 🎮 Gameplay Systems (`Assets/00 Scripts/Gameplay/`)

- `GameplayManager` — Manages gameplay state (Running / Pause / GameOver / Cinematic)
- `TouchController` — Handles touch input
- `ResolutionManager` — Adapts to screen aspect ratios

### 🛠️ Helper Utilities (`Assets/00 Scripts/Helper/`)

- `Singleton<T>` & `SingletonController<T,D>` — Singleton pattern with persistent data
- `DebugCustom` — Logging wrapper that only runs in the Editor
- `ExtensionMethods` — Utility extension methods
- `CSVReader` — CSV file reader
- `DateTimeHelper` — Date/time utilities
- `CustomBigValue` — Large number support (BigInteger)
- `Helper` — General static utility functions

---

## 📁 Folder Structure

```
Assets/
├── 00 Scripts/
│   ├── Data/          # ScriptableObject data containers
│   ├── Gameplay/      # Gameplay logic
│   ├── Helper/        # Utilities & base classes
│   ├── Manager/       # Core singleton managers
│   │   └── Controller/  # Data controllers (PlayerPrefs-backed)
│   ├── Object/        # In-world object scripts
│   ├── Scene/         # Scene-specific scripts (Splash, Loading, Login)
│   └── UI/            # All UI scripts
│       ├── Common/    # Reusable UI components
│       ├── Gameplay/  # In-game UI & popups
│       └── Home/      # Home screen UI & popups
├── 02 Prefabs/
│   ├── Common/        # Shared prefabs
│   ├── Gameplay/      # Gameplay prefabs
│   └── UI/            # UI prefabs
├── 06 Animator/       # Animation controllers
├── 07 Sound Effect/   # Audio clips
├── Scenes/            # Unity scenes (Splash, Loading, Main UI, Gameplay, etc.)
├── Resources/         # Runtime-loaded assets
│   ├── 00 Data/       # ScriptableObject assets
│   └── 01 Prefabs/    # Runtime-loaded prefabs
├── Plugins/
│   ├── Sirenix/       # Odin Inspector
│   ├── Demigiant/     # DOTween / DOTween Pro
│   └── CodeStage/     # Anti-Cheat Toolkit
├── Packages/          # NuGet packages (JWT, System.Text.Json, ...)
├── Spine/             # Spine runtime
├── art_template/      # Art asset presets & templates
└── TextMesh Pro/      # TMP assets
```

---

## 🔌 Built-in Plugins & Packages

### Unity Packages
| Package | Version | Purpose |
|---|---|---|
| `com.unity.purchasing` | 4.14.2 | In-App Purchase |
| `com.unity.inputsystem` | 1.18.0 | Input handling |
| `com.unity.cinemachine` | 2.10.5 | Camera system |
| `com.unity.feature.2d` | 2.0.2 | 2D tools bundle |
| `com.unity.timeline` | 1.8.10 | Cinematic timeline |
| `com.unity.ugui` | 2.0.0 | UI Toolkit |

### Third-party Plugins
| Plugin | Purpose |
|---|---|
| **Odin Inspector (Sirenix)** | Advanced Inspector tool |
| **DOTween Pro (Demigiant)** | Animation tweening |
| **Anti-Cheat Toolkit (CodeStage)** | Runtime cheat protection |
| **Spine Runtime** | Skeletal 2D animation |
| **TextMesh Pro** | Advanced text rendering |
| **TigerForge EasyEventManager** | Event system |
| **I2 Localization** | Multi-language support |
| **JWT 11.0.0** | JSON Web Token (server auth) |
| **Newtonsoft.Json** | JSON serialization |
| **TriangleNet** | Triangulation/mesh generation |

---

## 🚀 Getting Started

1. **Clone** the repository.
2. Open the project with **Unity 6000.3.10f1** or newer.
3. Open the **`Scenes/Splash Scene`** to run from the beginning.
4. Assign the `DataSystem` ScriptableObjects to the `DataSystem` GameObject in the Inspector.
5. Configure IAP pack IDs in `DataIAP`.
6. Customize `Constant.cs` for the project's scene names, events, and layers.

---

## 🔐 Security Notes

### What is Protected ✅

| Item | Status |
|---|---|
| **Debug logging** | All logs are wrapped in `#if UNITY_EDITOR` — no leakage in builds |
| **Anti-Cheat** | **Anti-Cheat Toolkit** (CodeStage) is integrated to protect runtime data |
| **No hardcoded secrets** | No API keys, tokens, or passwords are committed to source code |
| **`.gitignore`** | Configured to Unity standards — excludes Library, build artifacts, and user settings |
| **Data storage** | User data is stored locally via `PlayerPrefs` + JSON with no sensitive PII |

### Development Warnings ⚠️

| Item | Recommendation |
|---|---|
| **`IsTester` flag** | `GameManager.IsTester` enables hack/test UI — ensure it is set to `false` in release builds |
| **JWT package** | `JWT 11.0.0` is installed — if using server authentication, keep the JWT secret server-side and never commit it to the client |
| **PlayerPrefs** | `PlayerPrefs` data can be read/modified on rooted/jailbroken devices — use Anti-Cheat Toolkit encryption if needed |
| **IAP validation** | Server-side receipt validation is recommended to prevent purchase fraud |

---

## 📝 Code Conventions

- Singleton MonoBehaviour: inherit from `Singleton<T>`
- Controller with persistent data: inherit from `SingletonController<T, D>` where `D : ControllerCachedData`
- Event communication: use `TigerForge.EventManager` with keys defined in `Constant.cs`
- Logging: use `DebugCustom.Log()` instead of `Debug.Log()` directly
- UI popup: inherit from `UIBase`, register via `UIManager.GetUI("Popup Name")`

---

## 📄 License

This project is an internal template. Please do not distribute commercial plugins (Odin Inspector, DOTween Pro, Anti-Cheat Toolkit) without valid licenses.
