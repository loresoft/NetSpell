<%@ Page Language="C#" validaterequest="false" Trace="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<script runat="server">

    protected void Page_Load(Object Src, EventArgs E) {
            if (!IsPostBack) {
                BrowserInfo.Text = Page.Request.UserAgent;
                FreeTextBox1.Text = "<H3><FONT face=arial><FONT color=#008000>Free</FONT>TextBox</FONT></H3><P>type here...</P></FONT>";
            }
    }
    
    protected void SaveButton_Click(Object Src, EventArgs E) {
    
            Output.Text = "<h3>Output</h3><div class=\"CodeBlock\">" + FreeTextBox1.Text + "</div>" +
                "<h3>HTML</h3><div class=\"CodeBlock\">" + Server.HtmlEncode(FreeTextBox1.Text).Replace("\n","<br>") + "</div>";
    }

</script>
<html>
<head>
    <title>FreeTextBox 2.0 development</title> <script language="JavaScript" src="spell.js" type="text/javascript"></script>
    <script language="JavaScript">
		function FTB_SpellCheck(ftbName)
		{
			checkSpellingById(ftbName + "_Editor");
		}
    </script>
</head>
<body>
    <form runat="server">
        <h2><span class="FTB">Free</span>TextBox 2.0 
        </h2>
        <i>ASP.NET HTML editor for IE and Mozilla.</i> 
        <p>
            Your Browser: 
            <asp:Literal id="BrowserInfo" runat="server"></asp:Literal>
        </p>
        <FTB:FREETEXTBOX id="FreeTextBox1" runat="Server">
            <TOOLBARS>
                <FTB:TOOLBAR runat="server">
                    <FTB:TOOLBARBUTTON title="SpellCheck" runat="server" FunctionName="FTB_SpellCheck" ButtonImage="spellcheck" />
                </FTB:TOOLBAR>
            </TOOLBARS>
        </FTB:FREETEXTBOX>
        <asp:Button id="SaveButton" onclick="SaveButton_Click" runat="server" Text="Save"></asp:Button>
        <br />
        <br />
        <asp:Literal id="Output" runat="server"></asp:Literal>
    </form>
</body>
</html>
