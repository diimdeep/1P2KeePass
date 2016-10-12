# 1P2KeePass

Plugin for KeePass 2.x to import from 1Password Interchange Format (1pif).

This version created by exodusd works with more 1Password record types, including Secure Notes.

Tested with 1Password 4.6.0 and KeePass 2.34.

## Download & Install

Download the plugin binary (.plgx file) by downloading the 1P2KeePass.plgx file on this repository.

Install the plugin by copying the .plgx file you downloaded to your KeePass's plugins folder (e.g. C:\Program Files (x86)\KeePass Password Safe 2\Plugins).

Please note that **this does not work for records with custom fields**! It will return a 'value out of bounds' error.

Attachments and custom icons for each record will not be imported.

## Plugin Development and Build Instructions

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

### Todo

- [ ] Support more types of records
- [ ] Plugin Icon
- [ ] Update Checking
 
### Status

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
