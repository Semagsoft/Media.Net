Class MainWindow
    Public NowPlayingFileName As String = Nothing,
        NowPlayingIndex As Integer, NowPlayingList As New Collections.Specialized.StringCollection,
        MediaType As String = Nothing, IsPlaying As Boolean = False, IsFullScreen As Boolean = False

#Region "Reuseable Code"

    Private Sub LoadMedia(ByVal filename As String)
        Dim file As New IO.FileInfo(filename)
        If file.Extension.ToLower = ".avi" OrElse file.Extension.ToLower = ".m4a" OrElse file.Extension.ToLower = ".mp3" OrElse file.Extension.ToLower = ".mp4" OrElse file.Extension.ToLower = ".mpeg" OrElse file.Extension.ToLower = ".mpg" OrElse file.Extension.ToLower = ".wav" OrElse file.Extension.ToLower = ".wave" OrElse file.Extension.ToLower = ".wma" Then
            MoviePlayer.Source = New Uri(filename)
            If file.Extension.ToLower = ".avi" OrElse file.Extension.ToLower = ".mp4" OrElse file.Extension.ToLower = ".mpeg" OrElse file.Extension.ToLower = ".mpg" OrElse file.Extension.ToLower = ".wmv" Then
                ContentListBox.Visibility = Windows.Visibility.Collapsed
                MovieView.Visibility = Windows.Visibility.Visible
            End If
            MoviePlayer.Play()
            IsPlaying = True
            NowPlayingFileName = filename
            MediaType = "Movie"
            Dim pauseimg As New BitmapImage(New Uri(My.Application.Info.DirectoryPath + "\Images\Playback\pause16.png")), pauseicon As New Image
            pauseicon.Height = 16
            pauseicon.Width = 16
            pauseicon.Source = pauseimg
            PlayMenuItem.Icon = pauseicon
            Dim playbuttonimg As New BitmapImage(New Uri(My.Application.Info.DirectoryPath + "\Images\Playback\pause32.png")), playbuttonicon As New Image
            playbuttonicon.Height = 32
            playbuttonicon.Width = 32
            playbuttonicon.Source = playbuttonimg
            PlayButton.Content = playbuttonicon
            StopManuItem.IsEnabled = True
            StopButton.IsEnabled = True
        Else
            MessageBox.Show("File Type Not Supported!", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub

    Private Sub PauseMedia()
        If IsPlaying Then
            MoviePlayer.Pause()
            IsPlaying = False
            Dim pauseimg As New BitmapImage(New Uri(My.Application.Info.DirectoryPath + "\Images\Playback\play16.png")), pauseicon As New Image
            pauseicon.Height = 16
            pauseicon.Width = 16
            pauseicon.Source = pauseimg
            PlayMenuItem.Icon = pauseicon
            Dim playbuttonimg As New BitmapImage(New Uri(My.Application.Info.DirectoryPath + "\Images\Playback\play32.png")), playbuttonicon As New Image
            playbuttonicon.Height = 32
            playbuttonicon.Width = 32
            playbuttonicon.Source = playbuttonimg
            PlayButton.Content = playbuttonicon
        Else
            MoviePlayer.Play()
            IsPlaying = True
            Dim playimg As New BitmapImage(New Uri(My.Application.Info.DirectoryPath + "\Images\Playback\pause16.png")), playicon As New Image
            playicon.Height = 16
            playicon.Width = 16
            playicon.Source = playimg
            PlayMenuItem.Icon = playicon
            Dim playbuttonimg As New BitmapImage(New Uri(My.Application.Info.DirectoryPath + "\Images\Playback\pause32.png")), playbuttonicon As New Image
            playbuttonicon.Height = 32
            playbuttonicon.Width = 32
            playbuttonicon.Source = playbuttonimg
            PlayButton.Content = playbuttonicon
            StopManuItem.IsEnabled = True
            StopButton.IsEnabled = True
        End If
    End Sub

#End Region

#Region "MainWindow"

    Private Sub MainWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If My.Computer.Info.OSVersion >= "6.0" Then
            AppHelper.ExtendGlassFrame(Me, New Thickness(-1, -1, -1, -1))
        End If
        SidebarListBox.SelectedIndex = 0
    End Sub

#End Region

#Region "MainMenu"

#Region "File"

    Private Sub OpenMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles OpenMenuItem.Click, OpenButton.Click
        Dim open As New Microsoft.Win32.OpenFileDialog
        open.Filter = "Supported Media(*.avi;*.m4a;*.mp3;*.mp4;*.mpeg;*.mpg;*.wav;*.wave;*.wma;*.wmv)|*.avi;*.m4a;*.mp3;*.mp4;*.mpeg;*.mpg;*.wav;*.wave;*.wma;*.wmv|Music(*.m4a;*.mp3;*.wav;*.wave;*.wma)|*.m4a;*.mp3;*.wav;*.wave;*.wma|Movies(*.avi;*.mp4;*.mpeg;*.mpg;*.wmv)|*.avi;*.mp4;*.mpeg;*.mpg;*.wmv|All Files(*.*)|*.*"
        If open.ShowDialog = True Then
            LoadMedia(open.FileName)
        End If
    End Sub

    Private Sub ExitMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles ExitMenuItem.Click
        Close()
    End Sub

#End Region

#Region "View"

    Private Sub FullscreenMenuItem_Click(sender As Object, e As RoutedEventArgs) Handles FullscreenMenuItem.Click
        If IsFullScreen Then
            MovieView.Margin = New Thickness(0, 60, 0, 0)
            WindowStyle = Windows.WindowStyle.SingleBorderWindow
            Topmost = False
            WindowState = fullscreensetting
            IsFullScreen = False
            FullscreenMenuItem.IsChecked = False
        Else
            MovieView.Margin = New Thickness(0, 0, 0, 0)
            WindowStyle = Windows.WindowStyle.None
            Topmost = True
            WindowState = Windows.WindowState.Maximized
            IsFullScreen = True
            FullscreenMenuItem.IsChecked = True
        End If
    End Sub

#End Region

#Region "Playback"

    Private Sub PlayMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles PlayMenuItem.Click, PlayButton.Click
        If NowPlayingFileName IsNot Nothing Then
            PauseMedia()
        Else
            If ContentListBox.SelectedItem IsNot Nothing Then
                Dim item As ListViewItem = TryCast(ContentListBox.SelectedItem, ListViewItem)
                LoadMedia(TryCast(item.Tag, String))
            End If
        End If
    End Sub

    Private Sub StopManuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles StopManuItem.Click, StopButton.Click
        MoviePlayer.Stop()
        IsPlaying = False
        MovieView.Visibility = Windows.Visibility.Collapsed
        ContentListBox.Visibility = Windows.Visibility.Visible
        StopManuItem.IsEnabled = False
        StopButton.IsEnabled = False
        Dim playimg As New BitmapImage(New Uri(My.Application.Info.DirectoryPath + "\Images\Playback\play16.png")), playicon As New Image
        playicon.Height = 16
        playicon.Width = 16
        playicon.Source = playimg
        PlayMenuItem.Icon = playicon
        Dim playbuttonimg As New BitmapImage(New Uri(My.Application.Info.DirectoryPath + "\Images\Playback\play32.png")), playbuttonicon As New Image
        playbuttonicon.Height = 32
        playbuttonicon.Width = 32
        playbuttonicon.Source = playbuttonimg
        PlayButton.Content = playbuttonicon
    End Sub

#End Region

#Region "Help"

    Private Sub OnlineHelpMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles OnlineHelpMenuItem.Click
        Process.Start("http://mediadotnet.codeplex.com")
    End Sub

    Private Sub WebsiteMenuItem_Click(sender As Object, e As RoutedEventArgs) Handles WebsiteMenuItem.Click
        Process.Start("http://semagsoft.com")
    End Sub

    Private Sub DonateMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles DonateMenuItem.Click
        Process.Start("https://www.paypal.com/us/cgi-bin/webscr?cmd=_flow&SESSION=EhW2MocbvjnBxdX-wbJbVkNK4Djd3ZkRVeFbYF8A1-YKC6cMSi79BUYe8ey&dispatch=5885d80a13c0db1f8e263663d3faee8deaa77efc63a6eb429928d42bdf5d9d2c")
    End Sub

    Private Sub AboutMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles AboutMenuItem.Click
        Dim about As New AboutDialog
        about.Owner = Me
        about.ShowDialog()
    End Sub

#End Region

#End Region

#Region "Sidebar"

    Private Sub SidebarListBox_SelectionChanged(sender As Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles SidebarListBox.SelectionChanged
        If SidebarListBox.SelectedIndex = 0 Then
            ContentListBox.Tag = "Bands"
            ContentListBox.Items.Clear()
            For Each d As String In My.Computer.FileSystem.GetDirectories(My.Computer.FileSystem.SpecialDirectories.MyMusic)
                Dim dir As New IO.DirectoryInfo(d), item As New ListViewItem
                item.Content = dir.Name
                item.Tag = dir.FullName
                item.Height = 32
                ContentListBox.Items.Add(item)
            Next
            If Not IsPlaying Then
                PlayMenuItem.IsEnabled = False
                PlayButton.IsEnabled = False
            End If
            If ContentListBox.Items.Count > 0 Then
                ContentListBox.SelectedIndex = 0
                ContentListBox.Focus()
            End If
        ElseIf SidebarListBox.SelectedIndex = 1 Then
            ContentListBox.Tag = "Movies"
            ContentListBox.Items.Clear()
            If My.Settings.Options_MoviesFolder = Nothing Then
                Dim res As MessageBoxResult = MessageBox.Show("Movies folder not found, do you want to find it?", "Movies", MessageBoxButton.YesNo)
                If res = MessageBoxResult.Yes Then
                    Dim folderpicker As New System.Windows.Forms.FolderBrowserDialog
                    If folderpicker.ShowDialog = Forms.DialogResult.OK Then
                        My.Settings.Options_MoviesFolder = folderpicker.SelectedPath
                        My.Settings.Save()
                        SidebarListBox_SelectionChanged(Nothing, Nothing)
                    End If
                End If
            Else
                For Each f As String In My.Computer.FileSystem.GetFiles(My.Settings.Options_MoviesFolder)
                    Dim file As New IO.FileInfo(f)
                    If file.Extension.ToLower = ".avi" OrElse file.Extension.ToLower = ".mp4" OrElse file.Extension.ToLower = ".mpeg" OrElse file.Extension.ToLower = ".mpg" OrElse file.Extension.ToLower = ".wmv" Then
                        Dim i As New ListViewItem
                        i.Content = file.Name
                        i.Tag = file.FullName
                        i.Height = 32
                        ContentListBox.Items.Add(i)
                    End If
                Next
                If Not IsPlaying Then
                    PlayMenuItem.IsEnabled = True
                    PlayButton.IsEnabled = True
                End If
                If ContentListBox.Items.Count > 0 Then
                    ContentListBox.SelectedIndex = 0
                    ContentListBox.Focus()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Content"

    Private Sub ContentListBox_KeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles ContentListBox.KeyDown
        If e.Key = Key.Enter Then
            ContentListBox_MouseDoubleClick(Nothing, New Input.MouseButtonEventArgs(Input.Mouse.PrimaryDevice, 0, MouseButton.Left))
        End If
    End Sub

    Private Sub ContentListBox_MouseDoubleClick(sender As Object, e As System.Windows.Input.MouseButtonEventArgs) Handles ContentListBox.MouseDoubleClick
        If ContentListBox.SelectedItem IsNot Nothing AndAlso e.ChangedButton = MouseButton.Left Then
            If SidebarListBox.SelectedIndex = 0 Then
                If ContentListBox.Tag Is "Bands" Then
                    Dim listitem As ListBoxItem = TryCast(ContentListBox.SelectedItem, ListBoxItem)
                    Dim s As String = TryCast(listitem.Tag, String)
                    ContentListBox.Items.Clear()
                    For Each d As String In My.Computer.FileSystem.GetDirectories(s)
                        Dim dir As New IO.DirectoryInfo(d), item As New ListViewItem
                        item.Content = dir.Name
                        item.Tag = dir.FullName
                        item.Height = 32
                        ContentListBox.Items.Add(item)
                    Next
                    If ContentListBox.Items.Count > 0 Then
                        ContentListBox.SelectedIndex = 0
                    End If
                    ContentListBox.Tag = "Albums"
                ElseIf ContentListBox.Tag Is "Albums" Then
                    Dim listitem As ListBoxItem = TryCast(ContentListBox.SelectedItem, ListBoxItem)
                    Dim s As String = TryCast(listitem.Tag, String)
                    ContentListBox.Items.Clear()
                    For Each f As String In My.Computer.FileSystem.GetFiles(s)
                        Dim file As New IO.FileInfo(f)
                        If file.Extension.ToLower = ".m4a" OrElse file.Extension.ToLower = ".mp3" OrElse file.Extension.ToLower = ".wav" OrElse file.Extension.ToLower = ".wave" OrElse file.Extension.ToLower = ".wma" Then
                            Dim i As New ListViewItem
                            i.Content = file.Name
                            i.Tag = file.FullName
                            i.Height = 32
                            If NowPlayingFileName Is i.Tag Then
                                i.Background = Brushes.LightGray
                            End If
                            ContentListBox.Items.Add(i)
                        End If
                    Next
                    If ContentListBox.Items.Count > 0 Then
                        ContentListBox.SelectedIndex = 0
                        PlayMenuItem.IsEnabled = True
                        PlayButton.IsEnabled = True
                    End If
                    ContentListBox.Tag = "Songs"
                ElseIf ContentListBox.Tag Is "Songs" Then
                    If NowPlayingList.Count > 0 Then
                        NowPlayingList.Clear()
                    End If
                    For Each i As ListBoxItem In ContentListBox.Items
                        i.Background = Brushes.White
                        NowPlayingList.Add(TryCast(i.Tag, String))
                    Next
                    Dim listitem As ListBoxItem = TryCast(ContentListBox.SelectedItem, ListBoxItem)
                    LoadMedia(TryCast(listitem.Tag, String))
                    NowPlayingIndex = ContentListBox.SelectedIndex
                    listitem.Background = Brushes.LightGray
                End If
            ElseIf SidebarListBox.SelectedIndex = 1 Then
                Dim file As ListBoxItem = TryCast(ContentListBox.Items.Item(ContentListBox.SelectedIndex), ListBoxItem)
                LoadMedia(TryCast(file.Tag, String))
            End If
        End If
    End Sub

#End Region

#Region "Other"

    Private Sub MoviePlayer_MediaEnded(sender As Object, e As System.Windows.RoutedEventArgs) Handles MoviePlayer.MediaEnded
        MoviePlayer.Stop()
        MovieView.Visibility = Windows.Visibility.Collapsed
        ContentListBox.Visibility = Windows.Visibility.Visible
        StopManuItem.IsEnabled = False
        StopButton.IsEnabled = False
    End Sub

    Private fullscreensetting As WindowState = Windows.WindowState.Normal
    Private Sub MoviePlayer_MouseLeftButtonDown(sender As Object, e As System.Windows.Input.MouseButtonEventArgs) Handles MoviePlayer.MouseLeftButtonDown
        If e.ClickCount = 2 Then
            FullscreenMenuItem_Click(Nothing, Nothing)
        End If
    End Sub

#End Region

End Class