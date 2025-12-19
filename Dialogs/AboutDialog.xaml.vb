Public Class AboutDialog

    Private Sub AboutDialog_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        VersionTextBlock.Text = "Version: " + My.Application.Info.Version.Major.ToString + "." + My.Application.Info.Version.Minor.ToString
        CopyrightTextBlock.Text = My.Application.Info.Copyright
    End Sub
End Class