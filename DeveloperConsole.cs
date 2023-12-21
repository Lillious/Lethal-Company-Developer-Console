using GameNetcodeStuff;
using Lethal_Library;
using MelonLoader;
using Non_Lethal_Dev_Console;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[assembly: MelonInfo(typeof(DeveloperConsole), "Non-Lethal Developer Console", "0.0.1", "Lillious, .Zer0")]
[assembly: MelonGame("ZeekerssRBLX", "Lethal Company")]
namespace Non_Lethal_Dev_Console
{
    public class DeveloperConsole : MelonMod
    {
        private Library LC_Lib = new Library();
        [SerializeField] private Sprite sprite;
        private TMP_InputField CommandInput;
        private TextMeshProUGUI CommandOutput;
        private List<string> CommandHistory = new List<string>();
        private Canvas DevConsole;
        private string result = "";
        private TMP_FontAsset GameFont;
        private PlayerControllerB CurrentPlayer;

        private void CommandRunner(string command)
        {
            if (string.IsNullOrEmpty(command)) return;
            command = command.ToLower();

            var args = command.Split(' ');

            switch (args[0])
            {
                // Utilities
                // Exits the game
                case "exit":
                    DevConsole.gameObject.SetActive(false);
                    Application.Quit();
                    return;

                // Clears the console output
                case "clear":
                    CommandHistory.Clear();
                    return;

                // Opens the help page on github
                case "help":
                    Application.OpenURL("https://github.com/Lillious/Lethal-Company-Developer-Console/blob/master/README.md");
                break;

                // PLAYER COMMANDS
                // set <property> <value>
                case "set":
                    if (args.Length < 1)
                    {
                        result = "Error: Invalid command";
                        break;
                    }
                    switch (args[1])
                    {
                        case "group_credits":
                            {
                                Terminal terminal = LC_Lib.GetTerminal();
                                LC_Lib.SetGroupCredits(terminal, int.Parse(args[2]));
                                result = $"Group Credits: {LC_Lib.GetGroupCredits(terminal)}";
                                break;
                            }
                        // Sets the Player's Health
                        case "health":
                            LC_Lib.SetPlayerHealth(CurrentPlayer, int.Parse(args[2]));
                            result = $"Set Health to {args[2]}";
                            break;
                        // Sets the Player's Speed
                        case "speed":
                            LC_Lib.SetPlayerSpeed(CurrentPlayer, int.Parse(args[2]));
                            result = $"Set Speed to {args[2]}";
                            break;
                        // Sets the Player's Jump Force
                        case "jump":
                            LC_Lib.SetPlayerJumpForce(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Jump_Force to {args[2]}";
                            break;
                        // Sets the Player's Climb Speed
                        case "climb_speed":
                            LC_Lib.SetClimbSpeed(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Climb_Speed to {args[2]}";
                            break;
                        // Sets the Player's Drunkness
                        case "drunkness":
                            LC_Lib.SetDrunkness(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Drunkness to {args[2]}";
                            break;
                        // Sets the Player's drunk inertia
                        case "drunk_inertia":
                            LC_Lib.SetDrunknessInertia(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Drunk_Inertia to {args[2]}";
                            break;
                        // Sets the Player's drunk recovery time
                        case "drunk_recovery_time":
                            LC_Lib.SetDrunkRecoveryTime(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Drunk_Recovery_Time to {args[2]}";
                            break;
                        // Sets the Player's Health Regen Timer
                        case "health_regen_timer":
                            LC_Lib.SetHealthRegenTimer(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Health Regen Time to {args[2]}";
                            break;
                        // Sets the Player's Grab Distance
                        case "grab_distance":
                            LC_Lib.SetGrabDistance(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Grab_Distance to {args[2]}";
                            break;
                        // Sets the Player's Exhaustion
                        case "exhaust":
                            LC_Lib.SetExhausted(CurrentPlayer, bool.Parse(args[2]));
                            result = $"Set Exhaustion to {args[2]}";
                            break;
                        // Sets the Player's Max Insanity
                        case "max_insanity":
                            LC_Lib.SetMaxInsanity(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Max Insanity to {args[2]}";
                            break;
                        // Sets the Player's insanity speed multiplier
                        case "insanity_speed_multiplier":
                            LC_Lib.SetInsanitySpeedMultiplier(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Insanity Speed Multiplier to {args[2]}";
                            break;
                        // Sets the Player's Minimum Velocity To Take Damage
                        case "min_velocity_to_take_damage":
                            LC_Lib.SetMinVelocityToTakeDamage(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Minimum Velocity To Take Damage to {args[2]}";
                            break;
                        // Sets the Player's Level
                        case "level":
                            LC_Lib.SetLevelNumber(CurrentPlayer, int.Parse(args[2]));
                            result = $"Set Level to {args[2]}";
                            break;
                        // Set player's position
                        case "position":
                            if (args.Length != 5)
                            {
                                result = "Error: Invalid Arguments";
                                break;
                            }
                            Vector3 CurrentPosition = LC_Lib.GetPlayerPosition(CurrentPlayer);
                            float x, y, z;
                            // - means x y or z position value
                            // X position
                            if (args[2] == "-")
                            {
                                x = CurrentPosition.x;
                            }
                            else
                            {
                                x = float.Parse(args[3]);
                            }
                            // Y position
                            if (args[3] == "-")
                            {
                                y = CurrentPosition.y;
                            }
                            else
                            {
                                y = float.Parse(args[4]);
                            }
                            // Z position
                            if (args[4] == "-")
                            {
                                z = CurrentPosition.z;
                            }
                            else
                            {
                                z = float.Parse(args[5]);
                            }

                            LC_Lib.TeleportPlayer(CurrentPlayer, new Vector3(x, y, z));
                            result = $"Set Position to {x}, {y}, {z}\n" +
                                $"New position is {CurrentPosition.x}, {CurrentPosition.y}, {CurrentPosition.z}";
                            break;
                        // Sets  the player's hindered multiplier
                        case "hindered_multiplier":
                            LC_Lib.SetHinderedMultiplier(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set Hindered Multiplier to {args[2]}";
                            break;
                        // Set player's hindered status
                        case "hindered":
                            LC_Lib.SetHindered(CurrentPlayer, int.Parse(args[2]));
                            result = $"Set Hindered Status to {args[2]}";
                            break;
                        // Sets the players sinking speed multiplier
                        case "sinking_speed_multiplier":
                            LC_Lib.SetSinkingSpeedMultiplier(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set sinking speed multiplier to {args[2]}";
                            break;
                        // Sets the player's sprint meter value
                        case "sprint_meter":
                            LC_Lib.SetSprintMeterValue(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set sprint meter value to {args[2]}";
                            break;
                        // Sets the player's throw power
                        case "throw_power":
                            LC_Lib.SetThrowPower(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set throw power to {args[2]}";
                            break;
                        default:
                            result = "Error: Invalid command";
                            break;
                    }
                break;

                // get <property>
                case "get":
                    if (args.Length < 1)
                    {
                        result = "Error: Invalid command";
                        break;
                    }
                    switch (args[1])
                    {
                        // Returns the player's group credits
                        case "group_credits":
                            {
                                Terminal terminal = LC_Lib.GetTerminal();
                                result = $"Group Credits: {LC_Lib.GetGroupCredits(terminal)}";
                            }
                            break;
                        // Returns the player's health
                        case "health":
                            result = $"Health is {LC_Lib.GetPlayerHealth(CurrentPlayer)}";
                            break;
                        // Returns the player's speed
                        case "speed":
                            result = $"Speed is {LC_Lib.GetPlayerSpeed(CurrentPlayer)}";
                            break;
                        // Returns the player's jump force
                        case "jump":
                            result = $"Jump Force: {LC_Lib.GetPlayerJumpForce(CurrentPlayer)}";
                            break;
                        // Returns the player's climb speed
                        case "climb_speed":
                            result = $"Climb Speed: {LC_Lib.GetClimbSpeed(CurrentPlayer)}";
                            break;

                        // Returns the player's drunkness
                        case "drunkness":
                            result = $"Drunkness: {LC_Lib.GetDrunkness(CurrentPlayer)}";
                            break;
                        // Returns if the player is drunk
                        case "is_drunk":
                            if (LC_Lib.IsDrunk(CurrentPlayer))
                            {
                                result = $"Player is drunk";
                                break;
                            }
                            else
                            {
                                result = $"Player is not drunk";
                                break;
                            }
                        // Returns the player's drunkness inertia
                        case "drunk_inertia":
                            result = $"Drunk Inertia: {LC_Lib.GetDrunknessInertia(CurrentPlayer)}";
                            break;
                        // Returns the player's drunk recovery time
                        case "drunk_recovery_time":
                            result = $"Drunk Recovery Time: {LC_Lib.GetDrunkRecoveryTime(CurrentPlayer)}";
                            break;

                        // Returns the player's grab distance
                        case "grab_distance":
                            result = $"Grab Distance: {LC_Lib.GetGrabDistance(CurrentPlayer)}";
                            break;
                        // Returns the player's exhaustion
                        case "exhaust":
                            result = $"Exhaustion: {LC_Lib.IsExhausted(CurrentPlayer)}";
                            break;

                        // Returns the player's max insanity
                        case "max_insanity":
                            result = $"Max Insanity: {LC_Lib.GetMaxInsanity(CurrentPlayer)}";
                            break;
                        // Returns the player's insanity speed multiplier
                        case "insanity_speed_multiplier":
                            result = $"Insanity Speed Multiplier: {LC_Lib.GetInsanitySpeedMultiplier(CurrentPlayer)}";
                            break;

                        // Returns the player's minimum velocity to take damage
                        case "min_velocity_to_take_damage":
                            result = $"Minimum Velocity To Take Damage: {LC_Lib.GetMinVelocityToTakeDamage(CurrentPlayer)}";
                            break;
                        // Returns the player's level
                        case "level":
                            result = $"Level: {LC_Lib.GetLevelNumber(CurrentPlayer)}";
                            break;
                        // Returns the player's position
                        case "position":
                            Vector3 CurrentPosition = LC_Lib.GetPlayerPosition(CurrentPlayer);
                            result = $"Position: {CurrentPosition.x}, {CurrentPosition.y}, {CurrentPosition.z}";
                            break;
                        // Returns the player's hindered multiplier
                        case "hindered_multiplier":
                            result = $"Hindered Multiplier: {LC_Lib.GetHinderedMultiplier(CurrentPlayer)}";
                            break;
                        // Returns the player's hindered status
                        case "hindered":
                            result = $"Hindered Status: {LC_Lib.IsHindered(CurrentPlayer)}";
                            break;
                        // Checks if the player is typing in chat
                        case "is_typing":
                            if (LC_Lib.IsTypingInChat(CurrentPlayer))
                            {
                                result = $"Player is typing";
                                break;
                            }
                            else
                            {
                                result = $"Player is not typing";
                                break;
                            }
                        // Check if player is under water
                        case "is_under_water":
                            if (LC_Lib.IsUnderWater(CurrentPlayer))
                            {
                                result = $"Player is under water";
                                break;
                            }
                            else
                            {
                                result = $"Player is not under water";
                                break;
                            }
                        // Check if player is dead
                        case "is_dead":
                            if (LC_Lib.IsDead(CurrentPlayer))
                            {
                                result = $"Player is dead";
                                break;
                            }
                            else
                            {
                                result = $"Player is not dead";
                                break;
                            }
                        // Check if the player is sliding
                        case "is_sliding":
                            if (LC_Lib.IsSliding(CurrentPlayer))
                            {
                                result = $"Player is sliding";
                                break;
                            }
                            else
                            {
                                result = $"Player is not sliding";
                                break;
                            }
                        // Check if the player is sinking
                        case "is_sinking":
                            if (LC_Lib.IsSinking(CurrentPlayer))
                            {
                                result = $"Player is sinking";
                                break;
                            }
                            else
                            {
                                result = $"Player is not sinking";
                                break;
                            }
                        // Check if the player is alone
                        case "is_alone":
                            if (LC_Lib.IsAlone(CurrentPlayer))
                            {
                                result = $"Player is alone";
                                break;
                            }
                            else
                            {
                                result = $"Player is not alone";
                                break;
                            }
                        // Checks if the player is inside the factory
                        case "is_inside_factory":
                            if (LC_Lib.IsInsideFactory(CurrentPlayer))
                            {
                                result = $"Player is inside the factory";
                                break;
                            }
                            else
                            {
                                result = $"Player is outside the factory";
                                break;
                            }
                        // Checks if the player is inside the elevator
                        case "is_inside_elevator":
                            if (LC_Lib.IsInsideElevator(CurrentPlayer))
                            {
                                result = $"Player is inside the elevator";
                                break;
                            }
                            else
                            {
                                result = $"Player is outside the elevator";
                                break;
                            }
                        // Checks if the player is inspecting an item
                        case "is_inspecting_item":
                            if (LC_Lib.IsInspectingItem(CurrentPlayer))
                            {
                                result = $"Player is inspecting an item";
                                break;
                            }
                            else
                            {
                                result = $"Player is not inspecting an item";
                                break;
                            }
                        // Checks if the player is climbing a ladder
                        case "is_climbing_ladder":
                            if (LC_Lib.IsClimbingLadder(CurrentPlayer))
                            {
                                result = $"Player is climbing a ladder";
                                break;
                            }
                            else
                            {
                                result = $"Player is not climbing a ladder";
                                break;
                            }
                        // Checks if the player is holding an item
                        case "is_holding_item":
                            if (LC_Lib.IsHoldingItem(CurrentPlayer))
                            {
                                result = $"Player is holding an item";
                                break;
                            }
                            else
                            {
                                result = $"Player is not holding an item";
                                break;
                            }
                        // Checks if the player is inside the ship
                        case "is_inside_ship":
                            if (LC_Lib.IsInsideShip(CurrentPlayer))
                            {
                                result = $"Player is inside the ship";
                                break;
                            }
                            else
                            {
                                result = $"Player is not inside the ship";
                                break;
                            }
                        // Checks the player's two-handed value
                        case "is_two_handed":
                            if (LC_Lib.IsTwoHanded(CurrentPlayer))
                            {
                                result = $"Player is two-handed";
                                break;
                            }
                            else
                            {
                                result = $"Player is not two-handed";
                                break;
                            }
                        // Checks if the player is crouching
                        case "is_crouching":
                            if (LC_Lib.IsCrouching(CurrentPlayer))
                            {
                                result = $"Player is crouching";
                                break;
                            }
                            else
                            {
                                result = $"Player is not crouching";
                                break;
                            }
                        // Checks if the player's voice is muffled
                        case "is_voice_muffled":
                            if (LC_Lib.IsVoiceMuffled(CurrentPlayer))
                            {
                                result = $"Player's voice is muffled";
                                break;
                            }
                            else
                            {
                                result = $"Player's voice is not muffled";
                                break;
                            }
                        // Returns the players sinking speed multiplier
                        case "sinking_speed_multiplier":
                            result = $"Player's sinking speed multiplier: {LC_Lib.GetSinkingSpeedMultiplier(CurrentPlayer)}";
                            break;
                        // Returns the player's place of death
                        case "place_of_death":
                            result = $"Player's place of death: x: {LC_Lib.GetPlaceOfDeath(CurrentPlayer).x}, y: {LC_Lib.GetPlaceOfDeath(CurrentPlayer).y}, z: {LC_Lib.GetPlaceOfDeath(CurrentPlayer).z}";
                            break;
                        // Returns the player's spawn point
                        case "spawn_point":
                            result = $"Player's spawn point: x:{LC_Lib.GetSpawnPoint(CurrentPlayer).x}, y: {LC_Lib.GetSpawnPoint(CurrentPlayer).y}, z: {LC_Lib.GetSpawnPoint(CurrentPlayer).z}";
                            break;
                        // Returns if the player is using the jetpack
                        case "jetpack_controls":
                            if (LC_Lib.GetJetpackControls(CurrentPlayer))
                            {
                                result = "Player is using jetpack";
                                break;
                            }
                            else
                            {
                                result = "Player is not using jetpack";
                                break;
                            }
                        // Returns the player's health regen timer
                        case "health_regen_timer":
                            result = $"Player's health regen timer: {LC_Lib.GetHealthRegenTimer(CurrentPlayer)}";
                            break;
                        // Returns if the player just connected
                        case "just_connected":
                            if (LC_Lib.JustConnected(CurrentPlayer))
                            {
                                result = "Player just connected";
                                break;
                            }
                            else
                            {
                                result = "Player did not just connect";
                                break;
                            }
                        // Returns if the player has disconnected
                        case "has_disconnected":
                            if (LC_Lib.HasDisconnected(CurrentPlayer))
                            {
                                result = "Player has disconnected";
                                break;
                            }
                            else
                            {
                                result = "Player has not disconnected";
                                break;
                            }
                        // Returns the player's sprint meter value
                        case "sprint_meter":
                            result = $"Player Sprint Meter Value: {LC_Lib.GetSprintMeterValue(CurrentPlayer)}";
                            break;
                        // Returns the player's throw power
                        case "throw_power":
                            result = $"Player's Throw Power: {LC_Lib.GetThrowPower(CurrentPlayer)}";
                            break;
                        // Returns the player's ID
                        case "player_id":
                            result = $"Player's ID: {LC_Lib.GetPlayerID(CurrentPlayer)}";
                            break;
                        // Returns the player's suit ID
                        case "player_suit_id":
                            result = $"Player's Suit ID: {LC_Lib.GetPlayerSuitID(CurrentPlayer)}";
                            break;
                        // Returns the player's carry weight
                        case "carry_weight":
                            result = $"Player's Carry Weight: {LC_Lib.GetPlayerCarryWeight(CurrentPlayer)}";
                            break;

                            default:
                                result = "Error: Invalid command";
                            break;
                            }
                break;

                // Actions
                // action <action>
                case "action":
                    if (args.Length < 1)
                    {
                        result = "Error: Invalid command";
                        break;
                    }
                    switch (args[1])
                    {
                        case "add_helmet":
                            LC_Lib.AddHelmet();
                            result = $"Added helmet";
                            break;
                        case "remove_helmet":
                            LC_Lib.RemoveHelmet();
                            result = $"Removed helmet";
                            break;
                        case "no_clip":
                            switch (args[2])
                            {
                                case "on":
                                    if (LC_Lib.IsInsideShip(CurrentPlayer))
                                    {
                                        result = "Error: Player is inside the ship";
                                        break;
                                    }
                                    LC_Lib.ToggleNoclip(CurrentPlayer, true);
                                    result = $"Enabled No Clip";
                                    break;
                                case "off":
                                    LC_Lib.ToggleNoclip(CurrentPlayer, false);
                                    result = $"Disabled No Clip";
                                    break;
                            }
                            break;
                        case "eject":
                            if (!LC_Lib.IsHost())
                            {
                                result = "Error: You are not the host"; break;
                            }
                            LC_Lib.Eject();
                            result = $"Starting ejection sequence";
                            break;
                        default:
                            result = "Error: Invalid command";
                            break;
                    }
                break;

                // Teleports
                // teleport <location>
                case "teleport":
                    if (args.Length < 1)
                    {
                        result = "Error: Invalid command";
                        break;
                    }
                    switch (args[1])
                        {
                            case "ship":
                                Vector3 ShipPosition = LC_Lib.GetSpawnPoint(CurrentPlayer);
                                LC_Lib.TeleportPlayer(CurrentPlayer, ShipPosition);
                                result = $"Teleported to the ship";
                                break;
                        }
                break;

                default:
                    result = "Error: Invalid Command";
                    break;
            }

            if (!string.IsNullOrEmpty(result))
            {
                CommandHistory.Add(result);
            }
            
            UpdateCommandHistory();
        }

        private void UpdateCommandHistory()
        {
            CommandInput.text = "";
            CommandOutput.text = "";
            if (CommandHistory.Count > 10) CommandHistory.RemoveAt(0);
            if (CommandHistory.Count == 0) return;
            foreach (string cmd in CommandHistory)
            {
                CommandOutput.text += "\n" + cmd;
            }
        }

        public override void OnUpdate()
        {

            if (!LC_Lib.IsInitialized() && LC_Lib.IsInGame())
            {
                CurrentPlayer = LC_Lib.SearchForControlledPlayer();
                GameFont = GameObject.Find("Weight").GetComponent<TextMeshProUGUI>().font;
                LC_Lib.SetInitialized(true);
                return;
            }

            if (LC_Lib.IsInitialized() && LC_Lib.IsInGame())
            {

                // Initialize Dev Console and hide it
                // Check if dev console exists
                if (DevConsole == null)
                {
                    try
                    {
                        DrawUI();
                        DevConsole.gameObject.SetActive(false);
                    }
                    catch (System.Exception e)
                    {
                        MelonLogger.Msg($"An error occured: {e}");
                    }
                }

                try
                {
                    if (Keyboard.current.enterKey.wasPressedThisFrame && DevConsole.gameObject.activeInHierarchy)
                    {

                        CommandHistory.Add(CommandInput.text);
                        CommandRunner(CommandInput.text); // Execute command

                        UpdateCommandHistory();
                        CommandInput.Select();
                        CommandInput.ActivateInputField();
                    }
                }
                catch (System.Exception e)
                {
                    MelonLogger.Msg($"An error occured: {e}");
                    CommandHistory.Add("Error: Failed to execute command");
                    UpdateCommandHistory();
                    CommandInput.Select();
                    CommandInput.ActivateInputField();
                }

                try
                {
                    if (Keyboard.current.backquoteKey.wasPressedThisFrame)
                    {
                        // Check if player is in the game terminal
                        if (LC_Lib.IsInTerminalMenu(CurrentPlayer)) return;
                        CommandInput.text = "";
                        if (DevConsole.gameObject.activeInHierarchy)
                        {
                            DevConsole.gameObject.SetActive(false);

                            CurrentPlayer.enabled = true;
                        }
                        else
                        {
                            DevConsole.gameObject.SetActive(true);
                            CurrentPlayer.enabled = false;
                            CommandInput.Select();
                            CommandInput.ActivateInputField();
                        }
                    }
                }
                catch
                {
                    MelonLogger.Msg("Error: Failed to toggle Dev Console");
                }

                /* Override values */
                // NoClip
                if (LC_Lib.IsNoClip())
                {
                    // Check if player is dead
                    if(LC_Lib.IsDead(CurrentPlayer))
                    {
                        LC_Lib.ToggleNoclip(CurrentPlayer, false);
                        return;
                    }
                    // Check if player is in the ship
                    if (LC_Lib.IsInsideShip(CurrentPlayer)) return;

                    CurrentPlayer.fallValue = 0;
                    CurrentPlayer.fallValueUncapped = 0;
                    if (!DevConsole.gameObject.activeInHierarchy && !LC_Lib.IsTypingInChat(CurrentPlayer))
                    {
                        // Up
                        if (Keyboard.current.spaceKey.isPressed)
                        {
                            // Up Left
                            if (Keyboard.current.aKey.isPressed)
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.up * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.07f;
                            }
                            // Up Right
                            else if (Keyboard.current.dKey.isPressed) 
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.up * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.07f;
                            }
                            // Up Forward
                            else if (Keyboard.current.wKey.isPressed)
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.up * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.forward * 0.07f;
                            }
                            // Up Backwards
                            else if (Keyboard.current.sKey.isPressed)
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.up * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.forward * 0.07f;
                            }
                            else
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.up * 0.1f;
                            }
                        }
                        // Down
                        else if (Keyboard.current.leftCtrlKey.isPressed)
                        {
                            // Down Left
                            if (Keyboard.current.aKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.up * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.07f;
                            }
                            // Down Right
                            else if (Keyboard.current.dKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.up * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.07f;
                            }
                            // Down Forward
                            else if (Keyboard.current.wKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.up * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.forward * 0.07f;
                            }
                            // Down Backwards
                            else if (Keyboard.current.sKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.up * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.forward * 0.07f;
                            }
                            else
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.up * 0.1f;
                            }
                        }
                        // Left
                        else if (Keyboard.current.aKey.isPressed)
                        {
                            // Left and Right
                            if (Keyboard.current.dKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.07f;
                            }
                            // Left and Forward
                            else if (Keyboard.current.wKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.forward * 0.07f;
                            }
                            // Left and Backwards
                            else if (Keyboard.current.sKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.forward * 0.07f;
                            }
                            else
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.1f;
                            }
                        } 
                        // Right
                        else if (Keyboard.current.dKey.isPressed)
                        {
                            // Right and Left
                            if (Keyboard.current.aKey.isPressed)
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.07f;
                            }
                            // Right and Forward
                            else if (Keyboard.current.wKey.isPressed)
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.forward * 0.07f;
                            }
                            // Right and Backwards
                            else if (Keyboard.current.sKey.isPressed)
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.forward * 0.07f;
                            }
                            else
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.1f;
                            }
                        }
                        // Forward
                        else if (Keyboard.current.wKey.isPressed)
                        {
                            // Forward and Left
                            if (Keyboard.current.aKey.isPressed)
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.forward * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.07f;
                            }
                            // Forward and Right
                            else if (Keyboard.current.dKey.isPressed)
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.forward * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.07f;
                            }
                            else
                            {
                                CurrentPlayer.transform.position += CurrentPlayer.transform.forward * 0.1f;
                            }
                        }
                        // Backwards
                        else if (Keyboard.current.sKey.isPressed)
                        {
                            // Backwards and Left
                            if (Keyboard.current.aKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.forward * 0.07f;
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.07f;
                            }
                            // Backwards and Right
                            else if (Keyboard.current.dKey.isPressed)
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.forward * 0.07f;
                                CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.07f;
                            }
                            else
                            {
                                CurrentPlayer.transform.position -= CurrentPlayer.transform.forward * 0.1f;
                            }
                        }
                    }
                }
            }
        }

