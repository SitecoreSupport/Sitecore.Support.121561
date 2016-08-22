# Sitecore.Support.121561
Implement Debug mode to MongoDbProcessingPool that writes StackTrace and UtcNow to all records in ProcessingPool collection

## Description
If `debug` mode is enabled by `<Debug>true</Debug>` in `App_Config/Include/zzz/Sitecore.Support.121561.config` file, `StackTrace` property will be added to all new records in `ProcessingPool` collection in `tracking.contact` MongoDB database.

## License  
This patch is licensed under the [Sitecore Corporation A/S License for GitHub](https://github.com/sitecoresupport/Sitecore.Support.121561/blob/master/LICENSE).  

## Download  
Downloads are available via [GitHub Releases](https://github.com/sitecoresupport/Sitecore.Support.121561/releases).  
