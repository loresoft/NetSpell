<%@ Page Language="VB" ClassName="PopUpSpell" Explicit="True" Strict="True" %>
<%@ import Namespace="System.IO" %>
<%@ import Namespace="NetSpell.SpellChecker" %>
<%@ import Namespace="NetSpell.SpellChecker.Dictionary" %>
<script runat="server">

    Protected WithEvents SpellChecker As NetSpell.SpellChecker.Spelling
    Protected WithEvents WordDictionary As NetSpell.SpellChecker.Dictionary.WordDictionary

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
         ' if modal frame, quit
         If Me.ModalFrame.Visible = True Then
             Return
         End If

         ' add client side events
         Me.Suggestions.Attributes.Add("onChange", "")
         Me.SpellingBody.Attributes.Add("onLoad", "")

         ' load spell checker settings
         Me.LoadValues()
         Select Case Me.SpellMode.Value
             Case "start"
                 Me.EnableButtons()
                 Me.SpellChecker.SpellCheck()
             Case "suggest"
                 Me.EnableButtons()
             Case Else
                 Me.DisableButtons()
         End Select

    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
         ' show iframe for modal support
         If Not Request.Params("Modal") Is Nothing Then
             Me.ModalFrame.Visible = True
             Me.SuggestionForm.Visible = False
             Return
         End If

         ' get dictionary from cache
         Me.WordDictionary = CType(HttpContext.Current.Cache("WordDictionary"), WordDictionary)

         If Me.WordDictionary Is Nothing Then
             ' if not in cache, create new
             Me.WordDictionary = New NetSpell.SpellChecker.Dictionary.WordDictionary()
             Me.WordDictionary.EnableUserFile = False

             'getting folder for dictionaries
             Dim folderName As String =  ConfigurationSettings.AppSettings("DictionaryFolder")

             folderName = Me.MapPath(Path.Combine(Request.ApplicationPath, folderName))
             Me.WordDictionary.DictionaryFolder = folderName

             'load and initialize the dictionary
             Me.WordDictionary.Initialize()

             ' Store the Dictionary in cache
             HttpContext.Current.Cache.Insert("WordDictionary", Me.WordDictionary, New CacheDependency(Path.Combine(folderName, Me.WordDictionary.DictionaryFile)))
         End If

         ' create spell checker
         Me.SpellChecker = New NetSpell.SpellChecker.Spelling()
         Me.SpellChecker.ShowDialog = False
         Me.SpellChecker.Dictionary = Me.WordDictionary

    End Sub

    Private Sub SpellChecker_DoubledWord(ByVal sender As Object, ByVal e As NetSpell.SpellChecker.SpellingEventArgs) Handles SpellChecker.DoubledWord
         Me.SaveValues()
         Me.CurrentWord.Text = Me.SpellChecker.CurrentWord
         Me.Suggestions.Items.Clear()
         Me.ReplacementWord.Text = String.Empty
         Me.SpellMode.Value = "suggest"
         Me.StatusText.Text = String.Format("Word: {0} of {1}", Me.SpellChecker.WordIndex + 1, Me.SpellChecker.WordCount)
    End Sub

    Private Sub SpellChecker_EndOfText(ByVal sender As Object, ByVal e As System.EventArgs) Handles SpellChecker.EndOfText
         Me.SaveValues()
         Me.SpellMode.Value = "end"
         Me.DisableButtons()
         Me.StatusText.Text = String.Format("Word: {0} of {1}", Me.SpellChecker.WordIndex + 1, Me.SpellChecker.WordCount)
    End Sub

    Private Sub SpellChecker_MisspelledWord(ByVal sender As Object, ByVal e As NetSpell.SpellChecker.SpellingEventArgs)  Handles SpellChecker.MisspelledWord
         Me.SaveValues()
         Me.CurrentWord.Text = Me.SpellChecker.CurrentWord
         Me.SpellChecker.Suggest()
         Me.Suggestions.DataSource = Me.SpellChecker.Suggestions
         Me.Suggestions.DataBind()
         Me.ReplacementWord.Text = String.Empty
         Me.SpellMode.Value = "suggest"
         Me.StatusText.Text = String.Format("Word: {0} of {1}", Me.SpellChecker.WordIndex + 1, Me.SpellChecker.WordCount)
    End Sub

    Private Sub EnableButtons()
         Me.IgnoreButton.Enabled = True
         Me.IgnoreAllButton.Enabled = True
         Me.AddButton.Enabled = True
         Me.ReplaceButton.Enabled = True
         Me.ReplaceAllButton.Enabled = True
         Me.ReplacementWord.Enabled = True
         Me.Suggestions.Enabled = True
    End Sub

    Private Sub DisableButtons()
         Me.IgnoreButton.Enabled = False
         Me.IgnoreAllButton.Enabled = False
         Me.AddButton.Enabled = False
         Me.ReplaceButton.Enabled = False
         Me.ReplaceAllButton.Enabled = False
         Me.ReplacementWord.Enabled = False
         Me.Suggestions.Enabled = False
    End Sub

    Private Sub SaveValues()
         Me.CurrentText.Value = Me.SpellChecker.Text
         Me.WordIndex.Value = Me.SpellChecker.WordIndex.ToString()

         ' save ignore words
         Dim ignore() As String = CType(Me.SpellChecker.IgnoreList.ToArray(Type.GetType(String)), String())

         Me.IgnoreList.Value = String.Join("|", ignore)

         ' save replace words
         Dim tempArray As ArrayList =  New ArrayList(Me.SpellChecker.ReplaceList.Keys)
         Dim replaceKey() As String = CType(tempArray.ToArray(Type.GetType(String)), String())

         Me.ReplaceKeyList.Value = String.Join("|", replaceKey)
         tempArray = New ArrayList(Me.SpellChecker.ReplaceList.Values)

         Dim replaceValue() As String = CType(tempArray.ToArray(Type.GetType(String)), String())

         Me.ReplaceValueList.Value = String.Join("|", replaceValue)

         ' saving user words
         tempArray = New ArrayList(Me.SpellChecker.Dictionary.UserWords.Keys)

         Dim userWords() As String = CType(tempArray.ToArray(Type.GetType(String)), String())

         Response.Cookies("UserWords").Value = String.Join("|", userWords)
         Response.Cookies("UserWords").Path = "/"
         Response.Cookies("UserWords").Expires = DateTime.Now.AddMonths(1)
    End Sub

    Private Sub LoadValues()
         If Me.CurrentText.Value.Length > 0 Then
             Me.SpellChecker.Text = Me.CurrentText.Value
         End If

         If Me.WordIndex.Value.Length > 0 Then
             Me.SpellChecker.WordIndex = Integer.Parse(Me.WordIndex.Value)
         End If

         ' restore ignore list
         If Me.IgnoreList.Value.Length > 0 Then
             Me.SpellChecker.IgnoreList.Clear()
             Me.SpellChecker.IgnoreList.AddRange(Me.IgnoreList.Value.Split("|"c))
         End If

         ' restore replace list
         If Me.ReplaceKeyList.Value.Length > 0 And Me.ReplaceValueList.Value.Length > 0 Then
             Dim replaceKeys() As String =  Me.ReplaceKeyList.Value.Split("|"c)
             Dim replaceValues() As String =  Me.ReplaceValueList.Value.Split("|"c)

             Me.SpellChecker.ReplaceList.Clear()
             If replaceKeys.Length = replaceValues.Length Then
                 Dim i As Integer
                 For  i = 0 To  replaceKeys.Length- 1  Step  i + 1
                     If replaceKeys(i).Length > 0 Then
                         Me.SpellChecker.ReplaceList.Add(replaceKeys(i), replaceValues(i))
                     End If
                 Next
             End If
         End If

         ' restore user words
         Me.SpellChecker.Dictionary.UserWords.Clear()
         If Not Request.Cookies("UserWords") Is Nothing Then
             Dim userWords() As String =  Request.Cookies("UserWords").Value.Split("|"c)

             Dim i As Integer
             For  i = 0 To  userWords.Length- 1  Step  i + 1
                 If userWords(i).Length > 0 Then
                     Me.SpellChecker.Dictionary.UserWords.Add(userWords(i), userWords(i))
                 End If
             Next
         End If
    End Sub

    Private Sub IgnoreButton_Click(ByVal sender As Object, ByVal e As EventArgs)
         Me.SpellChecker.IgnoreWord()
         Me.SpellChecker.SpellCheck()
    End Sub

    Private Sub IgnoreAllButton_Click(ByVal sender As Object, ByVal e As EventArgs)
         Me.SpellChecker.IgnoreAllWord()
         Me.SpellChecker.SpellCheck()
    End Sub

    Private Sub AddButton_Click(ByVal sender As Object, ByVal e As EventArgs)
         Me.SpellChecker.Dictionary.Add(Me.SpellChecker.CurrentWord)
         Me.SpellChecker.SpellCheck()
    End Sub

    Private Sub ReplaceButton_Click(ByVal sender As Object, ByVal e As EventArgs)
         Me.SpellChecker.ReplaceWord(Me.ReplacementWord.Text)
         Me.CurrentText.Value = Me.SpellChecker.Text
         Me.SpellChecker.SpellCheck()
    End Sub

    Private Sub ReplaceAllButton_Click(ByVal sender As Object, ByVal e As EventArgs)
         Me.SpellChecker.ReplaceAllWord(Me.ReplacementWord.Text)
         Me.CurrentText.Value = Me.SpellChecker.Text
         Me.SpellChecker.SpellCheck()
    End Sub

