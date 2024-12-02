using System;
using system.Entities;

namespace system.Interface;

public interface ISongRepo
{

    Task<Song> AddSong(Song song);
    public Task<Song> UpdateSong(Song song);

    Task<bool> DeleteSong(int id);

    Task<IEnumerable<Song>> GetAllSong(); // Get all song
    Task<Song?> GetSongById(int id);       // Get a song by ID

}
