Public Class DemoForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents DemoRichText As System.Windows.Forms.RichTextBox
    Friend WithEvents SpellButton As System.Windows.Forms.Button
    Friend WithEvents WordDictionary As NetSpell.SpellChecker.Dictionary.WordDictionary
    Friend WithEvents SpellChecker As NetSpell.SpellChecker.Spelling
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader
        Me.DemoRichText = New System.Windows.Forms.RichTextBox
        Me.SpellButton = New System.Windows.Forms.Button
        Me.WordDictionary = New NetSpell.SpellChecker.Dictionary.WordDictionary(Me.components)
        Me.SpellChecker = New NetSpell.SpellChecker.Spelling(Me.components)
        Me.SuspendLayout()
        '
        'DemoRichText
        '
        Me.DemoRichText.AcceptsTab = True
        Me.DemoRichText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DemoRichText.AutoWordSelection = True
        Me.DemoRichText.Location = New System.Drawing.Point(16, 8)
        Me.DemoRichText.Name = "DemoRichText"
        Me.DemoRichText.ShowSelectionMargin = True
        Me.DemoRichText.Size = New System.Drawing.Size(440, 328)
        Me.DemoRichText.TabIndex = 0
        Me.DemoRichText.Text = "Becuase people are realy bad spelers, ths produc was desinged to prevent speling " & _
        "erors in a text area like ths."
        '
        'SpellButton
        '
        Me.SpellButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SpellButton.Location = New System.Drawing.Point(376, 344)
        Me.SpellButton.Name = "SpellButton"
        Me.SpellButton.Size = New System.Drawing.Size(80, 23)
        Me.SpellButton.TabIndex = 1
        Me.SpellButton.Text = "Spell Check"
        '
        'WordDictionary
        '
        Me.WordDictionary.DictionaryFolder = CType(configurationAppSettings.GetValue("WordDictionary.DictionaryFolder", GetType(System.String)), String)
        '
        'SpellChecker
        '
        Me.SpellChecker.Dictionary = Me.WordDictionary
        '
        'DemoForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(472, 382)
        Me.Controls.Add(Me.SpellButton)
        Me.Controls.Add(Me.DemoRichText)
        Me.Name = "DemoForm"
        Me.Text = "NetSpell Demo"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub SpellButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpellButton.Click
        Me.SpellChecker.Text = Me.DemoRichText.Text
        Me.SpellChecker.SpellCheck()
    End Sub

    Private Sub SpellChecker_DeletedWord(ByVal sender As Object, ByVal e As NetSpell.SpellChecker.SpellingEventArgs) Handles SpellChecker.DeletedWord
        'save existing selecting
        Dim start As Integer = Me.DemoRichText.SelectionStart
        Dim length As Integer = Me.DemoRichText.SelectionLength

        'select word for this event
        Me.DemoRichText.Select(e.TextIndex, e.Word.Length)
        'delete word
        Me.DemoRichText.SelectedText = ""

        If ((start + length) > Me.DemoRichText.Text.Length) Then
            length = 0
        End If
        'restore selection
        Me.DemoRichText.Select(start, length)
    End Sub

    Private Sub SpellChecker_ReplacedWord(ByVal sender As Object, ByVal e As NetSpell.SpellChecker.ReplaceWordEventArgs) Handles SpellChecker.ReplacedWord
        'save existing selecting
        Dim start As Integer = Me.DemoRichText.SelectionStart
        Dim length As Integer = Me.DemoRichText.SelectionLength

        'select word for this event
        Me.DemoRichText.Select(e.TextIndex, e.Word.Length)
        'replace word
        Me.DemoRichText.SelectedText = e.ReplacementWord

        If ((start + length) > Me.DemoRichText.Text.Length) Then
            length = 0
        End If
        'restore selection
        Me.DemoRichText.Select(start, length)
    End Sub
End Class
