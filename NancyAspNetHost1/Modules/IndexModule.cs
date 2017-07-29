﻿using System;
using System.IO;
using Nancy;
using Microsoft.AspNet.SignalR.StockTicker;
using Newtonsoft.Json;

namespace NancyAspNetHost1.Modules
{
    public class IndexModule : NancyModule
    {
        static string GlobalLog;
        public IndexModule()
        {
            Get["/MessageCache"] = _ => JsonConvert.SerializeObject( InMemoryMessageCache.Get());
            Get["/Report"] = parameters =>
			{
				return View["report"];
			};
            Get["/QueryTicker"] = parameters =>
            {
                return View["QueryTicker"];
            };
            Get["/"] = parameters => { return HandleQuery(parameters); };


        }

        private String HandleQuery(dynamic parameters)
        {
            String query = "Query: ";
            query += Request.Url.Query;


            WriteToLog(query) ;
            return query;
        }

        private void WriteToLog(string log)
        {


            StockTicker stockeTicker = new StockTicker();
            stockeTicker.SendMessage(log);
          

            System.Diagnostics.Debug.WriteLine(log);

        }
    }
}
