<%@ page language="C#"%>
<%@ import namespace="System.IO"%>
<%@ import namespace="NetSpell.SpellChecker"%>
<%@ import namespace="NetSpell.SpellChecker.Dictionary"%>
<script runat="server">
NetSpell.SpellChecker.Spelling SpellChecker;
NetSpell.SpellChecker.Dictionary.WordDictionary WordDictionary;
void Page_Load(object sender, EventArgs e)
{
    // if modal frame, quit
    if (this.ModalFrame.Visible)
        return;

    // add client side events
    this.Suggestions.Attributes.Add("onChange", "javascript: changeWord(this);");
    this.SpellingBody.Attributes.Add("onLoad", "javascript: initialize();");

    // load spell checker settings
    this.LoadValues();
    switch (this.SpellMode.Value)
    {
        case "start" :
            this.EnableButtons();
            this.SpellChecker.SpellCheck();
            break;

        case "suggest" :
            this.EnableButtons();
            break;

        case "load" :
        case "end" :
        default :
            this.DisableButtons();
            break;
    }
}
void Page_Init(object sender, EventArgs e)
{
    // show iframe for modal support
    if (Request.Params["Modal"] != null)
    {
        this.ModalFrame.Visible = true;
        this.SuggestionForm.Visible = false;
        return;
    }

    // get dictionary from cache
    this.WordDictionary = (WordDictionary)HttpContext.Current.Cache["WordDictionary"];
    if (this.WordDictionary == null)
    {
        // if not in cache, create new
        this.WordDictionary = new NetSpell.SpellChecker.Dictionary.WordDictionary();
        this.WordDictionary.EnableUserFile = false;

        //getting folder for dictionaries
        string folderName = ConfigurationSettings.AppSettings["DictionaryFolder"];

        folderName = this.MapPath(Path.Combine(Request.ApplicationPath, folderName));
        this.WordDictionary.DictionaryFolder = folderName;

        //load and initialize the dictionary
        this.WordDictionary.Initialize();

        // Store the Dictionary in cache
        HttpContext.Current.Cache.Insert("WordDictionary", this.WordDictionary, new CacheDependency(Path.Combine(folderName, this.WordDictionary.DictionaryFile)));
    }

    // create spell checker
    this.SpellChecker = new NetSpell.SpellChecker.Spelling();
    this.SpellChecker.ShowDialog = false;
    this.SpellChecker.Dictionary = this.WordDictionary;

    // adding events
    this.SpellChecker.MisspelledWord += new NetSpell.SpellChecker.Spelling.MisspelledWordEventHandler(this.SpellChecker_MisspelledWord);
    this.SpellChecker.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.SpellChecker_EndOfText);
    this.SpellChecker.DoubledWord += new NetSpell.SpellChecker.Spelling.DoubledWordEventHandler(this.SpellChecker_DoubledWord);
}
void SpellChecker_DoubledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
{
    this.SaveValues();
    this.CurrentWord.Text = this.SpellChecker.CurrentWord;
    this.Suggestions.Items.Clear();
    this.ReplacementWord.Text = string.Empty;
    this.SpellMode.Value = "suggest";
    this.StatusText.Text = string.Format("Word: {0} of {1}", this.SpellChecker.WordIndex + 1, this.SpellChecker.WordCount);
}
void SpellChecker_EndOfText(object sender, System.EventArgs e)
{
    this.SaveValues();
    this.SpellMode.Value = "end";
    this.DisableButtons();
    this.StatusText.Text = string.Format("Word: {0} of {1}", this.SpellChecker.WordIndex + 1, this.SpellChecker.WordCount);
}
void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
{
    this.SaveValues();
    this.CurrentWord.Text = this.SpellChecker.CurrentWord;
    this.SpellChecker.Suggest();
    this.Suggestions.DataSource = this.SpellChecker.Suggestions;
    this.Suggestions.DataBind();
    this.ReplacementWord.Text = string.Empty;
    this.SpellMode.Value = "suggest";
    this.StatusText.Text = string.Format("Word: {0} of {1}", this.SpellChecker.WordIndex + 1, this.SpellChecker.WordCount);
}
void EnableButtons()
{
    this.IgnoreButton.Enabled = true;
    this.IgnoreAllButton.Enabled = true;
    this.AddButton.Enabled = true;
    this.ReplaceButton.Enabled = true;
    this.ReplaceAllButton.Enabled = true;
    this.ReplacementWord.Enabled = true;
    this.Suggestions.Enabled = true;
}
void DisableButtons()
{
    this.IgnoreButton.Enabled = false;
    this.IgnoreAllButton.Enabled = false;
    this.AddButton.Enabled = false;
    this.ReplaceButton.Enabled = false;
    this.ReplaceAllButton.Enabled = false;
    this.ReplacementWord.Enabled = false;
    this.Suggestions.Enabled = false;
}
void SaveValues()
{
    this.CurrentText.Value = this.SpellChecker.Text;
    this.WordIndex.Value = this.SpellChecker.WordIndex.ToString();

    // save ignore words
    string[] ignore = (string[])this.SpellChecker.IgnoreList.ToArray(typeof(string));

    this.IgnoreList.Value = String.Join("|", ignore);

    // save replace words
    ArrayList tempArray = new ArrayList(this.SpellChecker.ReplaceList.Keys);
    string[] replaceKey = (string[])tempArray.ToArray(typeof(string));

    this.ReplaceKeyList.Value = String.Join("|", replaceKey);
    tempArray = new ArrayList(this.SpellChecker.ReplaceList.Values);

    string[] replaceValue = (string[])tempArray.ToArray(typeof(string));

    this.ReplaceValueList.Value = String.Join("|", replaceValue);

    // saving user words
    tempArray = new ArrayList(this.SpellChecker.Dictionary.UserWords.Keys);

    string[] userWords = (string[])tempArray.ToArray(typeof(string));

    Response.Cookies["UserWords"].Value = String.Join("|", userWords);;
    Response.Cookies["UserWords"].Path = "/";
    Response.Cookies["UserWords"].Expires = DateTime.Now.AddMonths(1);
}
void LoadValues()
{
    if (this.CurrentText.Value.Length > 0)
        this.SpellChecker.Text = this.CurrentText.Value;

    if (this.WordIndex.Value.Length > 0)
        this.SpellChecker.WordIndex = int.Parse(this.WordIndex.Value);

    // restore ignore list
    if (this.IgnoreList.Value.Length > 0)
    {
        this.SpellChecker.IgnoreList.Clear();
        this.SpellChecker.IgnoreList.AddRange(this.IgnoreList.Value.Split('|'));
    }

    // restore replace list
    if (this.ReplaceKeyList.Value.Length > 0 && this.ReplaceValueList.Value.Length > 0)
    {
        string[] replaceKeys = this.ReplaceKeyList.Value.Split('|');
        string[] replaceValues = this.ReplaceValueList.Value.Split('|');

        this.SpellChecker.ReplaceList.Clear();
        if (replaceKeys.Length == replaceValues.Length)
        {
            for (int i = 0; i < replaceKeys.Length; i++)
            {
                if (replaceKeys[i].Length > 0)
                    this.SpellChecker.ReplaceList.Add(replaceKeys[i], replaceValues[i]);
            }
        }
    }

    // restore user words
    this.SpellChecker.Dictionary.UserWords.Clear();
    if (Request.Cookies["UserWords"] != null)
    {
        string[] userWords = Request.Cookies["UserWords"].Value.Split('|');

        for (int i = 0; i < userWords.Length; i++)
        {
            if (userWords[i].Length > 0)
                this.SpellChecker.Dictionary.UserWords.Add(userWords[i], userWords[i]);
        }
    }
}
void IgnoreButton_Click(object sender, EventArgs e)
{
    this.SpellChecker.IgnoreWord();
    this.SpellChecker.SpellCheck();
}
void IgnoreAllButton_Click(object sender, EventArgs e)
{
    this.SpellChecker.IgnoreAllWord();
    this.SpellChecker.SpellCheck();
}
void AddButton_Click(object sender, EventArgs e)
{
    this.SpellChecker.Dictionary.Add(this.SpellChecker.CurrentWord);
    this.SpellChecker.SpellCheck();
}
void ReplaceButton_Click(object sender, EventArgs e)
{
    this.SpellChecker.ReplaceWord(this.ReplacementWord.Text);
    this.CurrentText.Value = this.SpellChecker.Text;
    this.SpellChecker.SpellCheck();
}
void ReplaceAllButton_Click(object sender, EventArgs e)
{
    this.SpellChecker.ReplaceAllWord(this.ReplacementWord.Text);
    this.CurrentText.Value = this.SpellChecker.Text;
    this.SpellChecker.SpellCheck();
}

