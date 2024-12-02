using System;
using Microsoft.EntityFrameworkCore;
using system.Entities;

namespace system.context;

public class SongContext:DbContext
{
    public SongContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
    public DbSet<Song> Songs => Set<Song>();

   
}
