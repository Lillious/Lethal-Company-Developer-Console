# Lethal Company Developer Console

<br>

> [!IMPORTANT]
> **Required Dependencies**
> 
> - [Lethal Company Mod Library](https://github.com/Lillious/Lethal-Company-Mod-Library)

<br>

## Install Instructions

<br>

> [!TIP]
> **Don't know how to install? Review the information below**

### MelonLoader
- Download [MelonLoader.Installer.exe](https://github.com/LavaGang/MelonLoader/releases/latest)
- Run MelonLoader.Installer.exe
- Click the **select** button.
- Select Lethal Company.exe in your Game's Installation Folder.
- Uncheck **Latest** and select version **v0.6.1** from the Drop-Down List.
- Once the installation is successful, open the game through Steam.
- Place **Lethal_Library.dll** and **Non-Lethal Dev Console.dll** into the newly created Mods folder inside the Game's Installation Folder.

<br>

### BepInEx
- Download the appropriate [BepInEx](https://github.com/BepInEx/BepInEx/releases) version.
- Extract contents to the Lethal Company root game folder.
- Download the appropriate [BepInEx.MelonLoader.Loader](https://github.com/BepInEx/BepInEx.MelonLoader.Loader/releases) version for your chosen BepInEx version.
- Extract contents to the Lethal Company root game folder.
- Place **Lethal_Library.dll** and **Non-Lethal Dev Console.dll** into the <Game_Root>/MLLoader/Mods/ folder

<br>

## Utility Commands

<br>

> [!TIP]
> **Available console utility commands**

- Help
- Clear
- Exit

<br>

## Commands

<br>

> [!TIP]
> **Available console commands and usages**

### Set
> [!NOTE]
> Syntax: ``set <property> <value(s)>``
> 
> Example: ``set position 13.5 12 -14``

### Available Properties
- Health
- Speed
- Jump
- Climb_Speed
- Drunkess
- Is_Drunk
- Drunk_Interia
- Drunk_Recovery_Time
- Grab_Distance
- Exhaust
- Max_Insanity
- Min_Velocity_To_Take_Damage
- Level
- Position
- Hindered_Multiplier
- Hindered
- Group_Credits

<hr>

### Get
> [!NOTE]
> Syntax: ``get <property>``
> 
> Example: ``get position``

### Available Properties
- See available properties for set
- Is_Under_Water
- Is_Typing
- Is_Dead
- Is_Crouching
- Is_Voice_Muffled
- is_sliding
- is_sinking
- is_alone
- is_inside_factory
- is_inside_elevator
- is_inspecting_item
- is_climbing_ladder
- is_holding_item
- is_inside_ship
- is_two_handed

<hr>

### Action
> [!NOTE]
> Syntax: ``action <action> <property | optional> <property2 | optional>``
> 
> Example: ``action helmet on``

### Available Actions
- Helmet ``on | off``
- Remove_Helmet
- No_Clip ``on | off``
- Eject
- Damage ``Player Name`` ``Damage``
- Heal ``Player Name``
- Kill ``Player Name | all``
- Blood ``add | remove`` ``Player Name``

<hr>

### Teleport
> [!NOTE]
> Syntax: ``teleport <location> or teleport <player> or teleport <player> <player2>``
> 
> Example: ``teleport ship``

### Available Locations
- Ship

<hr>

#### Credits: [Contributors](https://github.com/Lillious/Lethal-Company-Developer-Console/graphs/contributors)
