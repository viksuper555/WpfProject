namespace WpfProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using WpfProject.Common;
    using WpfProject.Models;

    public class RequestService : IRequestService
    {
        public ObservableCollection<string> GetAddressCandidates(string name)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            if (string.IsNullOrEmpty(name))
            {
                return result;
            }

            string url = Constants.DefaultUrl
                + "findAddressCandidates?SingleLine="
                + name
                + "&f=json";

            string json = this.SendGetRequest(url);
            AddressCandidateObject addressCandidate = Newtonsoft.Json.JsonConvert.DeserializeObject<AddressCandidateObject>(json);
            foreach (var c in addressCandidate.candidates)
            {
                if (result.Contains(c.address))
                {
                    continue;
                }

                result.Add(c.address);
            }

            return result;
        }

        public ObservableCollection<string> GetSuggestions(string name)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            if (string.IsNullOrEmpty(name))
            {
                return result;
            }

            string url = Constants.DefaultUrl
                + "suggest?f=json&text="
                + name;

            string json = this.SendGetRequest(url);
            SuggestionObject suggestion = Newtonsoft.Json.JsonConvert.DeserializeObject<SuggestionObject>(json);

            foreach (var s in suggestion.suggestions)
            {
                result.Add(s.text);
            }

            return result;
        }

        private string SendGetRequest(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            request.ContentType = "application/json";
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Request rejected!");
                }

                string result = string.Empty;
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd().Replace("\0", string.Empty);
                }

                return result;
            }
        }
    }
}
