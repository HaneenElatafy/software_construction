using System;
using Microsoft.AspNetCore.Mvc;
using system.Entities;
using system.Interface;

namespace system.Endpoints;


[ApiController]
[Route("api/song")]
public class SongController : ControllerBase
{

    private readonly ISongRepo _SongRepo;

    public SongController(ISongRepo SongRepo)
    {
        _SongRepo = SongRepo;
    }

    [HttpPost("AddSong")]
    public async Task<IActionResult> AddSong([FromBody] Song song)
    {
        if (song == null)
        {
            return BadRequest(new { message = "Invalid song data provided." });
        }

        try
        {
            await _SongRepo.AddSong(song); // AddGame modifies `vgame` directly
            return CreatedAtAction(nameof(AddSong), new { id = song.Id }, song);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }





    [HttpPut
    ("UpdateSong")]
    public async Task<IActionResult> UpdateSong(Song song)
    {


        await _SongRepo.UpdateSong(song);
        return Ok(song);
    }

    [HttpDelete("DeleteSong/{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
        var result = await _SongRepo.DeleteSong(id);

        if (!result)
        {
            return NotFound(new { message = "Song not found with the provided ID." });
        }

        return Ok(new { message = "Song deleted successfully." });
    }

    [HttpGet("GetAllSong")]
    public async Task<IActionResult> GetAllSong()
    {
        var song = await _SongRepo.GetAllSong();
        return Ok(song);
    }

    [HttpGet("GetSongById/{id}")]
    public async Task<IActionResult> GetSongById(int id)
    {
        var song = await _SongRepo.GetSongById(id);

        if (song == null)
        {
            return NotFound(new { message = "Song not found with the provided ID." });
        }

        return Ok(song);
    }

}


