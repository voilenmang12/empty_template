# 🎮 Unity Game Template

> **Template cơ bản phục vụ cho việc phát triển game nhanh chóng trên Unity**
> *(A base template for rapid Unity game development)*

---

## 📋 Giới thiệu / Overview

Đây là một **Unity project template** được xây dựng sẵn với đầy đủ các hệ thống cốt lõi, giúp các nhóm phát triển game bắt đầu dự án mới nhanh hơn mà không cần thiết lập từ đầu.

This is a **ready-to-use Unity project template** packed with essential core systems, enabling development teams to jump-start new game projects without boilerplate setup.

- **Unity Version:** 6000.3.10f1 (Unity 6)
- **Platform:** Mobile (iOS / Android)

---

## 🏗️ Kiến trúc hệ thống / System Architecture

### 🔧 Manager Systems (`Assets/00 Scripts/Manager/`)

| Script | Mô tả |
|---|---|
| `GameManager` | Quản lý game state, điều hướng scene, ads hooks |
| `UIManager` | Quản lý toàn bộ UI/popup với pooling & flying objects |
| `AudioManager` | Quản lý nhạc nền và SFX |
| `DataSystem` | Trung tâm dữ liệu — sprites, prefabs, sound, fx, IAP |
| `SceneHelper` | Chuyển cảnh có loading screen |
| `ObjectPooler` | Object pool để tối ưu hiệu năng |
| `TimerController` | Timer toàn cục — tick, update, new day/week/month |
| `IAPManager` | Tích hợp Unity Purchasing (IAP) |
| `IAPController` | Xử lý logic sau khi mua hàng |

### 🧩 Controller Systems (`Assets/00 Scripts/Manager/Controller/`)

Các controller dùng pattern `SingletonController<T, D>` — tự động serialize/deserialize JSON vào `PlayerPrefs`:

| Controller | Dữ liệu lưu trữ |
|---|---|
| `PlayerResource` | Coin, Gem, Energy |
| `PlayerInfoController` | Lịch sử level đã chơi |
| `GameSettingController` | Âm nhạc, âm thanh, rung |
| `AchievementController` | Tiến độ thành tích |

### 🖥️ UI Systems (`Assets/00 Scripts/UI/`)

- **`UIBase`** — Base class cho mọi panel/popup với animation, back key, block panel
- **`UiHome` / `UiGameplay`** — Màn hình chính và gameplay
- **Popup sẵn có:** WinGame, LoseGame, PauseGame, Setting, Endgame, ConfirmAction, ShowReward, và nhiều hơn nữa
- **Common UI:** CommonButton, CommonFill, CommonResourceBar, SoundButton, ToggleButton, UIFlyingObject
- **`SafeArea`** — Tự động xử lý safe area trên iOS/Android

### 📦 Data Systems (`Assets/00 Scripts/Data/`)

| Script | Mô tả |
|---|---|
| `DataSample` | Demo load data từ google sheet qua csv |
| `DataSprites` | Sprite atlas theo loại resource |
| `DataGamePrefabs` | Prefab references trung tâm |
| `DataSoundEffect` | Mapping ESfx → AudioClip |
| `DataFx` | Animation effects |
| `DataIAP` | Cấu hình gói IAP |
| `DataSample` | ScriptableObject mẫu |

### 🎮 Gameplay Systems (`Assets/00 Scripts/Gameplay/`)

- `GameplayManager` — Quản lý state gameplay (Running / Pause / GameOver / Cinematic)
- `TouchController` — Xử lý input chạm màn hình
- `ResolutionManager` — Thích nghi tỷ lệ màn hình

### 🛠️ Helper Utilities (`Assets/00 Scripts/Helper/`)

- `Singleton<T>` & `SingletonController<T,D>` — Pattern singleton có lưu dữ liệu
- `DebugCustom` — Logging wrapper chỉ hoạt động trong Editor
- `ExtensionMethods` — Extension methods tiện ích
- `CSVReader` — Đọc file CSV
- `DateTimeHelper` — Tiện ích ngày giờ
- `CustomBigValue` — Hỗ trợ giá trị số lớn (BigInteger)
- `Helper` — Static utility functions chung

---

