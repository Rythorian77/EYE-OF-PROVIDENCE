'Created by David (AKA Justin | Lucian | Rythorian)

Imports System.IO
Imports System.Net.NetworkInformation

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim watched As String = "C:\Users"
        Dim fsw As New FileSystemWatcher(watched) With {
            .IncludeSubdirectories = True,
            .NotifyFilter = NotifyFilters.FileName Or NotifyFilters.LastWrite
        }

        AddHandler fsw.Changed, AddressOf Fsw_changed
        AddHandler fsw.Created, AddressOf Fsw_changed
        AddHandler fsw.Deleted, AddressOf Fsw_changed
        AddHandler fsw.Renamed, AddressOf Fsw_changed
        fsw.EnableRaisingEvents = True
    End Sub

    Private Sub Fsw_changed(sender As Object, e As FileSystemEventArgs)

        SetLabelTxt("Monitoring: " & e.FullPath, Label2)
    End Sub

    Public Shared Sub SetLabelTxt(text As String, lbl As Label)
        If lbl.InvokeRequired Then
            lbl.Invoke(New setLabelTxtInvoker(AddressOf SetLabelTxt), text, lbl)
        Else
            lbl.Text = text
        End If
    End Sub

    Public Delegate Sub setLabelTxtInvoker(text As String, lbl As Label)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    'Network Sniffer
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim adapters As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        Dim adapter As NetworkInterface
        For Each adapter In adapters
            Dim properties As IPInterfaceProperties = adapter.GetIPProperties()
            RichTextBox1.AppendText("Description: " & adapter.Description & vbNewLine)
            RichTextBox1.AppendText("DNS suffix: " & properties.DnsSuffix & vbNewLine)
            RichTextBox1.AppendText("DNS enabled: " & properties.IsDnsEnabled.ToString & vbNewLine)
            RichTextBox1.AppendText("Dynamically configured DNS: " & properties.IsDynamicDnsEnabled.ToString & vbNewLine)
            RichTextBox1.AppendText(vbNewLine)
        Next adapter
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim proc As New Process()

        proc.StartInfo.FileName = "netsh"
        proc.StartInfo.Arguments = "wlan show networks mode=bssid"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Public Sub Deltypedurls()
        Dim reg4 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Internet Explorer", True)
        reg4.CreateSubKey("TypedURLs") '//Typedurls-Url typed
        reg4.DeleteSubKeyTree("TypedURLs") '//delete subkey and all subkeys
        '//in it
    End Sub

    Public Sub DelRecfiles()
        Dim reg5 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\ CurrentVersion\Explorer", True)
        reg5.CreateSubKey("RunMRU") '//Runmru-Recent files run.
        reg5.DeleteSubKeyTree("RunMRU") '//delete subkey and all subkeys in
        '//it
    End Sub

    Public Sub DelRecdocs()
        Dim reg5 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer", True)
        reg5.CreateSubKey("RecentDocs") '//Recentdocs-Recent documents opened
        reg5.DeleteSubKeyTree("RecentDocs") '//delete subkey and all subkeys
        '//in it
    End Sub

    Public Sub DelComD()
        Dim reg5 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer", True)
        reg5.CreateSubKey("ComDlg32") '//Comdlg32-Common dialog
        reg5.DeleteSubKeyTree("ComDlg32") '//delete subkey and all subkeys in
        '//it
    End Sub

    Public Sub DelStMru()
        Dim reg5 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer", True)
        reg5.CreateSubKey("StreamMRU")         '//StreamMru-Stream history.
        reg5.DeleteSubKeyTree("StreamMRU") '//delete subkey and all subkeys
        '//in it
    End Sub

    Public Sub DelIECache()
        Dim di As New DirectoryInfo(Environment.GetFolderPath(
            Environment.SpecialFolder.InternetCache))
        On Error GoTo err
        ' Create the directory only if it does not already exist.
        If di.Exists = False Then
            di.Create()
        End If
        '//Set folder to normal attributes to allow easy deletion (and to get
        '//rid of any read-only attributes, which make it hard to delete the
        '//files/folder)
        File.SetAttributes(Environment.GetFolderPath(
            Environment.SpecialFolder.InternetCache).ToString,
            FileAttributes.Normal)
        Dim Cache1 As String
        Dim Cache2() As String

        Cache2 = Directory.GetFiles(Environment.GetFolderPath(
            Environment.SpecialFolder.InternetCache))
        For Each Cache1 In Cache2 '//Get all files in Temporary internet
            '//files, folder and then set their attribute to normal, and then
            '//delete them.
            File.SetAttributes(Cache1, FileAttributes.Normal)
            File.Delete(Cache1)
        Next
        ' The true indicates that if subdirectories
        ' or files are in this directory, they are to be deleted as well.

        ' Delete the directory.
        di.Delete(True)

err:    '///IGNORE ERROR///
    End Sub

    Public Sub Delhistory()
        Dim di As New DirectoryInfo(Environment.GetFolderPath(
            Environment.SpecialFolder.History))
        On Error GoTo err
        ' Create the directory only if it does not already exist.
        If di.Exists = False Then
            di.Create()
        End If
        '//Set folder to normal attributes to allow easy deletion (and to get
        '//rid of any read-only attributes, which make it hard to delete the
        '//files/folder)
        File.SetAttributes(Environment.GetFolderPath(
           Environment.SpecialFolder.History).ToString, FileAttributes.Normal)
        Dim history1 As String
        Dim history2() As String
        history2 = Directory.GetFiles(Environment.GetFolderPath(
            Environment.SpecialFolder.History))
        For Each history1 In history2 '//Get all files in history folder and
            '//then set their attribute to normal, and then delete them.
            File.SetAttributes(history1, FileAttributes.Temporary)
            File.Delete(history1)
        Next
        ' The true indicates that if subdirectories
        ' or files are in this directory, they are to be deleted as well.
        ' Delete the directory.
        di.Delete(True)

