using System;
using Microsoft.EntityFrameworkCore;
using ProcessRUsAssessment.Data;
using ProcessRUsAssessment.Models;
using ProcessRUsAssessment.Shared.Responses;

namespace ProcessRUsAssessment.Services
{
    public class FruitsService
    {
        private readonly AppDbContext _dbContext;
        public FruitsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string[]> GetRandomFruitsAsync()
        {
            Random random = new Random();
            var fruitsInDb = await _dbContext.Fruits.ToListAsync();
            var fruits = fruitsInDb.OrderBy(x => random.Next())
                .Take(5)
                .ToList();

            var randomFruitList = new List<string>();
            foreach (var fruit in fruits)
            {
                randomFruitList.Add(fruit.Name);
            }

            string[] randomFruits = randomFruitList.ToArray();

            return randomFruits;
        }
    }
}

