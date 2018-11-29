using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Service;

namespace View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var crud = new BookCrud();

            crud.TestGetById();
        }
    }
}