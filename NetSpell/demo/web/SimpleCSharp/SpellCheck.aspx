<%@ Page Language="C#" Trace="True" %>
<%@ import Namespace="System.IO" %>
<%@ import Namespace="NetSpell.SpellChecker" %>
<%@ import Namespace="NetSpell.SpellChecker.Dictionary" %>
<script runat="server">

    NetSpell.SpellChecker.Spelling SpellChecker;
    NetSpell.SpellChecker.Dictionary.WordDictionary WordDictionary;
    string spellMode = "load";

    void Page_Load(object sender, EventArgs e) {

        Page.Trace.Write("Page_Load");
        // add client side events
        Suggestions.Attributes.Add("onChange", "javascript: changeWord(this);");



        // start spell checking
        this.LoadValues();

        switch (spellMode)
        {
            case "load" :

                break;
            case "start" :
                this.SpellChecker.SpellCheck();
                break;
            case "checking" :

                break;
            case "end" :

                break;
        }

        SpellingBody.Attributes.Add("onLoad", "javascript: updateCallingPage();");

    }

    void Page_Init(object sender, EventArgs e) {

        // get dictionary from cache
        this.WordDictionary = (WordDictionary)HttpContext.Current.Cache["WordDictionary"];
        if (this.WordDictionary == null)
        {
            // if not in cache, create new
            this.WordDictionary = new NetSpell.SpellChecker.Dictionary.WordDictionary();
            this.WordDictionary.EnableUserFile = false;
            //getting folder for dictionaries
            string folderName = ConfigurationSettings.AppSettings["DictionaryFolder"];
            folderName =  this.MapPath(Path.Combine(Request.ApplicationPath, folderName));

            this.WordDictionary.DictionaryFolder = folderName;
            //load and initialize the dictionary
            this.WordDictionary.Initialize();

            // Store the Dictionary in cache
            HttpContext.Current.Cache.Insert("WordDictionary", this.WordDictionary,
                new CacheDependency(Path.Combine(folderName, this.WordDictionary.DictionaryFile)));
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

        this.SuggestionForm.Visible = true;
        this.SpellcheckComplete.Visible = false;

        this.Suggestions.Items.Clear();
        this.ReplacementWord.Text = string.Empty;
    }

    void SpellChecker_EndOfText(object sender, System.EventArgs e)
    {
        Page.Trace.Write("SpellChecker_EndOfText");

        this.SaveValues();

        this.SuggestionForm.Visible = false;
        this.SpellcheckComplete.Visible = true;
    }

    void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
    {
        Page.Trace.Write("SpellChecker_MisspelledWord");
        this.SaveValues();
        this.CurrentWord.Text = this.SpellChecker.CurrentWord;

        this.SuggestionForm.Visible = true;
        this.SpellcheckComplete.Visible = false;

        this.SpellChecker.Suggest();

        this.Suggestions.DataSource = this.SpellChecker.Suggestions;
        this.Suggestions.DataBind();

        this.ReplacementWord.Text = string.Empty;
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

        if (Request.Params["SpellMode"] != null)
        {
            spellMode = Request.Params["SpellMode"];
        }

        if (Request.Params["CurrentText"] != null)
        {
            this.SpellChecker.Text = Request.Params["CurrentText"];
        }

        if (Request.Params["WordIndex"] != null)
        {
            this.SpellChecker.WordIndex = int.Parse(Request.Params["WordIndex"]);
        }

        string ignoreList;
        string[] replaceKeys;
        string[] replaceValues;
        string[] userWords;

        // restore ignore list
        if (Request.Params["IgnoreList"] != null)
        {
            ignoreList = Request.Params["IgnoreList"];
            this.SpellChecker.IgnoreList.Clear();
            this.SpellChecker.IgnoreList.AddRange(ignoreList.Split('|'));
        }

        // restore replace list
        if (Request.Params["ReplaceKeyList"] != null
            && Request.Params["ReplaceValueList"] != null)
        {
            replaceKeys = Request.Params["ReplaceKeyList"].Split('|');
            replaceValues = Request.Params["ReplaceValueList"].Split('|');

            this.SpellChecker.ReplaceList.Clear();
            if (replaceKeys.Length == replaceValues.Length)
            {
                for (int i = 0; i < replaceKeys.Length; i++)
                {
                    if(replaceKeys[i].Length > 0)
                    {
                        this.SpellChecker.ReplaceList.Add(replaceKeys[i], replaceValues[i]);
                    }
                }
            }
        }

        // restore user words
        this.SpellChecker.Dictionary.UserWords.Clear();
        if (Request.Cookies["UserWords"] != null)
        {
            userWords = Request.Cookies["UserWords"].Value.Split('|');
            for (int i = 0; i < userWords.Length; i++)
            {
                if(userWords[i].Length > 0)
                {
                    this.SpellChecker.Dictionary.UserWords.Add(userWords[i], userWords[i]);
                }
            }
        }
    }

    void IgnoreButton_Click(object sender, EventArgs e) {
        this.SpellChecker.IgnoreWord();
        this.SpellChecker.SpellCheck();
    }

    void IgnoreAllButton_Click(object sender, EventArgs e) {
        this.SpellChecker.IgnoreAllWord();
        this.SpellChecker.SpellCheck();
    }

    void AddButton_Click(object sender, EventArgs e) {
        this.SpellChecker.Dictionary.Add(this.SpellChecker.CurrentWord);
        this.SpellChecker.SpellCheck();
    }

    void ReplaceButton_Click(object sender, EventArgs e) {
        Page.Trace.Write("ReplaceButton_Click");

        this.SpellChecker.ReplaceWord(this.ReplacementWord.Text);
        this.CurrentText.Value = this.SpellChecker.Text;
        this.SpellChecker.SpellCheck();
    }

    void ReplaceAllButton_Click(object sender, EventArgs e) {
        this.SpellChecker.ReplaceAllWord(this.ReplacementWord.Text);
        this.CurrentText.Value = this.SpellChecker.Text;
        this.SpellChecker.SpellCheck();
    }

</script>
<html>
<head>
    <title>Spell Check</title>
    <link href="spell.css" type="text/css" rel="stylesheet" />
    <script language="JavaScript" src="spell.js"></script>
</head>
<body id="SpellingBody" runat="server">
    <form id="SpellingForm" name="SpellingForm" method="post" runat="server">
        <input id="WordIndex" type="hidden" name="WordIndex" runat="server" />
        <input id="CurrentText" type="hidden" name="CurrentText" runat="server" />
        <input id="IgnoreList" type="hidden" name="IgnoreList" runat="server" />
        <input id="ReplaceKeyList" type="hidden" name="ReplaceKeyList" runat="server" />
        <input id="ReplaceValueList" type="hidden" name="ReplaceValueList" runat="server" />
        <input id="SpellMode" type="hidden" name="SpellChecking" value="load" runat="server" />
        <asp:panel id="LoadingForm" runat="server" EnableViewState="False" Visible="True">
            <font face="Arial Black" size="+1">Loading Spell Checker . . .</font>
        </asp:panel>
        <asp:panel id="SuggestionForm" runat="server" EnableViewState="False" Visible="False">
            <table cellspacing="0" cellpadding="5" width="100%" border="0">
                <tbody>
                    <tr>
                        <td valign="top" width="275">
                            <i>Word Not in Dictionary:</i>
                            <br />
                            <asp:Label id="CurrentWord" runat="server" forecolor="Red" font-bold="True"></asp:Label>
                            <br />
                            <br />
                            <i>Change To:</i>
                            <br />
                            <asp:TextBox id="ReplacementWord" runat="server" EnableViewState="False" Width="230px" Columns="30" CssClass="suggestion"></asp:TextBox>
                            <br />
                            <i>Suggestions:</i>
                            <br />
                            <asp:ListBox id="Suggestions" runat="server" EnableViewState="False" Width="230px" CssClass="suggestion" Rows="8"></asp:ListBox>
                        </td>
                        <td valign="top" align="middle" width="100">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Button id="IgnoreButton" onclick="IgnoreButton_Click" runat="server" EnableViewState="False" CssClass="button" Text="Ignore"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button id="IgnoreAllButton" onclick="IgnoreAllButton_Click" runat="server" EnableViewState="False" CssClass="button" Text="Ignore All"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>&nbsp;</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button id="AddButton" onclick="AddButton_Click" runat="server" EnableViewState="False" CssClass="button" Text="Add"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>&nbsp;</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button id="ReplaceButton" onclick="ReplaceButton_Click" runat="server" EnableViewState="False" CssClass="button" Text="Replace"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button id="ReplaceAllButton" onclick="ReplaceAllButton_Click" runat="server" EnableViewState="False" CssClass="button" Text="Replace All"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>&nbsp;</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input class="button" onclick="closeSpellChecker()" type="button" value="Cancel" name="btnCancel" /></td>
                                    </tr>
                                    <tr></tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:panel>
        <asp:panel id="SpellcheckComplete" runat="server" EnableViewState="False" Visible="False">
            <table width="375">
                <tbody>
                    <tr>
                        <td align="middle">
                            <p>
                                <font face="Arial Black"></font>
                            </p>
                            <p>
                                <font face="Arial Black" size="3">Spell Check Complete.</font>
                            </p>
                            <p></p>
                        </td>
                    </tr>
                    <tr>
                        <td align="middle">
                            <input class="button" onclick="closeSpellChecker();" type="button" value="OK" name="btnCancel" /></td>
                    </tr>
                </tbody>
            </table>
        </asp:panel>
    </form>
</body>
</html>
