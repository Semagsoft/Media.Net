Public Class ContentItem
    Inherits ListBoxItem

    Private ContentItemView As New DockPanel, ContentTitle As New TextBlock

    Public MediaURL As String = Nothing, IsMediaFile As Boolean = False, IsPlaying As Boolean = False, MediaType As String = "Folder"

    Public Sub New(ByVal url As String, ByVal isFile As Boolean)
        If My.Computer.FileSystem.DirectoryExists(url) Then
            MediaURL = url
            MediaType = "Folder"
            IsMediaFile = False
        ElseIf My.Computer.FileSystem.FileExists(url) Then
            Dim file As New IO.FileInfo(url)
            If file.Extension = ".avi" Then
                MediaURL = url
                MediaType = "Movie"
                IsMediaFile = True
            ElseIf file.Extension = ".mp3" OrElse file.Extension = ".wav" OrElse file.Extension = ".wave" Then
                MediaURL = url
                MediaType = "Music"
                IsMediaFile = True
            End If
        Else
            Exit Sub
        End If
    End Sub
End Class