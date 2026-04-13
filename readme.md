<p align="center">
  <img src="yaobanner.png" alt="YAO Banner">
</p>

# 🌸 YAO - YanSim Alpha Optimizer v1.2 🌸

**YAO** is a lightweight BepInEx plugin designed to fix Yandere Simulator's performance bottlenecks, specifically targeting CPU overhead and RAM bloat in crowded areas. 

---

## ✨ Key Features

* 🚀 **Extreme FPS Boost:** Massive performance gain near the fountain and in hallways.
  * *Tested: 30 FPS ➔ **70+ FPS***
* 🧠 **Memory Management:** Reduces RAM usage and stutters via automated GC tuning and component caching.
* 💃 **Adaptive Logic:** Throttles or disables StudentScript updates for NPCs outside your immediate area.
* 👁️ **Optimized Vision:** Students no longer perform line-of-sight checks every single frame, drastically reducing CPU load.

---

## 🎀 Installation

1. Ensure you have **BepInEx 5 (x64)** installed.
2. Download the [latest release](https://github.com/notmieu/YAO-YanSim-Optimizer/releases).
3. Drag and drop `YAO.dll` and `YAO.ini` into:
   `YandereSimulator/BepInEx/plugins/`
4. Launch the game and enjoy the smoothness!

---

## 💬 Community & Support

Join the official Discord server for updates, support, and feedback:
🔗 [**Join YAO Discord Server**](https://discord.gg/nUt3SEfynS)

---

## ⚙️ Configuration

Settings can be tweaked in `YAO.ini`:
* `MaxLogicDistance`: Distance (in meters) before student scripts are throttled (Default: **25.0**).
* `UseAdaptiveLogic`: Toggle the main optimization engine.

---

## ❓ Troubleshooting

* **Mod not loading?** Check the BepInEx console for `Optimizer: Done`.
* **Students lagging/teleporting?** Increase `MaxLogicDistance` in the `.ini` file.
* **Crashes?** Make sure you don't have conflicting older optimization mods installed.

---
<p align="center">
  <b>Version:</b> 1.2.2 | <b>Dev:</b> Mieu | <b>License:</b> MIT
</p>
