﻿using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    private readonly EasyLearnDbContext _context;

    public StudentRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}