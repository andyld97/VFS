# VFS
VFS (Virtual File System) is a file format which makes it possible to store any files and directories just into one file. See more in the [documentation](https://github.com/andy123456789088/VFS/blob/master/Documentation/Documentation%20VFS.pdf).

## Future updates
- [ ] Implementing new version in **VFS Application** and in **Setup**
- [ ] Implementing new version in **PHP** and **C++**
- [ ] Extend the PHP and QT C++ code with more features
- [ ] Refresh the old format (working byte-wise with a buffer,
                              integrating a possiblity to get the current progress,
                              adding support to create a file from a directory(-array))

## Changelog

**Version 1.0.0.3**
- Added new file format
- New directory structure on GitHub
- Renamed file extension from ".ap" to ".vhp"
- Revising comments in VFS-Library

**Version 1.0.0.2**
- A lot bugfixes

**Version 1.0.0.1**
- Created a new application to manage files with a new graphical user-interface

**Version 1.0.0.0**
- First release

## Usage

I want to explain the usage with an example. Represent you have this directory structure:
```
C:\Data\Documents\ToDo.docx
C:\Data\Documents\Meeting.docx
C:\Data\Images\Computer.png
C:\Data\Images\Notebook.png
C:\Data\Passwort.txt
```

At first we create a new file:

**Old Format**
```csharp
VFS currentFileSystem = new VFS(string.empty, "_PATH_OF_THE_FILE_YOU_WANT_TO_CREATE", 128, 45);

File pwFile = new File("Passwort.txt");
pwFile.SetByte(System.IO.ReadAllBytes("C:\Data\Passwort.txt");
currentFileSystem.RootDirectory.GetFiles().Add(pwFile);

Directory currentDirData = new Directory("Data");

Directory currentDirDocuments = new Directory("Documents");
File toDoFile = new File("ToDo.docx");
File meetingFile = new File("Meeting.docx");

toDoFile.SetBytes(System.IO.ReadAllBytes("C:\Data\Documents\ToDo.docx"));
meetingFile.SetBytes(System.IO.ReadAllBytes("C:\Data\Documents\Meeting.docx"));

Directory currentDirImages = new Directory("Images");
File computerFile = new File("Computer.png");
File notebookFile = new File("Notbook.png");

computerFile.SetBytes(System.IO.ReadAllBytes("C:\Data\Images\Computer.png"));
notebookFile.SetBytes(System.IO.ReadAllBytes("C:\Data\Images\Notebook.png"));

currentDirData.GetSubDirectories().Add(currentDirDocuments);
currentDirData.GetSubDirectories().Add(currentDirImages);

currentFileSystem.RootDirectory.GetSubDirectories().Add(currentDirData);
currentFileSystem.Save();
```
It's of course very inconvenient. But currently this version doesn't have a method. But you can look [here](https://github.com/andy123456789088/VFS/blob/master/Applications/VFS/VFS/GUI/frmPack.cs#L60) how it works.

**New Format (thread): General usage**
```csharp
string currentPath = "_PATH_OF_THE_FILE_YOU_WANT_TO_CREATE";

ModifiedVFST currentVFS = new ModifiedVFST(string.Empty, currentPath, "_YOUR_WORKSPACE_PATH", 128, 45, 32768); // 32768 is the default buffer-size

curentVFS.OnFinished += delegate (Result rst)
{
      if (rst != null && rst.SpecialCode == Methods.CREATE)
      {
          // For instance extract the VFS
          currentVFS.Read(currentPath);
      }
};
currentVFS.CreateVHP(@"C:\Data");
```
Firstly, you can see it's much easier than bevor, but in the future I want to implement this into the old format.
The OnFinished-event is called when a the methods achieve the finish and you get a [Result](https://github.com/andy123456789088/VFS/blob/master/Library/ModifiedVFS/Wrapper/Result.cs)-instance which delievers you information: 
- Succeeded or not
- The result as `object`
- The type of the result for casting
- And the code to identify which method has called this event

In this example there is a condition in the event which avoids a endless loop. So you have to be careful, if
you just put another command in this event without preventing an endless loop you get an endless loop:

```csharp
curentVFS.OnFinished += delegate (Result rst)
{
    currentVFS.Read(currentPath); // Don't do this
};
currentVFS.CreateVHP(@"C:\Data");
```

**New Format (thread): Getting the elapsed time**

You need to use a timer to to display the elapsed time:
```csharp
Timer tmr = new Timer();
tmr.Interval = 1000;
tmr.Tick += delegate {
    if (currentVFS.CurrentStopWatch != null)
    {
        label1.Text = currentVFS.CurrentStopWatch.Elapsed.ToString();
    }
};
tmr.Enabled = true;

// Now you can call a method:
currentVFS.Read("_FILE_");
```

**New Format (thread and non-thread): Getting the current progress**
```csharp
Progress.OnValueChanged += delegate (double s, double d)
{
    progressParticular.Invoke(new Action(() => { progressParticular.Value = Convert.ToInt32(s * 100); }));
    progressGeneral.Invoke(new Action(() => { progressGeneral.Value = Convert.ToInt32(d * 100); }));
};
```

As you can see there are 2 progress values: The main progress and the progress of the current action. And to change 
the values of the `ProgressBars` you need to `Invoke` to access the `GUI-Thread` (like you can see in this example).

**New Format (non-thread): General**

```csharp
string currentPath = "_PATH_OF_THE_FILE_YOU_WANT_TO_CREATE";

ModifiedVFS currentVFS = new ModifiedVFS(string.Empty, currentPath, "_YOUR_WORKSPACE_PATH", 128, 45, 32768); // 32768 is the default buffer-size

currentVFS.CreateVHP(@"C:\Data");
currentVFS.Read(currentPath);
```
The only problem is that this can block the `UI-Thread` if you choose big files or much directories. 
In `ModifiedVFS` the actual logic is implemented and `ModifiedVFST` is basically a wrapper which wraps the methods with
a thread, a stopwatch and catches the exceptions. Consequently, I recommend to use `ModifiedVFST` instead of `ModifiedVFS`.

I don't want to explain each method, because I've made XML-comments for each method and I think that these comments should
epxpain enough. The labeling is similar (not equal) to the `System.IO`-Methods.
Finally, I recommend taking a look at this [class diagram](https://github.com/andy123456789088/VFS/blob/master/Documentation/Overview.png). Of course you can also use IntelliSense.