</script>
<html>
    <head>
        <title>Spell Check</title>
        <link href="spell.css" type="text/css" rel="stylesheet" />
        <script language="JavaScript" type="text/javascript">
var iFormIndex = 0;
var iElementIndex = -1;
var parentWindow;

function initialize()
{

    var spellMode = document.getElementById("SpellMode").value;

    try
    {
        iFormIndex = parseInt(document.getElementById("FormIndex").value);
        iElementIndex = parseInt(document.getElementById("ElementIndex").value);
    }
    catch(e)
    {
        iFormIndex = 0;
        iElementIndex = -1;
    }

    if (parent.window.dialogArguments)
        parentWindow = parent.window.dialogArguments;
    else if (top.opener)
        parentWindow = top.opener;

    switch (spellMode)
    {
        case "start" :
            //do nothing client side
            break;
        case "suggest" :
            //update text from parent document
            updateText();
            //wait for input
            break;
        case "end" :
            //update text from parent document
            updateText();
            //fall through to default
        default :
            //get text block from parent document
            if(loadText())
                document.SpellingForm.submit();
            else
                endCheck()

            break;
    }
}

function loadText()
{
    if (!parentWindow.document)
        return false;
    
    var oDocument = parentWindow.document;

    //loop through all forms on parent document starting with last form index
    for (iFormIndex; iFormIndex < oDocument.forms.length; iFormIndex++)
    {
        var oForm = oDocument.forms[iFormIndex];
        //loop through all elements starting with last element index +1
        for(++iElementIndex; iElementIndex < oForm.elements.length; iElementIndex++)
        {
            var sTagName = oForm.elements[iElementIndex].tagName;
            var newText = "";

            //look for input or textarea elements
            if ((sTagName == "INPUT" && oForm.elements[iElementIndex].type == "text") || sTagName == "TEXTAREA")
                newText = oForm.elements[iElementIndex].value;

            if (newText.length > 0)
            {
                updateSettings(newText, 0, iFormIndex, iElementIndex, "start");
                document.getElementById("StatusText").innerText = "Checking " + oForm.elements[iElementIndex].name;
                return true;
            }
        }
        iElementIndex = -1;
    }
    return false;
}