## 📁 Cấu trúc thư mục / Folder Structure

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

## 🔌 Plugins & Packages tích hợp sẵn

### Unity Packages
| Package | Version | Mục đích |
|---|---|---|
| `com.unity.purchasing` | 4.14.2 | In-App Purchase |
| `com.unity.inputsystem` | 1.18.0 | Input handling |
| `com.unity.cinemachine` | 2.10.5 | Camera system |
| `com.unity.feature.2d` | 2.0.2 | 2D tools bundle |
| `com.unity.timeline` | 1.8.10 | Cinematic timeline |
| `com.unity.ugui` | 2.0.0 | UI Toolkit |

### Third-party Plugins
| Plugin | Mục đích |
|---|---|
| **Odin Inspector (Sirenix)** | Công cụ Inspector nâng cao |
| **DOTween Pro (Demigiant)** | Animation tween |
| **Anti-Cheat Toolkit (CodeStage)** | Bảo vệ chống cheat |
| **Spine Runtime** | Skeleton 2D animation |
| **TextMesh Pro** | Rendering chữ nâng cao |
| **TigerForge EasyEventManager** | Hệ thống event |
| **I2 Localization** | Đa ngôn ngữ |
| **JWT 11.0.0** | JSON Web Token (server auth) |
| **Newtonsoft.Json** | JSON serialization |
| **TriangleNet** | Triangulation/mesh generation |

---

## 🚀 Cách bắt đầu / Getting Started

1. **Clone** repo về máy.
2. Mở project bằng **Unity 6000.3.10f1** hoặc mới hơn.
3. Mở scene **`Scenes/Splash Scene`** để chạy từ đầu.
4. Gắn các `DataSystem` ScriptableObjects vào Inspector của `DataSystem` GameObject.
5. Cấu hình IAP pack IDs trong `DataIAP`.
6. Tùy chỉnh `Constant.cs` cho tên scene, event và layer của dự án.

---

## 🔐 Bảo mật / Security Notes

### Những gì đã được bảo vệ ✅

| Hạng mục | Trạng thái |
|---|---|
| **Debug logging** | Tất cả log đã được bọc trong `#if UNITY_EDITOR` — không rò rỉ ra bản build |
| **Anti-Cheat** | Tích hợp sẵn **Anti-Cheat Toolkit** (CodeStage) để bảo vệ dữ liệu runtime |
| **Không hardcode secret** | Không có API key, token hay password nào được commit vào source code |
| **`.gitignore`** | Đã thiết lập đúng chuẩn Unity — bỏ qua Library, build artifacts, user settings |
| **Lưu dữ liệu** | Dữ liệu người dùng được lưu local qua `PlayerPrefs` + JSON, không có PII nhạy cảm |

### Lưu ý khi phát triển ⚠️

| Hạng mục | Khuyến nghị |
|---|---|
| **`IsTester` flag** | `GameManager.IsTester` bật chế độ hack/test UI — đảm bảo tắt (`false`) trong bản release |
| **JWT package** | Đã cài `JWT 11.0.0` — nếu dùng xác thực server, hãy giữ bí mật JWT secret phía server, không commit vào client |
| **PlayerPrefs** | Dữ liệu `PlayerPrefs` có thể bị đọc/chỉnh sửa trên thiết bị đã root/jailbreak — sử dụng Anti-Cheat Toolkit để mã hóa nếu cần |
| **IAP validation** | Nên thêm xác thực receipt phía server để chống gian lận mua hàng |

---

## 📝 Quy ước code / Code Conventions

- Singleton MonoBehaviour: kế thừa `Singleton<T>`
- Controller có lưu dữ liệu: kế thừa `SingletonController<T, D>` với `D : ControllerCachedData`
- Event communication: dùng `TigerForge.EventManager` với các key trong `Constant.cs`
- Logging: dùng `DebugCustom.Log()` thay cho `Debug.Log()` trực tiếp
- UI popup: kế thừa `UIBase`, đăng ký qua `UIManager.GetUI("Popup Name")`

---

## 📄 License

Dự án này là template nội bộ. Vui lòng không phân phối plugin thương mại (Odin Inspector, DOTween Pro, Anti-Cheat Toolkit) kèm theo mà không có license hợp lệ.
