using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using StudentService.Core.Interfaces;
using StudentService.Core.Models;

namespace StudentService.Function
{
    public  class StudentDataUpload
    {
        private readonly IStudentRepository _studentRepository;

        public StudentDataUpload(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [FunctionName("StudentDataUpload")]
        public async Task Run([BlobTrigger("studentdata/{name}", Connection = "")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            if (myBlob.Length > 0)
            {
                using (var reader = new StreamReader(myBlob))
                {
                    var currentNumber = 1;
                    var currentLine = await reader.ReadLineAsync();
                   
                    while (currentLine != null)
                    {
                      await  ProcessCurrentLineAsync(name, currentLine, currentNumber, log);                       
                        currentLine = await reader.ReadLineAsync();
                        currentNumber++;
                        
                    }
                }
            }
        }

        private async  Task ProcessCurrentLineAsync(string name, string line, int lineNumber, ILogger log)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                log.LogInformation($"{name}: {lineNumber} is empty.");
                return ;
            }

            var studentData = line.Split(',');
            if (studentData.Length != 6)
            {
                log.LogError($"{name}: {lineNumber} invalid data: {line}.");
                return  ;
            }

            if(!DateTime.TryParse(studentData[4], out DateTime dob))
            {
                log.LogError($"{name}: {lineNumber} Invalid DOB: {studentData[4]}.");
                return ;
            }
             var student =   new Student()
            {
                FirstName = studentData[0],
                LastName = studentData[1],
                Email = studentData[2],
                PhoneNumber = studentData[3],
                DateOfBirth = dob,
                Department  = studentData[5]
            };
           await _studentRepository.AddAsync(student, new PartitionKey(student.Department));
           log.LogInformation($"{name}: {lineNumber} Added student: {student.LastName} {student.FirstName} with id: {student.Id} .");
                      

        }


       

    }
}
