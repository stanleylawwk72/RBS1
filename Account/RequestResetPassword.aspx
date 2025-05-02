<%@ Page Title="Request Reset Password Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="RequestResetPassword.aspx.cs" Inherits="RequestResetPassword" EnableEventValidation = "false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainRequestResetPassword">
    <h2>
        Reset password
    </h2>
        <span class="failureNotification">
               <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span>
        <asp:ValidationSummary ID="CheckUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckValidationGroup_Edit"/>
         <div class="accountInfo">
                <fieldset class="ResetPassword">
                    <legend>Account Information</legend>
                    <p>
                    <td><asp:Label ID="lblEmailAddress" runat="server" AssociatedControlID="lblEmailAddress">Email Address:</asp:Label></td>
                            <td><asp:TextBox ID="txtEmailAddress" runat="server" CssClass="textEntry"></asp:TextBox><asp:TextBox ID="txtEmailAddress_Old" runat="server" CssClass="textEntry" Visible="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmailAddress_Edit" runat="server" ControlToValidate="txtEmailAddress" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            CssClass="failureNotification"  ErrorMessage="The email format is incorrect! " ToolTip="" 
                            Display="Dynamic" ValidationGroup="CheckValidationGroup_Edit">*</asp:RegularExpressionValidator>
                    </p>
                </fieldset>

                <p class="submitButton">
                    <asp:Button ID="RequestResetPasswordButton" runat="server" CommandName="ResetPassword" Text="Request reset password"  OnClick="btnRequestResetPassword_Click"  ValidationGroup="CheckValidationGroup_Edit" CssClass="button"/>
                    <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="button" OnClick="CancelButton_Click"  />    
                </p>
        </div>
</asp:Content>