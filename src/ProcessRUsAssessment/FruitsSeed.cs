using System;
using Microsoft.EntityFrameworkCore;
using ProcessRUsAssessment.Data;
using ProcessRUsAssessment.Models;

namespace ProcessRUsAssessment
{
    public static class FruitsSeed
    {
        static string[] fruits = new string[] { "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grapefruit", "Honeydew Melon", "Indian Gooseberry", "Jackfruit", "Kiwi", "Lemon", "Mango", "Nectarine", "Orange", "Papaya", "Quince", "Raspberry", "Strawberry", "Tangerine" };
        public static async Task SeedFruitsAsync(AppDbContext dbContext)
        {
            if (!await dbContext.Fruits.AnyAsync())
            {
                fruits.ToList().ForEach(fruit => dbContext.Add(new Fruit(fruit)));
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

