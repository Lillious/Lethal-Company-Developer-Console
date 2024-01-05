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
    public class Terminal_RCE : MonoBehaviour
    {
        public void Awake()
        {
            // Log that the script has been added to the terminal
            Debug.Log("ScriptComponent has been added to the terminal");
        }

        public void Start()
        {
            // Deactivate the terminal
            gameObject.SetActive(false);
        }
    }

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

            var args = command.Split(' ');

            switch (args[0].ToLower())
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
                    return;

                // PLAYER COMMANDS
                // set <property> <value>
                case "set":
                    if (args.Length <= 2)
                    {
                        result = "Error: Missing command arguments";
                        break;
                    }

                    switch (args[1].ToLower())
                    {
                        case "group_credits":
                        case "gc":
                        case "credits":
                            {
                                Terminal terminal = LC_Lib.GetTerminal();
                                LC_Lib.SetGroupCredits(terminal, int.Parse(args[2]));
                                result = $"Group Credits: {LC_Lib.GetGroupCredits(terminal)}";
                                break;
                            }
                        // Sets the Player's Health
                        case "health":
                        case "hp":
                            LC_Lib.SetPlayerHealth(CurrentPlayer, int.Parse(args[2]));
                            result = $"Set Health to {args[2]}";
                            break;
                        // Sets the Player's Speed
                        case "speed":
                            LC_Lib.SetPlayerSpeed(CurrentPlayer, int.Parse(args[2]));
                            result = $"Set speed to {args[2]}";
                            break;
                        // Sets the Player's Jump Force
                        case "jump_force":
                        case "jump":
                            LC_Lib.SetPlayerJumpForce(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set jump force to {args[2]}";
                            break;
                        // Sets the Player's Climb Speed
                        case "climb_speed":
                        case "climb":
                            LC_Lib.SetClimbSpeed(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set climb speed to {args[2]}";
                            break;
                        // Sets the Player's Drunkness
                        case "drunkness":
                        case "drunk":
                            LC_Lib.SetDrunkness(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set drunkness to {args[2]}";
                            break;
                        // Sets the Player's drunk inertia
                        case "drunk_inertia":
                        case "drunk_inert":
                            LC_Lib.SetDrunknessInertia(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set drunk intertia to {args[2]}";
                            break;
                        // Sets the Player's drunk recovery time
                        case "drunk_recovery_time":
                        case "drunk_recovery":
                            LC_Lib.SetDrunkRecoveryTime(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set drunkness recovery time to {args[2]}";
                            break;
                        // Sets the Player's Health Regen Timer
                        case "health_regen_timer":
                        case "health_regen":
                        case "hp_regen_timer":
                        case "hp_regen":
                            LC_Lib.SetHealthRegenTimer(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set health regeneration time to {args[2]}";
                            break;
                        // Sets the Player's Grab Distance
                        case "grab_distance":
                        case "gd":
                            LC_Lib.SetGrabDistance(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set grab distance to {args[2]}";
                            break;
                        // Sets the Player's Exhaustion
                        case "exhaust":
                            LC_Lib.SetExhausted(CurrentPlayer, bool.Parse(args[2]));
                            result = $"Set exhaustion to {args[2]}";
                            break;
                        // Sets the Player's Max Insanity
                        case "max_insanity":
                        case "max_insane":
                            LC_Lib.SetMaxInsanity(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set max insanity to {args[2]}";
                            break;
                        // Sets the Player's insanity speed multiplier
                        case "insanity_speed_multiplier":
                        case "insanity_speed":
                            LC_Lib.SetInsanitySpeedMultiplier(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set insanity speed multiplier to {args[2]}";
                            break;
                        // Sets the Player's Minimum Velocity To Take Damage
                        case "min_velocity_to_take_damage":
                        case "min_velocity":
                            LC_Lib.SetMinVelocityToTakeDamage(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set minimum velocity to take damage to {args[2]}";
                            break;
                        // Sets the Player's Level
                        case "level":
                        case "lvl":
                            LC_Lib.SetLevelNumber(CurrentPlayer, int.Parse(args[2]));
                            result = $"Set level to {args[2]}";
                            break;
                        // Set player's position
                        case "position":
                        case "pos":
                            if (args.Length != 5)
                            {
                                result = "Error: Missing command arguments";
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
                            result = $"Set position to {x}, {y}, {z}\n" +
                                $"New position is {CurrentPosition.x}, {CurrentPosition.y}, {CurrentPosition.z}";
                            break;
                        // Sets  the player's hindered multiplier
                        case "hindered_multiplier":
                        case "hindered_multi":
                            LC_Lib.SetHinderedMultiplier(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set hindered multiplier to {args[2]}";
                            break;
                        // Set player's hindered status
                        case "hindered":
                        case "hinder":
                            LC_Lib.SetHindered(CurrentPlayer, int.Parse(args[2]));
                            result = $"Set hindered status to {args[2]}";
                            break;
                        // Sets the players sinking speed multiplier
                        case "sinking_speed_multiplier":
                        case "sinking_speed":
                            LC_Lib.SetSinkingSpeedMultiplier(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set sinking speed multiplier to {args[2]}";
                            break;
                        // Sets the player's sprint meter value
                        case "sprint_meter":
                        case "sprint":
                            LC_Lib.SetSprintMeterValue(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set sprint meter value to {args[2]}";
                            break;
                        // Sets the player's throw power
                        case "throw_power":
                        case "throw":
                            LC_Lib.SetThrowPower(CurrentPlayer, float.Parse(args[2]));
                            result = $"Set throw power to {args[2]}";
                            break;
                        // Sets the player's voice to be muffled
                        case "voice_muffled":
                        case "muffled":
                            if (bool.Parse(args[2]))
                            {
                                LC_Lib.SetVoiceMuffled(CurrentPlayer, true);
                                result = "Player's voice is now muffled";
                                break;
                            }
                            else
                            {
                                LC_Lib.SetVoiceMuffled(CurrentPlayer, false);
                                result = "Player's voice is now unmuffled";
                                break;
                            }
                        default:
                            result = "Error: Invalid command argument";
                            break;
                    }
                break;

                // get <property>
                case "get":
                    if (args.Length <= 1)
                    {
                        result = "Error: Missing command arguments";
                        break;
                    }
                    switch (args[1].ToLower())
                    {
                        // Returns the player's group credits
                        case "group_credits":
                        case "gc":
                            {
                                Terminal terminal = LC_Lib.GetTerminal();
                                result = $"Group credits: {LC_Lib.GetGroupCredits(terminal)}";
                            }
                            break;
                        // Returns the player's health
                        case "health":
                        case "hp":
                            result = $"Health: {LC_Lib.GetPlayerHealth(CurrentPlayer)}";
                            break;
                        // Returns the player's speed
                        case "speed":
                            result = $"Speed: {LC_Lib.GetPlayerSpeed(CurrentPlayer)}";
                            break;
                        // Returns the player's jump force
                        case "jump_force":
                        case "jump":
                            result = $"Jump force: {LC_Lib.GetPlayerJumpForce(CurrentPlayer)}";
                            break;
                        // Returns the player's climb speed
                        case "climb_speed":
                        case "climb":
                            result = $"Climb speed: {LC_Lib.GetClimbSpeed(CurrentPlayer)}";
                            break;
                        // Returns the player's drunkness
                        case "drunkness":
                        case "drunk":
                            result = $"Drunkness: {LC_Lib.GetDrunkness(CurrentPlayer)}";
                            break;
                        // Returns if the player is drunk
                        case "is_drunk":
                        case "drunk_status":
                        case "isdrunk":
                            result = $"Drunk: {LC_Lib.IsDrunk(CurrentPlayer)}";
                            break;
                        // Returns the player's drunkness inertia
                        case "drunk_inertia":
                        case "drunk_inert":
                            result = $"Drunk inertia: {LC_Lib.GetDrunknessInertia(CurrentPlayer)}";
                            break;
                        // Returns the player's drunk recovery time
                        case "drunk_recovery_time":
                        case "drunk_recovery":
                            result = $"Drunk recovery time: {LC_Lib.GetDrunkRecoveryTime(CurrentPlayer)}";
                            break;
                        // Returns the player's grab distance
                        case "grab_distance":
                        case "gd":
                            result = $"Grab distance: {LC_Lib.GetGrabDistance(CurrentPlayer)}";
                            break;
                        // Returns the player's exhaustion
                        case "exhaust":
                            result = $"Exhaustion: {LC_Lib.IsExhausted(CurrentPlayer)}";
                            break;
                        // Returns the player's max insanity
                        case "max_insanity":
                        case "max_insane":
                            result = $"Max insanity: {LC_Lib.GetMaxInsanity(CurrentPlayer)}";
                            break;
                        // Returns the player's insanity speed multiplier
                        case "insanity_speed_multiplier":
                        case "insanity_speed":
                            result = $"Insanity speed multiplier: {LC_Lib.GetInsanitySpeedMultiplier(CurrentPlayer)}";
                            break;
                        // Returns the player's minimum velocity to take damage
                        case "min_velocity_to_take_damage":
                        case "min_velocity":
                            result = $"Minimum velocity to take damage: {LC_Lib.GetMinVelocityToTakeDamage(CurrentPlayer)}";
                            break;
                        // Returns the player's level
                        case "level":
                        case "lvl":
                            result = $"Level: {LC_Lib.GetLevelNumber(CurrentPlayer)}";
                            break;
                        // Returns the player's position
                        case "position":
                        case "pos":
                            Vector3 CurrentPosition = LC_Lib.GetPlayerPosition(CurrentPlayer);
                            result = $"Position: {CurrentPosition.x}, {CurrentPosition.y}, {CurrentPosition.z}";
                            break;
                        // Returns the player's hindered multiplier
                        case "hindered_multiplier":
                        case "hindered_multi":
                            result = $"Hindered multiplier: {LC_Lib.GetHinderedMultiplier(CurrentPlayer)}";
                            break;
                        // Returns the player's hindered status
                        case "hindered":
                        case "hinder":
                            result = $"Hindered: {LC_Lib.IsHindered(CurrentPlayer)}";
                            break;
                        // Checks if the player is typing in chat
                        case "is_typing":
                        case "typing":
                            result = $"Is typing: {LC_Lib.IsTypingInChat(CurrentPlayer)}";
                            break;
                        // Check if player is under water
                        case "is_under_water":
                        case "under_water":
                            result = $"Under water: {LC_Lib.IsUnderWater(CurrentPlayer)}";
                            break;
                        // Check if player is dead
                        case "is_dead":
                        case "dead":
                            result = $"Dead: {LC_Lib.IsDead(CurrentPlayer)}";
                            break;
                        // Check if the player is sliding
                        case "is_sliding":
                        case "sliding":
                            result = $"Sliding: {LC_Lib.IsSliding(CurrentPlayer)}";
                            break;
                        // Check if the player is sinking
                        case "is_sinking":
                        case "sinking":
                            result = $"Sinking: {LC_Lib.IsSinking(CurrentPlayer)}";
                            break;
                        // Check if the player is alone
                        case "is_alone":
                        case "alone":
                            result = $"Alone: {LC_Lib.IsAlone(CurrentPlayer)}";
                            break;
                        // Checks if the player is inside the factory
                        case "is_inside_factory":
                        case "inside_factory":
                            result = $"Inside factory: {LC_Lib.IsInsideFactory(CurrentPlayer)}";
                            break;
                        // Checks if the player is inside the elevator
                        case "is_inside_elevator":
                        case "inside_elevator":
                            result = $"Inside elevator: {LC_Lib.IsInsideElevator(CurrentPlayer)}";
                            break;
                        // Checks if the player is inspecting an item
                        case "is_inspecting_item":
                        case "inspecting_item":
                        case "inspecting":
                            result = $"Inspecting item: {LC_Lib.IsInspectingItem(CurrentPlayer)}";
                            break;
                        // Checks if the player is climbing a ladder
                        case "is_climbing_ladder":
                        case "climbing_ladder":
                        case "climbing":
                            result = $"Climbing ladder: {LC_Lib.IsClimbingLadder(CurrentPlayer)}";
                            break;
                        // Checks if the player is holding an item
                        case "is_holding_item":
                        case "holding_item":
                        case "holding":
                            result = $"Holding item: {LC_Lib.IsHoldingItem(CurrentPlayer)}";
                            break;
                        // Checks if the player is inside the ship
                        case "is_inside_ship":
                        case "inside_ship":
                            result = $"Inside ship: {LC_Lib.IsInsideShip(CurrentPlayer)}";
                            break;
                        // Checks the player's two-handed value
                        case "is_two_handed":
                        case "two_handed":
                            result = $"Two handed: {LC_Lib.IsTwoHanded(CurrentPlayer)}";
                            break;
                        // Checks if the player is crouching
                        case "is_crouching":
                        case "crouching":
                            result = $"Crouching: {LC_Lib.IsCrouching(CurrentPlayer)}";
                            break;
                        // Checks if the player's voice is muffled
                        case "is_voice_muffled":
                        case "muffled":
                            result = $"Voice muffled: {LC_Lib.IsVoiceMuffled(CurrentPlayer)}";
                            break;
                        // Returns the players sinking speed multiplier
                        case "sinking_speed_multiplier":
                        case "sinking_speed":
                            result = $"Sinking speed multiplier: {LC_Lib.GetSinkingSpeedMultiplier(CurrentPlayer)}";
                            break;
                        // Returns the player's place of death
                        case "place_of_death":
                        case "death_place":
                            // Check if player is dead first
                            if (!LC_Lib.IsDead(CurrentPlayer))
                            {
                                result = "Error: Player is not dead";
                                break;
                            }
                            Vector3 death_location = LC_Lib.GetPlaceOfDeath(CurrentPlayer);
                            result = $"Place of death: x: {death_location.x}, y: {death_location.y}, z: {death_location.z}";
                            break;
                        // Returns the player's spawn point
                        case "spawn_point":
                        case "spawn":
                            Vector3 spawn_location = LC_Lib.GetSpawnPoint(CurrentPlayer);
                            result = $"Spawn point: x:{spawn_location.x}, y: {spawn_location.y}, z: {spawn_location.z}";
                            break;
                        // Returns if the player is using the jetpack
                        case "jetpack_controls":
                        case "jetpack":
                            result = $"Jetpack controls: {LC_Lib.GetJetpackControls(CurrentPlayer)}";
                            break;
                        // Returns the player's health regen timer
                        case "health_regen_timer":
                        case "health_regen":
                            result = $"Health regen timer: {LC_Lib.GetHealthRegenTimer(CurrentPlayer)}";
                            break;
                        // Returns the player's sprint meter value
                        case "sprint_meter":
                        case "sprint":
                            result = $"Sprint meter value: {LC_Lib.GetSprintMeterValue(CurrentPlayer)}";
                            break;
                        // Returns the player's throw power
                        case "throw_power":
                        case "throw":
                            result = $"Throw power: {LC_Lib.GetThrowPower(CurrentPlayer)}";
                            break;
                        // Returns the player's ID
                        case "player_id":
                        case "id":
                            result = $"ID: {LC_Lib.GetPlayerID(CurrentPlayer)}";
                            break;
                        // Returns the player's suit ID
                        case "player_suit_id":
                        case "suit_id":
                            result = $"Suit ID: {LC_Lib.GetPlayerSuitID(CurrentPlayer)}";
                            break;
                        // Returns the player's carry weight
                        case "carry_weight":
                        case "weight":
                            result = $"Carry weight: {LC_Lib.GetPlayerCarryWeight(CurrentPlayer)}";
                            break;
                        default:
                            result = "Error: Invalid command argument";
                            break;
                    }
                break;

                // Actions
                // action <action>
                case "action":
                    if (args.Length <= 1)
                    {
                        result = "Error: Missing command arguments";
                        break;
                    }
                    switch (args[1].ToLower())
                    {
                        case "helmet":
                        case "helm":
                            switch (args[2].ToLower())
                            {
                                case "true":
                                case "t":
                                case "on":
                                    LC_Lib.AddHelmet();
                                    result = $"Enabled helmet";
                                    break;
                                case "false":
                                case "f":
                                case "off":
                                    LC_Lib.RemoveHelmet();
                                    result = $"Disabled helmet";
                                    break;
                            }
                            break;
                        case "no_clip":
                        case "noclip":
                        case "nc":
                            switch (args[2].ToLower())
                            {
                                case "true":
                                case "t":
                                case "on":
                                    if (LC_Lib.IsInsideShip(CurrentPlayer))
                                    {
                                        result = "Error: Player is inside the ship";
                                        break;
                                    }
                                    LC_Lib.ToggleNoclip(CurrentPlayer, true);
                                    result = $"Enabled No Clip";
                                    break;
                                case "false":
                                case "f":
                                case "off":
                                    LC_Lib.ToggleNoclip(CurrentPlayer, false);
                                    result = $"Disabled No Clip";
                                    break;
                            }
                            break;
                        case "eject":
                        case "ej":
                            if (!LC_Lib.IsHost())
                            {
                                result = "Error: You are not the host";
                                break;
                            }
                            LC_Lib.Eject();
                            result = $"Starting ejection sequence";
                            break;
                        case "infinite_sprint":
                        case "inf_sprint":
                            switch (args[2].ToLower())
                            {
                                case "true":
                                case "t":
                                case "on":
                                    LC_Lib.ToggleInfiniteSprint(true);
                                    result = $"Enabled Infinite Sprint";
                                    break;
                                case "false":
                                case "f":
                                case "off":
                                    LC_Lib.ToggleInfiniteSprint(false);
                                    result = $"Disabled Infinite Sprint";
                                    break;
                            }
                            break;
                        case "damage":
                        case "dmg":
                            {
                                if (args.Length <= 3)
                                {
                                    result = "Error: Missing command arguments";
                                    break;
                                }
                                PlayerControllerB player = LC_Lib.GetPlayerByName(args[2]);
                                // Check if player exists
                                if (!player)
                                {
                                    result = $"Could not find player {args[1]}";
                                    break;
                                }

                                int Damage = args.Length > 3 ? int.Parse(args[3]) : 0;

                                LC_Lib.DamagePlayer(player, Damage);

                                result = $"Damaged {LC_Lib.GetPlayerName(player)} for {Damage} damage";
                            }
                            break;
                        case "heal":
                            {
                                if (args.Length <= 2)
                                {
                                    result = "Error: Missing command arguments";
                                    break;
                                }
                                PlayerControllerB player = LC_Lib.GetPlayerByName(args[2]);
                                // Check if player exists
                                if (!player)
                                {
                                    result = $"Could not find player {args[2]}";
                                    break;
                                }

                                LC_Lib.HealPlayer(player);

                                result = $"Healed {LC_Lib.GetPlayerName(player)}";
                            }
                            break;
                        case "kill":
                            {
                                if (args.Length <= 2)
                                {
                                    result = "Error: Missing command arguments";
                                    break;
                                }

                                // Kill all players
                                if (args[2] == "all")
                                {
                                    List<PlayerControllerB> Players = LC_Lib.GetAllPlayers();
                                    foreach (PlayerControllerB _player in Players)
                                    {
                                        LC_Lib.KillPlayer(_player);
                                    }
                                    result = $"Killed all players";
                                    break;
                                }

                                PlayerControllerB player = LC_Lib.GetPlayerByName(args[2]);
                                // Check if player exists
                                if (!player)
                                {
                                    result = $"Could not find player {args[2]}";
                                    break;
                                }

                                LC_Lib.KillPlayer(player);

                                result = $"Killed {LC_Lib.GetPlayerName(player)}";
                            }
                            break;
                        case "blood":
                            {
                                if (args.Length <= 3)
                                {
                                    result = "Error: Missing command arguments";
                                    break;
                                }

                                PlayerControllerB player = LC_Lib.GetPlayerByName(args[3]);
                                // Check if player exists
                                if (!player)
                                {
                                    result = $"Could not find player {args[3]}";
                                    break;
                                }

                                switch (args[2])
                                {
                                    case "add":
                                        {
                                            LC_Lib.AddBloodToPlayerBody(player);
                                            result = $"Added blood to {LC_Lib.GetPlayerName(player)}";
                                        }
                                        break;
                                    case "remove":
                                        {
                                            LC_Lib.RemoveBloodFromPlayerBody(player);
                                            result = $"Removed blood from {LC_Lib.GetPlayerName(player)}";
                                        }
                                        break;
                                    default:
                                        result = "Error: Invalid command argument";
                                        break;
                                }
                            }
                            break;
                        default:
                            result = "Error: Invalid command argument";
                            break;
                    }
                break;
                // Teleports
                // teleport <location>
                case "teleport":
                case "tp":
                    if (args.Length <= 1)
                    {
                        result = "Error: Missing command arguments";
                        break;
                    }

                    // Teleports current player to location
                    switch (args[1].ToLower())
                    {
                        case "ship":
                            Vector3 ShipPosition = LC_Lib.GetSpawnPoint(CurrentPlayer);
                            LC_Lib.TeleportPlayer(CurrentPlayer, ShipPosition);
                            result = $"Teleported to the ship";
                            break;
                    }

                    // Teleport current player to another player
                    if (args.Length == 2)
                    {
                        if (args[1].ToLower() == LC_Lib.GetPlayerName(CurrentPlayer).ToLower())
                        {
                            result = $"Cannot teleport player to themselves";
                            break;
                        }

                        PlayerControllerB player = LC_Lib.GetPlayerByName(args[1]);
                        if (!player)
                        {
                            result = $"Could not find player {args[1]}";
                            break;
                        }

                        LC_Lib.TeleportToPlayer(CurrentPlayer, LC_Lib.GetPlayerName(player));
                        result = $"Teleported to {LC_Lib.GetPlayerName(player)}";
                        break;
                    }

                    // Teleport a player to another player
                    if (args.Length == 3)
                    {
                        if (args[1].ToLower() == args[2].ToLower())
                        {
                            result = $"Cannot teleport player to themselves";
                            break;
                        }

                        PlayerControllerB player = LC_Lib.GetPlayerByName(args[1]);
                        if (!player)
                        {
                            result = $"Could not find player {args[1]}";
                            break;
                        }

                        PlayerControllerB player2 = LC_Lib.GetPlayerByName(args[2]);
                        if (!player2)
                        {
                            result = $"Could not find player {args[2]}";
                            break;
                        }

                        LC_Lib.TeleportPlayerToPlayer(LC_Lib.GetPlayerName(player), LC_Lib.GetPlayerName(player2));
                        result = $"Teleported {LC_Lib.GetPlayerName(player)} to {LC_Lib.GetPlayerName(player2)}";
                        break;
                    }
                    break;

                default:
                    result = "Error: Invalid command";
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

                // Keybind toggles

                // Toggle NoClip
                if (Keyboard.current.zKey.wasPressedThisFrame)
                {
                    if (LC_Lib.IsNoClip())
                    {
                        LC_Lib.ToggleNoclip(CurrentPlayer, false);
                    }
                    else
                    {
                        LC_Lib.ToggleNoclip(CurrentPlayer, true);
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

                if (Keyboard.current.escapeKey.wasPressedThisFrame && DevConsole.gameObject.activeInHierarchy)
                {
                    DevConsole.gameObject.SetActive(false);
                    CurrentPlayer.enabled = true;
                }

                /* Override values */

                // Infinite Sprint
                if (LC_Lib.IsInfiniteSprint())
                {
                    CurrentPlayer.isSprinting = false;
                }

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
            Dev_Console_InputField_Transform.sizeDelta = new Vector2(scaler.referenceResolution.x, 7);
            Dev_Console_InputField_Transform.localPosition = new Vector3(0, (Dev_Console_InputField_Transform.localPosition.y + 7), 0);
            Dev_Console_InputField.AddComponent<Image>().color = new Color(0, 0, 0, 0.3f);
            CommandInput = Dev_Console_InputField.AddComponent<TMP_InputField>();
            var img = Dev_Console_InputField_Transform.GetComponent<Image>();
            img.type = Image.Type.Sliced;
            img.sprite = sprite;
            sprite = img.sprite;
            CommandInput.targetGraphic = img;
            CommandInput.caretWidth = 1;
            CommandInput.characterLimit = 50;
            var textArea = new GameObject("Text Area", typeof(RectMask2D));
            Dev_Console_InputField_Transform.GetComponent<TMP_InputField>().textViewport = textArea.GetComponent<RectTransform>();
            var textArea_Transform = textArea.GetComponent<RectTransform>();
            textArea_Transform.anchorMin = new Vector2(0.5f, 0);
            textArea_Transform.anchorMax = new Vector2(0.5f, 0);
            textArea_Transform.pivot = new Vector2(0.5f, 1);
            textArea_Transform.sizeDelta = new Vector2(scaler.referenceResolution.x, 7);
            textArea_Transform.localPosition = new Vector3(0, (Dev_Console_InputField_Transform.localPosition.y + 7), 0);
            textArea.transform.SetParent(Dev_Console_InputField_Transform.transform);
            textArea.GetComponent<RectTransform>().localPosition = Vector3.zero;
            var text = new GameObject("Text", typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            text.GetComponent<TextMeshProUGUI>().fontSize = 7;
            text.GetComponent<TextMeshProUGUI>().font = GameFont;
            text.transform.SetParent(textArea.transform);
            var text_Transform = text.GetComponent<RectTransform>();
            text_Transform.anchorMin = new Vector2(0.5f, 0);
            text_Transform.anchorMax = new Vector2(0.5f, 0);
            text_Transform.pivot = new Vector2(0.5f, 1);
            text_Transform.sizeDelta = new Vector2(scaler.referenceResolution.x, 8);
            text_Transform.localPosition = Vector3.zero;
            var text_Text = text.GetComponent<TextMeshProUGUI>();
            text_Text.color = new Color32(229, 89, 1, 255);
            text_Text.fontSize = 6;
            text_Text.font = GameFont;
            text_Text.alignment = TextAlignmentOptions.TopLeft;
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