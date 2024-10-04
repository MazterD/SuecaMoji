# SuecaMoji

This repository is being used to host the code and assets for the second experiment in the Master of Science thesis titled "SuperFacial: Enhancing Facial Expressions Using Avatar Distortions to Improve Collaboration in Virtual Environments", written by Afonso Dias and supervised by Prof. Joaquim Jorge, both affiliated with Instituto Superior Técnico. The objective of this program is to verify if the use of exaggerated facial expressions in a collaborative virtual environment has any advantages over the use of more natural facial expressions.

Link to the GitHub repository hosting the first experiment's prototype: https://github.com/MazterD/SuperFacial

### Introduction

This project consists of a VR multiplayer game where the users play "Sueca", a traditional portuguese card game, but with a twist: players are able to use a code language based on facial expressions to communicate with their teammates. These facial expressions can either be more realistic or exaggerated versions of six emotions: happiness, sadness, anger, disgust, fear and surprise.

Multiplayer features are done using PUN as a cloud service. We also have a master client which will be the one that will actually play the game and contact the PUN cloud directy. Each player will give orders to the master client, which will in turn make changes to the game based on those orders and propagate the results to every player.

### Requirements

Below we present the tools and software that we have used when developing this project. Please do keep in mind that if you use an alternative setup the project's integrity may become compromised, leading to abnormal behaviour.

- Four Meta Quest 2 HMDs with two Quest 2 controllers each
- Unity version 2022.3.14f1
- Unity modules:
    - Android Build Support
    - OpenJDK
    - Android SDK and NDK Tools
- Unity packages:
    - Photon PUN 2
    - Meta XR All-in-One SDK
    - XR Plug-in Management
    - Oculus XR Plugin
    - TextMeshPro


### Setting up the Unity Packages

Meta XR All-in-One SDK is available in the Unity Asset Store (link: https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657). To install this package you must login into the store with your Unity user and add the package to your asset library. From there you can find the package in your Package Manager. After the installation, you can open the Project Setup Tool (in the Unity Editor: Meta > Tools > Project Setup Tool) and apply all recommended changes for PC and Android.

To install the Oculus XR Plugin follow the instructions on this link: https://developers.meta.com/horizon/documentation/unity/unity-project-setup#install-the-oculus-xr-plugin. This tutorial will also go over the installation of the XR Plug-in Management.

PUN requires you to have an account in order to get access to their cloud. Instructions on how to properly setup this plugin are available on this link: https://doc.photonengine.com/pun/current/getting-started/initial-setup
 

### How to run this project

1 - Download and unzip the GitHub project
2 - Open a new Unity project using the stock 3D template
3 - Install the necessary packages (mentioned in the previous section) using the Package Manager/Unity Asset Store
4 - Drag and drop the unzipped GitHub project files into the Unity project's "Assets" folder
5 - Build the project while selecting Android as a platform. You can either build directily into the HMDs or build the .apk first and then import it into the headsets
6 - Click the "Play" button on Unity with the Scene open. This will launch the master client
7 - Lauch the application on each of the HMDs, one at a time
8 - On the master client click on one of the three buttons that show up on the middle of the screen (titled "Game 1", "Game 2" and "Game 3") to start the game

### Game Scenarios

Game 1:
- Player 1 has no access to facial expressions
- Player 2 has no access to facial expressions
- Player 3 has no access to facial expressions
- Player 4 has no access to facial expressions

Game 2:
- Player 1 has no access to facial expressions
- Player 2 has access to natural facial expressions
- Player 3 has no access to facial expressions
- Player 4 has access to natural facial expressions

Game 3:
- Player 1 has access to exaggerated facial expressions
- Player 2 has no access to facial expressions
- Player 3 has access to exaggerated facial expressions
- Player 4 has no access to facial expressions


### Controls

Left Controller Hand Trigger - Code Language Panel
Right Controller Hand Trigger - Facial Expression Selection Menu
Right Controller Index Trigger - Select Card/Expression
