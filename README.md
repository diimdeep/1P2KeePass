Plugin for KeePass 2.x to import from 1Password Interchange Format (1pif).

This version created by exodusd works with more 1Password record types, including Secure Notes.

Tested with 1Password 4.6.0 and KeePass 2.34.

### Download & Install

Download the plugin binary (.plgx file) by downloading the 1P2KeePass.plgx file on this repository.

Install the plugin by copying the .plgx file you downloaded to your KeePass's plugins folder (e.g. C:\Program Files (x86)\KeePass Password Safe 2\Plugins).

### Structure

**1P2KeePass\** project is plugin source code and dependenices. 
**PackagePLGX\** project is for compiling plugin to .plgx for distribution.
  
To debug your plugin, use Debug Configuration. The "PluginSrc" project will build, and copy the required files to \KeePassDistribution folder.
Hit Start Debugging (F5) to start \KeePassDistribution\KeePass.exe with the attached debugger.

In the project's Properties -> Debug menu, you will need to change the 'Start external program' path and the 'Working directory' path to absolute paths on your machine, point them to \KeePassDistribution\KeePass.exe and \KeePassDistribution

To package plugin for distribution/release, switch to Release Configuration and select Rebuild Solution, or manually rebuild the PackagePLGX project by opening the "Post-build Event" menu.
You will find 1P2KeePass.plgx in the root directory.

The **TestData** folder contains .1pif and .kdbx files for testing the plugin. **The password is test**
The **KeePassDistribution** contains the KeePass Portable executable.

Read [KeePass Plugin Development](http://keepass.info/help/v2_dev/plg_index.html) for more info on how to develop a KeePass plugin.

### Dependencies

- Newtonsoft.Json 
- Visual Studio 2013 (IDE)

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
| securenotes.SecureNote           | Secure Note     | Done    | Done      |
| wallet.computer.License          | Sofware license | Partial |           |
| wallet.financial.CreditCard      | CreditCard      | Partial |           |
| wallet.government.DriversLicense |                 |         |           |
| wallet.membership.RewardProgram  |                 |         |           |

|  Records  |    What is it ?   | Parsing | Importing |
| --------- | ----------------- | ------- | --------- |
| Trashed   | Records in trash  | Done    | Done      |
| Favorited | Favorited records |         |           |
