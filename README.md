# ASpaceGame

![Work in Progress](https://img.shields.io/badge/status-WIP-orange)<br>[![semantic-release: angular](https://img.shields.io/badge/semantic--release-angular-e10079?logo=semantic-release)](https://github.com/semantic-release/semantic-release)

## Introduction

Hello and welcome to this Github repository. Let's start by telling you that I am a big Star Trek fan and around the end of the 90s I remember playing a game called **Star Trek Starship Creator Warp II**.

This game allowed you to create your own starship, with a variety of parts and components, and then take her on missions, either provided by the game or by you, thanks to the included mission editor.

This project of mine, that I called **ASpaceGame** (**A** due to the fact that I'd like branding everything with my own personal website, [arduinilive.com](https://www.arduinilive.com), and **SpaceGame** because I'm not very creative with names :)), is a freely inspired creation based on that very old and terrific game.

I want to try to create something of mine which is inspired by that old software, but which can also be considered mine. This is **NOT** an attempt to infringe any Copyright or Intellectual Property, but just a fan-made project; for this reason, I'm not going to use any reference to Star Trek or any other copyrighted material, but I will try to come up with my own names, ideas and concepts.

## Code Architecture

The code is written mainly in C# and it's structured according to the following points:

- **ASpaceGame.CoreComponents**: this is a DLL class library that contains the core components of the game, such as classes, enums and utilities related to the inner core
- **ASpaceGame.Tests**: this is a DLL class library that contains the unit tests for the core components

## Contributing

If you wish to contribute to this project, please refer to the [CONTRIBUTING.md](CONTRIBUTING.md) file for more information.

Be aware that I'm not dedicated 100% of my time to this project, I'm doing this in my spare time, so please be patient if I don't respond immediately or at all; I'll certainly try to do my best.

## List of all default branches

### main
The _main_ branch is the **production** repository; on this branch only _pull requests_ from the _develop_ branch are accepted. Everytime a push is made on this branch, the associated action activates to perform the following operations:

1. Latest version checkout (from _main_)
2. Code building
3. Unit tests execution
4. Tag creation
5. New release with changelog

### develop
The _develop_ branch is a container for all staging activities. Each developer creates a specific issue branch from the _develop_ one and once they have finished their work, they merge everything into this branch. This is basically a container of all **unreleased content**.

### Code of Conduct

Please be respectful and considerate when interacting with others.

## Commits Rules

### <a name="commit"></a> Commit Message Format

*This specification is inspired by and supersedes the [AngularJS commit message format][commit-message-format].*

We have very precise rules over how our Git commit messages must be formatted.
This format leads to **easier to read commit history**.

Each commit message consists of a **header**, a **body**, and a **footer**.


```
<header>
<BLANK LINE>
<body>
<BLANK LINE>
<footer>
```

