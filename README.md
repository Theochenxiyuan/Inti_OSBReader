# Inti_OSBReader - UX Fork

This is a personal fork of the [original Inti_OSBReader](https://github.com/ska242/Inti_OSBReader) — an experimental tool to read Inti Creates `.osb` files (sprites, frames, palettes, animations).

The fork focuses on improving the user experience for day-to-day use while keeping the original core logic intact. Some code and design changes were assisted by AI (opencode).

## Key Differences from the Original

- **Folder sidebar** — Open an entire folder and navigate files via a sidebar tree view
- **Splitter layout** — Directory tree, animation list, and preview panel are resizable via drag handles (no fixed coordinates)
- **Auto-load first animation** — Open a file and it immediately starts playing the first animation
- **Palette refresh** — Switching palettes updates the preview instantly
- **Flip refresh** — Flip applies immediately to the current frame
- **Status bar** — Displays file name, animation count, frame count, and palette count
- **Controls disabled until file loaded** — Prevents accidental clicks on save/view actions when no file is open
- **Window title** — Shows current file name (`filename.osb - Inti OSB Reader`)
- **Recent Files & Recent Folders** — Quick access to previously opened files and folders
- **Fixed typos** — Corrected misspellings in menus and exported file names
- **Bug fixes** — Missing `Path.Combine` in "Save All Palettes" (produced wrong file paths), `SolidBrush` memory leaks in palette rendering
- **Collapsible bottom palette bar** via `TableLayoutPanel`

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
