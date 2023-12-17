### 3.1.41
- Fixed an issue where Risk of Rain 2 could not launch when selecting the EGS platform

### 3.1.40
- Added Wrestling Empire support
- GodotML setup script is now called for Dome Keeper

### 3.1.39
- Added support for more games:
  - RUMBLE
  - Dome Keeper
  - Skul: The Hero Slayer
  - Sons Of The Forest
  - The Ouroboros King
- Bug fixed where game launch modal didn't always appear if clicked from certain screens
- _Note: Credits for all changes in this release go the Thunderstore team_


### 3.1.38

- Outward Definitive Edition now launches correctly
    - The bug previously caused Steam to prompt about launch arguments and would discard them even if the user accepted
      them.

### 3.1.37

- Proton detection should work correctly on Linux platforms once again
    - Reasonable fallbacks should be provided if the manager fails to keep depots updated
- Added games:
    - Brotato
    - Ancient Dungeon VR
    - Atrio: The Dark Wild
    - Ultimate Chicken Horse
- BONELAB Oculus executable is now selectable
- Online searching no longer requires underscores in the name (Thanks to MSchmoecker)

### 3.1.36

- Temporary workaround to force Proton on Linux systems
    - Place a `.forceproton` file in the game directory whilst a solution is in development
- The blank screen on game selection has another potential fix
- BONELAB can now be used on copies from the Oculus Store
- ULTRAKILL support added
- `dotnet` files will no longer appear in the config editor for BepInEx 6 BE builds.

### 3.1.35

- Improved online search performance
- Config names wrap if too long
- Deprecated mods are hidden by default in the online section
    - Deprecated mods can be made visible in the filters modal within the online section
- Potential fix for blank game selection screen
- Added support for .npc files in Hard Bullet
- Internal refactoring

### 3.1.34

- Profile exports changed to use Thunderstore provided host
- Significantly improved search performance in the online tab
- Game selection server tab modified to reduce user mis-clicks

### 3.1.33

- Added games:
    - Ravenfield
    - Aloft
    - Cult of the Lamb
    - Chrono Ark (64 bit)
    - BONELAB
    - Trombone Champ
    - Rogue : Genesia
    - Across the Obelisk
- Xbox Game Pass support
    - Thanks to starfi5h
- Other improvements thanks to the Thunderstore team:
    - Game can now be changed without an app restart
    - Internal refactors and improvements
