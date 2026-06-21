# Inti_OSBReader - UX Fork

This is a personal fork of the [original Inti_OSBReader](https://github.com/ska242/Inti_OSBReader) — an experimental tool to read Inti Creates `.osb` files (sprites, frames, palettes, animations).

The fork focuses on improving the user experience for day-to-day use while keeping the original core logic intact. Some code and design changes were assisted by AI (opencode).

## What's New

### Layout & Navigation
- **Folder sidebar** — Open an entire folder and navigate files via a tree view (original only had single-file open)
- **Resizable panels** — Directory tree, animation list, and preview area use `SplitContainer` with drag handles instead of fixed coordinates
- **Status bar** — Shows current file name, animation count, frame count, and palette count
- **Window title** — Displays current file name (`filename.osb - Inti OSB Reader`)

### Convenience
- **Auto-play first animation** — Opening a file automatically selects and plays the first animation
- **Pause / Play** — Button and Spacebar to pause or resume the current animation
- **Recent Files & Recent Folders** — Quick access to previously opened items in the File menu
- **Drag-and-drop** — Drop `.osb` / `.scb` files onto the window to open them
- **Export GIF button** — Explicit "GIF" button instead of hidden click-on-preview behavior
- **Tooltips** — On palette buttons, Flip, Pause/Play, and GIF export
- **Disabled controls when no file loaded** — Prevents accidental clicks on save/view actions

### Bug Fixes (original code)
- **"Save All Palettes" path** — Missing `Path.Combine` produced incorrect file paths
- **`SolidBrush` leak** — Brushes created in a loop were never disposed in `displayPallete`

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

```
dotnet publish Inti_creates_files_Reader\Inti_creates_files_Reader.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
```

The executable will be at:
```
Inti_creates_files_Reader\bin\Release\net8.0-windows\win-x64\publish\Inti_creates_files_Reader.exe
```
