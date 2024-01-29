﻿using DailyPlanner.Domain.Enums;
using DailyPlanner.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Services.DateAndTime;

namespace DailyPlanner.Infrastructure.Data
{
    public class DailyPlannerDbInitializer
    {
        private readonly DailyPlannerDbContext _context;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<DailyPlannerDbInitializer> _logger;

        public DailyPlannerDbInitializer(DailyPlannerDbContext context,
            ILogger<DailyPlannerDbInitializer> logger, IDateTimeService dateTimeService)
        {
            _logger = logger;
            _context = context;
            _dateTimeService = dateTimeService;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Database.MigrateAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database");
                throw;
            }
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await TrySeedAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }

        private async Task TrySeedAsync(CancellationToken cancellationToken = default)
        {
            if (!_context.Boards.Any())
            {
                await _context.Boards.AddAsync(new Board
                {
                    Title = "Daily Planner",
                    IsPrivate = true,
                    IsFavorite = true,
                    CreatedBy = new("26D3DD11-C40A-463A-A90B-1E2FCFFB4AE9"),
                    Columns = new List<Column>
                    {
                        new() { Title = "Is done" },
                        new() {
                            Title = "Needs to be done",
                            Cards = new List<Card>
                            {
                                new() {
                                    Title = "Create Identity project",
                                    Description = "Сreate an API that will authenticate and authorize users and store their personal data",
                                    Priority = CardPriority.Medium,
                                    StartDate = _dateTimeService.Now.AddDays(5),
                                    EndDate = _dateTimeService.Now.AddDays(14),
                                },
                                new() {
                                    Title = "Create Client",
                                    Description = "Сreate a React project through which users will interact with the application",
                                    Priority = CardPriority.Medium,
                                    StartDate = _dateTimeService.Now.AddDays(14),
                                }
                            }
                        },
                        new() {
                            Title = "In progress",
                            Cards = new List<Card>
                            {
                                new() {
                                    Title = "Сreate tests",
                                    Description = "Сreate unit and integration tests for each required level of the application",
                                    Priority = CardPriority.High,
                                    StartDate = _dateTimeService.Now,
                                    EndDate = _dateTimeService.Now.AddDays(7),
                                }
                            }
                        },
                    }
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}