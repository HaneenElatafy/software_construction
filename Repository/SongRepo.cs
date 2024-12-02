

using Microsoft.EntityFrameworkCore;
using system.context;
using system.Entities;
using system.Interface;
using System.Collections.Generic; // For IEnumerable
using System.Threading.Tasks; // For async/await

namespace system.Repository;


public class SongRepo : ISongRepo
{
    private readonly SongContext _context;

    public SongRepo(SongContext context)
    {
        _context = context;
    }

    public async Task<Song> AddSong(Song song)
    {
        var existingSong = await _context.Songs.FirstOrDefaultAsync(g => g.Name == song.Name);
        if (existingSong == null)
        {
            song.Id = 0; // to ensure EF Core treats it as a new record
            await _context.Songs.AddAsync(song);
            _context.SaveChanges();
            return song;
        }

        else
        {
            throw new Exception($"A song with the name '{song.Name}' already exists.");
        }

    }

    public async Task<Song> UpdateSong(Song song)
    {

        // Find the existing song by Id
        var existingSong = await _context.Songs.FindAsync(song.Id);

        if (existingSong == null)
        {
            throw new KeyNotFoundException($"song with Id {song.Id} not found.");
        }

        if (song.Name != null)
        {
            existingSong.Name = song.Name;
        }
        if (song.Artist != null)
        {
            existingSong.Artist = song.Artist;
        }

        // Save changes to the database
        await _context.SaveChangesAsync();

        return existingSong;
    }

    public async Task<bool> DeleteSong(int id)
    {

        var song = await _context.Songs.FindAsync(id);
        if (song == null)
        {
            return false; // song not found
        }

        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();
        return true; // song deleted successfully
    }

    public async Task<IEnumerable<Song>> GetAllSong()
    {
        return await _context.Songs.ToListAsync();
    }

    public async Task<Song?> GetSongById(int id)
    {
        return await _context.Songs.FindAsync(id);
    }

}

