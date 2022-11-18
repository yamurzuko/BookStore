using System;
using BookStore.DBOperations;
using BookStore.Entities;

namespace Tests.TestSetup
{
	public static class Authors
	{
		public static void AddAuthors(this BookStoreDBContext context)
		{
            context.Authors.AddRange(

                new Author
                {
                    Name = "Orhan",
                    Surname = "Veli",
                    BirthDate = new DateTime(1990, 10, 05)
                },
                new Author
                {
                    Name = "Ahmet",
                    Surname = "Ucar",
                    BirthDate = new DateTime(1960, 11, 24)
                },
                new Author
                {
                    Name = "Tuba",
                    Surname = "Güler",
                    BirthDate = new DateTime(1972, 08, 08)
                }
            );
        }
	}
}

