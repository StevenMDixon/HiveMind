using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiveMind.Server.Models;


[Table("drone_data")]
public class Drone
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Required, Column("host_name")]
    public string HostName { get; set; }
}
