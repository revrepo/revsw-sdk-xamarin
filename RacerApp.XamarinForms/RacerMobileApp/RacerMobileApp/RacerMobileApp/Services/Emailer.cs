using Plugin.Messaging;
using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.Services
{
  public  class Emailer
    {


        public static void SendEmail( SessionResult sessionResult, string addressee, string subject)
        {
            var emailTask = MessagingPlugin.EmailMessenger;
            if (emailTask.CanSendEmail)
            {
              
                var email = new EmailMessageBuilder()
                  .To(addressee)                 
                  .Subject(subject)
                  .BodyAsHtml(CreateHtml(sessionResult))
                  .Build();

                emailTask.SendEmail(email);
            }
        }

       public static string CreateHtml(SessionResult sessionResult)
        {
            var html =
                start +
                 "<h3>" + sessionResult.Uri + " </h3>" +
                 "<h3>" + sessionResult.Date.ToString() + "</h3>" +
                 "<table>" +
                 "<tr>" +
                       "<td> № </td>" +
                       "<td> RevAPM </td>" +
                       "<td> Default </td>" +
                 "</tr>";

            for (int i = 0; i < sessionResult.DetailedReportList.Count; i++)
            {
                var report = sessionResult.DetailedReportList[i];

                html += "<tr>" +
                       "<td>"+ (i+1).ToString() + "</td>" +
                       "<td>"+ report.RevDuration +"("+report.RevResponseSizeBytes+")"+"</td>" +
                       "<td>" + report.Duration + "(" + report.DefaultResponseSizeBytes + ")" + "</td>" +
                 "</tr>";
            }

            html += "</table>" +
                    "<h3> Summary </h3>";

            html += "<table>" +
                    "<tr>" +
                       "<td> Ms</td>" +
                       "<td> RevAPM </td>" +
                       "<td> Default </td>" +
                    "</tr>" +
                    "<tr>" +
                       "<td> Min </td>" +
                       "<td>" + StaticticsCalculator.CalculateMinValue(sessionResult.RevTestsResult) + "</td>" +
                       "<td>" + StaticticsCalculator.CalculateMinValue(sessionResult.DefaultTestsResult) + "</td>" +
                    "</tr>" +
                    "<tr>" +
                       "<td> Max </td>" +
                       "<td>" + StaticticsCalculator.CalculateMaxValue(sessionResult.RevTestsResult) + "</td>" +
                       "<td>" + StaticticsCalculator.CalculateMaxValue(sessionResult.DefaultTestsResult) + "</td>" +
                    "</tr>" +
                    "<tr>" +
                       "<td> Average </td>" +
                       "<td>" + StaticticsCalculator.CalculateAverageValue(sessionResult.RevTestsResult) + "</td>" +
                       "<td>" + StaticticsCalculator.CalculateAverageValue(sessionResult.DefaultTestsResult) + "</td>" +
                    "</tr>" +
                    "<tr>" +
                       "<td> Mediana </td>" +
                       "<td>" + StaticticsCalculator.CalculateMedianaValue(sessionResult.RevTestsResult) + "</td>" +
                       "<td>" + StaticticsCalculator.CalculateMedianaValue(sessionResult.DefaultTestsResult) + "</td>" +
                    "</tr>" +
                    "<tr>" +
                       "<td> Stand. Deviation </td>" +
                       "<td>" + StaticticsCalculator.CalculateStandardDeviation(sessionResult.RevTestsResult) + "</td>" +
                       "<td>" + StaticticsCalculator.CalculateStandardDeviation(sessionResult.DefaultTestsResult) + "</td>" +
                    "</tr>" +
                    "<tr>" +
                       "<td> Expected value </td>" +
                       "<td>" + StaticticsCalculator.CalculateWeighteedAverage(sessionResult.RevTestsResult) + "</td>" +
                       "<td>" + StaticticsCalculator.CalculateWeighteedAverage(sessionResult.DefaultTestsResult) + "</td>" +
                    "</tr>" +
                  "</table>" +
              "</html>";

            return html;



        }

        private const string start = @"<html>

                        <body>
                        <style>
                        h1 {
                          color: black;
                         margin: 16px;
                         font - size:300 %;
                         }
                         th, td {
                         border: 1px solid black;
                         }                     
                        table {
                        color: black; 
                        font - size:100 %;
                        margin: 16;     
                        width: 100%;   
                        border: 1px solid black;              
                         }
                        
                         </style>";
        
    }
}