</script>
<html>
<head>
    <title>Spell Check</title>
    <link href="spell.css" type="text/css" rel="stylesheet" />
    <script language="JavaScript" src="spell.js" type="text/javascript"></script>
</head>
<body id="SpellingBody" style="MARGIN: 0px" runat="server">
    <form id="SpellingForm" name="SpellingForm" method="post" runat="server">
        <input id="WordIndex" type="hidden" value="0" name="WordIndex" runat="server" />
        <input id="CurrentText" type="hidden" name="CurrentText" runat="server" />
        <input id="IgnoreList" type="hidden" name="IgnoreList" runat="server" />
        <input id="ReplaceKeyList" type="hidden" name="ReplaceKeyList" runat="server" />
        <input id="ReplaceValueList" type="hidden" name="ReplaceValueList" runat="server" />
        <input id="FormIndex" type="hidden" value="0" name="FormIndex" runat="server" />
        <input id="ElementIndex" type="hidden" value="-1" name="ElementIndex" runat="server" />
        <input id="SpellMode" type="hidden" value="load" name="SpellMode" runat="server" />
        <asp:panel id="ModalFrame" runat="server" enableviewstate="False" visible="False">
            <iframe id="SpellCheckFrame" hidefocus="hidefocus" name="SpellCheckFrame" src="SpellCheck.aspx" frameborder="0" width="100%" scrolling="yes" height="100%" runat="server"></iframe>
        </asp:panel>
        <asp:panel id="SuggestionForm" runat="server" enableviewstate="False" visible="true">
            <table cellspacing="0" cellpadding="5" width="100%">
                <tbody>
                    <tr>
                        <td valign="center" align="middle">
                            <table cellspacing="0" cellpadding="2">
                                <tbody>
                                    <tr>
                                        <td style="WIDTH: 250px">
                                            <em>Word Not in Dictionary:</em>
                                        </td>
                                        <td>
                                            <asp:button id="IgnoreButton" onclick="IgnoreButton_Click" runat="server" enableviewstate="False" text="Ignore" cssclass="button" enabled="False"></asp:button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label id="CurrentWord" runat="server" forecolor="Red" font-bold="True"></asp:Label></td>
                                        <td>
                                            <asp:button id="IgnoreAllButton" onclick="IgnoreAllButton_Click" runat="server" enableviewstate="False" text="Ignore All" cssclass="button" enabled="False"></asp:button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <em>Change To:</em>
                                        </td>
                                        <td>
                                            <p></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:textbox id="ReplacementWord" runat="server" enableviewstate="False" cssclass="suggestion" enabled="False" width="230px" columns="30"></asp:textbox>
                                        </td>
                                        <td>
                                            <asp:button id="AddButton" onclick="AddButton_Click" runat="server" enableviewstate="False" text="Add" cssclass="button" enabled="False"></asp:button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <em>Suggestions:</em>
                                        </td>
                                        <td>
                                            <p></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="5">
                                            <asp:listbox id="Suggestions" runat="server" enableviewstate="False" cssclass="suggestion" enabled="False" width="230px" rows="8"></asp:listbox>
                                        </td>
                                        <td>
                                            <asp:button id="ReplaceButton" onclick="ReplaceButton_Click" runat="server" enableviewstate="False" text="Replace" cssclass="button" enabled="False"></asp:button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:button id="ReplaceAllButton" onclick="ReplaceAllButton_Click" runat="server" enableviewstate="False" text="Replace All" cssclass="button" enabled="False"></asp:button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input class="button" id="btnCancel" onclick="closeWindow()" type="button" value="Cancel" name="btnCancel" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label id="StatusText" runat="Server" forecolor="DimGray" font-size="8pt">Loading
                                            ...</asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:panel>
    </form>
</body>
</html>
