using Azure.Cosmos;
using StudentService.Core.Interfaces;
using StudentService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentService.Infrastructure.Data
{
    public class StudentRepository : GenericRepository<Student>,IStudentRepository
    {

        private readonly CosmosClient _cosmosClient;
        public StudentRepository(CosmosClient cosmosClient)
                    :base(cosmosClient )
        {
            _cosmosClient = cosmosClient;
        }
        public override string DatabaseId => "student";

        public override string ContainerId => "studentcollection";

        public override string PartitionKey => nameof(Student.Department).ToLower();
    }
}
