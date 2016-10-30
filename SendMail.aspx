<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="SendMail" validateRequest=false %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
<link href="css/adminCss.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="js/TextEdit.js"></script>
    <style type="text/css">
        .style1
        {
            BACKGROUND-COLOR: #ECF9FC;
            text-align: right;
            padding-left: 3px;
            width: 202px;
            height: 22px;
        }
        .style2
        {
            padding: 5px;
            background-color: #FFFFFF;
            height: 22px;
        }
    </style>
</head>

<body >
    <form id="form1" runat="server" onSubmit="SetIframe();">
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" >
            <tr>
                <td bgcolor="#9AA29A" height="5px">
                </td>
            </tr>
      </table>
        <table width="99%" border="0" align="center" cellpadding="1" cellspacing="1" class="tabGg" >
            <tr>
                <td colspan="4" align="center" bgcolor="#ffffff"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td align="center" class="F_B font14">
                        E-Mail</td>
                  </tr>
                </table>                </td>
            </tr>
            <tr>
                <td bgcolor="#ffffff" class="style1">
                    To</td>
                <td bgcolor="#ffffff" class="style2" colspan="3" style="color: red">
                    <asp:TextBox ID="txtSendTo" runat="server" Width="629px"></asp:TextBox></td>
            </tr>
            <tr>
                <td bgcolor="#ffffff" class="r_bg" height="24" style="width: 202px">
                    CC</td>
                <td bgcolor="#ffffff" class="right_bg" colspan="3" style="color: red">
                    <asp:TextBox ID="TextBox1" runat="server" Width="629px"></asp:TextBox></td>
            </tr>
            <tr>
                <td bgcolor="#ffffff" class="r_bg" height="24" style="width: 202px">
                    Subject</td>
                <td bgcolor="#ffffff" class="right_bg" colspan="3" style="color: red">
                    <asp:TextBox ID="txtSubject" runat="server" Width="629px"></asp:TextBox></td>
            </tr>
            <tr>
                <td bgcolor="#ffffff" class="r_bg" height="24" style="width: 202px">
                    Attachment</td>
                <td bgcolor="#ffffff" class="right_bg" colspan="3" style="color: red">
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="r_bg" style="width: 202px" >                </td>
                <td colspan="3" bgcolor="#FFFFFF" class="right_bg" >
                <DIV id="divContent">
				<asp:panel id="pnlContentMgmt" style="Z-INDEX: 133; LEFT: 100px;  TOP: 175px" runat="server" width="854px">
					<TABLE style="WIDTH: 700px; BORDER-COLLAPSE: collapse; HEIGHT: 130px" borderColor="black" cellSpacing="0" cellPadding="0" width="500" align="left" bgColor="buttonface" border="1">
						<TR>
							<TD height="20">
								<TABLE height="18" cellSpacing="1" cellPadding="0" border="0">
									<TR>
										<TD><IMG height="21" src="MailImages/VertLine2.jpg" width="10">
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('cut')" onmouseout="button_out(this);"><IMG height="19" alt="Cut" hspace="1" src="MailImages/Cut.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('copy')" onmouseout="button_out(this);"><IMG height="19" alt="Copy" hspace="1" src="MailImages/Copy.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('paste')" onmouseout="button_out(this);"><IMG height="19" alt="Paste" hspace="1" src="MailImages/Paste.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD><IMG height="21" src="MailImages/VertLine.jpg" width="10">
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('createLink')" onmouseout="button_out(this);"><IMG height="19" alt="Link" hspace="2" src="MailImages/Link.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('Unlink')" onmouseout="button_out(this);"><IMG height="19" alt="Unlink" hspace="2" src="MailImages/unlink.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<%--<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('SelectAll')" onmouseout="button_out(this);">
											<IMG height="19" alt="Select All" hspace="2" src="Images/SelectAll.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>--%>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('RemoveFormat')" onmouseout="button_out(this);"><IMG height="19" alt="Remove Format" hspace="2" src="MailImages/UnSelect.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="setImage()" onmouseout="button_out(this);"><IMG height="19" alt="Insert Image" hspace="2" src="MailImages/InsertImage.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
									
										<TD><IMG height="21" src="MailImages/VertLine.jpg" width="10">
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('bold')" onmouseout="button_out(this);"><IMG height="19" alt="Bold" hspace="1" src="MailImages/Bold.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('italic')" onmouseout="button_out(this);"><IMG height="19" alt="Italic" hspace="1" src="MailImages/Italic.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('underline')" onmouseout="button_out(this);"><IMG height="19" alt="Underline" hspace="1" src="MailImages/Under.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD><IMG height="21" src="MailImages/VertLine.jpg" width="10"></TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('justifyleft')" onmouseout="button_out(this);"><IMG height="19" alt="Justify Left" hspace="1" src="MailImages/Left.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('justifycenter')" onmouseout="button_out(this);"><IMG height="19" alt="Center" hspace="1" src="MailImages/Center.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('justifyright')" onmouseout="button_out(this);"><IMG height="19" alt="Justify Right" hspace="1" src="MailImages/Right.gif" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD><IMG height="21" src="MailImages/VertLine.jpg" width="10"></TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('insertorderedlist')" onmouseout="button_out(this);"><IMG height="19" alt="Ordered List" hspace="2" src="MailImages/numlist.GIF" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="cmdExec('insertunorderedlist')" onmouseout="button_out(this);"><IMG height="19" alt="Unordered List" hspace="2" src="MailImages/bullist.GIF" width="20" align="absMiddle" vspace="1">
											</DIV>
										</TD>
										<TD><IMG height="21" src="MailImages/VertLine.jpg" width="10"></TD>
										<TD>
											<DIV onmouseup="button_up(this);" class="cbtn" onmousedown="button_down(this);" onmouseover="button_over(this);" onclick="foreColor()" onmouseout="button_out(this);"><IMG id="BtnColor" height="19" alt="Forecolor" hspace="2" src="MailImages/fgcolor.gif" width="20" align="absMiddle" vspace="1"></DIV>
										</TD>
										<TD><IMG height="21" src="MailImages/VertLine.jpg" width="10"></TD>
										<TD><SELECT style="WIDTH: 80px" onchange="cmdExec('fontname',this[this.selectedIndex].value);" name="selfontname">
												<OPTION selected>Font</OPTION>
												<OPTION value="Arial">Arial</OPTION>
												<OPTION value="Arial Black">Arial Black</OPTION>
												<OPTION value="Arial Narrow">Arial Narrow</OPTION>
												<OPTION value="Comic Sans MS">Comic Sans MS</OPTION>
												<OPTION value="Courier New">Courier New</OPTION>
												<OPTION value="System">System</OPTION>
												<OPTION value="Tahoma">Tahoma</OPTION>
												<OPTION value="Times">Times</OPTION>
												<OPTION value="Verdana">Verdana</OPTION>
												<OPTION value="Wingdings">Wingdings</OPTION>
											</SELECT>
										</TD>
										<TD>
                                            <select name="selfontsize" 
                            onchange="cmdExec('fontsize',this[this.selectedIndex].value);">
												<option selected="">Size</option>
												<option value="1">1</option>
												<option value="2">2</option>
												<option value="3">3</option>
												<option value="4">4</option>
												<option value="5">5</option>
												<option value="6">6</option>
												<option value="7">7</option>
												<option value="8">8</option>
												<option value="9">9</option>
												<option value="10">10</option>
											</select>
										</TD>
										<TD><IMG height="21" src="MailImages/VertLine2.jpg" width="10"></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD style="height: 5px"></TD>
						</TR>
						<TR>
							<TD id="mam" height="180">
								<IFRAME id="NewsBody_rich" src="../MsgBody.htm" width="100%" height="100%" Font-Name="Verdana" style="width: 100%">
								</IFRAME>
							</TD>
						</TR>
					
					</TABLE>
				</asp:panel>
			</DIV>
                </td>
            </tr>
              <tr>
                <td bgcolor="#ffffff" class="r_bg" height="24" style="width: 202px">
                    </td>
                <td bgcolor="#ffffff" class="right_bg" colspan="3" style="color: red">
                    <asp:Button ID="btnSend" runat="server" CssClass="submit" Text=" Send " OnClick="btnSend_Click" /><br />
                    <asp:HiddenField ID="hidContent" runat="server" />
                </td>
            </tr>
      </table>
      	
    </form>
</body>
</html>
<script type="text/javascript"> 
function SetIframe() 
{ 
var content=document.getElementById("NewsBody_rich").contentWindow.document.body.innerHTML; 
document.getElementById("hidContent").value=content; 
} 
</script> 

