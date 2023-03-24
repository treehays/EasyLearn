﻿using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}