<div align="center">
   
![YAO Banner](banner.png)

# YAO — Yandere Simulator Optimizer v2.1
</div>
*YAO** is a lightweight BepInEx optimization mod built to fix Yandere Simulator's severe performance issues, specifically targeting CPU overhead, bone transform loops, physics simulation, and RAM bloat in crowded areas.
---

### Key Features
* **Extreme FPS Boost:** Massive performance gains near the fountain, in hallways, and crowded areas (*Tested: 30 FPS ➔ 70+ FPS*).
* **Zero-Reflection Engine:** Replaced heavy reflection loops with statically cached field handles, eliminating CPU overhead.
* **Animator & Skeleton Culling:** Halts bone transform calculations on distant or offscreen students.
* **Cloth & Hair Dynamics Culling:** Disables heavy `Cloth` physics simulation on student skirts and long hair.
* **Audio & Secondary Camera Optimization:** Mutes distant spatialized audio and turns off idle secondary render cameras to drastically cut GPU Draw Calls.
* **Memory & GC Tuning:** Reduces RAM usage and stuttering via automated Garbage Collector tuning and component caching.
---

### Installation
#### 🪟 Windows
1. Download `YAO_Setup.exe` from the [latest release](https://github.com/notmieu/YAO-Yandere-Simulator-Optimizer/releases).
2. Launch `YAO_Setup.exe` (it automatically detects your game directory).
3. Click **INSTALL** and launch the game!
#### 🐧 Linux
1. Download `YAO_Setup_Linux` (or `./YAO_Setup_Linux.sh`).
2. Run the installer script, select your game path, and click **INSTALL**.

> 💡 **Uninstalling:** Launch the installer anytime and click **REMOVE** to cleanly remove all mod files.

---
### Community & Support
Join the official Discord server for updates, support, and feedback:  
💬 [**Join the YAO Discord Server**](https://discord.gg/nUt3SEfynS)

---
### Configuration
Settings can be customized in `BepInEx/plugins/YAO.ini`:
* `AdaptiveLogicDistance`: Distance (in meters) before student scripts are paused (Default: **30.0**).
* `UpdateTickRate`: Seconds between NPC update passes (Default: **0.15**).
* `ShadowDistanceThreshold`: Distance beyond which student shadows are culled (Default: **20.0**).
* `IKDisableDistance`: Distance threshold for head/eye IK tracking disable (Default: **8.0**).
* `EnableAnimatorCulling`: Toggles Animator bone culling on distant NPCs (Default: **true**).
* `EnableClothOptimization`: Toggles skirt and hair cloth physics culling (Default: **true**).
---

### Troubleshooting
* **Game Not Detected?** Click the `. . .` button in the installer and manually select your `YandereSimulator.exe` folder.
* **Mod not loading?** Make sure you are running the latest version of Yandere Simulator (Unity 6 engine).
* **Black screen on launch?** YAO displays a custom full-screen splash screen on startup that fades out smoothly after 2.5 seconds.
---

<div align="center">
<code>Version: v2.1</code> • <code>Dev: mieu</code> • <code>License: MIT</code>
</div>
