using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace system.Entities;

public class Song
{
[Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
// Ensures auto-increment behavior
public int Id { get;set;}

public required  string? Name {get;set;}

public  string? Artist {get;set;}



}
