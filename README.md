​A simple plugin that lets you press a configurable key (default F11) to temporarily disable the mouse cursor locking so you can move you mouse out of the main Tarkov window. This is useful for multi-monitor setups.​


Warning. I wanted this quickly and was lazy because while I've worked with Harmony I've never done a BepInEx mod so I threw the request to ChatGPT and let it generate the actual code.
​

# Running SPTarkov on Linux with Wine (GE-Proton or GE-Wine) – Beginner-Friendly Setup Guide

This guide walks you through everything you need to get **SPTarkov** running reliably on **Linux** using **Wine**, **GE-Proton**, or **GE-Wine** (through Lutris). It covers how to fix issues like crashes, mouse capture bugs, multi-monitor problems, and setup for DXVK and BepInEx plugins.


# THE INFORMATION BELOW IS USED AT YOUR OWN RISK.
What worked for me may not work for you. The author takes no responsibility for you breaking your own setup by following any of the suggestions below. Be Careful. Take Backups. Keep track of where you are. Undo it if it doesnt work! You have been warned!

---

# Having Joy with Multiple Monitors and SPTarkov on Linux

## 🧩 What You’ll Need
- **Lutris** installed
- **GE-Proton** or **GE-Wine** (install with ProtonUp-Qt or copy from Steam)
- **SPTarkov** already extracted and ready
- **Wine prefix** (this is like a Windows install folder, e.g. `WINEPREFIX="$PWD"` or a custom path)

---

## 🔧 Step 1: Environment Variables for Wine/GE-Proton/GE-Wine
Set these in Lutris under the **Runner Options > Environment Variables**, or export them in your terminal/launcher script:

```bash
WINEFSYNC=1
WINEESYNC=1
DXVK_HUD=0
PROTON_NO_ESYNC=0
PROTON_NO_FSYNC=0
```

> ✅ If you're using **GE-Proton** installed via Steam, it lives in something like:
> `~/.local/share/Steam/compatibilitytools.d/GE-Proton9-25/`

So to run regedit with it, you'd do:
```bash
WINEPREFIX="$PWD" ~/.local/share/Steam/compatibilitytools.d/GE-Proton9-25/files/bin/wine regedit
```

> 🧠 This method works the same way for **GE-Wine** — just locate the Wine binary in your GE-Wine directory instead.

---

## 🧰 Step 2: Enable Wine Virtual Desktop (Registry Edit)
This step avoids freezing, mouse trap issues, and crashes when switching monitors or alt-tabbing.

### How to Do It:
1. Run:
```bash
WINEPREFIX="$PWD" path/to/wine regedit
```
(Use your actual GE-Proton or GE-Wine wine binary path as shown above.)

2. Inside `regedit`, navigate to:
```
HKEY_CURRENT_USER\Software\Wine\Explorer
```
If it doesn't exist, create the keys manually:
- Right-click `Wine` > New > Key > `Explorer`

3. Inside `Explorer`, create a new **String Value**:
- Name: `Desktops`
- Type: `REG_SZ`
- Value: `2560x1440` *(or your actual screen resolution)*

✅ This forces Wine to run the game in a contained window matching your screen resolution. It prevents Unity (the engine Tarkov uses) from losing its mind on focus loss.

---

## 🪟 Tip: Why Use a Virtual Desktop and When
You might not *need* this in all setups, but it helps when:
- Using multi-monitor layouts
- Running tiling window managers like i3
- Experiencing mouse getting trapped or crashing when alt-tabbing

---

## 🪟 Tip: Multi-Monitor & i3 Window Manager Advice
If you’re using multiple monitors or something like i3:
- **Don’t move the Tarkov window between monitors while it’s running**
- Avoid using **borderless windowed mode** unless you're using the virtual desktop
- Use the **mouse capture toggle plugin** (see below) to escape traps

---

## ⚙️ Step 3: Optional – DXVK Configuration
If you want to disable DXVK’s performance overlay or logging, you can create a config file:

Create `dxvk.conf` in your game folder:
```bash
${GAMEDIR}/dxvk.conf
```

With the following content:
```ini
# Disable HUD and suppress logs
hud = 0
logLevel = none
```

Tell DXVK to use this file by setting this environment variable:
```bash
export DXVK_CONFIG_FILE="${GAMEDIR}/dxvk.conf"
```

Or in Lutris:
```
DXVK_CONFIG_FILE=${GAMEDIR}/dxvk.conf
```

---

## 🔌 Step 4: Mouse Capture Toggle Plugin (BepInEx)
To get full control over mouse trapping behavior (common in Unity games like Tarkov), use the custom plugin you built.

### Install location:
```bash
/<LocationOfSPTarkov>/BepInEx/plugins/MouseEscapePlugin/
```

### Plugin features:
- Press **F11** to toggle mouse lock on/off
- Mouse lock resumes automatically on click or key press
- Great for workflow flexibility, especially in tiling window managers

Double-check that the plugin is loading by looking in `BepInEx/LogOutput.log` after launch.

---

## ✅ Final Setup Checklist
- [x] GE-Proton or GE-Wine installed and selected in Lutris
- [x] Environment variables set (FSYNC/ESYNC, etc.)
- [x] Wine registry virtual desktop enabled
- [x] DXVK config used (optional)
- [x] Mouse capture toggle plugin installed
- [x] No crashes on alt-tab, workspace switch, or monitor changes

---

🎉 That’s it! You’re now ready to play SPTarkov on Linux with better stability, mouse control, and multi-monitor support.

Let me know if you’d like a zipped mod installer, GitHub release setup, or BepInEx build helper script!


