<%@ Page Language="C#" ValidateRequest=false %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<script runat="server">

protected void Page_Load(Object Src, EventArgs E) {
	if (!IsPostBack) {
		FreeTextBox1.Text = "<p>some <b>Bold</b> and <u>underlined</u> and <font color=\"#008000\">colored</font> text<p><ul><li>bulleted list 1</li></ul>";
	}
}

protected void SaveButton_Click(Object Src, EventArgs E) {

	Output.Text = FreeTextBox1.Text;
}


</script>
<html>
<head>
	<title>Example 1</title>
<script language="JavaScript" src="spell.js" type="text/javascript"></script>
</head>
<body>

    <form runat="server">
    	
    	<h2><a href="javascript:document.location.href=document.location.href"><span style="color:green;">Free</span>TextBox</a></h2>
    	
    	<div>    	    		
		<FTB:FreeTextBox ToolbarStyleConfiguration="Office2003" OnSaveClick="SaveButton_Click" id="FreeTextBox1" runat="Server">
			<Toolbars>
				<FTB:Toolbar runat="server">
					<FTB:NetSpell runat="server" />
                </FTB:Toolbar>
			</Toolbars>
		</FTB:FreeTextBox>
		
		<asp:Button id="SaveButton" Text="Save" onclick="SaveButton_Click" runat="server" />
		</div>
		
		<div style="display:none;">
			<textarea id="Debug" style="width:500px; height: 250px;"></textarea>
		</div>
		
		<div>
		<asp:Literal id="Output" runat="server" />
		</div>
	</form>
</body>
</html>
