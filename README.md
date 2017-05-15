Plugin for KeePass 2.x to import from 1Password Interchange Format (1pif).

Tested with 1Password 4.1.2 and KeePass 2.27.

Download binary [here](https://github.com/diimdeep/1P2KeePass/releases)

## Dev

### Structure

**1P2KeePass\** project is plugin source code and dependenices. 
**PackagePLGX\** project is for compiling plugin to .plgx for distribution.
  
To debug plugin use Debug configuration. Only PluginSrc project will build, and copy necessary artifacts to \KeePassDistribution folder.
Hit Start Debugging(F5) to start \KeePassDistribution\KeePass.exe with attached debugger.
In project Properties -> Debug you will need to change 'Start external program' path and 'Working directory' path to absolute paths on you machine to \KeePassDistribution\KeePass.exe and \KeePassDistribution

To package plugin for distribution switch to Release configuration and rebuild solution or manually rebuild PackagePLGX project. (look in Post-build event)
You will find 1P2KeePass.plgx in root directory.

**TestData** folder contains .1pif and .kdbx (password: test) for testing.
**KeePassDistribution** is where KeePass portable lives.

Read [KeePass Plugin Development](http://keepass.info/help/v2_dev/plg_index.html) for more info.

### Dependencies

- .Net 2.0
- Newtonsoft.Json 

### macOS

Build and run using [Mono 5](http://www.mono-project.com/docs/about-mono/releases/5.0.0/)

Install Mono SDK `brew cask install mono-mdk` 

```bash
$ make <command>
		update_deps # download libs
		update_keepass # download keepass
		build # build plugin as .dll
		release # compile plugin as .plgx
		distrib # zip .plgx
		run_debug # run keepass with .plgx
		run_release # run keepass with .dll
```

### TODO

- [ ] Support more types of records
- [ ] Plugin Icon
- [ ] Update Checking
 
### Status  


|           Record type            |   What is it ?  | Parsing | Importing |
| -------------------------------- | --------------- | ------- | --------- |
| system.folder.Regular            | Folder          | Done    | Done      |
| webforms.WebForm                 | Bookmark        | Done    | Done      |
| identities.Identity              | Contact         | Partial |           |
| securenotes.SecureNote           |                 | Partial |           |
| wallet.computer.License          | Sofware license | Partial |           |
| wallet.financial.CreditCard      | CreditCard      | Partial |           |
| wallet.government.DriversLicense |                 |         |           |
| wallet.membership.RewardProgram  |                 |         |           |

|  Records  |    What is it ?   | Parsing | Importing |
| --------- | ----------------- | ------- | --------- |
| Trashed   | Records in trash  | Done    | Done      |
| Favorited | Favorited records |         |           |