        private void DrawUI()
        {
            var canvas = new GameObject("Canvas").AddComponent<Canvas>();
            DevConsole = canvas;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999999;
            var scaler = canvas.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvas.gameObject.AddComponent<GraphicRaycaster>();

            var Dev_Console_Background = new GameObject("Dev_Console_Background");
            Dev_Console_Background.transform.parent = canvas.transform;
            var Dev_Console_Background_Transform = Dev_Console_Background.AddComponent<RectTransform>();
            Dev_Console_Background.transform.localPosition = Vector3.zero;
            Dev_Console_Background.AddComponent<RawImage>();
            Dev_Console_Background.GetComponent<RawImage>().color = new Color(0, 0, 0, 0.9f);
            Dev_Console_Background_Transform.anchorMin = new Vector2(0.5f, 1);
            Dev_Console_Background_Transform.anchorMax = new Vector2(0.5f, 1);
            Dev_Console_Background_Transform.pivot = new Vector2(0.5f, 1);
            Dev_Console_Background_Transform.sizeDelta = new Vector2(scaler.referenceResolution.x, 100);

            var Dev_Console_InputField = new GameObject("Dev_Console_InputField");
            Dev_Console_InputField.transform.parent = Dev_Console_Background.transform;
            Dev_Console_InputField.transform.localPosition = new Vector3(0, 0, 0);
            var Dev_Console_InputField_Transform = Dev_Console_InputField.AddComponent<RectTransform>();
            Dev_Console_InputField_Transform.anchorMin = new Vector2(0.5f, 0);
            Dev_Console_InputField_Transform.anchorMax = new Vector2(0.5f, 0);
            Dev_Console_InputField_Transform.pivot = new Vector2(0.5f, 1);
            Dev_Console_InputField_Transform.sizeDelta = new Vector2(scaler.referenceResolution.x, 13);
            Dev_Console_InputField_Transform.localPosition = new Vector3(10, (Dev_Console_InputField_Transform.localPosition.y + 13), 0);
            Dev_Console_InputField.AddComponent<Image>().color = new Color(0, 0, 0, 0);
            CommandInput = Dev_Console_InputField.AddComponent<TMP_InputField>();
            var img = Dev_Console_InputField_Transform.GetComponent<Image>();
            img.type = Image.Type.Sliced;
            img.sprite = sprite;
            CommandInput.targetGraphic = img;
            CommandInput.caretWidth = 0;
            var textArea = new GameObject("Text Area", typeof(RectMask2D));
            Dev_Console_InputField_Transform.GetComponent<TMP_InputField>().textViewport = textArea.GetComponent<RectTransform>();
            var textArea_Transform = textArea.GetComponent<RectTransform>();
            textArea_Transform.anchorMin = new Vector2(0.5f, 0);
            textArea_Transform.anchorMax = new Vector2(0.5f, 0);
            textArea_Transform.pivot = new Vector2(0.5f, 1);
            textArea_Transform.sizeDelta = new Vector2(scaler.referenceResolution.x, 13);
            textArea_Transform.localPosition = new Vector3(0, (Dev_Console_InputField_Transform.localPosition.y + 13), 0);
            textArea.transform.SetParent(Dev_Console_InputField_Transform.transform);
            textArea.GetComponent<RectTransform>().localPosition = Vector3.zero;
            var text = new GameObject("Text", typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            text.GetComponent<TextMeshProUGUI>().fontSize = 8;
            text.GetComponent<TextMeshProUGUI>().font = GameFont;
            text.transform.SetParent(textArea.transform);
            var text_Transform = text.GetComponent<RectTransform>();
            text_Transform.anchorMin = new Vector2(0.5f, 0);
            text_Transform.anchorMax = new Vector2(0.5f, 0);
            text_Transform.pivot = new Vector2(0.5f, 1);
            text_Transform.sizeDelta = new Vector2(scaler.referenceResolution.x, 13);
            text_Transform.localPosition = Vector3.zero;
            var text_Text = text.GetComponent<TextMeshProUGUI>();
            text_Text.color = new Color32(229, 89, 1, 255);
            text_Text.fontSize = 8;
            text_Text.font = GameFont;
            text_Text.alignment = TextAlignmentOptions.MidlineLeft;
            Dev_Console_InputField_Transform.GetComponent<TMP_InputField>().textComponent = text.GetComponent<TextMeshProUGUI>();
            Dev_Console_InputField_Transform.GetComponent<TMP_InputField>().Select();
            Dev_Console_InputField_Transform.GetComponent<TMP_InputField>().ActivateInputField();

            var Dev_Console_ViewPort = new GameObject("Dev_Console_ViewPort");
            Dev_Console_ViewPort.transform.parent = canvas.transform;
            var Dev_Console_ViewPort_Transform = Dev_Console_ViewPort.AddComponent<RectTransform>();
            Dev_Console_ViewPort.transform.localPosition = Vector3.zero;
            Dev_Console_ViewPort_Transform.anchorMin = new Vector2(0.5f, 1);
            Dev_Console_ViewPort_Transform.anchorMax = new Vector2(0.5f, 1);
            Dev_Console_ViewPort_Transform.pivot = new Vector2(0.5f, 1);
            Dev_Console_ViewPort_Transform.sizeDelta = new Vector2(scaler.referenceResolution.x, 100);

            var Dev_Console_Text = new GameObject("Dev_Console_Text");
            Dev_Console_Text.transform.parent = Dev_Console_ViewPort.transform;
            Dev_Console_Text.transform.localPosition = Vector3.zero;
            var Dev_Console_Text_Transform = Dev_Console_Text.AddComponent<RectTransform>();
            Dev_Console_Text_Transform.localPosition = new Vector3(10, Dev_Console_Text_Transform.localPosition.y + 13, 0);
            Dev_Console_Text_Transform.anchorMin = new Vector2(0, 1);
            Dev_Console_Text_Transform.anchorMax = new Vector2(1, 1);
            Dev_Console_Text_Transform.pivot = new Vector2(0.5f, 1);
            Dev_Console_Text_Transform.sizeDelta = new Vector2(0, 100 - 13);

            CommandOutput = Dev_Console_Text.AddComponent<TextMeshProUGUI>();
            CommandOutput.color = new Color32(229, 89, 1, 255);
            CommandOutput.fontSize = 8;
            CommandOutput.font = GameFont;
            CommandOutput.alignment = TextAlignmentOptions.BottomLeft;
        }
    }
}