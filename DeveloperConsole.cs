﻿using GameNetcodeStuff;
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
        private bool initialized = false;
        private string result = "";
        private TMP_FontAsset GameFont;
        private PlayerControllerB CurrentPlayer;
        private CharacterController CurrentPlayerController;

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

                // PLAYER COMMANDS
                // set <player> <property> <value>
                case "set":
                    if (args.Length < 2)
                    {
                        result = "Error: Invalid command";
                        break;
                    }
                    {
                        PlayerControllerB Player = LC_Lib.GetPlayer(args[1]);
                        if (Player is null)
                        {
                            switch (args[1])
                            {
                                // Sets the player's group credits
                                case "group_credits":
                                    {
                                        Terminal terminal = LC_Lib.GetTerminal();
                                        LC_Lib.SetGroupCredits(terminal, int.Parse(args[2]));
                                        result = $"Group Credits: {LC_Lib.GetGroupCredits(terminal)}";
                                    }
                                    break;
                                default:
                                    result = "Error: Invalid command";
                                    break;
                            }
                        }
                        else
                        {
                            switch (args[2])
                            {
                                // Sets the Player's Health
                                case "health":
                                    LC_Lib.SetPlayerHealth(Player, int.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s health to {args[3]}";
                                    break;
                                // Sets the Player's Speed
                                case "speed":
                                    LC_Lib.SetPlayerSpeed(Player, int.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s speed to {args[3]}";
                                    break;
                                // Sets the Player's Jump Force
                                case "jump":
                                    LC_Lib.SetPlayerJumpForce(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s jump force to {args[3]}";
                                    break;
                                // Sets the Player's Climb Speed
                                case "climb_speed":
                                    LC_Lib.SetClimbSpeed(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s climb speed to {args[3]}";
                                    break;
                                // Sets the Player's Drunkness
                                case "drunkness":
                                    LC_Lib.SetDrunkness(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s drunkness to {args[3]}";
                                    break;
                                // Sets the Player's drunk inertia
                                case "drunk_inertia":
                                    LC_Lib.SetDrunknessInertia(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s drunk_inertia to {args[3]}";
                                    break;
                                // Sets the Player's drunk recovery time
                                case "drunk_recovery_time":
                                    LC_Lib.SetDrunkRecoveryTime(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s drunk recovery time to {args[3]}";
                                    break;
                                // Sets the Player's Grab Distance
                                case "grab_distance":
                                    LC_Lib.SetGrabDistance(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s Grab Distance to {args[3]}";
                                    break;
                                // Sets the Player's Exhaustion
                                case "exhaust":
                                    LC_Lib.SetExhausted(Player, bool.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s Exhaustion to {args[3]}";
                                    break;
                                // Sets the Player's Max Insanity
                                case "max_insanity":
                                    LC_Lib.SetMaxInsanity(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s Max Insanity to {args[3]}";
                                    break;
                                // Sets the Player's insanity speed multiplier
                                case "insanity_speed_multiplier":
                                    LC_Lib.SetInsanitySpeedMultiplier(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s Insanity Speed Multiplier to {args[3]}";
                                    break;
                                // Sets the Player's Minimum Velocity To Take Damage
                                case "min_velocity_to_take_damage":
                                    LC_Lib.SetMinVelocityToTakeDamage(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s Minimum Velocity To Take Damage to {args[3]}";
                                    break;
                                // Sets the Player's Level
                                case "level":
                                    LC_Lib.SetLevelNumber(Player, int.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s Level to {args[3]}";
                                    break;
                                // Set player's position
                                case "position":
                                    if (args.Length != 6)
                                    {
                                        result = "Error: Invalid Arguments";
                                        break;
                                    }
                                    Vector3 CurrentPosition = LC_Lib.GetPlayerPosition(Player);
                                    float x, y, z;
                                    // - means x y or z position value
                                    // X position
                                    if (args[3] == "-")
                                    {
                                        x = CurrentPosition.x;
                                    }
                                    else
                                    {
                                        x = float.Parse(args[3]);
                                    }
                                    // Y position
                                    if (args[4] == "-")
                                    {
                                        y = CurrentPosition.y;
                                    }
                                    else
                                    {
                                        y = float.Parse(args[4]);
                                    }
                                    // Z position
                                    if (args[5] == "-")
                                    {
                                        z = CurrentPosition.z;
                                    }
                                    else
                                    {
                                        z = float.Parse(args[5]);
                                    }

                                    LC_Lib.TeleportPlayer(Player, new Vector3(x, y, z));
                                    result = $"Set Player {args[1]}'s Position to {x}, {y}, {z}\n" +
                                        $"Player {args[1]}'s new position is {CurrentPosition.x}, {CurrentPosition.y}, {CurrentPosition.z}";
                                    break;
                                // Sets  the player's hindered multiplier
                                case "hindered_multiplier":
                                    LC_Lib.SetHinderedMultiplier(Player, float.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s Hindered Multiplier to {args[3]}";
                                    break;
                                // Set player's hindered status
                                case "hindered":
                                    LC_Lib.SetHindered(Player, int.Parse(args[3]));
                                    result = $"Set Player {args[1]}'s Hindered Status to {args[3]}";
                                    break;
                                default:
                                    result = "Error: Invalid command";
                                    break;
                            }
                        }
                    }
                    break;

                // get <player> <property>
                case "get":
                    if (args.Length < 2)
                    {
                        result = "Error: Invalid command";
                        break;
                    }
                    {
                        PlayerControllerB Player = LC_Lib.GetPlayer(args[1]);
                        if (Player is null)
                        {
                            switch (args[1]) {
                                // Returns the player's group credits
                                case "group_credits":
                                    {
                                        Terminal terminal = LC_Lib.GetTerminal();
                                        result = $"Group Credits: {LC_Lib.GetGroupCredits(terminal)}";
                                    }
                                break;
                                default:
                                    result = "Error: Invalid command";
                                    break;
                            }
                        }
                        else
                        {
                            switch (args[2])
                            {
                                // Returns the player's health
                                case "health":
                                    result = $"Player {args[1]}'s health is {LC_Lib.GetPlayerHealth(Player)}";
                                    break;
                                // Returns the player's speed
                                case "speed":
                                    result = $"Player {args[1]}'s speed is {LC_Lib.GetPlayerSpeed(Player)}";
                                    break;
                                // Returns the player's jump force
                                case "jump":
                                    result = $"Player {args[1]}'s Jump Force: {LC_Lib.GetPlayerJumpForce(Player)}";
                                    break;
                                // Returns the player's climb speed
                                case "climb_speed":
                                    result = $"Player {args[1]}'s Climb Speed: {LC_Lib.GetClimbSpeed(Player)}";
                                    break;

                                // Returns the player's drunkness
                                case "drunkness":
                                    result = $"Player {args[1]}'s Drunkness: {LC_Lib.GetDrunkness(Player)}";
                                    break;
                                // Returns if the player is drunk
                                case "is_drunk":
                                    if (LC_Lib.IsDrunk(Player))
                                    {
                                        result = $"Player {args[1]} is drunk";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not drunk";
                                        break;
                                    }
                                // Returns the player's drunkness inertia
                                case "drunk_inertia":
                                    result = $"Player {args[1]}'s Drunk Inertia: {LC_Lib.GetDrunknessInertia(Player)}";
                                    break;
                                // Returns the player's drunk recovery time
                                case "drunk_recovery_time":
                                    result = $"Player {args[1]}'s Drunk Recovery Time: {LC_Lib.GetDrunkRecoveryTime(Player)}";
                                    break;

                                // Returns the player's grab distance
                                case "grab_distance":
                                    result = $"Player {args[1]}'s Grab Distance: {LC_Lib.GetGrabDistance(Player)}";
                                    break;
                                // Returns the player's exhaustion
                                case "exhaust":
                                    result = $"Player {args[1]}'s Exhaustion: {LC_Lib.IsExhausted(Player)}";
                                    break;

                                // Returns the player's max insanity
                                case "max_insanity":
                                    result = $"Player {args[1]}'s Max Insanity: {LC_Lib.GetMaxInsanity(Player)}";
                                    break;
                                // Returns the player's insanity speed multiplier
                                case "insanity_speed_multiplier":
                                    result = $"Player {args[1]}'s Insanity Speed Multiplier: {LC_Lib.GetInsanitySpeedMultiplier(Player)}";
                                    break;

                                // Returns the player's minimum velocity to take damage
                                case "min_velocity_to_take_damage":
                                    result = $"Player {args[1]}'s Minimum Velocity To Take Damage: {LC_Lib.GetMinVelocityToTakeDamage(Player)}";
                                    break;
                                // Returns the player's level
                                case "level":
                                    result = $"Player {args[1]}'s Level: {LC_Lib.GetLevelNumber(Player)}";
                                    break;
                                // Returns the player's position
                                case "position":
                                    Vector3 CurrentPosition = LC_Lib.GetPlayerPosition(Player);
                                    result = $"Player {args[1]}'s Position: {CurrentPosition.x}, {CurrentPosition.y}, {CurrentPosition.z}";
                                    break;
                                // Returns the player's hindered multiplier
                                case "hindered_multiplier":
                                    result = $"Player {args[1]}'s Hindered Multiplier: {LC_Lib.GetHinderedMultiplier(Player)}";
                                    break;
                                // Returns the player's hindered status
                                case "hindered":
                                    result = $"Player {args[1]}'s Hindered Status: {LC_Lib.IsHindered(Player)}";
                                    break;
                                // Checks if the player is typing in chat
                                case "is_typing":
                                    if (LC_Lib.IsTypingInChat(Player))
                                    {
                                        result = $"Player {args[1]} is typing";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not typing";
                                        break;
                                    }
                                // Check if player is under water
                                case "is_under_water":
                                    if (LC_Lib.IsUnderWater(Player))
                                    {
                                        result = $"Player {args[1]} is under water";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not under water";
                                        break;
                                    }
                                // Check if player is dead
                                case "is_dead":
                                    if (LC_Lib.IsDead(Player))
                                    {
                                        result = $"Player {args[1]} is dead";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not dead";
                                        break;
                                    }
                                // Check if the player is sliding
                                case "is_sliding":
                                    if (LC_Lib.IsSliding(Player))
                                    {
                                        result = $"Player {args[1]} is sliding";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not sliding";
                                        break;
                                    }
                                // Check if the player is sinking
                                case "is_sinking":
                                    if (LC_Lib.IsSinking(Player))
                                    {
                                        result = $"Player {args[1]} is sinking";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not sinking";
                                        break;
                                    }
                                // Check if the player is alone
                                case "is_alone":
                                    if (LC_Lib.IsAlone(Player))
                                    {
                                        result = $"Player {args[1]} is alone";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not alone";
                                        break;
                                    }
                                // Checks if the player is inside the factory
                                case "is_inside_factory":
                                    if (LC_Lib.IsInsideFactory(Player))
                                    {
                                        result = $"Player {args[1]} is inside the factory";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is outside the factory";
                                        break;
                                    }
                                // Checks if the player is inside the elevator
                                case "is_inside_elevator":
                                    if (LC_Lib.IsInsideElevator(Player))
                                    {
                                        result = $"Player {args[1]} is inside the elevator";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is outside the elevator";
                                        break;
                                    }
                                // Checks if the player is inspecting an item
                                case "is_inspecting_item":
                                    if (LC_Lib.IsInspectingItem(Player))
                                    {
                                        result = $"Player {args[1]} is inspecting an item";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not inspecting an item";
                                        break;
                                    }
                                // Checks if the player is climbing a ladder
                                case "is_climbing_ladder":
                                    if (LC_Lib.IsClimbingLadder(Player))
                                    {
                                        result = $"Player {args[1]} is climbing a ladder";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not climbing a ladder";
                                        break;
                                    }
                                // Checks if the player is holding an item
                                case "is_holding_item":
                                    if (LC_Lib.IsHoldingItem(Player))
                                    {
                                        result = $"Player {args[1]} is holding an item";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not holding an item";
                                        break;
                                    }
                                // Checks if the player is inside the ship
                                case "is_inside_ship":
                                    if (LC_Lib.IsInsideShip(Player))
                                    {
                                        result = $"Player {args[1]} is inside the ship";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not inside the ship";
                                        break;
                                    }
                                // Checks the player's two-handed value
                                case "is_two_handed":
                                    if (LC_Lib.IsTwoHanded(Player))
                                    {
                                        result = $"Player {args[1]} is two-handed";
                                        break;
                                    }
                                    else
                                    {
                                        result = $"Player {args[1]} is not two-handed";
                                        break;
                                    }
                                    

                                    default:
                                        result = "Error: Invalid command";
                                    break;
                            }
                        }
                    }
                    break;

                // Actions
                // action <player> <action>
                case "action":
                    if (args.Length < 2)
                    {
                        result = "Error: Invalid command";
                        break;
                    }
                    {
                        PlayerControllerB Player = LC_Lib.GetPlayer(args[1]);
                        if (Player is null)
                        {
                            switch (args[1])
                            {
                                case "add_helmet":
                                    LC_Lib.AddHelmet();
                                    result = $"Added helmet to Player {args[1]}'s head";
                                    break;
                                case "remove_helmet":
                                    LC_Lib.RemoveHelmet();
                                    result = $"Removed helmet from Player {args[1]}'s head";
                                    break;
                                case "no_clip":
                                    switch (args[2])
                                    {
                                        case "on":
                                            if (LC_Lib.IsInsideShip(CurrentPlayer)) {
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
                                default:
                                    result = "Error: Invalid command";
                                    break;
                            }
                        }
                        else
                        {
                            switch (args[2])
                            {
                                case "add_blood":
                                    LC_Lib.AddBloodToPlayerBody(Player);
                                    result = $"Added blood to Player {args[1]}'s body";
                                    break;
                                case "remove_blood":
                                    LC_Lib.RemoveBloodFromPlayerBody(Player);
                                    result = $"Removed blood from Player {args[1]}'s body";
                                    break;
                                default:
                                    result = "Error: Invalid command";
                                    break;
                            }
                        }
                    }
                    break;

                // Teleports
                // teleport <player> <location>
                case "teleport":
                    if (args.Length < 2)
                    {
                        result = "Error: Invalid command";
                        break;
                    }
                    {
                        PlayerControllerB Player = LC_Lib.GetPlayer(args[1]);
                        if (Player is null)
                        {
                            result = "Error: Player not found";
                            break;
                        }
                        switch (args[2])
                        {
                            case "ship":
                                Vector3 ShipPosition = LC_Lib.GetSpawnPoint(Player);
                                LC_Lib.TeleportPlayer(Player, ShipPosition);
                                result = $"Teleported Player {args[1]} to the ship";
                                break;
                        }
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

            if (!initialized && LC_Lib.IsInGame())
            {
                CurrentPlayer = LC_Lib.SearchForControlledPlayer();
                CurrentPlayerController = CurrentPlayer.GetComponent<CharacterController>();
                GameFont = GameObject.Find("Weight").GetComponent<TextMeshProUGUI>().font;
                initialized = true;
                return;
            }

            if (initialized && LC_Lib.IsInGame())
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
                    if (!DevConsole.gameObject.activeInHierarchy)
                    {
                        if (Keyboard.current.spaceKey.isPressed)
                        {
                            CurrentPlayer.transform.position += CurrentPlayer.transform.up * 0.1f;
                        }
                        else if (Keyboard.current.leftCtrlKey.isPressed)
                        {
                            CurrentPlayer.transform.position -= CurrentPlayer.transform.up * 0.1f;
                        }
                        else if (Keyboard.current.wKey.isPressed)
                        {
                            CurrentPlayer.transform.position += CurrentPlayer.transform.forward * 0.1f;
                        }
                        else if (Keyboard.current.sKey.isPressed)
                        {
                            CurrentPlayer.transform.position -= CurrentPlayer.transform.forward * 0.1f;
                        }
                        else if (Keyboard.current.aKey.isPressed)
                        {
                            CurrentPlayer.transform.position -= CurrentPlayer.transform.right * 0.1f;
                        }
                        else if (Keyboard.current.dKey.isPressed)
                        {
                            CurrentPlayer.transform.position += CurrentPlayer.transform.right * 0.1f;
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