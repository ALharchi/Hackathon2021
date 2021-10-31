using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Roy_T.AStar.Graphs;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Paths;
using System.IO;
using Newtonsoft.Json;

namespace PollutionSolution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PathController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] int start, [FromQuery] int finish)
        {
            var maxAgentSpeed = Velocity.FromKilometersPerHour(140);
            using (StreamReader r = new StreamReader("C:/Users/msei/source/repos/Hackathon2021/Resources/NodeList.json"))
            {
                string json = r.ReadToEnd();
                List<Models.Node> items = JsonConvert.DeserializeObject<List<Models.Node>>(json);
                // Create directed graph with node a and b, and a one-way direction from a to b
                var nodes = new List<Node>();

                var speed = 20;

                foreach (var item in items)
                {
                    var node = new Node(new Position(item.X, item.Y));
                    nodes.Add(node);
                }
                for (var i = 0; i < nodes.Count; i++)
                {
                    var children = items[i].Children;
                    var node = nodes[i];
                    foreach (var child in children)
                    {
                        var nodeB = nodes[child - 1];
                        node.Connect(nodeB, Velocity.FromKilometersPerHour(speed - items[child - 1].Pollution));
                    }
                }


                var pathFinder = new PathFinder();
                var path = pathFinder.FindPath(nodes[start], nodes[finish], maximumVelocity: maxAgentSpeed);


                return Ok(path);
            }
        }
    }
}
