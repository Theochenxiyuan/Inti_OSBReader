# Inti_OSBReader - UX Fork

This is a personal fork of the [original Inti_OSBReader](https://github.com/ska242/Inti_OSBReader) — an experimental tool to read Inti Creates `.osb` files (sprites, frames, palettes, animations).

The fork focuses on improving the user experience for day-to-day use while keeping the original core logic intact. Some code and design changes were assisted by AI (opencode).

## What's New

### Layout & Navigation
- **Folder sidebar** — Open an entire folder and navigate files via a tree view (original only had single-file open)
- **Resizable panels** — Directory tree, animation list, and preview area use `SplitContainer` with drag handles instead of fixed coordinates
- **Status bar** — Shows current file name, animation count, frame count, and palette count
- **Window title** — Displays current file name (`filename.osb - Inti OSB Reader`)
- **Auto-load last folder** — Remembers and reopens the last used folder on startup

### File Browser
- **File name filter** — Exclude files by name pattern (e.g. `_pal,_ex_pal`), comma-separated, persisted in settings
- **Search box** — Show only matching files, auto-expands all subdirectories

### Convenience
- **Auto-play first animation** — Opening a file automatically selects and plays the first animation
- **Pause / Play** — Button and Spacebar to pause or resume the current animation
- **Recent Files & Recent Folders** — Quick access to previously opened items in the File menu
- **Drag-and-drop** — Drop `.osb` / `.scb` files onto the window to open them
- **Export GIF button** — Explicit "GIF" button instead of hidden click-on-preview behavior
- **Sprite Sheet button** — Open sprite sheet view from the toolbar
- **Sprite Sheet window** — Title shows filename, Save PNG is an explicit button
- **Tooltips** — On palette buttons, Flip, Pause/Play, GIF, and Sprite buttons
- **Disabled controls when no file loaded** — Prevents accidental clicks on save/view actions

### External Palette Management
- **Stack multiple palettes** — Each click of "Add" loads another external palette file and appends its palettes, just like the original program
- **Per-file external palette list** — Each OSB file remembers its own set of external palette files
- **Palette source label** — Shows the count and first palette name when multiple palettes are stacked (e.g. `Palettes (3): pl001.osb +2 more`)
- **Auto-reapply on file switch** — External palettes are automatically reapplied in order when switching between files
- **Remove last palette** — The `×` button removes the most recently added external palette; `File → Add color palette` appends another
- **Persists across restarts** — Palette bindings saved in settings

### Bug Fixes (original code)
- **"Save All Palettes" path** — Missing `Path.Combine` produced incorrect file paths
- **`SolidBrush` leak** — Brushes created in a loop were never disposed in `displayPallete`
- **Open file dialog filter** — Malformed filter string caused a broken filter entry
- **Tspeed save** — No longer writes to settings every 10ms; saves on focus leave with min value 1

### Typo Fixes
- Menu: `Documneted data` → `Documented data`
- Menu: `fremes related data` → `Frames related data`
- Label: `Pallete:` → `Palette:`
- Shortcut hint: `Ctrtl+S` → `Ctrl+S`
- Export filename: `_ColoerPalettes.txt` → `_ColorPalettes.txt`
- Shortcut hint: `Ctrl+Shit+Alt+S` → `Ctrl+Shift+Alt+S`

## Usage

The original decryption workflow still applies:

1. Download the tools and names file from [bnnm/vgm-tools](https://github.com/bnnm/vgm-tools/tree/master/misc/inti):
   - `intidec.py`
   - `inti-renamer.py`
   - `names/Dragon Marked for Death (PC).txt`
2. Rename `Dragon Marked for Death (PC).txt` to `names.txt`
3. Copy the `DataHash` folder (from the game) to the same directory as the scripts
4. Run in a command prompt:
   ```
   python inti-renamer.py -a "DataHash\**\*"
   python intidec.py "DataHash\**\*.osb" -k obj90210 -z
   ```
5. A folder named `_out` will appear. Open it with this program.

## Build

Local release build:

```powershell
.\publish.ps1
```

Or directly:

```
dotnet publish Inti_creates_files_Reader\Inti_creates_files_Reader.csproj -c Release -p:PublishProfile=Release
```

The self-contained executable will be at:
```
Inti_creates_files_Reader\bin\Release\net8.0-windows\win-x64\publish\Inti_creates_files_Reader.exe
```

`dotnet build` can still be used for development; it produces a small framework-dependent build in `Inti_creates_files_Reader\bin\Release\net8.0-windows`.
