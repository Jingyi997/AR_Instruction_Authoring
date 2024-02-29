# AR INSTRUCTION AUTHORING

This repo contains two Mixed-Reality assembly instruction authoring applications working on HoloLens2 with Unity UWP.

The two applications were employed in a user study presented in "On the Benefits of Image-Schematic Metaphors when Designing Mixed Reality Systems" at CHI2024.

# OPENING AND USING THE PROJECT

To get started with the "Baseline"/"ISM-Enhanced" Unity project, follow these simple steps:

1. Unzip "Baseline.zip"/"ISM-Enhanced.zip" into your chosen directory on your computer.

2. Open Unity Hub on your device. If Unity Hub is not yet installed, it can be downloaded from the Unity website.

3. Within Unity Hub, press the 'Open' button and navigate to the directory where you unzipped "Baseline.zip"/"ISM-Enhanced.zip". Select the directory to add it to your Projects list in Unity Hub.

4. This project was developed and saved using Unity Editor version 2021.3.15f1. To avoid compatibility issues, please ensure you have this specific version installed before proceeding. Find "Baseline"/"ISM-Enhanced" under the Projects tab in Unity Hub, and click it to open the project in the Unity Editor. If you do not have version 2021.3.15f1 installed, please visit the Unity download archive page (https://unity.com/releases/editor/archive) where you have the options to download and install all previous versions of the Unity Editor.

5. Once the project is open in the Unity Editor, go to the 'Assets' folder, select the main scene named "MainScene" and hold and drag "MainScene" to the hierarchy. If Unity auto-generated any new empty scene in the hierarchy, please right-click and remove the empty scene.

6. When using the "Baseline" project, an extra step is required to enable hand-dwell button selection. Firstly, select any button under "MixedRealitySceneContent" in the hierarchy, then go to the "DwellHandler" section in the "Inspector", and choose "Hand" for "Dwell Trigger Pointer Type". You can also adjust parameters like "Dwell Start Delay" in the same section.
