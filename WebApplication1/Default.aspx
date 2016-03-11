<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:Button Text="Button" ID="btn" OnClick="btn_Click" runat="server" />
    <asp:Label ID="lbl" runat="server" />
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px"></asp:DetailsView>--%>
    <div>
        <%--<asp:ScriptManager runat="server" ID="sm1" />
        <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <p>Update Panel 1 <%=((int)Application["PagerequestCount"]).ToString() %></p>                
                <asp:UpdatePanel runat="server" ID="up2">
                    <ContentTemplate>
                        <asp:Button Text="Button" ID="btn" OnClick="btn_Click" runat="server" />
                        <p>Update Panel 2 <%=((int)Application["PagerequestCount"]).ToString() %></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <hr />
        <asp:UpdatePanel runat="server" ID="up3" UpdateMode="Conditional">
            <ContentTemplate>
                <p>Update Panel 3 <%=((int)Application["PagerequestCount"]).ToString() %></p>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <%--<asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
                <p>UP1</p>
                <asp:UpdatePanel runat="server" ID="up2" ChildrenAsTriggers="false" UpdateMode="">
                    <ContentTemplate>
                        <p>Up2</p>
                        <asp:UpdatePanel runat="server" ID="up3">
                            <ContentTemplate>
                                <p>UP3</p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>--%>

        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <asp:Timer runat="server" ID="Timer1" Interval="10000" ></asp:Timer>
                <asp:Label runat="server" Text="Page not refreshed yet." ID="Label1">
                </asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label runat="server" Text="Label" ID="Label2"></asp:Label>
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
