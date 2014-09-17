1P2KeePass Plugin for KeePass to import from 1Password Interchange Format (1pif).

**PluginSrc** project is core of plugin, produces plugin .dll.  
**Loader** project is for debugging. It is coping plugin .dll to KeePassDistribution\ where KeePass.exe should be located.  
This project should be StartUp project to start KeePass and attach debugger.  
In this project Properties on tab Debug you will need to change program path and working directory path to absolute paths on you machine to 1P2KeePass\KeePassDistribution\ and 1P2KeePass\KeePassDistribution\KeePass.exe  

**TestData** folder contains .1pif and .kdbx (password: test) for testing.


#### Dependencies

- Newtonsoft.Json 


#### Status  

Tested with 1Password 4.1.2 and KeePass 2.25


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

#### TODO

- [ ] Support more types of records
- [ ] Plugin Icon
- [ ] Update Checking
 


[KeePass Plugin Development](http://keepass.info/help/v2_dev/plg_index.html)
