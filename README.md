# 1P2KeePass

Plugin for KeePass 2.x to import from 1Password Interchange Format (1pif).


## Download & Install

Download archive from [releases](https://github.com/diimdeep/1P2KeePass/releases) page and unzip .plgx file in KeePass plugins folder (e.g. C:\Program Files (x86)\KeePass Password Safe 2\Plugins).

Please note that **this does not work for records with custom fields**! It will return a 'value out of bounds' error.

Attachments and custom icons for each record will not be imported.


## Plugin Development

### Dependencies

- .Net 2.0
- Newtonsoft.Json

### Windows Instructions

`1P2KeePass\` Visual Studio project is plugin source code and dependenices. 
`PackagePLGX\` Visual Studio project is for compiling plugin to .plgx for distribution as an alternative you can run `build_plgx.run`.


To debug your plugin, use Debug Configuration. The "PluginSrc" project will build, and copy the required files to \KeePassDistribution folder.
Hit Start Debugging (F5) to start \KeePassDistribution\KeePass.exe with the attached debugger.

In the project's Properties -> Debug menu, you will need to change the 'Start external program' path and the 'Working directory' path to absolute paths on your machine, point them to \KeePassDistribution\KeePass.exe and \KeePassDistribution

To package plugin for distribution/release, switch to Release Configuration and select Rebuild Solution, or manually rebuild the PackagePLGX project by opening the "Post-build Event" menu.
You will find 1P2KeePass.plgx in the root directory.

The **TestData** folder contains .1pif and .kdbx files for testing the plugin. **The password is test**
The **KeePassDistribution** contains the KeePass Portable executable.

Read [KeePass Plugin Development](http://keepass.info/help/v2_dev/plg_index.html) for more info on how to develop a KeePass plugin.


### macOS Instructions

You can run KeePass and develop and build plugin using [Mono 5](http://www.mono-project.com/docs/about-mono/releases/5.0.0/)

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

### Status

Tested with 1Password 4.6.0 and KeePass 2.35

Please note that **this does not work for records with custom fields**! It will return a 'value out of bounds' error.

Attachments and custom icons for each record will not be imported.

|           Record type            |   1Password Description   | Parse   |  Import   |
| -------------------------------- | ------------------------- | ------- | --------- |
| system.folder.Regular            | Folder                    | Done    | Done      |
| webforms.WebForm                 | Bookmark                  | Done    | Done      |
| securenotes.SecureNote           | Secure Note               | Done    | Done      |
| wallet.computer.License          | Software License          | Partial | Partial   |
| wallet.financial.CreditCard      | Credit Card               | Partial | Partial   |
| wallet.government.DriversLicense | Driver's License          | Partial | Partial   |
| passwords.Password               | Generated Password        | No      | No        |
| wallet.membership.RewardProgram  | Rewards Program           | No      | No        |
| identities.Identity              | Identity                  | No      | No        |

|  Records   |    1Password Description              | Parse           | Import           |
| ---------- | ------------------------------------- | -------------   | ---------------- |
| Trashed    | Records in 1Password's trash          | Done            | Done             |
| Favorited  | Records in 1Password's favorites menu |                 |                  |
|Attachments | Files that are attached to a 1P record| No (Diff. File) | No (Diff. File)  |
|Custom Field|Fields added to 1P records manually    | No (Error)      | No (Error)       |


#### Import test 1Password 6.7.1

Result 39/45  

- Records not imported
    + Driver's License (Driver Licenses)
    + Hilton HHonors (Reward programs)
    + Business Identity (Identities)
    + Forums (Identities)
    + Garage Door Code (Passwords)
    + Personal Identity (Identities)
- Fields not imported
    + WebMD (Logins)
        * Field: website 2
        * Fields: web form details
        * Fields: previously used passwords