err:    '///IGNORE ERROR///
    End Sub

    Public Sub Delrecent()
        Dim di As New DirectoryInfo(
            Environment.GetFolderPath(Environment.SpecialFolder.Recent))
        On Error GoTo err
        ' Create the directory only if it does not already exist.
        If di.Exists = False Then
            di.Create()
        End If
        '//Set folder to normal attributes to allow easy deletion (and to get
        '//rid of any read-only attributes, which make it hard to delete the
        '//files/folder)
        File.SetAttributes(Environment.GetFolderPath(
            Environment.SpecialFolder.Recent).ToString, FileAttributes.Normal)
        Dim recentk As String
        Dim Recentl() As String

        Recentl = Directory.GetFiles(Environment.GetFolderPath(
            Environment.SpecialFolder.Recent))
        For Each recentk In Recentl '//Get all files in recent folder and then
            '//set their attribute to normal, and then delete them.
            File.SetAttributes(recentk, FileAttributes.Normal)
            File.Delete(recentk)
        Next
        ' The true indicates that if subdirectories
        ' or files are in this directory, they are to be deleted as well.
        ' Delete the directory.
        di.Delete(True)

err:    '///IGNORE ERROR///
    End Sub

    Public Sub DelCookies()
        Dim di As New DirectoryInfo(Environment.GetFolderPath(
            Environment.SpecialFolder.Cookies))
        On Error GoTo err
        ' Create the directory only if it does not already exist.
        If di.Exists = False Then
            di.Create()
        End If
        '//Set folder to normal attributes to allow easy deletion (and to get
        '//rid of any read-only attributes, which make it hard to delete the
        '//files/folder)
        File.SetAttributes(Environment.GetFolderPath(
            Environment.SpecialFolder.Cookies).ToString, FileAttributes.Normal)
        Dim Cookie1 As String '//Cookie1
        Dim Cookie2() As String '//Cookie2
        Cookie2 = Directory.GetFiles(
            Environment.GetFolderPath(Environment.SpecialFolder.Cookies))
        For Each Cookie1 In Cookie2 '//Get all files that have .txt extension
            '//and set their attribute to normal and then delete them.
            If InStr(Cookie1, ".txt", CompareMethod.Text) Then
                File.SetAttributes(Cookie1, FileAttributes.Normal)
                File.Delete(Cookie1)
            End If
        Next
        ' The true indicates that if subdirectories
        ' or files are in this directory, they are to be deleted as well.
        ' Delete the directory.
        di.Delete(True)
err:    '///IGNORE ERROR///
    End Sub

    Public Sub Deltemp()
        Dim di As New DirectoryInfo("C:\Documents and Settings\" +
            Environment.UserName + "\Local Settings\Temp")
        On Error GoTo err
        ' Create the directory only if it does not already exist.
        If di.Exists = False Then
            di.Create()
        End If

        '//Set folder to normal attributes to allow easy deletion (and to get
        '//rid of any read-only attributes, which make it hard to delete the
        '//files/folder)
        File.SetAttributes("C:\Documents and Settings\" + Environment.UserName + "\Local Settings\Temp", FileAttributes.Normal)
        Dim temp1 As String
        Dim temp2() As String
        temp2 = IO.Directory.GetFiles("C:\Documents and Settings\" +
            Environment.UserName + "\Local Settings\Temp")
        For Each temp1 In temp2 '//Get all files in Temp folder and then set
            '//their attribute to normal, and then delete them.
            File.SetAttributes(temp1, FileAttributes.Normal)
            File.Delete(temp1)
        Next
        ' The true indicates that if subdirectories
        ' or files are in this directory, they are to be deleted as well.

        ' Delete the directory.
        di.Delete(True)

err:    '///IGNORE ERROR///
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "netsh"
        proc.StartInfo.Arguments = "wlan show network"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "netsh"
        proc.StartInfo.Arguments = "wlan show interfaces"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "netsh"
        proc.StartInfo.Arguments = "wlan show all"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim proc As New Process()

        proc.StartInfo.FileName = "netstat"
        proc.StartInfo.Arguments = "-a"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim ipProps As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
        For Each connection As TcpConnectionInformation In ipProps.GetActiveTcpConnections
            Dim builder As New System.Text.StringBuilder
            builder.AppendFormat("{0} -> {1} - {2}{3}", connection.LocalEndPoint, connection.RemoteEndPoint, connection.State, Environment.NewLine)
            RichTextBox1.AppendText(builder.ToString())
        Next
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "netstat"
        proc.StartInfo.Arguments = "-n"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "netstat"
        proc.StartInfo.Arguments = "-o"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "netstat"
        proc.StartInfo.Arguments = "-s"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "netstat"
        proc.StartInfo.Arguments = "-r"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "whoami"
        proc.StartInfo.Arguments = "/all"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "arp"
        proc.StartInfo.Arguments = "/a"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "cleanmgr"
        proc.StartInfo.Arguments = "/verylowdisk"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()
        MsgBox(proc.StandardOutput.ReadToEnd())
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "msinfo32"
        proc.StartInfo.Arguments = "/pch"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim proc As New System.Diagnostics.Process()

        proc.StartInfo.FileName = "mstsc.exe "
        proc.StartInfo.Arguments = "/admin"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.UseShellExecute = False
        proc.Start()

    End Sub

End Class