using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMotownFestival.Api.Data;
using RMotownFestival.Api.Domain;
using RMotownFestival.DAL;

namespace RMotownFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FestivalController : ControllerBase
    {
        private MotownDbContext _context;
        private TelemetryClient _telemetryClient;

        public FestivalController(MotownDbContext context, TelemetryClient telemetryClient)
        {
            _context = context;
            _telemetryClient = telemetryClient;
        }

        [HttpGet("LineUp")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Schedule))]
        public ActionResult GetLineUp()
        {
            return Ok(FestivalDataSource.Current.LineUp);
        }

        [HttpGet("Artists")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Domain.Artist>))]
        public async Task<ActionResult> GetArtists(bool? withRatings)
        {
            var artists = await _context.Artists.ToListAsync();

            if(withRatings.HasValue && withRatings.Value)
            {
                _telemetryClient.TrackEvent("With ratings");
            }
            else
            {
                _telemetryClient.TrackEvent("Without ratings");
            }

            return Ok(FestivalDataSource.Current.Artists);
        }

        [HttpGet("Stages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Domain.Stage>))]
        public ActionResult GetStages()
        {
            return Ok(FestivalDataSource.Current.Stages);
        }

        [HttpPost("Favorite")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ScheduleItem))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult SetAsFavorite(int id)
        {
            var schedule = FestivalDataSource.Current.LineUp.Items
                .FirstOrDefault(si => si.Id == id);
            if (schedule != null)
            {
                schedule.IsFavorite = !schedule.IsFavorite;
                return Ok(schedule);
            }
            return NotFound();
        }

    }
}