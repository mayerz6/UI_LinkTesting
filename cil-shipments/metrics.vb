Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium
Imports System.Net
Imports System.Net.Http

Imports System.Collections.ArrayList
Imports OpenQA.Selenium.Support.UI

Public Class metrics

    Public driver As IWebDriver
    Public url As String

    Public siteTitle As String

    ' Based CONSTRUCTOR Sub to initiate our CLASS object
    Sub New()

        Dim chromeOptions As ChromeOptions = New ChromeOptions()
        chromeOptions.AddArguments("--headless")

        url = "https://www.canadapharmacyonline.com"
        driver = New ChromeDriver(chromeOptions)
        driver.Navigate().GoToUrl(url)

    End Sub

    ' Overloaded CONSTRUCTOR Sub to initiate our CLASS object based on a URL strings
    Sub New(ByVal url As String)
        Dim chromeOptions As ChromeOptions = New ChromeOptions()
        Me.url = url
        chromeOptions.AddArgument("--headless")

        driver = New ChromeDriver(chromeOptions)
        driver.Navigate().GoToUrl(Me.url)
        Dim wait As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(1))

    End Sub

    Sub LoadAboutPage()

        ' Based on which URL is entered locate the ABOUT link within the site's primary navigation
        Dim aboutPage As IWebElement = driver.FindElement(By.XPath("//*[@id='baloons']/div[1]/table/tbody/tr/td[1]/a"))
        aboutPage.Click()

    End Sub

    Function TestResults(ByVal links As List(Of IWebElement)) As String

        Dim httpClient As HttpClient = New HttpClient
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse
        Dim results As String = ""

        Dim i As Integer = 1

        For Each linkbutton As IWebElement In links

            ' Test condition for EMPTY link tags
            If linkbutton.GetAttribute("href") IsNot Nothing Then

                Try

                    Dim code As Integer

                    request = WebRequest.Create(linkbutton.GetAttribute("href").ToString())
                    request.Method = "HEAD"
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0"
                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
                    request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5")

                    response = request.GetResponse()
                    code = response.StatusCode

                    results += "<br>" + i.ToString()
                    results += " - <b class='link'>" + linkbutton.Text + "</b>"
                    results += " - " + linkbutton.GetAttribute("href").ToString()
                    results += " - <b class='success-code'>Status Code (" + code.ToString() + ") - OK!</b>"

                    i = i + 1

                    response.Close()
                    Continue For


                Catch ex As WebException

                    If ex.Message = "The remote server returned an error: (999) Request denied." Then

                        results += "<br>" + i.ToString()
                        results += " - <b class='link'>" + linkbutton.Text + "</b>"
                        results += " - " + linkbutton.GetAttribute("href").ToString()
                        results += " - <b class='warning-code'>Status Code (" + 999.ToString() + ") - Request Denied!</b>"
                        '     site_links.InnerHtml += " - <b>" + code.ToString() + "</b>"

                        i = i + 1
                        Continue For

                    ElseIf ex.Message = "The remote server returned an error: (404) Not Found." Then

                        results += "<br>" + i.ToString()
                        results += " - <b class='link'>" + linkbutton.Text + "</b>"
                        results += " - " + linkbutton.GetAttribute("href").ToString()
                        results += " - <b class='warning-code'>Status Code (" + 404.ToString() + ") - Page Not Found!</b>"
                        '     site_links.InnerHtml += " - <b>" + code.ToString() + "</b>"

                        i = i + 1
                        Continue For

                    ElseIf ex.Message = "The remote server returned an error: (403) Forbidden." Then

                        results += "<br>" + i.ToString()
                        results += " - <b class='link'>" + linkbutton.Text + "</b>"
                        results += " - " + linkbutton.GetAttribute("href").ToString()
                        results += " - <b class='warning-code'>Status Code (" + 403.ToString() + ") - Forbidden Page Found!</b>"

                        i = i + 1
                        Continue For

                    Else


                        results += "<br>" + i.ToString()
                        results += " - <b>" + linkbutton.Text + "<b>"
                        results += " - " + linkbutton.GetAttribute("href").ToString()
                        results += " - <b>" + ex.Message.ToString() + "</b>"
                        '     site_links.InnerHtml += " - <b>" + code.ToString() + "</b>"

                        i = i + 1
                        Continue For

                    End If

                Catch ex As System.UriFormatException
                    If ex.Message = "Invalid URI: The URI is empty." Then
                        results += "<br>" + i.ToString()
                        results += " - <b class='link'>" + linkbutton.Text + "</b>"
                        results += " - <b class='error-code'>Link Tag has no associated URL - Link Error!</b>"
                        '     site_links.InnerHtml += " - <b>" + code.ToString() + "</b>"

                        i = i + 1
                        Continue For
                    End If

                End Try

            Else

                i = i + 1
                Continue For

            End If


        Next

        Return results

    End Function

    Function LinkTest() As List(Of IWebElement)

        Dim list As New List(Of IWebElement)
        Dim validURL As New ArrayList(list)
        Dim brokenURL As New ArrayList(list)

        '    driver.FindElement(By.Id("tblRenewalAgent")).FindElements(By.TagName("div"))
        list = driver.FindElements(By.TagName("a")).ToList()

        Return list

    End Function

    Function BlogLinkTest() As List(Of IWebElement)

        Dim list As New List(Of IWebElement)

        ' A few LIST variables meant to store specific link tags.
        Dim validURL As New ArrayList(list)
        Dim brokenURL As New ArrayList(list)

        ' Select element QUERY designed to locate ALL link tags within a standard BLOG post
        ' BLOG posts have a unique container DIV tag with a CLASS of CONTENT
        list = driver.FindElements(By.CssSelector("div#content a")).ToList()

        Return list

    End Function


    Function CPO_BlogLinkTest() As List(Of IWebElement)

        Dim list As New List(Of IWebElement)

        ' A few LIST variables meant to store specific link tags.
        Dim validURL As New ArrayList(list)
        Dim brokenURL As New ArrayList(list)

        ' Select element QUERY designed to locate ALL link tags within a standard BLOG post
        ' BLOG posts have a unique container DIV tag with a CLASS of CONTENT
        list = driver.FindElements(By.CssSelector("div.content a")).ToList()

        Return list

    End Function


    Function CPK_BlogLinkTest() As List(Of IWebElement)

        Dim list As New List(Of IWebElement)

        ' A few LIST variables meant to store specific link tags.
        Dim validURL As New ArrayList(list)
        Dim brokenURL As New ArrayList(list)

        ' Select element QUERY designed to locate ALL link tags within a standard BLOG post
        ' BLOG posts have a unique container DIV tag with a CLASS of CONTENT
        list = driver.FindElements(By.CssSelector("div#content a")).ToList()

        Return list

    End Function

    Function CPW_BlogLinkTest() As List(Of IWebElement)

        Dim list As New List(Of IWebElement)

        ' A few LIST variables meant to store specific link tags.
        Dim validURL As New ArrayList(list)
        Dim brokenURL As New ArrayList(list)

        ' Select element QUERY designed to locate ALL link tags within a standard BLOG post
        ' BLOG posts have a unique container DIV tag with a CLASS of CONTENT
        list = driver.FindElements(By.CssSelector("div#content a")).ToList()

        Return list

    End Function

    Function GetSiteTitle() As String

        Me.siteTitle = Me.driver.Title
        Return Me.siteTitle

    End Function

    Sub CloseInstance()

        Me.driver.Close()
        Me.driver.Quit()

    End Sub

End Class
