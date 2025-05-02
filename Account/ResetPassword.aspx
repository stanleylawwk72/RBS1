<%@ Page Title="Reset Password Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="ResetPassword.aspx.cs" Inherits="ResetPassword" EnableEventValidation = "false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainResetPassword">
    <h2>
        Change Password
    </h2>
    <p>
        Use the form below to change your password.
    </p>
        <span class="failureNotification">
               <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span>
        <asp:ValidationSummary ID="CheckUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckUserPasswordValidationGroup"/>
         <div class="accountInfo">
                <fieldset class="ResetPassword">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" CssClass="failureNotification" ErrorMessage="New Password is required." Display="Dynamic" ToolTip="New Password is required." ValidationGroup="CheckUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPwd" runat="server" ControlToValidate="NewPassword" 
                            ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"
                            CssClass="failureNotification"  ErrorMessage="The password should be At least one lower case letter; At least one upper case letter; At least special character; At least one number; At least 8 characters length" ToolTip="The password should be At least one lower case letter; At least one upper case letter; At least special character; At least one number; At least 8 characters length" 
                            Display="Dynamic" ValidationGroup="CheckUserPasswordValidationGroup">*</asp:RegularExpressionValidator>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="CheckUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"   CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="CheckUserPasswordValidationGroup">*</asp:CompareValidator>
                    </p>
                </fieldset>

                <p class="submitButton">
                    <asp:Button ID="ResetPasswordPushButton" runat="server" CommandName="ResetPassword" Text="Change Password"  OnClick="btnChangePwd_Click"  ValidationGroup="CheckUserPasswordValidationGroup" CssClass="button"/>
                    <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="button" OnClick="CancelButton_Click"  />   
                </p>
        </div>



    </asp:ResetPassword>
</asp:Content>