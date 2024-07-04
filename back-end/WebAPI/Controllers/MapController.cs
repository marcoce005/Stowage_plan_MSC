using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/container")]      //     http://localhost:5000/api/container/
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public class cell
        {
            public int id { get; set; }
            public int discharge { get; set; }
            public float weight { get; set; }
            public float dimension { get; set; }
        }

        [HttpPost("GetStowagePlan")]
        public string GetStowagePlan(int x, int y, int z, [FromBody] List<cell> cellList)
        {
            float[] tons = new float[x * y * z];
            int[] priority = new int[x * y * z];
            int[] id = new int[x * y * z];

            for (int i = 0; i < x * y * z; i++)
            {
                id[i] = cellList[i].id;
                tons[i] = cellList[i].weight;
                priority[i] = cellList[i].discharge;
            }

            Test.Program p = new Test.Program(x, y, z, cellList[0].dimension);
            int[,,] mat = p.ex1(tons, priority, id);
            string result = "";
            result += "\n\nDisposizione container tramite ID";
            for (int i = 0; i < z; i++)
            {
                result += "\n\nLayer " + i;
                for (int j = 0; j < y; j++)
                {
                    result += "\n";
                    for (int k = 0; k < x; k++)
                        result += mat[k, j, i] + "\t";
                }
            }
            return result;
        }
    }
}