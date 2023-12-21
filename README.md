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

<br>

> [!WARNING]
> **Not supported at this time**

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

### Available Properties without Player
> [!NOTE]
> Syntax: ``set <property> <value>``
> 
> Example: ``set group_credits 1000``
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

### Available Properties without Player
> [!NOTE]
> Syntax: ``get <property> <value>``
> 
> Example: ``get group_credits 1000``
- See available set commands without player

<hr>

### Action
> [!NOTE]
> Syntax: ``action | optional> <action> <property | optional>``
> 
> Example: ``action remove_helmet`` or ``action add_blood``

### Available Actions
- Add_Helmet
- Remove_Helmet
- Add_Blood
- Remove_Blood
- No_Clip ``on | off``

<hr>

### Teleport
> [!NOTE]
> Syntax: ``teleport <location>``
> 
> Example: ``teleport ship``

### Available Locations
- Ship

<hr>

#### Credits: [Contributors](https://github.com/Lillious/Lethal-Company-Developer-Console/graphs/contributors)
