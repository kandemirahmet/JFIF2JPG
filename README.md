# JFIF2JPG

A simple Windows Forms application for renaming `.jfif` image files to `.jpg`.

## About

JFIF2JPG is a lightweight Windows desktop utility that allows users to quickly rename JFIF file extensions to JPG.

The application does **not re-encode or modify the image data**. It simply changes the file extension from `.jfif` to `.jpg`.

## Features

- Rename `.jfif` files to `.jpg`
- Select multiple files at once
- Drag and drop files into the application
- Simple and easy-to-use Windows Forms interface
- Displays the number of successful and failed operations
- No installation required
- Standalone executable for Windows x64

## How to Use

1. Launch `JFIF2JPG.exe`.
2. Select one or more `.jfif` files using **Select Files**, or drag and drop them into the application.
3. The application renames the selected files from `.jfif` to `.jpg`.
4. The status area displays the result of the operation.

## Requirements

- Windows 10 or later
- x64 Windows system

The published version is self-contained and does not require a separate .NET installation.

## Technology

- C#
- .NET 8
- Windows Forms
- Visual Studio

## License

This project is provided for personal and educational use.

## Project Structure

```text
JFIF2JPG/
├── JFIF2JPG.csproj
├── JFIF2JPG.ico
├── JFIF2JPG.slnx
├── MainForm.cs
├── MainForm.Designer.cs
├── MainForm.resx
└── Program.cs
