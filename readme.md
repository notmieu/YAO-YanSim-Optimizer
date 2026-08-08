<div align="center">

![YAO Banner](yaobanner.png)

# YAO — YanSim Alpha Optimizer v1.2

</div>

**YAO** is a lightweight BepInEx plugin built to fix Yandere Simulator's performance issues, specifically targeting CPU overhead and RAM bloat in crowded areas.

---

### Key Features

* **Extreme FPS Boost:** Significant performance gains near the fountain and in hallways (*Tested: 30 FPS ➔ 70+ FPS*).
* **Memory Management:** Reduces RAM usage and stutters via automated GC tuning and component caching.
* **Adaptive Logic:** Throttles or disables `StudentScript` updates for NPCs outside your immediate area.
* **Optimized Vision:** Students no longer run line-of-sight checks on every single frame, drastically cutting CPU load.

---

### Installation

1. Make sure you have **BepInEx 5 (x64)** installed.
2. Download the [latest release](https://github.com/notmieu/YAO-YanSim-Optimizer/releases).
3. Drop `YAO.dll` and `YAO.ini` into:
   `YandereSimulator/BepInEx/plugins/`
4. Launch the game and enjoy the smoothness!

---

### Community & Support

Join the official Discord for updates, support, and feedback:  
💬 [**Join the YAO Discord Server**](https://discord.gg/nUt3SEfynS)

---

### Configuration

You can tweak the settings in `YAO.ini`:
* `MaxLogicDistance`: Distance (in meters) before student scripts are throttled (Default: **25.0**).
* `UseAdaptiveLogic`: Toggles the main optimization engine.

---

### Troubleshooting

* **Mod not loading?** Check the BepInEx console for `Optimizer: Done`.
* **Students lagging or teleporting?** Try increasing `MaxLogicDistance` in your `.ini` file.
* **Crashes?** Make sure you don't have conflicting/outdated optimization mods installed.

---

<div align="center">

`Version: idk` • `Dev: Mieu` • `License: MIT`

</div>
