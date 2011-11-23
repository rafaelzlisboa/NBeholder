<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NBeholder.aspx.cs" Inherits="NBeholderWeb.NBeholder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>NBeholder Report</title>
    <link rel="stylesheet" href="bootstrap.css" />
</head>

<body>

	<div class="container">

		<h1 class="hero-unit">NBeholder Report</h1>

        <asp:Repeater ID="assembliesRepeater" runat="server">
        <ItemTemplate>
        <div class="well">
            <h2><%# Eval("AssemblyName") %></h2>
            <table class="zebra-striped">
                <tr>
                    <td><span class="label">Version</span></td>
                    <td><%# Eval("Version") %></td>
                </tr>
				<tr>
					<td><span class="label">Culture</span></td>
					<td><%# Eval("Culture") %></td>
				</tr>
				<tr>
					<td><span class="label">Filename</span></td>
					<td><%# Eval("Filename") %></td>
				</tr>
				<tr>
					<td><span class="label">Filesize</span></td>
					<td><%# Eval("Filesize") %></td>
				</tr>
				<tr>
					<td><span class="label">PublicKeyToken</span></td>
					<td><%# Eval("PublicKeyToken") %></td>
				</tr>
            </table>
        </div>
        </ItemTemplate>
        </asp:Repeater>

	</div>

</body>
</html>
