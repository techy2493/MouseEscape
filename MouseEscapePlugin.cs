using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace MouseEscapePlugin
{
    [BepInPlugin("com.techy.mousescape.toggle", "Mouse Escape", "1.0.0")]
    [BepInProcess("EscapeFromTarkov.exe")]
    public class MouseEscapePlugin : BaseUnityPlugin
    {
        private static bool overrideLock = false;
        private static KeyCode toggleKey;
        private ConfigEntry<KeyCode> toggleKeyConfig;

        private void Awake()
        {
            toggleKeyConfig = Config.Bind("General", "ToggleKey", KeyCode.F11, "Key to toggle mouse lock override on/off");
            toggleKey = toggleKeyConfig.Value;

            Logger.LogInfo($"MouseEscapePlugin plugin loaded. Toggle key set to {toggleKey}.");
        }

        private void Update()
        {
            // Toggle mouse lock override
            if (Input.GetKeyDown(toggleKey))
            {
                overrideLock = !overrideLock;
                Logger.LogInfo($"[MouseEscapePlugin] Override {(overrideLock ? "ENABLED" : "DISABLED")} (Key: {toggleKey})");
            }

            if (overrideLock)
            {
                // Release cursor lock while override is enabled
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Re-enable lock on any key or mouse button click
                if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                {
                    overrideLock = false;
                    Logger.LogInfo("[MouseEscapePlugin] Override canceled due to user input.");
                }
            }
        }
    }
}