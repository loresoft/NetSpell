<%@ Page language="c#" Codebehind="spell.aspx.cs" AutoEventWireup="false" Inherits="NetSpell.Demo.Web.spell" ValidateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NetSpell Spell Checking</title>
		<LINK href="spell.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="spell.js">
		</script>
	</HEAD>
	<body id="SpellingBody" runat="server">
		<form id="SpellingForm" name="SpellingForm" method="post" runat="server">
			<input id="WordIndex" type="hidden" name="WordIndex" runat="server"> <input id="CurrentText" type="hidden" name="CurrentText" runat="server">
			<input id="IgnoreList" type="hidden" name="IgnoreList" runat="server"> <input id="ReplaceList" type="hidden" name="ReplaceList" runat="server">
			<asp:panel id="SuggestionForm" runat="server" Visible="False">
				<TABLE cellSpacing="0" cellPadding="3" width="375" bgColor="#ffffff" border="1">
					<TR>
						<TD class="highlight" colSpan="2"><FONT face="Arial Black">Spell Checking</FONT></TD>
					</TR>
					<TR>
						<TD vAlign="top" width="275"><I>Word Not in Dictionary:</I><BR>
							<asp:Label id="CurrentWord" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label><BR>
							<BR>
							<I>Change To:</I><BR>
							<asp:TextBox id="ReplacementWord" runat="server" Width="230px" Columns="30" EnableViewState="False"></asp:TextBox><BR>
							<I>Suggestions:</I><BR>
							<asp:ListBox id="Suggestions" runat="server" Width="230px" Rows="8" EnableViewState="False"></asp:ListBox></TD>
						<TD class="highlight" vAlign="top" align="center" width="100">
							<TABLE>
								<TR>
									<TD>
										<asp:Button id="IgnoreButton" runat="server" CssClass="button" Text="Ignore"></asp:Button></TD>
								</TR>
								<TR>
									<TD>
										<asp:Button id="IgnoreAllButton" runat="server" CssClass="button" Text="Ignore All"></asp:Button></TD>
								</TR>
								<TR>
									<TD>
										<P>&nbsp;</P>
									</TD>
								</TR>
								<TR>
									<TD>
										<asp:Button id="ReplaceButton" runat="server" CssClass="button" Text="Replace"></asp:Button></TD>
								</TR>
								<TR>
									<TD>
										<asp:Button id="ReplaceAllButton" runat="server" CssClass="button" Text="Replace All"></asp:Button></TD>
								</TR>
								<TR>
									<TD>
										<P>&nbsp;</P>
									</TD>
								</TR>
								<TR>
									<TD><INPUT class="button" onclick="self.close();" type="button" value="Cancel" name="btnCancel"></TD>
								</TR>
								<TR>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="SpellcheckComplete" runat="server">
				<TABLE width="375">
					<TR>
						<TD align="center">
							<P><FONT face="Arial Black"></FONT>&nbsp;</P>
							<P><FONT face="Arial Black" size="3">Spell Check Complete.</FONT></P>
							<P>&nbsp;</P>
						</TD>
					</TR>
					<TR>
						<TD align="center"><INPUT class="button" onclick="self.close();" type="button" value="OK" name="btnCancel"></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>
