using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using BFS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GameController : ControllerBase
    {


        public GameController() { }

        [HttpPost]
        public IActionResult SolvePuzzle(InputData input)
        {
            int[,] matrix = input.GetTilesMatrix();
            Field field = new Field(matrix);
            BFSSolver solver = new BFSSolver(field);
            List<Field> fields = solver.Search();
            List<int[]> outputArrays = fields
                .Select(a => a.FlattenTiles())
                .ToList();
            OutputData outputData = new OutputData(outputArrays);
            return Ok(outputData);
        }
    }
}
