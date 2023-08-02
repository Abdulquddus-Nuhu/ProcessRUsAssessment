using System;
using Microsoft.EntityFrameworkCore;
using ProcessRUsAssessment.Data;
using ProcessRUsAssessment.Models;
using ProcessRUsAssessment.Shared.Responses;

namespace ProcessRUsAssessment.Services
{
    public class FruitsRepository
    {
        private readonly AppDbContext _dbContext;
        public FruitsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FruitResponse>> GetRandomFruitsAsync()
        {
            var fruits = await _dbContext.Fruits.ToListAsync();

            Random random = new Random();
            var randomFruits = fruits.OrderBy(x => random.Next())
                .Take(5)
                .Select(x => new FruitResponse { Name = x.Name})
                .ToList();

            return randomFruits;
        }
    }
}

