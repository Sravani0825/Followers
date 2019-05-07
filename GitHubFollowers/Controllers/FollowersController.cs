using System;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using System.Data;
using System.IO;

namespace GitHubFollowers.Controllers
{
    public class FollowersController : ApiController
    {
        public string Get()
        {
            return "Retrieves Followers of a GitHub account";
        }

        // Loop in JSON data retrieved and converted to datatable and call the function to get 3 levels deep data(upto 5 followers of each github account)
        [HttpGet, Route("api/Followers/GetFollowers/{id}")]
        public IHttpActionResult Get(string id)
        {
            DataTable dt = new DataTable();
            dt = GetGITHubFollowers(id);

            DataTable dtFollowers = new DataTable();
            dtFollowers.Merge(dt);

            DataTable dtL1 = new DataTable();
            DataTable dtL2 = new DataTable();
            DataTable dtL3 = new DataTable();
            string followerL1 = String.Empty;
            string followerL2 = String.Empty;
            string followerL3 = String.Empty;

            foreach (DataRow drow in dt.Rows)
            {
                followerL1 = drow["login"].ToString();
                dtL1 = GetGITHubFollowers(followerL1);

                dtFollowers.Merge(dtL1);

                foreach (DataRow drow1 in dtL1.Rows)
                {
                    followerL2 = drow1["login"].ToString();
                    dtL2 = GetGITHubFollowers(followerL2);

                    dtFollowers.Merge(dtL2);

                    foreach (DataRow drow2 in dtL2.Rows)
                    {
                        followerL3 = drow2["login"].ToString();
                        dtL3 = GetGITHubFollowers(followerL3);

                        dtFollowers.Merge(dtL3);
                    }
                }

            }
            string json = string.Empty;

            return Ok(new { res = dtFollowers });

        }

        [NonAction]
        public DataTable GetGITHubFollowers(string userid)
        {
            DataTable dtFollowers = new DataTable();
            // Retrieves upto 5 followers of a github account
            string apiUrl = "https://api.github.com/users/USERID/followers?per_page=5";
            apiUrl = apiUrl.Replace("USERID", userid);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            DataTable dt = new DataTable();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.UserAgent = "CL Proj App";
            // Basic authentication to pass in username and password of your github account
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("Username:Password")));
            string json;


            try
            {
                using (HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = httpResponse.GetResponseStream())
                    {
                        stream.ReadTimeout = 300000;
                        json = (new StreamReader(stream)).ReadToEnd();
                    }
                }
                dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

            }
            catch (WebException ex)
            {
                string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }

            return dt;
        }
    }
}
