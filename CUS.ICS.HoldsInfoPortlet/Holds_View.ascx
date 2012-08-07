<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Holds_View.ascx.cs" Inherits="CUS.ICS.HoldsInfo.Holds_View" %>

<%@ Register 
    TagPrefix="common"
	assembly="Jenzabar.Common"
	Namespace="Jenzabar.Common.Web.UI.Controls"
%>

<div class="pSection">
 	<p class="pContent">Holds prevent you from being able to register for classes. Make sure you have all your holds cleared before your scheduled day of registration arrives.</p>
 	<br />
 	<asp:Label ID="lblMsg" runat="server" />
     <br />
    <asp:DataList ID="dataListHolds" runat="server" Width="100%"> 
        <ItemTemplate>
            <h6><asp:Label ID="Label3" runat="server" text='<%# Eval("HOLD_DESC") + ", " + Eval("Building") + ", " + Eval("Phone")%>' /></h6>
        </ItemTemplate>
    </asp:DataList>

	    <common:ErrorDisplay visible="false" ID="lblError" runat="server" />
</div>