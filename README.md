# ASpaceGame

![GitHub Release Date](https://img.shields.io/github/release-date/KingMordas/ASpaceGame)
![GitHub Release](https://img.shields.io/github/v/release/KingMordas/ASpaceGame)
![GitHub License](https://img.shields.io/github/license/KingMordas/ASpaceGame)
<br>
![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/KingMordas/ASpaceGame/total)
![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/KingMordas/ASpaceGame/latest/total)
![GitHub forks](https://img.shields.io/github/forks/KingMordas/ASpaceGame?style=flat)
![GitHub watchers](https://img.shields.io/github/watchers/KingMordas/ASpaceGame?style=flat)
<br>
[![semantic-release: angular](https://img.shields.io/badge/semantic--release-angular-e10079?logo=semantic-release)](https://github.com/semantic-release/semantic-release)
![GitHub branch check runs](https://img.shields.io/github/check-runs/KingMordas/ASpaceGame/main)

## Introduction

Hello and welcome to this Github repository. Let's start by telling you that I am a big Star Trek fan and around the end of the 90s I remember playing a game called **Star Trek Starship Creator Warp II**.

This game allowed you to create your own starship, with a variety of systems and facilities, and then take her on missions, either provided by the game or by you, thanks to the included mission editor.

This project of mine, that I called **ASpaceGame** (**A** due to the fact that I'd like branding everything with my own personal website, [arduinilive.com](https://www.arduinilive.com) - the A it's from there, and **SpaceGame** because I'm not very creative with names :)), is a freely inspired creation based on that very old and (from my pov) terrific game.

I want to create something of mine which is **inspired** by that old software, but with my own touch (and that of the community, should they desire to participate).

> This is **NOT** an attempt to infringe any Copyright or Intellectual Property, but just a fan-made project based on a 20+ years software.
>
> I'm not going to use any references to Star Trek or any other copyrighted material, coming up with my own names, ideas and concepts.
>
> Please, be aware that most of the terminology used in Star Trek has become very common in sci-fi productions (i.e. warp drive, shields, etc.); I'll do my best to try to avoid them when possible. Should you find something that is not appropriate, please let me know and I'll do my best to remove it.

## How to

### Core Game Mechanics

Several game mechanics are required for the software to properly work as intended.

In order to have these resources properly created and available at runtime, you need to execute the `ASpaceGame.CoreComponents.ASpaceGameStarter.Start()` from your developed UI; the method represents an idempotent series of instructions finalized to create the above mentioned game mechanics.

> **Note**: The `ASpaceGame.CoreComponents.Officer` class is a core mechanics that requires the user to manually create some officers to be later assigned to the various starships; it is actually not covered by the initialization process.

## Contributing

If you wish to contribute to this project, please refer to the [CONTRIBUTING.md](CONTRIBUTING.md) file for more information.

Be aware that I'm not dedicated 100% of my time to this project, I'm doing this in my spare time, so please be patient if I don't respond immediately or at all; I'll certainly try to do my best.

### Code of Conduct

Please be respectful and considerate when interacting with others.

## License

We all know how much Star Trek stuff and words are common in many things of our day-to-day language, however I made the best effort to avoid using terminology that could be associated with Star Trek, when possible of course.

Regardless, please do let me know if something is out-of-place or mis-used and I'll do my best to remove it. Just remember that this is a fan-made project by a software developer enthusiast.

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
