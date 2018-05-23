using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace WebServer.Application.Models
{
    public class Survey
    {
        private string firstname;
        private string lastname;
        private string birthday;
        private string gender;
        private string status;
        private string recommendations;
        private List<string> own;

        public Survey(string firstname, string lastname, string birthday, string gender,
            string status, string recommendations, List<string> own)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.birthday = birthday;
            this.gender = gender;
            this.status = status;
            this.recommendations = recommendations;
            this.own = own;
        }
        
        public bool HasValidData()
        {
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(birthday))
            {
                return false;
            }
            this.SaveInformationInFile();
            return true;
        }

        private void SaveInformationInFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            var filePath = newPath + @".\Application\Data\survey-results.txt";
            
            using(var streamWriter=new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(this.ToString());
            }
        }

        public override string ToString()
        {
            return $"FirstName: {this.firstname}, LastName: {this.lastname}, Birthday: {this.birthday}, " +
                $"Gender: {this.gender}, Status: {this.status}, Recommendations: {this.recommendations}, "
                + $"Own: [{string.Join(", ",own)}]";
        }
    }
}
