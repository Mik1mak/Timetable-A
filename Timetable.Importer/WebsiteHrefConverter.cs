using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.API.DTO.InputModels;
using TimetableA.API.DTO.OutputModels;

namespace TimetableA.Importer
{
    public static class WebsiteHrefConverter
    {
        public static string GetHref(this AuthenticateResponse response, string baseUrl, IEnumerable<int> addedGroupsIds)
        {
            return $"{baseUrl}/?id={response.Id}" +
                $"&key={response.EditKey}" +
                $"&returnUrl=%2F%3F{string.Join("%26", addedGroupsIds.Select(id => $"g%3D{id}"))}";
        }

        public static AuthenticateRequest GetLoginInfo(this string url)
        {
            AuthenticateRequest loginInfo = new();

            string urlQuery = new Uri(url).Query;

            foreach (string param in urlQuery.Split('&'))
            {
                string[] splitedParam = param.TrimStart('?').Split('=');

                if(splitedParam[0] == "id")
                {
                    loginInfo.Id = int.Parse(splitedParam[1]);
                }
                else if(splitedParam[0] == "key")
                {
                    loginInfo.Key = splitedParam[1];
                }
            }
            
            return loginInfo;
        }
    }
}
