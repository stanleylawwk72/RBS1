<%@ Page Title="Maintenance Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenancePage.aspx.cs" Inherits="ResourceBorrowing.MaintenancePage" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MaintenanceContent">
    <h2>
       We’ll be back soon!
    </h2>
           <p>
           <asp:Label ID="lblMessage1" runat="server" AssociatedControlID="lblMessage1" Text=" "/>
            </p>
            <p><asp:Label ID="lblMessage2" runat="server" AssociatedControlID="lblMessage2" Text=" "/>
    </p> 
</asp:Content>
