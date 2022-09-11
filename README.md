# ScanOrganizer

![GitHub All Releases](https://img.shields.io/github/downloads/devinspitz/ScanOrganizer/total)
![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/devinspitz/ScanOrganizer?label=release)
![Platforms](https://img.shields.io/static/v1?label=platform:&message=windows10%20|%20linux(untested)&color=green)
[![CodeFactor](https://codefactor.io/repository/github/devinspitz/scanorganizer/badge)](https://www.codefactor.io/repository/github/devinspitz/scanorganizer)
[![CircleCI](https://circleci.com/gh/devinSpitz/PavlovRconWebserver/tree/master.svg?style=shield)](https://circleci.com/gh/devinSpitz/PavlovRconWebserver/tree/circleci-project-setup)
[![Discord](https://badgen.net/discord/members/G5VpbgdYey)](http://dc.spitzen.solutions)  


<img src="./Art/storage-20-512.png" width="200" />


Features:
=======
* Sort your scans (Images, Pdfs) into folders by SortTags
* Add Folders to the monitoring to automatically sort new scans
* Add PrimaryFolder or SecondaryFolder SortTags to define the folder structure



Installation:
=======
* Download the latest release
* I made a small how-to for making the ScanOrganizer a service so it does start automatically on restart etc.
  * [NSSM How to](https://github.com/devinSpitz/ScanOrganizer/wiki/NSSM---How-to)
* After the Scan Organizer should be ready to configure via your Browser: [http://localhost:5000/](http://localhost:5000/)
    * [Quick Start Guide](https://github.com/devinSpitz/ScanOrganizer/wiki/Quick-Start-Guide)

Tutorials:
=======
* [Quick Start Guide](https://github.com/devinSpitz/ScanOrganizer/wiki/Quick-Start-Guide)
* [NSSM How to](https://github.com/devinSpitz/ScanOrganizer/wiki/NSSM---How-to)
* [Naps How-to](https://github.com/devinSpitz/ScanOrganizer/wiki/NAPS---How-To)

Good to know:
=======
* Images are just internally converted to pdfs on a file per file base. Multiple images are not getting grouped into single files.

Tips:
=======
* Use this software with naps2 to also group your scans into one file when needed. [Naps2](https://github.com/cyanfish/naps2)  
   * I made a little how to [Naps How-to](https://github.com/devinSpitz/ScanOrganizer/wiki/NAPS---How-To)

Linux:
=======
* Would be cool if someone with linux and a scanner could test this software and give me some feedback.  
   * I think it should work but I don't have a linux machine with a scanner to test it.




Donate:
=======
Feel free to support my work by donating:  

<a href="https://www.paypal.com/donate?hosted_button_id=JYNFKYARZ7DT4">
<img src="https://www.paypalobjects.com/en_US/CH/i/btn/btn_donateCC_LG.gif" alt="Donate with PayPal" />
</a>

Business:
=======

For business inquiries please use:

<a href="mailto:&#x64;&#x65;&#x76;&#x69;&#x6e;&#x40;&#x73;&#x70;&#x69;&#x74;&#x7a;&#x65;&#x6e;&#x2e;&#x73;&#x6f;&#x6c;&#x75;&#x74;&#x69;&#x6f;&#x6e;&#x73;">&#x64;&#x65;&#x76;&#x69;&#x6e;&#x40;&#x73;&#x70;&#x69;&#x74;&#x7a;&#x65;&#x6e;&#x2e;&#x73;&#x6f;&#x6c;&#x75;&#x74;&#x69;&#x6f;&#x6e;&#x73;</a>


Credits:
=======

Boostrap:   
The most popular front-end framework for developing responsive, mobile first projects on the web.  
https://getbootstrap.com

Datatables:   
Add advanced interaction controls to your HTML tables the free & easy way  
https://github.com/DataTables/Dist-DataTables  

Hangfire  
An easy and reliable way to perform fire-and-forget, delayed and recurring, long-running, short-running,   
CPU or I/O intensive tasks inside ASP.NET applications. No Windows Service / Task Scheduler required. Even ASP.NET is not required. Backed by Redis, SQL Server, SQL Azure or MSMQ. This is a .NET alternative to Sidekiq, Resque and Celery.   
https://www.hangfire.io/   

Font Awesome  
Get vector icons and social logos on your website with Font Awesome, the web's most popular icon set and toolkit.   
https://fontawesome.com/  

Bootswatch  
Free themes for Bootstrap  
https://bootswatch.com/  

PdfiumViewer  
PDF viewer based on the PDFium project.  
https://github.com/pvginkel/PdfiumViewer  

TesseractSharp
.NET Wrapper for tessaract v5.0.0.20190623  
https://github.com/shift-technology/TesseractSharp

Newtonsoft.Json  
Json.NET is a popular high-performance JSON framework for .NET
https://github.com/JamesNK/Newtonsoft.Json

Thanks to all this people who worked for this nuget packages. Without that it wouldn't be possible to do this.  




<b>Powered by Spitz IT Solutions</b>  

For commercial licences you can find more information here: https://github.com/sponsors/devinSpitz  

<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License</a>.