function updateSettings(currentText, wordIndex, formIndex, elementIndex, mode)
{
    document.getElementById("CurrentText").value = currentText;
    document.getElementById("WordIndex").value = wordIndex;
    document.getElementById("FormIndex").value = formIndex;
    document.getElementById("ElementIndex").value = elementIndex;
    document.getElementById("SpellMode").value = mode;
}

function updateText()
{
    var oDocument = parentWindow.document;
    var oForm = oDocument.forms[iFormIndex];
    var tempText = document.getElementById("CurrentText").value;

    oForm.elements[iElementIndex].value = tempText;
}

function endCheck()
{
    alert("Spell Check Complete");
    closeWindow();
}

function closeWindow()
{
    if (top.opener || parent.window.dialogArguments)
	   self.close();
}

function changeWord(oElement)
{
    var k = oElement.selectedIndex;
    oElement.form.ReplacementWord.value = oElement.options[k].value;
}
        </script>
    </head>
    <body id="SpellingBody" style="MARGIN: 0px" runat="server">
        <form id="SpellingForm" name="SpellingForm" method="post" runat="server">
            <input id="WordIndex" type="hidden" name="WordIndex" runat="server" />
            <input id="CurrentText" type="hidden" name="CurrentText" runat="server" />
            <input id="IgnoreList" type="hidden" name="IgnoreList" runat="server" />
            <input id="ReplaceKeyList" type="hidden" name="ReplaceKeyList" runat="server" />
            <input id="ReplaceValueList" type="hidden" name="ReplaceValueList" runat="server" />
            <input id="FormIndex" type="hidden" value="0" name="FormIndex" runat="server" />
            <input id="ElementIndex" type="hidden" value="-1" name="ElementIndex" runat="server" />
            <input id="SpellMode" type="hidden" value="load" name="SpellMode" runat="server" />
            <asp:panel id="ModalFrame" runat="server" visible="False" enableviewstate="False">
                <iframe id="SpellCheckFrame" hidefocus="true" name="SpellCheckFrame" src="SpellCheck.aspx"
                    frameborder="0" width="100%" scrolling="no" height="100%" runat="server"></iframe>
            </asp:panel>
            <asp:panel id="SuggestionForm" runat="server" visible="true" enableviewstate="False">
                <table cellspacing="0" cellpadding="5" width="100%">
                    <tbody>
                        <tr>
                            <td align="center" valign="middle">
                                <table cellspacing="0" cellpadding="2">
                                    <tbody>
                                        <tr>
                                            <td style="width: 250px;">
                                                <em>Word Not in Dictionary:</em>
                                            </td>
                                            <td>
                                                <asp:button id="IgnoreButton" onclick="IgnoreButton_Click" runat="server" enableviewstate="False"
                                                    enabled="False" cssclass="button" text="Ignore">
                                                </asp:button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:label id="CurrentWord" runat="server" font-bold="True" forecolor="Red">
                                                </asp:label></td>
                                            <td>
                                                <asp:button id="IgnoreAllButton" onclick="IgnoreAllButton_Click" runat="server" enableviewstate="False"
                                                    enabled="False" cssclass="button" text="Ignore All">
                                                </asp:button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <em>Change To:</em>
                                            </td>
                                            <td>
                                                <p>&nbsp;</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:textbox id="ReplacementWord" runat="server" enableviewstate="False" enabled="False"
                                                    cssclass="suggestion" columns="30" width="230px">
                                                </asp:textbox>
                                            </td>
                                            <td>
                                                <asp:button id="AddButton" onclick="AddButton_Click" runat="server" enableviewstate="False"
                                                    enabled="False" cssclass="button" text="Add">
                                                </asp:button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <em>Suggestions:</em>
                                            </td>
                                            <td>
                                                <p>&nbsp;</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td rowspan="5">
                                                <asp:listbox id="Suggestions" runat="server" enableviewstate="False" enabled="False"
                                                    cssclass="suggestion" width="230px" rows="8">
                                                </asp:listbox>
                                            </td>
                                            <td>
                                                <asp:button id="ReplaceButton" onclick="ReplaceButton_Click" runat="server" enableviewstate="False"
                                                    enabled="False" cssclass="button" text="Replace">
                                                </asp:button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:button id="ReplaceAllButton" onclick="ReplaceAllButton_Click" runat="server"
                                                    enableviewstate="False" enabled="False" cssclass="button" text="Replace All">
                                                </asp:button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>&nbsp;</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>&nbsp;</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input class="button" onclick="closeWindow()" type="button" value="Cancel" name="btnCancel"
                                                    id="btnCancel" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:label id="StatusText" runat="Server" forecolor="DimGray" font-size="8pt">Loading ...</asp:label>
                                            </td>
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
