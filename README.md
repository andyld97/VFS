# VFS
VFS (Virtual File System) is a file format which makes it possible to store any files and directories just into one file. See more in the [documentation](https://github.com/andy123456789088/VFS/blob/master/Documentation/Documentation%20VFS.pdf).

## Future updates
- [X] Implementing new version in `VFS Application` and in `Setup`
- [ ] Finish GUI (The GUI is not ready, but for now you can use it for simple testing and working)
- [ ] Implementing new version in `PHP` and `C++`
- [ ] Extend the `PHP` and `QT C++` code with more features
- [ ] Refresh the old format (working byte-wise with a buffer)

## Changelog

**Version 1.0.0.5 (06.02.2018)**
- Revised structure (VFS Library is now written in .NET Standard)
- VFS can be used now in .NET Framework and in Universal Windows App (UWP)

**Version 1.0.0.4 (09.07.2017)**
- Replaced threads and BackgroundWorkers with async and await-tasks!
- Revised structure (Usage of this code is now more comfortable then bevor)
- Progress is now also available in SplitVFS
- Cleaned README.md
- Now .NET Framework 4.6.2 is needed

**Version 1.0.0.3 (02.01.2017)**
- Added new file format
- New directory structure on GitHub
- Renamed file extension from ".ap" to ".vhp"
- Revised comments in VFS-Library

**Version 1.0.0.2 (03.08.2016 - 17.10.2016)**
- A lot of bugfixes

**Version 1.0.0.1 (26.03.2016)**
- Created a new application to manage files with a new graphical user-interface

.**Version 1.0.0.0 (07.01.2015)**
- First release

## Usage

I want to explain the usage with an example. Imagine you have this directory structure:
```
C:\Data\Documents\ToDo.docx
C:\Data\Documents\Meeting.docx
C:\Data\Images\Computer.png
C:\Data\Images\Notebook.png
C:\Data\Passwort.txt
```
In the following examples I am going to explain how you can create and import files, because
I think that these explanations are enough to go on and use further methods. 

If you want to read a local file/folder, you always need to create the instance of FilePath/DirectoryPath and pass the path
in the constructor!

**.NET Framework**
 
**SplitVFS**
```csharp
using VFS;
using VFS.Net;

...

// MainCounter: 128 (how much bytes, see description)
// PackByte:     45 (which byte, see description)

public async Task CreateVFS()
{
  FilePath targetFile = new FilePath("_PATH_OF_THE_FILE_YOU_WANT_TO_CREATE");
  
  SplitVFS currentVFS = new SplitVFS(targetFile, NetStorage.NETStorageProvider, 128, 45);
  await currentVFS.Create(new DirectoryPath(@"C:\Data")); // All files and directorys from C:\Data will be used
}
```

**ExtendedVFS: General usage**
```csharp

using VFS;
using VFS.Net;

...

public async Task CreateVFS()
{
  // Workspace will automtically created in Application.StartupPath now
  
  FilePath targetFile = new FilePath("_PATH_OF_THE_FILE_YOU_WANT_TO_CREATE");
  ExtendedVFST currentVFS = new ExtendedVFST(targetFile, NetStorage.NETStorageProvider, 32768); // 32768 is the default buffer-size
  await currentVFS.Create(new FilePath(@"C:\Data"));
  await currentVFS.Read(targetFile);
  
  Result<string> rs = await currentVFS.ReadAllText(...);
  if (rs.Success)
  {
      MessageBox.Show(rs.Value);
  }
}

```
**.NET UWP**

**SplitVFS**
```csharp
using VFS;
using VFS.Uwp;

...

// MainCounter: 128 (how much bytes, see description)
// PackByte:     45 (which byte, see description)

// ApplicationData.Current.LocalFolder is a StorageFolder in UWP-Applications
// Now you can pass e.g. downloaded files to targetFile
// YOUR_STORAGE_FILE is from Type Windows.Storage.StorageFile

public async Task CreateVFS()
{
  FilePath targetFile = new FilePath(YOUR_STORAGE_FILE);
  
  SplitVFS currentVFS = new SplitVFS(targetFile, UwpStorage.UWPStorageProvider, 128, 45);
  await currentVFS.Create(new DirectoryPath(ApplicationData.Current.LocalFolder)); // All files and directorys from C:\Data will be used
}
```

**ExtendedVFS: General usage**
```csharp

using VFS;
using VFS.Uwp;
using Windows.Storage;

...

public async Task CreateVFS()
{
  // Workspace will automtically created in Application.StartupPath now
  // ApplicationData.Current.LocalFolder is a StorageFolder in UWP-Applications
  // Now you can pass e.g. downloaded files to targetFile
  // YOUR_STORAGE_FILE is from Type Windows.Storage.StorageFile
  
  FilePath targetFile = new FilePath(YOUR_STORAGE_FILE);
  ExtendedVFST currentVFS = new ExtendedVFS(targetFile, UwpStroage.UWPStorageProvider, 32768); // 32768 is the default buffer-size
  await currentVFS.Create(ApplicationData.Current.LocalFolder);
  
  Result<string> rs = await currentVFS.ReadAllText(...);
  if (rs.Success)
  {
      MessageBox.Show(rs.Value);
  }
}
```

Now it's very easy to use this methods without any events or something, because await just waits for finishing.
You can just disable your current window at the beginning and re-enable it at the end of this method.
All methods returns an Result-instance, so you can see if this operation was successfully and you can also get the
value.


**SplitVFS and ExtendedVFS: Getting the elapsed time/Showing a progress dialog**

The class [Progress](https://github.com/andy123456789088/VFS/blob/master/VFS/VFS/Progress.cs) has a static event that gets called if a method of `SplitVFS` or `ExtendedVFS` is called. See this implementation and read the comments:

```csharp

private void loadWindow()
{
     VFS.Progress.OnValueChanged += Progress_OnValueChanged;
}

private void Progress_OnValueChanged(double value, double step, VFS.VFS handle)
{
    // Compare handle to your current handle
    // If handle != current_handle you can just return;
    
    // step defines the value of the current progress
    // value defines the value of the entire progress
    
    // if value and step are equal to 0, you can show your dialog
    // if value and step are equal to 1, you can close/hide your dialog
    // You have to pass the values to your dialog or you have to pass handle to your dialog and do this
    // implementation in your progress-dialog
    
    // Be careful this method is called from a different thread!
    // The elapsed time you can get from handle.VStopWatch or from your own reference to your currentVFS-instance!
}
```

I don't want to explain each method, because I've made XML-comments for each method and I think that these comments should
epxpain enough. The naming is similar (not equal) to the `System.IO`-Namespaces.
Finally, I recommend taking a look at this [class diagram](https://github.com/andy123456789088/VFS/blob/master/Documentation/Overview.png). Of course you can also use IntelliSense.
