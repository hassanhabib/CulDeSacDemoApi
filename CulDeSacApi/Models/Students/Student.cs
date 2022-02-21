using System;
using System.Collections;
using System.Collections.Generic;
using CulDeSacApi.Models.LibraryAccounts;

namespace CulDeSacApi.Models.Students
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public LibraryAccount LibraryAccount { get; set; }
    }
}
