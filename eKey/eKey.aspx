<%@ Page Title="eKey Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="eKey.aspx.cs" Inherits="eKey" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContentAdmin">
    <h2>
       eKey Form
    </h2>
<asp:MultiView ID="mvEPOForms" runat="server" ActiveViewIndex="0">
    <asp:View ID="vEPOFormList" runat="server">
    <table>
    <tr>
    <td> <asp:Label ID="lbleKey" runat="server" Text="Existing Key:" Visible="false" /></td>
     <td><asp:TextBox ID="txtExistingKey" runat="server" enabled="false" Width="400" Visible="false" /></td>
    </tr>
     <tr>
    <td> <asp:Label ID="lblNeweKey" runat="server" Text="Key:" /></td>
     <td><asp:TextBox ID="txtNewKey" runat="server" enabled="true" Width="400" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblInput" runat="server" Text="Input:"/></td>
    <td><asp:TextBox ID="txtInput" runat="server" enabled="true"  Width="400" /></td>
    </tr>
        <tr>
    <td><asp:Label ID="lblOutput" runat="server" Text="Output:" /> </td>
    <td><asp:TextBox ID="txtOutput" runat="server" enabled="true" Width="400" /></td>
    </tr>
    <tr>
    <td><asp:Button ID="btnEnc" runat="server" Text="Encrypt"  OnClick="btnEnc_Click" CssClass="button" /> </td>
    <td><asp:Button ID="btnDnc" runat="server" Text="Decrypt"  OnClick="btnDnc_Click" CssClass="button" /></td>
    </tr>

    </table>
    </asp:View>
</asp:MultiView>

</asp:Content>