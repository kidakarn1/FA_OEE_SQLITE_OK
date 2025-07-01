
Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Web.Script.Serialization
Imports SharpCompress.Archives
Imports SharpCompress.Common
Public Class Update_Patch
	Public wc As New WebClient
	Public _WebClient As New System.Net.WebClient()
	Public Shared Property ArchiveFactory As Object
	Public Sub loading()
		AddHandler _WebClient.DownloadFileCompleted, AddressOf _DownloadFileCompleted
		'_WebClient.DownloadFileAsync(New System.Uri("http://" & Backoffice_model.svApi & "/Version_New_FA/NEW FA SYSTEM.zip"), My.Application.Info.DirectoryPath & "/New_folder/NEW FA SYSTEM.zip")
		_WebClient.DownloadFileAsync(New System.Uri("http://" & Backoffice_model.svApi & "/Version_New_FA/NEW FA SYSTEM.zip"), "C:/Program Files/New FA/NEW FA SYSTEM.zip")
		'wc.DownloadFileAsync(New System.Uri(My.Application.Info.DirectoryPath & "\New_FA.zip"), My.Application.Info.DirectoryPath & "\New_folder\New_FA.zip")
	End Sub
	Public Sub _DownloadFileCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
		ProgressBar1.Value = 1500
		Extra_patch()
	End Sub
	Public Shared Sub zipfile_patch()
		Dim zippath As String = My.Application.Info.DirectoryPath & "\New_folder\NEW FA SYSTEM.rar" 'zip file'
		ZipFile.CreateFromDirectory(spath, zippath)
	End Sub
	Public Sub Extra_patch()
		'Dim zippath As String = My.Application.Info.DirectoryPath & "\New_folder\NEW FA SYSTEM.zip"
		'Dim extra As String = My.Application.Info.DirectoryPath & "\New_folder\"
		Try
			Dim zippath As String = "C:/New FA/NEW_FA_SYSTEM.zip"
			Dim extra As String = "C:/New FA/"
			ZipFile.ExtractToDirectory(zippath, extra)
		Catch ex As Exception

		End Try
		My.Computer.FileSystem.DeleteFile("C:/New FA/NEW_FA_SYSTEM.zip")
		ProgressBar1.Value = ProgressBar1.Maximum
	End Sub
	Public Sub copy_file_exe_shell_startup()
		My.Computer.FileSystem.CopyFile(
	"C:/New FA/TBK FA System.exe",
	"C:\UserFiles\TestFiles2\testFile.txt",
	Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
	Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
	End Sub

	Private Sub Update_Patch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		ProgressBar1.Maximum = 2000
		ProgressBar1.Value = 200
		AddHandler _WebClient.DownloadFileCompleted, AddressOf _DownloadFileCompleted
		My.Computer.FileSystem.CreateDirectory("C:\New FA")
		_WebClient.DownloadFileAsync(New System.Uri("http://" & Backoffice_model.svApi & "/Version_New_FA/NEW FA SYSTEM.Zip"), "C:/New FA/NEW_FA_SYSTEM.Zip")
		'_WebClient.DownloadFileAsync(New System.Uri("http://" & Backoffice_model.svApi & "/Version_New_FA/NEW FA SYSTEM.zip"), My.Application.Info.DirectoryPath & "/New_folder/NEW FA SYSTEM.zip")
		'wc.DownloadFileAsync(New System.Uri(My.Application.Info.DirectoryPath & "\New_FA.zip"), My.Application.Info.DirectoryPath & "\New_folder\New_FA.zip")
	End Sub
End Class
