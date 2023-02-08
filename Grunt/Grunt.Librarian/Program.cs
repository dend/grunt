using OpenSpartan.Grunt.Core;
using OpenSpartan.Grunt.Librarian.Models;
using OpenSpartan.Grunt.Models.ApiIngress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSpartan.Grunt.Librarian
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Grunt Librarian - Halo Infinite API Indexer");
            Console.WriteLine("Developed by Den Delimarsky in 2022. Part of https://gruntapi.com");

            Halo5Client client = new(string.Empty, string.Empty);

            Configuration? container = new();

            Task.Run(async () =>
            {
                container = (await client.GetApiSettingsContainer()).Result!;
            }).GetAwaiter().GetResult();

            if (container != null && container.Endpoints != null)
            {
                List<ExportableFunction> functions = new();

                foreach (KeyValuePair<string, OnlineUriReference> endpoint in container.Endpoints)
                {
                    Authority endpointAuthority = (from c in container.Authorities where string.Equals(c.Key, endpoint.Value.AuthorityId, StringComparison.InvariantCultureIgnoreCase) select c).First().Value;
                    var endpointNamePieces = endpoint.Key.Split('_');

                    ExportableFunction func = new()
                    {
                        Name = endpoint.Key.Replace("_", ""),
                        EndpointPath = endpoint.Value.Path,
                        EndpointId = endpoint.Key,
                        AuthorityHost = endpointAuthority.Hostname,
                        AuthorityPort = endpointAuthority.Port,
                        QueryString = endpoint.Value.QueryString,
                        RequiresClearance = endpoint.Value.ClearanceAware,
                        RequiresSpartanToken = endpointAuthority.AuthenticationMethods is not null && endpointAuthority.AuthenticationMethods.Contains(AuthenticationMethod.SpartanTokenV4),
                    };

                    switch (func.Name)
                    {
                        case string s when s.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase):
                            func.Method = HttpMethod.Get;
                            break;
                        case string s when s.StartsWith("Post", StringComparison.InvariantCultureIgnoreCase):
                            func.Method = HttpMethod.Post;
                            break;
                        case string s when s.StartsWith("Delete", StringComparison.InvariantCultureIgnoreCase):
                            func.Method = HttpMethod.Delete;
                            break;
                        case string s when s.StartsWith("Upload", StringComparison.InvariantCultureIgnoreCase):
                            func.Method = HttpMethod.Post;
                            break;
                        case string s when s.StartsWith("Put", StringComparison.InvariantCultureIgnoreCase):
                            func.Method = HttpMethod.Put;
                            break;
                        default:
                            func.Method = HttpMethod.Get;
                            func.NeedsIntervention = true;
                            break;
                    }

                    func.Class = string.Concat(endpointNamePieces.Take(endpointNamePieces.Length - 1));
                    functions.Add(func);
                }

                var functionStub =
                @"public async Task<string> {0} ({1}) {{
                    var response = await ExecuteAPIRequest($""https://{2}:{3}{4}{5}"",
                                           HttpMethod.{6},
                                           {7},
                                           {8},
                                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

                    if (!string.IsNullOrEmpty(response))
                    {{
                        return response;
                    }}
                    else
                    {{
                        return string.Empty;
                    }}
                }}";

                var entityRegex = new Regex(@"\{(.*?)\}");

                foreach (var func in functions)
                {
                    if (func is not null)
                    {
                        var endpointCombo = string.Concat(func.EndpointPath, func.QueryString);
                        var functionParameters = entityRegex.Matches(endpointCombo);
                        var arguments = string.Empty;

                        if (functionParameters.Count > 0)
                        {
                            foreach (Match match in functionParameters.Cast<Match>())
                            {
                                arguments += "string " + match.Groups[1].Value.ToString() + ", ";
                            }
                            arguments = arguments.Trim().TrimEnd(',');
                        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        var functionCode = string.Format(functionStub,
                                                         func.Name,
                                                         arguments,
                                                         func.AuthorityHost,
                                                         func.AuthorityPort,
                                                         func.EndpointPath,
                                                         func.QueryString,
                                                         Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(func.Method != null ? func.Method.ToString().ToLower() : "GET"),
                                                         func.RequiresSpartanToken is not null ? func!.RequiresSpartanToken!.ToString().ToLower() : true,
                                                         func.RequiresClearance is not null ? func!.RequiresClearance!.ToString().ToLower() : false
                                                         );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                        if (func.NeedsIntervention == true)
                        {
                            functionCode = "//TODO: This function requires manual invtervention/checks.\n" + functionCode;
                        }

                        func.FunctionCode = functionCode;
                    }
                }

                var combinations = from f in functions
                                   group f.FunctionCode by f.Class into g
                                   select new { Class = g.Key, Functions = g.ToList() };

                foreach(var combo in combinations)
                {
                    Console.WriteLine("//================================================");
                    Console.WriteLine("// " + combo.Class);
                    Console.WriteLine("//================================================");

                    foreach(var function in combo.Functions)
                    {
                        Console.WriteLine(function);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
