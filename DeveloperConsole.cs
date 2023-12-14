using GameNetcodeStuff;
using Lethal_Library;
using MelonLoader;
using Non_Lethal_Dev_Console;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[assembly: MelonInfo(typeof(DeveloperConsole), "Non-Lethal Developer Console", "1.0.0", "Lillious, .Zer0")]
[assembly: MelonGame("ZeekerssRBLX", "Lethal Company")]
namespace Non_Lethal_Dev_Console
{
    public class DeveloperConsole : MelonMod
    {
        [SerializeField] private Sprite sprite;
        private TMP_InputField CommandInput;
        private TextMeshProUGUI CommandOutput;
        private List<string> CommandHistory = new List<string>();
        private Canvas DevConsole;
        static Library LC_Lib = new Library();
        private bool isInGame = false;
        private bool initialized = false;
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
                // help <command>
                case "help":
                    if (args.Length == 1)
                    {
                        result = "Commands:\n" +
                            "help - Displays this message\n" +
                            "clear - Clears the command history\n" +
                            "help set - Shows available commands for 'set'\n" +
                            "help get - Shows available commands for 'get'";
                        break;
                    }
                    switch (args[1])
                    {
                        case "set":
                            result = "Commands:\n" +
                            "set <player number> health <value> - Sets the player's health\n" +
                            "set <player number> speed <value> - Sets the player's sprint speed\n" +
                            "set <player number> jump <value> - Sets the player's jump force\n" +
                            "set <player number> climb_speed <value> - Sets the player's climb speed\n" +
                            "set <player number> drunkness <value> - Sets the player's drunkness\n" +
                            "set <player number> grab_distance <value> - Sets the player's grab distance";
                            break;
                        case "get":
                            result = "Commands:\n" +
                            "get <player number> health - Returns player's health\n" +
                            "get <player number> speed - Returns the player's speed\n" +
                            "get <player number> jump - Returns the player's jump force\n" +
                            "get <player number> climb_speed - Returns the player's climb speed\n" +
                            "get <player number> drunkness - Returns the player's drunkness\n" +
                            "get <player number> grab_distance - Returns the player's grab distance";
                            break;
                    }
                    break;

                // set <player> <property> <value>
                case "set":
                    {
                        PlayerControllerB Player = LC_Lib.GetPlayer(args[1]);
                        if (Player is null)
                        {
                            result = "Error: Player not found";
                            break;
                        }
                        // If "player" is typed
                        switch (args[2])
                        {
                            case "health":
                                LC_Lib.SetPlayerHealth(Player, int.Parse(args[3]));
                                result = $"Set Player {args[1]}'s health to {args[3]}";
                                break;
                            case "speed":
                                LC_Lib.SetPlayerSpeed(Player, int.Parse(args[3]));
                                result = $"Set Player {args[1]}'s speed to {args[3]}";
                                break;
                            // Set Current Player's Jump Force
                            case "jump":
                                LC_Lib.SetPlayerJumpForce(Player, float.Parse(args[3]));
                                result = $"Set Player {args[1]}'s jump force to {args[3]}";
                                break;
                            // Set Current Player's Climb Speed
                            case "climb_speed":
                                LC_Lib.SetClimbSpeed(Player, float.Parse(args[3]));
                                result = $"Set Player {args[1]}'s climb speed to {args[3]}";
                                break;
                            // Set Current Player's Drunkness
                            case "drunkness":
                                LC_Lib.SetDrunkness(Player, float.Parse(args[3]));
                                result = $"Set Player {args[1]}'s drunkness to {args[3]}";
                                break;
                            // Set Current Player's Grab Distance
                            case "grab_distance":
                                LC_Lib.SetGrabDistance(Player, float.Parse(args[3]));
                                result = $"Set Player {args[1]}'s Grab Distance to {args[3]}";
                                break;
                        }
                    }
                    break;

                // get <player> <property>
                case "get":
                    {
                        PlayerControllerB Player = LC_Lib.GetPlayer(args[1]);
                        if (Player is null)
                        {
                            result = "Error: Player not found";
                            break;
                        }
                        switch (args[2])
                        {
                            case "health":
                                result = $"Player {args[1]}'s health is {LC_Lib.GetPlayerHealth(Player)}";
                                break;
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
                            case "grab_distance":
                                result = $"Player {args[1]}'s Grab Distance: {LC_Lib.GetGrabDistance(Player)}";
                                break;
                        }
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(result))
            {
                CommandHistory.Add(result);
            }
            if (CommandHistory.Count > 10) CommandHistory.RemoveAt(0);
            CommandOutput.text = "";
            foreach (string cmd in CommandHistory)
            {
                CommandOutput.text += "\n" + cmd;
            }
        }

        private void Initialize()
        {
            if (!isInGame) return;
            CurrentPlayer = LC_Lib.SearchForControlledPlayer();
            GameFont = GameObject.Find("Weight").GetComponent<TextMeshProUGUI>().font;
            initialized = true;
        }

        public override void OnUpdate()
        {
            if (initialized && isInGame)
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
                    catch
                    {
                        MelonLogger.Msg("Error: Failed to draw UI");
                    }
                }

                try
                {
                    if (Keyboard.current.enterKey.wasPressedThisFrame && DevConsole.gameObject.activeInHierarchy)
                    {
                        if (CommandInput.text.ToLower() == "clear")
                        {
                            CommandHistory.Clear();
                            CommandOutput.text = "";
                        }
                        else
                        {
                            CommandHistory.Add(CommandInput.text);
                            CommandRunner(CommandInput.text); // Execute command
                        }

                        CommandInput.text = "";
                        CommandInput.Select();
                        CommandInput.ActivateInputField();
                    }
                }
                catch
                {
                    MelonLogger.Msg("Error: Failed to open dev console");
                }

                try
                {
                    if (Keyboard.current.backquoteKey.wasPressedThisFrame)
                    {
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
            }
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "SampleSceneRelay")
            {
                isInGame = true;
                Initialize();
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (sceneName == "SampleSceneRelay")
            {
                isInGame = false;
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
