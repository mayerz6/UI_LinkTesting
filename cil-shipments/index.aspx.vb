Imports System.Text
Imports System.IO
Imports System.Web

Imports System.Net.Http
Imports System.Net
Imports System
Imports OpenQA.Selenium
Imports cil_shipments.metrics

Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim cpk_url As String
        Dim cpw_url As String
        Dim cpo_url As String

        Dim list As New List(Of String)
        Dim validURL As New ArrayList(List)

        '  url = "https://www.canadianpharmacyking.com/KingsBlog/index.php/2019/12/Canadian-Pharmacy-King-Introduces-the-New-Product-for-PAH-OPSUMIT/"
        '  url = "https://www.canadianpharmacyking.com/KingsBlog/index.php/2019/05/Antidepressants-vs-Antipsychotics-Whats-the-Difference/"
        '  url = "https://www.canadianpharmacyking.com/KingsBlog/index.php/2018/12/19-Most-Prescribed-Pediatric-Medications-and-What-They-Do/"
        cpk_url = "https://www.canadianpharmacyking.com/KingsBlog/index.php/2018/10/Can-CBD-Balance-Your-Mental-Health-and-Slash-Impotence/"
        cpw_url = "https://www.canadianpharmacyworld.com/blog/heres-why-you-should-be-taking-nutrazul-vitamin-c-during-quarantine"
        cpo_url = "https://www.canadapharmacyonline.com/blog/When-Pessimism-Can-Make-You-Sick-The-Nocebo-Effect.html"




        '   feedback.InnerHtml = "<b class='feedback-styles'>" + site.GetSiteTitle() + "</b>"

        ' Instantiate an INSTANCE of the metrics OBJECT based on a specific URL
        Dim res As String
        Dim links As New List(Of IWebElement)
        Dim site As New metrics(cpk_url)

        ' Request the contents of the site's TITLE tag using our metrics OBJECT
        site_links.InnerHtml += "<br><br><hr><br>"
        site_links.InnerHtml += "<b class='feedback-styles'>" + site.GetSiteTitle() + "</b>"

        site_links.InnerHtml += "<br><br>"

        links = site.CPW_BlogLinkTest()

        res = site.TestResults(links)
        site_links.InnerHtml += res
        site.CloseInstance()



        ' Instantiate an INSTANCE of the metrics OBJECT based on a specific URL
        Dim res_1 As String
        Dim links_1 As New List(Of IWebElement)
        Dim site_1 As New metrics(cpw_url)

        ' Request the contents of the site's TITLE tag using our metrics OBJECT
        site_links.InnerHtml += "<br><br><hr><br>"
        site_links.InnerHtml += "<b class='feedback-styles'>" + site_1.GetSiteTitle() + "</b>"

        site_links.InnerHtml += "<br><br>"

        links_1 = site_1.CPW_BlogLinkTest()

        res_1 = site_1.TestResults(links_1)
        site_links.InnerHtml += res_1
        site_1.CloseInstance()


        ' Instantiate an INSTANCE of the metrics OBJECT based on a specific URL
        Dim res_2 As String
        Dim links_2 As New List(Of IWebElement)
        Dim site_2 As New metrics(cpo_url)

        ' Request the contents of the site's TITLE tag using our metrics OBJECT
        site_links.InnerHtml += "<br><br><hr><br>"
        site_links.InnerHtml += "<b class='feedback-styles'>" + site_2.GetSiteTitle() + "</b>"

        site_links.InnerHtml += "<br><br>"

        links_2 = site_2.CPO_BlogLinkTest()
        res_2 = site_2.TestResults(links_2)
        site_links.InnerHtml += res_2
        site_2.CloseInstance()






    End Sub

End Class