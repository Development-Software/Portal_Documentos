using System;
using System.Web.UI;
using System.IO;
using System.Net;

/// <summary>
/// Descripción breve de CASP
/// </summary>
public class CASP
{
    /** 
     * Authenticates a user with the given login and validation pages. After authentication 
     * the user's browser is redirected to the original page. 
     */

    public static String Authenticate(String LoginURL, String ValidateURL, Page Page)
    {
        return Authenticate(LoginURL, ValidateURL, Page, Page.Request.Url.AbsoluteUri.Split('?')[0]);
    }

    /** 
     * Authenticates a user with the given login and validation pages. After authentication 
     * the user's browser is redirected to the location given as the service URL. 
     */

    public static String Authenticate(String LoginURL, String ValidateURL, Page Page, String ServiceURL)
    {
        if (Page.Session["CASNetworkID"] != null) // user already logged in 
            return Page.Session["CASNetworkID"].ToString();
        else // user hasn't logged in 
        {
            if (Page.Request.QueryString["ticket"] != null) // ticket received 
            {
                try // read ticket and request validation 
                {
                    StreamReader Reader = new StreamReader(new WebClient().OpenRead(ValidateURL + "?ticket=" + Page.Request.QueryString["ticket"] + "&service=" + ServiceURL));

                    if ("yes".Equals(Reader.ReadLine())) // ticket validated 
                    {
                        // store network id in sesssion, return value 

                        return (String)(Page.Session["CASNetworkID"] = Reader.ReadLine());

                    }
                }
                catch (WebException) { }
            }

            // ticket was invalid, or didn't exist, so request ticket 

            Page.Response.Redirect(LoginURL + "?service=" + ServiceURL, true);
            return null;
        }
    }
}