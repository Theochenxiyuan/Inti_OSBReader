# Build a self-contained single-file release executable
# Output: Inti_creates_files_Reader\bin\Release\net8.0-windows\win-x64\publish\Inti_creates_files_Reader.exe

param(
    [switch]$Clean
)

$ErrorActionPreference = "Stop"
$project = "Inti_creates_files_Reader\Inti_creates_files_Reader.csproj"

if ($Clean) {
    dotnet clean $project -c Release -v q
    Remove-Item -Recurse -Force -ErrorAction SilentlyContinue "Inti_creates_files_Reader\bin"
    Remove-Item -Recurse -Force -ErrorAction SilentlyContinue "Inti_creates_files_Reader\obj"
}

dotnet publish $project -c Release -p:PublishProfile=Release -v q

Write-Host "Done. Output:"
Write-Host "  Inti_creates_files_Reader\bin\Release\net8.0-windows\win-x64\publish\Inti_creates_files_Reader.exe"
