using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StudentService.Core.Models
{
    public class Student : Entity
    {

        public Student() :base(true)
        {

        }

        [JsonPropertyName("fname")]
        public string FirstName { get; set; }

        [JsonPropertyName("lname")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("dob")]
        public DateTime DateOfBirth { get; set; }


        [JsonPropertyName("department")]
        public string Department { get; set; }
    }
}