The `header` is mandatory and must conform to the [Commit Message Header](#commit-header) format.

The `body` is mandatory for all commits except for those of type "docs".
When the body is present it must be at least 20 characters long and must conform to the [Commit Message Body](#commit-body) format.

The `footer` is optional. The [Commit Message Footer](#commit-footer) format describes what the footer is used for and the structure it must have.


#### <a name="commit-header"></a>Commit Message Header

```
<type>(<scope>): <short summary>
  │       │             │
  │       │             └─⫸ Summary in present tense. Not capitalized. No period at the end.
  │       │
  │       └─⫸ Commit Scope: animations|bazel|benchpress|common|compiler|compiler-cli|core|
  │                          elements|forms|http|language-service|localize|platform-browser|
  │                          platform-browser-dynamic|platform-server|router|service-worker|
  │                          upgrade|zone.js|packaging|changelog|docs-infra|migrations|
  │                          devtools
  │
  └─⫸ Commit Type: build|ci|docs|feat|fix|perf|refactor|test
```

The `<type>` and `<summary>` fields are mandatory, the `(<scope>)` field is optional.


##### Type

Must be one of the following:

* **build**: Changes that affect the build system or external dependencies (example scopes: gulp, broccoli, npm)
* **ci**: Changes to our CI configuration files and scripts (examples: CircleCi, SauceLabs)
* **docs**: Documentation only changes
* **feat**: A new feature
* **fix**: A bug fix
* **perf**: A code change that improves performance
* **refactor**: A code change that neither fixes a bug nor adds a feature
* **test**: Adding missing tests or correcting existing tests


##### Scope
The scope should be the name of the npm package affected (as perceived by the person reading the changelog generated from commit messages).

The following is the list of supported scopes:

* `animations`
* `bazel`
* `benchpress`
* `common`
* `compiler`
* `compiler-cli`
* `core`
* `elements`
* `forms`
* `http`
* `language-service`
* `localize`
* `platform-browser`
* `platform-browser-dynamic`
* `platform-server`
* `router`
* `service-worker`
* `upgrade`
* `zone.js`

There are currently a few exceptions to the "use package name" rule:

* `packaging`: used for changes that change the npm package layout in all of our packages, e.g. public path changes, package.json changes done to all packages, d.ts file/format changes, changes to bundles, etc.

* `changelog`: used for updating the release notes in CHANGELOG.md

* `dev-infra`: used for dev-infra related changes within the directories /scripts and /tools

* `docs-infra`: used for docs-app (angular.dev) related changes within the /adev directory of the repo

* `migrations`: used for changes to the `ng update` migrations.

* `devtools`: used for changes in the [browser extension](./devtools/README.md).

* none/empty string: useful for `test` and `refactor` changes that are done across all packages (e.g. `test: add missing unit tests`) and for docs changes that are not related to a specific package (e.g. `docs: fix typo in tutorial`).


##### Summary

Use the summary field to provide a succinct description of the change:

* use the imperative, present tense: "change" not "changed" nor "changes"
* don't capitalize the first letter
* no dot (.) at the end


#### <a name="commit-body"></a>Commit Message Body

Just as in the summary, use the imperative, present tense: "fix" not "fixed" nor "fixes".

Explain the motivation for the change in the commit message body. This commit message should explain _why_ you are making the change.
You can include a comparison of the previous behavior with the new behavior in order to illustrate the impact of the change.


#### <a name="commit-footer"></a>Commit Message Footer

The footer can contain information about breaking changes and deprecations and is also the place to reference GitHub issues, Jira tickets, and other PRs that this commit closes or is related to.
For example:

```
BREAKING CHANGE: <breaking change summary>
<BLANK LINE>
<breaking change description + migration instructions>
<BLANK LINE>
<BLANK LINE>
Fixes #<issue number>
```

or

```
DEPRECATED: <what is deprecated>
<BLANK LINE>
<deprecation description + recommended update path>
<BLANK LINE>
<BLANK LINE>
Closes #<pr number>
```

Breaking Change section should start with the phrase "BREAKING CHANGE: " followed by a summary of the breaking change, a blank line, and a detailed description of the breaking change that also includes migration instructions.

Similarly, a Deprecation section should start with "DEPRECATED: " followed by a short description of what is deprecated, a blank line, and a detailed description of the deprecation that also mentions the recommended update path.


### Revert commits

If the commit reverts a previous commit, it should begin with `revert: `, followed by the header of the reverted commit.

The content of the commit message body should contain:

- information about the SHA of the commit being reverted in the following format: `This reverts commit <SHA>`,
- a clear description of the reason for reverting the commit message.

## License

We all know how much Star Trek stuff and words are common in many things of our day-to-day language, however I made the best effort to avoid using terminology that could be associated with Star Trek, when possible of course.

Regardless, please do let me know if something is out-of-place or mis-used and I'll do my best to remove it. Just remember that this is a fan-made project by a software developer enthusiast.

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
